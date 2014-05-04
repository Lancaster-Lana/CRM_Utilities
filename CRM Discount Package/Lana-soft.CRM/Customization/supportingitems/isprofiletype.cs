using System.ComponentModel;

namespace LanaSoftCRM
{
	/// <summary>
	/// Controls whether the group represents a user selectable Profile for the Workplace
	/// </summary>
	public enum IsProfileType
	{
		None,
		[DescriptionAttribute("true")]True,
		[DescriptionAttribute("false")]False
	}
}    