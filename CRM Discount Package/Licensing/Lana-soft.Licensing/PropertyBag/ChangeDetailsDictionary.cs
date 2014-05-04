using System;
using System.Collections;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// Dictionary of ChangeDetail objects.
	/// </summary>
	public class ChangeDetailsDictionary : DictionaryBase
	{
		#region Conctruction
		public ChangeDetailsDictionary()
		{
		}
		#endregion

		#region DictionaryBase Stuff
		public ChangeDetail this[string fieldName]
		{
			get  
			{
				return (ChangeDetail)Dictionary[fieldName];
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

		public void Add(string fieldName, ChangeDetail value)  
		{
			Dictionary.Add(fieldName, value);
		}

		public bool Contains(string fieldName)  
		{
			return Dictionary.Contains(fieldName);
		}

		public void Remove(string fieldName)
		{
			Dictionary.Remove(fieldName);
		}

		public void CopyTo (string[] array , Int32 index )
		{
			Dictionary.CopyTo(array, index);
		}

		
		#endregion
	}
}
