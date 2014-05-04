using System;
using System.Collections;
using System.ComponentModel;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// Protepry bad collection for storing reflected database field values in objects.
	/// </summary>
	public class PropertyBagDictionary : DictionaryBase
	{
		#region Private Variables
		private ChangeDetailsDictionary _changes = new ChangeDetailsDictionary();
		#endregion

		#region Construction
		public PropertyBagDictionary()
		{
		}
		#endregion

		#region Public Stuff
		public ChangeDetailsDictionary ChangeDetails
		{
			get
			{
				return _changes;
			}
		}
		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Publics for dictionary access
		public object this[string fieldName]  
		{
			get  
			{
				return Dictionary[fieldName];
			}
			set  
			{
				Dictionary[fieldName] = value;
			}
		}

		public ICollection Keys  
		{
			get  
			{
				return Dictionary.Keys;
			}
		}

		public ICollection Values  
		{
			get  
			{
				return Dictionary.Values;
			}
		}

		public void Add( string fieldName, object value )  
		{
			Dictionary.Add( fieldName, value );
		}

		public bool Contains( string fieldName )  
		{
			return( Dictionary.Contains( fieldName ) );
		}

		public void Remove( string fieldName )
		{
			Dictionary.Remove( fieldName );
		}
		public void CopyTo(string[] array, Int32 index)
		{
			Dictionary.CopyTo(array, index);
		}
		#endregion

		#region Commit/Revert changes
		/// <summary>
		/// Used by calling object to indicate that properties values have been saved.
		/// </summary>
		internal void CommitChanges()
		{
			lock( InnerHashtable.SyncRoot )
			{
				ChangeDetails.Clear();
			}
		}

		internal void RevertChanges()
		{
			lock( InnerHashtable.SyncRoot )
			{
				ChangeDetail change;
				foreach( DictionaryEntry entry in ChangeDetails )
				{
					change = (ChangeDetail)entry.Value;
					this[(string)entry.Key] = change.OldValue;
				}

				ChangeDetails.Clear();
			}
		}
		#endregion

		#region Dictionary overrides
		protected override void OnInsertComplete(object key, object value)
		{
			if (Type.GetType("System.String") != key.GetType())
				throw new ArgumentException("Key must be of type String.", "key");

			string fieldName = (string)key;
			
			if (ChangeDetails.Contains(fieldName))
				ChangeDetails[fieldName].OldValue = null;
			else
				ChangeDetails.Add(fieldName, new ChangeDetail(value, null));

			if( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( fieldName ));
			}
		}

		protected override void OnSetComplete(object key, object oldValue, object newValue )  
		{
			if ( key.GetType() != Type.GetType("System.String") )
				throw new ArgumentException("Key must be of type String.", "key" );

			string fieldName = (string)key;
			
			if (ChangeDetails.Contains( fieldName ))
				ChangeDetails[fieldName].NewValue = newValue;
			else
				ChangeDetails.Add( fieldName, new ChangeDetail( newValue, oldValue ));

			if( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( fieldName ));
			}
		}

		protected override void OnRemoveComplete(object key, object value)
		{
			if ( key.GetType() != Type.GetType("System.String") )
				throw new ArgumentException("Key must be of type String.", "key" );

			string fieldName = (string)key;
			
			if (ChangeDetails.Contains( fieldName ))
				ChangeDetails[fieldName].NewValue = null;
			else
				ChangeDetails.Add( fieldName, new ChangeDetail( null, value ));

			if( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( fieldName ));
			}
		}
		#endregion

		#region Strong typed accessors

		#region Defaults
		private class Defaults
		{
			// Primitive data types
			public const Boolean DefaultBoolean = false;
			public const Byte DefaultByte = 0;
			public const Byte[] DefaultBytes = null;
			public const Int32 DefaultInt32 = 0;
			public const Int64 DefaultInt64 = 0L;
			public const Decimal DefaultDecimal = 0M;

            // complex data types
			public static readonly String DefaultString = String.Empty;
			public static readonly DateTime DefaultDateTime = DateTime.MinValue;
			public static readonly TimeSpan DefaultTimeSpan = TimeSpan.MinValue;
			public static readonly Guid DefaultGuid = Guid.Empty;
		}
		#endregion

		#region GetBoolean, GetByte, GetInt32, GetInt64
		public Boolean GetBoolean( string propertyName )
		{
			return GetBoolean( propertyName, Defaults.DefaultBoolean );
		}

		public Boolean GetBoolean( string propertyName, Boolean defaultValue )
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Boolean)boxedValue;
		}

		public Byte GetByte( string propertyName )
		{
			return GetByte( propertyName, Defaults.DefaultByte );
		}

		public Byte GetByte( string propertyName, Byte defaultValue )
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Byte)boxedValue;
		}

		public Byte[] GetBytes( string propertyName )
		{
			return GetBytes( propertyName, Defaults.DefaultBytes );
		}

		public Byte[] GetBytes( string propertyName, Byte[] defaultValue)
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Byte[])boxedValue;
		}

		public Int32 GetInt32( string propertyName )
		{
			return GetInt32( propertyName, Defaults.DefaultInt32 );
		}

		public Int32 GetInt32( string propertyName, Int32 defaultValue )
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Int32)boxedValue;
		}

		public Int64 GetInt64( string propertyName )
		{
			return GetInt64( propertyName, Defaults.DefaultInt64 );
		}

		public Int64 GetInt64( string propertyName, Int64 defaultValue )
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Int64)boxedValue;
		}

		public Decimal GetDecimal( string propertyName )
		{
			return GetDecimal( propertyName, Defaults.DefaultDecimal );
		}

		public Decimal GetDecimal( string propertyName, Decimal defaultValue )
		{
			object boxedValue = this[propertyName];

			if( boxedValue == null )
				return defaultValue;

			return (Decimal)boxedValue;
		}
		#endregion

		#region GetString, GetDateTime, GetTimeSpan, GetGuid

		public String GetString( string propertyName )
		{
			return GetString( propertyName, Defaults.DefaultString );
		}

		public String GetString( string propertyName, String defaultValue )
		{
			string value = this[propertyName] as string;

			if(value!=null)
				return value;
			else
				return defaultValue;
		}

		public DateTime GetDateTime( string propertyName )
		{
			return GetDateTime( propertyName, Defaults.DefaultDateTime );
		}

		public DateTime GetDateTime( string propertyName, DateTime defaultValue )
		{
			object value = this[propertyName];

			if( value == null || ! ( value is DateTime ))
				return defaultValue;

			return (DateTime)value;
		}

		public TimeSpan GetTimeSpan( string propertyName )
		{
			return GetTimeSpan( propertyName, Defaults.DefaultTimeSpan );
		}

		public TimeSpan GetTimeSpan( string propertyName, TimeSpan defaultValue )
		{
			object value = this[propertyName];

			if( value == null || ! ( value is TimeSpan ))
				return defaultValue;

			return (TimeSpan)value;
		}

		public Guid GetGuid( string propertyName )
		{
			return GetGuid( propertyName, Defaults.DefaultGuid );
		}

		public Guid GetGuid( string propertyName, Guid defaultValue )
		{
			object value = this[propertyName];

			if( value == null || ! ( value is Guid ))
				return defaultValue;

			return (Guid)value;
		}
		#endregion

		#endregion
	}
}
