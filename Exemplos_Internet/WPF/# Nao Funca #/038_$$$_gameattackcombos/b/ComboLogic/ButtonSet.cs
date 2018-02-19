using System.Collections.Generic;
using System.Xml;

namespace GG.GameAttackCombos.Logic {
	
	/// <summary>
	/// Represents a button set for a platform. A button set is a named list of buttons that 
	/// form a graphical related set.
	/// </summary>
	public class ButtonSet {

		#region Properties

		/// <summary>
		/// The platform this button set belongs to.
		/// </summary>
		public string Platform { get; private set; }

		/// <summary>
		/// The name of this button set.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The dictionary of buttons that make up this button set, keyed by ID.
		/// </summary>
		public Dictionary<string, Button> Buttons { get; private set; }

		#endregion


		/// <summary>
		/// Initializes a new instance of ButtonSet.
		/// </summary>
		/// <param name="platform">The platform for this button set.</param>
		/// <param name="name">The name of this button set.</param>
		public ButtonSet(string platform, string name) {
			Platform = platform;
			Name = name;
			Buttons = new Dictionary<string, Button>();
		}

		/// <summary>
		/// Loads button sets from an XML document.
		/// </summary>
		/// <param name="document">The XML document that defines the button set.s</param>
		/// <returns></returns>
		public static List<ButtonSet> LoadButtonSets(XmlDocument document) {
			List<ButtonSet> ButtonSets = new List<ButtonSet>();

			// Read the button sets.
			XmlNodeList ButtonSetNodes = document.SelectNodes("ButtonSets/ButtonSet");
			foreach (XmlNode ButtonSetNode in ButtonSetNodes) {
				// Create a new button set and add it to the list.
				ButtonSet DefinedButtonSet = new ButtonSet(
					ButtonSetNode.Attributes["platform"].Value, 
					ButtonSetNode.Attributes["name"].Value
				);
				ButtonSets.Add(DefinedButtonSet);

				// Read the buttons for this button set.
				XmlNodeList ButtonNodes = ButtonSetNode.SelectNodes("Button");
				foreach (XmlNode ButtonNode in ButtonNodes) {
					// Create a new button and add it to the button set.
					Button DefinedButton = new Button(
						ButtonNode.Attributes["id"].Value,
						DefinedButtonSet.Platform,
						ButtonNode.Attributes["iconKey"].Value
					);
					DefinedButtonSet.Buttons.Add(DefinedButton.Id, DefinedButton);
				}
			}

			return ButtonSets;
		}

	}

}