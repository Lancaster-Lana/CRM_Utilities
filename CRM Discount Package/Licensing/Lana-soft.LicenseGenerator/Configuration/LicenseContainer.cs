namespace LanaSoft.LicenseGenerator
{
	/// <summary>
	/// The license container specifier.
	/// </summary>
	public class LicenseContainer
	{
		#region Private Variables
		private string			_name;
		private string			_assembly;
		private string			_class;
		private string			_licenseTemplateFilename;
		#endregion

		#region Construction
		public LicenseContainer(string name, string assembly, string className, string licenseTemplateFilename)
		{
			_name = name;
			_assembly = assembly;
			_class = className;
			_licenseTemplateFilename = licenseTemplateFilename;
		}
		#endregion

		#region Publics
		/// <summary>
		/// Gets the name of the license container.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
		}

		/// <summary>
		/// Gets the license container assembly name.
		/// </summary>
		public string Assembly
		{
			get
			{
				return _assembly;
			}
		}

		/// <summary>
		/// Gets the license class name.
		/// </summary>
		public string Class
		{
			get
			{
				return _class;
			}
		}

		/// <summary>
		/// Gets the license class license template.
		/// </summary>
		public string LicenseTemplateFilename
		{
			get
			{
				return _licenseTemplateFilename;
			}
		}
		#endregion

		public override string ToString()
		{
			return Name;
		}
	}
}
