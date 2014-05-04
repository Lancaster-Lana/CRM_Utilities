using System.ComponentModel;

namespace LanaSoftCRM
{
	/// <summary>
	/// Specifies whether the sub-are can be viewed offline
	/// </summary>
	public enum AvailableOfflineType
	{
		None,
		[DescriptionAttribute("true")]True,
		[DescriptionAttribute("false")]False
	}
}    