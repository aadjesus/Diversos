using System.Collections.Generic;

namespace GG.GameAttackCombos.Logic {
	
	/// <summary>
	/// Represents an attack combo.
	/// </summary>
	public class AttackCombo {

		#region Properties

		/// <summary>
		/// The sequence of commands that make up this attack combo.
		/// </summary>
		public List<Command> CommandSequence { get; private set; }

		/// <summary>
		/// The next combo group in a chain that this attack combo leads to, if any.
		/// </summary>
		public ComboGroup NextGroupInChain { get; set; }

		#endregion


		/// <summary>
		/// Initializes an instance of AttackCombo.
		/// </summary>
		public AttackCombo() {
			CommandSequence = new List<Command>();
		}

	}

}