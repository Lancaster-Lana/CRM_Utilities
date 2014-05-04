
using System;

namespace LanaSoftCRM
{
	/// <summary>
	/// Specifies the user license requirement that determines whether the sub-area is displayed
	/// </summary>
	[Flags]
	public enum LicenseTypes
	{
		None			= 0,
		Professional	= 1,
		SmallBusiness	= 2,
		All				= Professional | SmallBusiness
	}
}
