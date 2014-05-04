using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LanaSoftCRM
{
	public class Utility
	{
		#region Methods

		/// <summary>
		/// Retrieves the description attribute from an enum value
		/// </summary>
		/// <param name="enumValue">The enum value who's description attribute to retrieve</param>
		/// <returns>Returns the description attribute of an enum value as a string</returns>
		public static string GetDescriptionAttribute(Enum enumValue)
		{
			// retrieves the information on the enum field (as opposed to the enum type itself)
			FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

			// retrieves all custom attributes of type DescriptionAttribute (should only be one)
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

			// if there is an attribute, then return it
			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].Description;
			}
			else
			{
				// throw an error message if there is no DescriptionAttribute on the enum field
				string errorMessage = string.Format("{0} does not have a DescriptionAttribute.", enumValue);
				throw new ArgumentException(errorMessage);
			}
		}

		/// <summary>
		/// Used to check that all the characters are a-z (capital and lower-case), 0-9, or _ (an underscore)
		/// </summary>
		/// <param name="input">String value whose characters are being checked</param>
		/// <returns>Returns true if all the characters of the input string are alpha-numeric (+ underscore), otherwise false</returns>
		public static bool IsAlphaNumeric(string input)
		{
			// set the pattern to check
			string pattern = "^[a-zA-Z0-9_]+$";

			if(Regex.IsMatch(input, pattern))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
	}
}