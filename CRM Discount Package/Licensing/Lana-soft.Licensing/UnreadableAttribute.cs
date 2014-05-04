using System;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// Indicates to the license validator that the property is not human readable in the license file.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class UnreadableAttribute : Attribute
	{
	}
}
