
using System;

namespace LanaSoftCRM
{
	/// <summary>
	/// Specifies the privileges needed to display this sub-area
	/// </summary>
	[Flags]
	public enum PrivilegeTypes
	{
		None		= 0,
		Append		= 1,
		AppendTo	= 2,
		Assign		= 4,
		Create		= 8,
		Delete		= 16,
		Read		= 32,	   
		Share		= 64,
		Write		= 128,
		All			= Append | AppendTo | Assign | Create | Delete | Read | Share | Write
	}
}
