
using System.ComponentModel;

namespace LanaSoftCRM
{
    /// <summary>
    /// Used to determine what kind of attribute is being added to MS CRM
    /// </summary>
    /// <remarks>The description attribute needs to match the "Type" element node in the import/export XML</remarks>
    public enum AttributeType
    {
        [DescriptionAttribute("bit")]
        Bit,
        [DescriptionAttribute("int")]
        Integer,
        [DescriptionAttribute("datetime")]
        DateTime,
        [DescriptionAttribute("float")]
        Float,
        [DescriptionAttribute("money")]
        Money,
        [DescriptionAttribute("nvarchar")]
        NVarchar,
        [DescriptionAttribute("ntext")]
        NText,
        [DescriptionAttribute("picklist")]
        Picklist
    }
}