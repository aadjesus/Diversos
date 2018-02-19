using System;
using System.Windows.Forms;

namespace SourceGrid2
{
	/// <summary>
	/// A dictionary with keys of type Guid and values of type Control
	/// </summary>
	public class ControlsRepository : System.Collections.DictionaryBase
	{
		private Control m_ParentControl;
		/// <summary>
		/// Initializes a new empty instance of the ControlsRepository class
		/// </summary>
		public ControlsRepository(Control p_ParentControl)
		{
			m_ParentControl = p_ParentControl;
		}

		/// <summary>
		/// Gets or sets the Control associated with the given Guid
		/// </summary>
		/// <param name="key">
		/// The Guid whose value to get or set.
		/// </param>
		public virtual Control this[Guid key]
		{
			get
			{
				return (Control) this.Dictionary[key];
			}
/*			set
			{
				this.Dictionary[key] = value;
			}*/
		}

		/// <summary>
		/// Adds an element with the specified key and value to this ControlsRepository.
		/// </summary>
		/// <param name="key">
		/// The Guid key of the element to add.
		/// </param>
		/// <param name="value">
		/// The Control value of the element to add.
		/// </param>
		public virtual void Add(Guid key, Control value)
		{
			this.Dictionary.Add(key, value);
			m_ParentControl.Controls.Add(value);
		}

		/// <summary>
		/// Determines whether this ControlsRepository contains a specific key.
		/// </summary>
		/// <param name="key">
		/// The Guid key to locate in this ControlsRepository.
		/// </param>
		/// <returns>
		/// true if this ControlsRepository contains an element with the specified key;
		/// otherwise, false.
		/// </returns>
		public virtual bool Contains(Guid key)
		{
			return this.Dictionary.Contains(key);
		}

		/// <summary>
		/// Determines whether this ControlsRepository contains a specific key.
		/// </summary>
		/// <param name="key">
		/// The Guid key to locate in this ControlsRepository.
		/// </param>
		/// <returns>
		/// true if this ControlsRepository contains an element with the specified key;
		/// otherwise, false.
		/// </returns>
		public virtual bool ContainsKey(Guid key)
		{
			return this.Dictionary.Contains(key);
		}

		/// <summary>
		/// Determines whether this ControlsRepository contains a specific value.
		/// </summary>
		/// <param name="value">
		/// The Control value to locate in this ControlsRepository.
		/// </param>
		/// <returns>
		/// true if this ControlsRepository contains an element with the specified value;
		/// otherwise, false.
		/// </returns>
		public virtual bool ContainsValue(Control value)
		{
			foreach (Control item in this.Dictionary.Values)
			{
				if (item == value)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Removes the element with the specified key from this ControlsRepository.
		/// </summary>
		/// <param name="key">
		/// The Guid key of the element to remove.
		/// </param>
		public virtual void Remove(Guid key)
		{
			if (ContainsKey(key))
			{
				m_ParentControl.Controls.Remove(this[key]);
				this.Dictionary.Remove(key);
			}
		}

		/// <summary>
		/// Gets a collection containing the keys in this ControlsRepository.
		/// </summary>
		public virtual System.Collections.ICollection Keys
		{
			get
			{
				return this.Dictionary.Keys;
			}
		}

		/// <summary>
		/// Gets a collection containing the values in this ControlsRepository.
		/// </summary>
		public virtual System.Collections.ICollection Values
		{
			get
			{
				return this.Dictionary.Values;
			}
		}
	}

}
