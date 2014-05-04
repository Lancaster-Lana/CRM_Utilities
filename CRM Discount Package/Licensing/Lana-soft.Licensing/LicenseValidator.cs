using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The base class for all license files.
	/// </summary>
	public abstract class LicenseValidator
	{
		#region Public Constants
		public const BindingFlags	BindingPublicFlags				= BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance;
		public const int			DefaultRSAKeySize				= 384;
		public const string			ValidityFlagPropertyName		= "IsValid";
		public const int			DefaultBlockSize				= 128;
		public const string			SymmetricAlgorithmName			= "Rijndael";
		public const string			ItemElementName					= "Item";
		public const string			InternalElementPrefix			= "Internal_";
		public const string			DateTimeFormat					= "u";
		#endregion

		#region Private Variables
		private PropertyBagDictionary			_propertyBag;
		private Type							_licenseType;
		private bool							_valid;
		private BinHexDecoder					_binHexDecoder;
		#endregion
		
		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Construction
		protected LicenseValidator()
		{
			_propertyBag = new PropertyBagDictionary();
			_propertyBag.PropertyChanged += new PropertyChangedEventHandler(PropertyBag_PropertyChanged);

			_licenseType = this.GetType();

			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the license file.
		/// </summary>
		/// <param name="licensePath"></param>
		protected LicenseValidator(string licensePath)
		{
			Validate(licensePath);
		}
		#endregion

		#region Public Stuff
		/// <summary>
		/// Gets the boolean value indicating if a license is valid.
		/// </summary>
		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				return _valid & (Properties.ChangeDetails.Count == 0);
			}
		}

		/// <summary>
		/// Performs a custom license initialization.
		/// </summary>
		public virtual void Initialize()
		{
		}

		/// <summary>
		/// Performs a license validation against the provided path to a license file.
		/// </summary>
		/// <param name="licensePath">A path to the license file on the hard drive.</param>
		public void Validate(string licensePath)
		{
			Properties.Clear();
			Properties.CommitChanges();
			_valid = false;

			if (null == licensePath)
				throw new ArgumentNullException("licensePath");

			if (0 == licensePath.Length)
				throw new ArgumentException("The path to the license file cannot be empty.", "licensePath");

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			try
			{
				xmlDocument.Load(licensePath);
			}
			catch (XmlException)
			{
				throw new LicenseValidationException(string.Format(CultureInfo.InvariantCulture, "The file '{0}' has invalid format.", licensePath));
			}

			// Searching for the Signature element in the document
			XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());
			namespaceManager.AddNamespace("dsig", SignedXml.XmlDsigNamespaceUrl);
			XmlElement signedElement = (XmlElement)xmlDocument.SelectSingleNode("//dsig:Signature", namespaceManager);
			if (null == signedElement)
				throw new LicenseValidationException(string.Format(CultureInfo.InvariantCulture, "The file '{0}' has invalid format.", licensePath));
			
			// Loading the signature for verification
			SignedXml signedXml = new SignedXml(xmlDocument);
			signedXml.LoadXml(signedElement);

			// Creating the RSA crypto service provider
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(DefaultRSAKeySize);
			rsa.FromXmlString(RSAPublicKeyXml);

			// Checking the signature
			bool valid = true;
			try
			{
				valid = signedXml.CheckSignature(rsa);
			}
			catch(ArgumentException)
			{
				valid = false;
			}
			catch(CryptographicException)
			{
				valid = false;
			}

			if (!valid)
				throw new LicenseValidationException(string.Format(CultureInfo.InvariantCulture, "The license specified '{0}' is not valid.", licensePath));

			// Filling up the license properties
			PropertyInfo[] properties = _licenseType.GetProperties(BindingPublicFlags);
			foreach (PropertyInfo property in properties)
			{
				if (property.Name == ValidityFlagPropertyName)
					continue;

				SetProperty(property, xmlDocument);
			}

			Properties.CommitChanges();
			_valid = true;
		}
		#endregion

		#region Protected Stuff
		/// <summary>
		/// Gets the property collection.
		/// </summary>
		protected PropertyBagDictionary Properties
		{
			get
			{
				return _propertyBag;
			}
		}
		#endregion

		#region Event Handlers
		private void PropertyBag_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = PropertyChanged;
			if (null != propertyChangedEventHandler)
				propertyChangedEventHandler(this, e);
		}
		#endregion

		#region Private Stuff
		private void SetProperty(PropertyInfo property, XmlDocument xmlDocument)
		{
			// Checking if the property is marked as Unredable
			bool unreadable = ((Attribute.GetCustomAttribute(property, typeof(UnreadableAttribute)) as UnreadableAttribute) != null);
			string propertyName = unreadable ? InternalElementPrefix + Encrypt(property.Name) : property.Name;

			// Getting the relevant XML element
			XmlNode propertyNode = xmlDocument.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", _licenseType.Name, propertyName));
			if (null == propertyNode)
				throw new LicenseValidationException(string.Format(CultureInfo.InvariantCulture, "The license specified is not valid {0} licence.", _licenseType.Name));
			string propertyValue = unreadable ? Decrypt(propertyNode.InnerText) : propertyNode.InnerText;

			// Checking the property type
			if (property.PropertyType.IsArray)
			{
				XmlNodeList itemNodeList = propertyNode.SelectNodes(ItemElementName);
				if (itemNodeList.Count > 0)
				{
					Type elementType = property.PropertyType.GetElementType();
					Array itemArray = Array.CreateInstance(elementType, itemNodeList.Count);

					for (int index = 0; index < itemNodeList.Count; index++)
					{
						propertyValue = unreadable ? Decrypt(itemNodeList[index].InnerText) : itemNodeList[index].InnerText;
						itemArray.SetValue(GetPropertyValue(elementType, propertyValue), index);
					}

					Properties[property.Name] = itemArray;
				}
			}
			else
				Properties[property.Name] = GetPropertyValue(property.PropertyType, propertyValue);
		}

		private string Encrypt(string value)
		{
			byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(value);
			
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream();
				
				SymmetricAlgorithm symmetricAlgorithm = null;
				try
				{
					symmetricAlgorithm = SymmetricAlgorithm.Create(SymmetricAlgorithmName);

					byte[] key, iv;
					GenerateCryptoData(out key, out iv);

					symmetricAlgorithm.Key = key;
					symmetricAlgorithm.BlockSize = DefaultBlockSize;
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

		private string Decrypt(string value)
		{
			// Checking if the BinHexDecoder has been initialized
			if (null == _binHexDecoder)
				_binHexDecoder = new BinHexDecoder();

            byte[] bytesToDecrypt = _binHexDecoder.DecodeBinHex(value.ToCharArray(), 0, true);
			
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream();
				
				SymmetricAlgorithm symmetricAlgorithm = null;
				try
				{
					symmetricAlgorithm = SymmetricAlgorithm.Create(SymmetricAlgorithmName);

					byte[] key, iv;
					GenerateCryptoData(out key, out iv);

					symmetricAlgorithm.Key = key;
					symmetricAlgorithm.BlockSize = DefaultBlockSize;
					symmetricAlgorithm.IV = iv;
					symmetricAlgorithm.Mode = CipherMode.ECB;
					symmetricAlgorithm.Padding = PaddingMode.PKCS7;
					
					CryptoStream encryptionStream = null;
					try
					{
						encryptionStream = new CryptoStream(memoryStream,
							symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV),
							CryptoStreamMode.Write);
						encryptionStream.Write(bytesToDecrypt, 0, bytesToDecrypt.Length);
					}
					finally
					{
						if (null != encryptionStream)
							encryptionStream.Close();
					}
					
					return Encoding.UTF8.GetString(memoryStream.ToArray());
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

		private void GenerateCryptoData(out byte[] key, out byte[] iv)
		{
			int length = DefaultBlockSize/8;

			key = new byte[length];
			iv = new byte[length];

			SHA256Managed hasher = new SHA256Managed();
			byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name));
			
			Array.Copy(hash, 0, key, 0, key.Length);
			Array.Copy(hash, 0, iv, 0, iv.Length);
		}

		private object GetPropertyValue(Type propertyType, string propertyStringValue)
		{
			object value = null;

			try
			{
				if (typeof(string) == propertyType)
					value = propertyStringValue;
				else if (propertyType.IsPrimitive)
					value = Convert.ChangeType(propertyStringValue, propertyType, NumberFormatInfo.InvariantInfo);
				else if (typeof(DateTime) == propertyType)
					value = XmlConvert.ToDateTime(propertyStringValue, DateTimeFormat);
				else if (propertyType.IsEnum)
					value = Enum.Parse(propertyType, propertyStringValue);
				else
					Trace.Fail(string.Format(CultureInfo.InvariantCulture, "An unknown property type {0} has been specified in the license file.",
						propertyType.FullName));
			}
			catch
			{
				throw new LicenseValidationException(string.Format(CultureInfo.InvariantCulture,  "The license specified is not valid {0} licence.", _licenseType.Name));
			}

			return value;
		}

		/// <summary>
		/// Gets the RSA XML public key representation.
		/// </summary>
		private string RSAPublicKeyXml
		{
			get
			{
				string rsaPublicKeyXml = String.Empty;

				Assembly assembly = typeof(LicenseValidator).Assembly;
				Stream publicKeyStream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".SignaturePublicKey.xml");
				StreamReader streamReader = new StreamReader(publicKeyStream);
				rsaPublicKeyXml = streamReader.ReadToEnd();
				streamReader.Close();
				publicKeyStream.Close();

				return rsaPublicKeyXml;
			}
		}
		#endregion
	}

}
