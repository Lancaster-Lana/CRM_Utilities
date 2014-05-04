using System.ComponentModel;

namespace LanaSoftCRM
{
    /// <summary>
    /// Controls whether groups of sub-areas are shown in the navigation pane
    /// </summary>
    public enum ShowGroupsType
    {
        None,
        [DescriptionAttribute("true")]
        True,
        [DescriptionAttribute("false")]
        False
    }
}