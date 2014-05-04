using System.ComponentModel;

namespace LanaSoftCRM
{
	/// <summary>
	/// Controls whether groups of sub-areas are shown in the navigation pane
	/// </summary>
	public enum WindowMode
	{
		Undefined = 0,
		[DescriptionAttribute("0")]Window = 1,
		[DescriptionAttribute("1")]ModalDialog = 2,
		[DescriptionAttribute("2")]ModelessDialog = 3
	}
}
