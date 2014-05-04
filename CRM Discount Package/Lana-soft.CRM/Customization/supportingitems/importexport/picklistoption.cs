
namespace LanaSoftCRM
{
    /// <summary>
    /// Used to add picklist values to a piclist attribute
    /// </summary>
    public struct PicklistOption
    {
        public int Value;
        public string Description;

        public PicklistOption(int value, string description)
        {
            Value = value;
            Description = description;
        }
    }
}
