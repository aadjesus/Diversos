using System.Text.RegularExpressions;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents a game code for downloading a game attack combo package.
	/// </summary>
	public class GameCode {

		// The pattern used to validate game codes.
		private const string GameCodePattern = @"^[a-fA-f0-9]{4}(?:\-[a-fA-f0-9]{4}){4}$";


		/// <summary>
		/// Determines if the specified game code is valid.
		/// </summary>
		/// <param name="code">The game code to check.</param>
		/// <returns>true if the game code is in a valid format; otherwise, false.</returns>
		public static bool ValidateGameCode(string code) {
			return Regex.IsMatch(code, GameCodePattern, RegexOptions.Singleline);
		}
		
	}

}