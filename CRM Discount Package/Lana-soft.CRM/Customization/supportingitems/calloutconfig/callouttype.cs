
namespace LanaSoftCRM
{
	public enum PreCalloutType
	{
		PreCreate,
		PreUpdate,
		PreDelete,
		PreAssign,
		PreSetState,
		PreMerge,
	}

	public enum PostCalloutType
	{
		PostCreate,
		PostUpdate,
		PostDelete,
		PostAssign,
		PostSetState,
		PostMerge
	}
}