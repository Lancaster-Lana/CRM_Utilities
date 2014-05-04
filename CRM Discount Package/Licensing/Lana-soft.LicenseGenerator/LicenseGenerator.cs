using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using LanaSoft.Licensing;

namespace LanaSoft.LicenseGenerator
{
	/// <summary>
	/// The class which generates a license based of the license class provided.
	/// </summary>
	internal sealed class LicenseGenerator
	{
		#region Private Constructor
		private LicenseGenerator()
		{
		}
		#endregion

		#region Public Stuff
		public static void Generate(object license, string fileName)
		{
			if (null == license)
				throw new ArgumentNullException("license");

			if (null == fileName)
				throw new ArgumentNullException("fileName");

			if (0 == fileName.Length)
				throw new ArgumentException("License file name is empty.", "fileName");

			MemoryStream xmlStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
			xmlTextWriter.Formatting = Formatting.Indented;

			Type licenseType = license.GetType();
			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteComment(string.Format(" Generated {0} ", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
			xmlTextWriter.WriteStartElement(licenseType.Name);

			PropertyInfo[] properties = licenseType.GetProperties(LicenseValidator.BindingPublicFlags);
			bool unreadable;
            foreach (PropertyInfo property in properties)
			{
				if (property.Name == LicenseValidator.ValidityFlagPropertyName)
					continue;

				unreadable = ((Attribute.GetCustomAttribute(property, typeof(UnreadableAttribute)) as UnreadableAttribute) != null);
				WritePropertyInfo(property, unreadable, license, xmlTextWriter);
			}

			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();

			xmlTextWriter.Close();
			xmlStream.Close();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Encoding.UTF8.GetString(xmlStream.ToArray()).Trim());
			
			SignedXml signedXml = new SignedXml(xmlDocument);
			
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(LicenseValidator.DefaultRSAKeySize);
			rsa.FromXmlString(RSAPrivateKeyXml);
			signedXml.SigningKey = rsa;

			Reference reference = new Reference("");
			reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(true));
			signedXml.AddReference(reference);

			signedXml.ComputeSignature();
			xmlDocument.DocumentElement.AppendChild(signedXml.GetXml());
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Save(fileName);
		}
		#endregion

		#region Private Stuff
		private static void WritePropertyInfo(PropertyInfo property, bool unreadable, object license, XmlTextWriter xmlTextWriter)
		{
			object propertyValue = property.GetValue(license, null);

			if (property.PropertyType.IsArray)
			{
				xmlTextWriter.WriteStartElement(unreadable ? LicenseValidator.InternalElementPrefix + Encrypt(license, property.Name) : property.Name);
				Array array = (Array)propertyValue;
				if (null != array)
				{
					lock (array.SyncRoot)
					{
						IEnumerator enumerator = array.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (null != enumerator.Current)
							{
								string stringEquivalent = GetPropertyStringEquivalent(enumerator.Current.GetType(), enumerator.Current);
								string value = unreadable ? Encrypt(license, stringEquivalent) : stringEquivalent;
								xmlTextWriter.WriteElementString(LicenseValidator.ItemElementName, value);
							}
							else
							{
								xmlTextWriter.WriteStartElement(LicenseValidator.ItemElementName);
								xmlTextWriter.WriteEndElement();
							}
						}
					}
				}
				xmlTextWriter.WriteEndElement();
			}
			else
			{
				string elementName = unreadable ? LicenseValidator.InternalElementPrefix + Encrypt(license, property.Name) : property.Name;
				string stringEquivalent = GetPropertyStringEquivalent(property.PropertyType, propertyValue);
				string value = unreadable ? Encrypt(license, stringEquivalent) : stringEquivalent;
				xmlTextWriter.WriteElementString(elementName, value);
			}
		}

		private static string GetPropertyStringEquivalent(Type propertyType, object value)
		{
			string stringEquivalent = String.Empty;

			if (typeof(string) == propertyType)
				stringEquivalent = (string)value;
			else if (propertyType.IsPrimitive)
				stringEquivalent = (string)Convert.ChangeType(value, typeof(string), NumberFormatInfo.InvariantInfo);
			else if (typeof(DateTime) == propertyType)
				stringEquivalent = XmlConvert.ToString((DateTime)value, LicenseValidator.DateTimeFormat);
			else if (propertyType.IsEnum)
				stringEquivalent = Enum.GetName(propertyType, value);
			else
				Trace.Fail(string.Format("An unknown property type {0} has been specified in the license file.",
					propertyType.FullName));

			return stringEquivalent;
		}

		private static string Encrypt(object license, string value)
		{
			byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(value);
			
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream();
				
				SymmetricAlgorithm symmetricAlgorithm = null;
				try
				{
					symmetricAlgorithm = SymmetricAlgorithm.Create(LicenseValidator.SymmetricAlgorithmName);

					byte[] key, iv;
					GenerateCryptoData(license, out key, out iv);

					symmetricAlgorithm.Key = key;
					symmetricAlgorithm.BlockSize = LicenseValidator.DefaultBlockSize;
					symmetricAlgorithm.IV = iv;
					symmetricAlgorithm.Mode = CipherMode.ECB;
					symmetricAlgorithm.Padding = PaddingMode.PKCS7;
					
					CryptoStream encryptionStream = null;
					try
					{
						encryptionStream = new CryptoStream(memoryStream,
							symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV),
							CryptoStreamMode.Write);
						encryptionStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
					}
					finally
					{
						if (null != encryptionStream)
							encryptionStream.Close();
					}
					
					byte[] encryptedData = memoryStream.ToArray();
					return BinHexEncoder.EncodeToBinHex(encryptedData, 0, encryptedData.Length);
				}
				finally
				{
					if (null != symmetricAlgorithm)
						symmetricAlgorithm.Clear();
				}
			}
			finally
			{
				if (null != memoryStream)
					memoryStream.Close();
			}
		}

		private static void GenerateCryptoData(object license, out byte[] key, out byte[] iv)
		{
			int length = LicenseValidator.DefaultBlockSize/8;

			key = new byte[length];
			iv = new byte[length];

			SHA256Managed hasher = new SHA256Managed();
			byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(license.GetType().Name));
			
			Array.Copy(hash, 0, key, 0, key.Length);
			Array.Copy(hash, 0, iv, 0, iv.Length);
		}

		/// <summary>
		/// Gets the RSA XML private key representation.
		/// </summary>
		private static string RSAPrivateKeyXml
		{
			get
			{
				string rsaPrivateKeyXml = String.Empty;

				Assembly assembly = typeof(LicenseGenerator).Assembly;
				Stream privateKeyStream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".SignaturePrivateKey.xml");
				StreamReader streamReader = new StreamReader(privateKeyStream);
				rsaPrivateKeyXml = streamReader.ReadToEnd();
				streamReader.Close();
				privateKeyStream.Close();

				return rsaPrivateKeyXml;
			}
		}
		#endregion
	}
}
