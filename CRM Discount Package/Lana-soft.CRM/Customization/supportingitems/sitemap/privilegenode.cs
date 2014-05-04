namespace LanaSoftCRM
{
	/// <summary>
	/// Used to add a privilege with the defined attributes to a sub area
	/// </summary>
	public struct PrivilegeNode
	{
		#region Fields

		private string _entityName;
		private PrivilegeTypes _privileges;
					
		#endregion

		#region Properties
		
		/// <summary>
		/// This is the entity that the privileges will be checked against
		/// </summary>
		public string EntityName
		{
			get
			{
				return _entityName;
			}
			set
			{
				_entityName = value;
			}
		}

		/// <summary>
		/// These are the privileges that will be applied to the entity
		/// The user must have these privileges in order to access the sub area
		/// </summary>
		public PrivilegeTypes Privileges
		{
			get
			{				
				return _privileges;
			}
			set
			{					
				_privileges = value;
			}
		}				

		#endregion	
	}
}