using System.Collections.Generic;

namespace GG.GameAttackCombos.Logic {
	
	/// <summary>
	/// Represents a group of attack combos.
	/// </summary>
	public class ComboGroup {

		#region Properties

		/// <summary>
		/// The name of this combo group; used to reference it in lists, etc.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The list of attack combos that make up this group.
		/// </summary>
		public List<AttackCombo> AttackCombos { get; private set; }

		#endregion


		/// <summary>
		/// Initializes an instance of ComboGroup.
		/// </summary>
		/// <param name="name">The name of this combo group.</param>
		public ComboGroup(string name) {
			Name = name;
			AttackCombos = new List<AttackCombo>();
		}

		/// <summary>
		/// Overriden to include the name of this combo group.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("Combo Group: {0}", Name);
		}

	}

}