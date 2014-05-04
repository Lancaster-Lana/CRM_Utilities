using System;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// Contains details of field change.
	/// </summary>
	public class ChangeDetail
	{
		#region Private Variables
		private DateTime _changeTime;
		private object _oldValue, _newValue;
		#endregion

		#region Contruction
		public ChangeDetail(object newValue) : this(newValue, null)
		{
		}

		public ChangeDetail(object newValue, object oldValue)
		{
			_changeTime = DateTime.Now;
			_oldValue = oldValue;
			_newValue = newValue;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the change time.
		/// </summary>
		public DateTime ChangeTime
		{
			get
			{
				return _changeTime;
			}
		}

		/// <summary>
		/// Gets or sets the old value.
		/// </summary>
		public object OldValue
		{
			get
			{
				return _oldValue;
			}
			set
			{
				_oldValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the new value.
		/// </summary>
		public object NewValue
		{
			get
			{
				return _newValue;
			}
			set
			{
				_newValue = value;
			}
		}
		#endregion
	}
}
