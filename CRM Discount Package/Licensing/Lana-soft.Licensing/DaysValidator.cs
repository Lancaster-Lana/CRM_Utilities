using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The days validator class.
	/// </summary>
	public sealed class DaysValidator
	{
		#region Defaults
		private const string		DaysValidKey		= "DV";
		#endregion

		#region Private Constructor
		private DaysValidator()
		{
		}
		#endregion

		#region Public Stuff
		/// <summary>
		/// Validates if an application license is not expired.
		/// </summary>
		/// <param name="daysValid">The number of days after the first run when the license expires.</param>
		/// <exception cref="LicenseExpiredException">Raised if trial period [daysValid] days is over.</exception>
		public static void ValidateApplication(int daysValid)
		{
			// If days validity is zero, it never expires
			if (0 == daysValid)
				return;

			// Getting the isolated storage for assembly
			IsolatedStorageFile isolatedStorageFile = null;
			try
			{
				isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly();
			
				if (null == isolatedStorageFile)
					return;

				BinaryFormatter binaryFormatter = new BinaryFormatter();

				// Getting the days value from the isolate storage
				DateTime firstUsageTime = DateTime.Today;
				bool keyExists = true;
				try
				{
					IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(DaysValidKey,
					                                                                                    FileMode.Open, FileAccess.Read, FileShare.Read, isolatedStorageFile );
					try
					{
						object value = binaryFormatter.Deserialize(isolatedStorageFileStream);
						if ((null != value) && (value is DateTime))
							firstUsageTime = (DateTime)value;
					}
					finally
					{
						if( isolatedStorageFileStream != null )
							isolatedStorageFileStream.Close();
					}
				}
					// File Not found
					// exception if deserialization fails
				catch(FileNotFoundException)
				{
					keyExists = false;
				}
				catch(ArgumentException)
				{
					keyExists = false;
				}
				catch(SecurityException)
				{
					keyExists = false;
				}
				catch(SerializationException)
				{
					keyExists = false;
				}
				catch(IsolatedStorageException)
				{
					keyExists = false;
				}
				catch(DirectoryNotFoundException)
				{
					keyExists = false;
				}
				catch(UnauthorizedAccessException)
				{
					keyExists = false;
				}

				// If a key doesn't exist, storing it
				if (!keyExists)
				{
					try
					{
						IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(DaysValidKey,
							FileMode.OpenOrCreate, isolatedStorageFile );
				
						try
						{
							binaryFormatter.Serialize(isolatedStorageFileStream, firstUsageTime);
						}
						finally
						{
							if( isolatedStorageFileStream != null )
								isolatedStorageFileStream.Close();
						}
					}
					catch(SecurityException)
					{
					}
					catch(ArgumentException)
					{
					}
					catch(IsolatedStorageException)
					{
					}					
					catch(FileNotFoundException)
					{
					}
					catch(SerializationException)
					{
					}					
					catch(DirectoryNotFoundException)
					{					
					}
					catch(UnauthorizedAccessException)
					{
					}

				}
				else
				{
					if ((DateTime.Now-firstUsageTime).Days > daysValid)
						throw new LicenseExpiredException();
				}
			}
			catch (SecurityException)
			{
			}			
			catch(ArgumentException)
			{
			}
			catch(IsolatedStorageException)
			{
			}					
			catch(FileNotFoundException)
			{
			}
			catch(SerializationException)
			{
			}					
			catch(DirectoryNotFoundException)
			{					
			}
			catch(UnauthorizedAccessException)
			{
			}
			finally
			{
				if (null != isolatedStorageFile)
					isolatedStorageFile.Close();
			}
		}
		#endregion
	}
}
