using System;

namespace LanaSoftCRM
{
	/// <summary>
	/// Specifies the type of client
	/// </summary>
	[Flags]
	public enum ClientTypes
	{
		Undefined					= -1,
		None						= 0,
		Outlook						= 1,
		OutlookLaptopClient			= 2,
		OutlookWorkstationClient	= 4,
		Web							= 8,
		All							= Outlook | OutlookLaptopClient | OutlookWorkstationClient | Web
	}
}
