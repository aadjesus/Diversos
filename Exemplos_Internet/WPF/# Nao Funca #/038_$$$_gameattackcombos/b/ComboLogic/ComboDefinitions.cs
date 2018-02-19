using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents the definitions of all attack combos configured.
	/// </summary>
	public class ComboDefinitions {

		/// <summary>
		/// The format for button mapping dictionary keys.
		/// </summary>
		const string ButtonMappingKeyFormat = "{0}|{1}";

		/// <summary>
		/// The file extension used for completeion save files.
		/// </summary>
		public const string CompletionSaveFileExtension = ".sav";

		/// <summary>
		/// Holds a list of the shortest path combos during their recursive calculations.
		/// </summary>
		private List<FlattenedCombo> ShortestPathCombos = new List<FlattenedCombo>();

		/// <summary>
		/// Holds a list of the combo groups to follow further during the recursive calculations
		/// of the shortest path combos.
		/// </summary>
		private List<ComboGroup> GroupsToFollow = new List<ComboGroup>();


		#region Properties

		/// <summary>
		/// Gets a list of the unique aspects assigned to this definition's various flattened combos.
		/// </summary>
		public List<string> AssignedAspects { get; private set; }

		/// <summary>
		/// A dictionary of available commands keyed by the commands' Ids.
		/// </summary>
		public Dictionary<string, Command> AvailableCommands { get; private set; }

		/// <summary>
		/// The mappings of commands to buttons by platform.
		/// </summary>
		/// <remarks>
		/// The button mappings are keyed by platform and command ID with the button ID as the
		/// value (e.g. "PS3|M", "Triangle").
		/// </remarks>
		public Dictionary<string, string> ButtonMappings { get; private set; }

		/// <summary>
		/// A dictionary of combo groups keyed by the combo groups' names.
		/// </summary>
		public Dictionary<string, ComboGroup> ComboGroups { get; private set; }

		/// <summary>
		/// Gets all attack combos in all groups flattened by following each chain to an end.
		/// </summary>
		/// <returns>Returns a list (List&lt;&gt;) of FlattenedCombos.</returns>
		public List<FlattenedCombo> FlattenedCombos { get; private set; }

		/// <summary>
		/// A list of the longest flattened combos built. This list is only valid after referencing
		/// FlattenedCombos.
		/// </summary>
		public List<FlattenedCombo> LongestCombos { get; private set; }

		/// <summary>
		/// The starting combo group; the root of all combos in a chain.
		/// </summary>
		public ComboGroup StartingComboGroup { get; private set; }

		#endregion


		/// <summary>
		/// Initializes an instance of ComboDefinitions by reading the configuration from a 
		/// specified definitions file.
		/// </summary>
		/// <param name="definitionsFilePath">The full path to a definitions file.</param>
		public ComboDefinitions(XmlDocument definitionsDocument) {
			// Prepare the needed objects.
			AssignedAspects = new List<string>();
			AvailableCommands = new Dictionary<string, Command>();
			ButtonMappings = new Dictionary<string, string>();
			ComboGroups = new Dictionary<string, ComboGroup>();

			// Read the definitions.
			ReadConfiguration(definitionsDocument);

			// Generate the flattened combos from the read definitions.
			LongestCombos = new List<FlattenedCombo>();
			FlattenedCombos = new List<FlattenedCombo>();
			GenerateFlattenedCombos();
		}

		/// <summary>
		/// Clears the completion status of all flattened combos.
		/// </summary>
		public void ClearCompletionStatus() {
			// Clear the IsCompleted flag of each FlattenedCombo.
			foreach (FlattenedCombo Combo in FlattenedCombos) {
				Combo.IsCompleted = false;
			}
		}

		/// <summary>
		/// Gets the ID of a button mapped to the specified platform and command ID.
		/// </summary>
		/// <param name="platform">The platform to get a mapped button for.</param>
		/// <param name="commandId">The ID of the command to get a mapped button for.</param>
		/// <returns></returns>
		public string GetMappedButtonId(string platform, string commandId) {
			// Try to find a button mapping for the platform and command ID.
			string Key = string.Format(ButtonMappingKeyFormat, platform, commandId);
			if (ButtonMappings.ContainsKey(Key)) {
				return ButtonMappings[Key];
			} else {
				return null;
			}
		}

		/// <summary>
		/// Loads any completed combos from the specified stream. This instance's 
		/// FlattenedCombos are updated based on the information found in the load stream.
		/// </summary>
		/// <param name="loadStream">The Stream to load from.</param>
		public void LoadCompletedCombos(Stream loadStream) {
			if (loadStream != null) {
				List<string> CompletedComboSequences = new List<string>();

				// Open the file for reading.
				using (StreamReader Reader = new StreamReader(loadStream)) {
					// Read the completed combos from the stream into a list of display sequences.
					string Line;
					while ((Line = Reader.ReadLine()) != null) {
						CompletedComboSequences.Add(Line);
					}
				}

				if (CompletedComboSequences.Count > 0) {
					// Sort the completed sequences.
					CompletedComboSequences.Sort();

					// Clone the list of flattened combos and sort it by DisplaySequence.
					List<FlattenedCombo> WorkingList = new List<FlattenedCombo>(FlattenedCombos);
					WorkingList.Sort((x, y) => x.DisplaySequence.CompareTo(y.DisplaySequence));

					// Look for a match of each completed combo sequence in the working list.
					// * Simultaneously traverse both sorted lists looking for a match.
					int j = 0;
					int ComparisonResult;
					for (int i = 0; i < CompletedComboSequences.Count; i++) {
						ComparisonResult = 1;
						while (j < WorkingList.Count && ComparisonResult > 0) {
							// Compare the two display sequences.
							ComparisonResult = CompletedComboSequences[i].CompareTo(WorkingList[j].DisplaySequence);
							if (ComparisonResult == 0) {
								// Mark the combo as completed.
								WorkingList[j].IsCompleted = true;
							}

							j++;
						}
					}
				}
			}
		}

		/// <summary>
		/// Saves the completed combos to the specified stream.
		/// </summary>
		/// <param name="saveStream">The Stream to save to.</param>
		public void SaveCompletedCombos(Stream saveStream) {
			if (saveStream != null) {
				// Open the file for writing.
				using (StreamWriter Writer = new StreamWriter(saveStream)) {
					// Traverse the list of FlattenedCombos, looking for completed ones.
					foreach (FlattenedCombo combo in FlattenedCombos) {
						if (combo.IsCompleted) {
							Writer.WriteLine(combo.DisplaySequence);
						}
					}
				}
			}
		}


		/// <summary>
		/// Adds an aspect to a flattened combo and ensures it is in the AssignedAspects list.
		/// </summary>
		/// <param name="combo">The FlattenedCombo to add the aspect to.</param>
		/// <param name="aspect">The aspect to add.</param>
		private void AddAspectToFlattenedCombo(FlattenedCombo combo, string aspect) {
			combo.Aspects.Add(aspect);
			if (!AssignedAspects.Contains(aspect)) {
				AssignedAspects.Add(aspect);
			}
		}

		/// <summary>
		/// Builds command sequences recursively to form the possible flattened combos.
		/// </summary>
		/// <param name="combo">An attack combo to build command sequences from.</param>
		/// <param name="startedSequence">A started command sequence in a chain.</param>
		private void BuildSequences(AttackCombo combo, List<Command> startedSequence) {
			// Combine any started sequence with the sequence for this combo as a new sequence.
			List<Command> CurrentSequence = new List<Command>();
			if (startedSequence != null) {
				CurrentSequence.AddRange(startedSequence);
			}
			CurrentSequence.AddRange(combo.CommandSequence);

			// Add this sequence to the master list as a new flat combo, if it contains at least 2 commands.
			if (CurrentSequence.Count > 1) {
				FlattenedCombo FlatCombo = new FlattenedCombo(CurrentSequence);
				FlattenedCombos.Add(FlatCombo);

				if (combo.NextGroupInChain == null) {
					// If this combo ends a chain, indicate that.
					AddAspectToFlattenedCombo(FlatCombo, "Complete Chain");

					// Test the combo for the longest calculated so far.
					TestComboForLongest(FlatCombo);
				}
			}

			if (combo.NextGroupInChain != null) {
				// Continue building on this chain.
				foreach (AttackCombo ChainedCombo in combo.NextGroupInChain.AttackCombos) {
					BuildSequences(ChainedCombo, CurrentSequence);
				}
			}
		}

		/// <summary>
		/// Recursively builds a list of shortest path sequences that are necessary to complete 
		/// in order to receive the "Combo Specialist" achievement/trophy.
		/// </summary>
		/// <param name="group">The initial combo group to calculate from.</param>
		/// <param name="startedSequence">Any started sequence to build onto.</param>
		private void BuildShortestPathSequences(ComboGroup group, List<Command> startedSequence) {
			// Create a new sequence of commands and initialize it with any started sequence.
			List<Command> Sequence = null;
			if (startedSequence == null) {
				startedSequence = new List<Command>();
			}

			// Traverse each combo in the group, add it to the list of shortest path 
			// combos, and check its next group for the need to continue down that path.
			Dictionary<ComboGroup, AttackCombo> CombosToContinue = new Dictionary<ComboGroup, AttackCombo>();
			foreach (AttackCombo Combo in group.AttackCombos) {
				// Create a new build sequence for this combo from any started sequence
				// plus its own command sequence.
				Sequence = new List<Command>(startedSequence);
				Sequence.AddRange(Combo.CommandSequence);

				// Add this sequence as a new shortest path combo, if it has more than 1 command.
				if (Sequence.Count > 1) {
					ShortestPathCombos.Add(new FlattenedCombo(Sequence));
				}

				if (Combo.NextGroupInChain != null) {
					// Check this combo's next group against any existing ones to continue.
					if (CombosToContinue.ContainsKey(Combo.NextGroupInChain)) {
						// Compare the existing combo to continue for this group with the current
						// combo by their command sequence counts.
						AttackCombo ComboToContinue = CombosToContinue[Combo.NextGroupInChain];
						if (Combo.CommandSequence.Count < ComboToContinue.CommandSequence.Count) {
							// Swap the combo to continue for this new shorter one.
							CombosToContinue[Combo.NextGroupInChain] = Combo;
						}
					} else if (!GroupsToFollow.Contains(Combo.NextGroupInChain)) {
						// Add the combo as one to continue for the next group.
						GroupsToFollow.Add(Combo.NextGroupInChain);
						CombosToContinue.Add(Combo.NextGroupInChain, Combo);
					}
				}
			}

			// Build onto the combos to continue recursively.
			foreach (AttackCombo Combo in CombosToContinue.Values) {
				// Create a new build sequence for this combo from any started sequence
				// plus its own command sequence.
				Sequence = new List<Command>(startedSequence);
				Sequence.AddRange(Combo.CommandSequence);

				BuildShortestPathSequences(Combo.NextGroupInChain, Sequence);
			}
		}

		/// <summary>
		/// Generates flattened combos for the definitions.
		/// </summary>
		private void GenerateFlattenedCombos() {
			// Start with the starting combo group, and build a flattened list of command sequences for all chained attack combos.
			FlattenedCombos.Clear();
			FlattenedCombos = new List<FlattenedCombo>();
			foreach (AttackCombo Combo in StartingComboGroup.AttackCombos) {
				BuildSequences(Combo, null);
			}

			// Add the "longest" aspect to any combos that have the highest count.
			foreach (FlattenedCombo LongCombo in LongestCombos) {
				AddAspectToFlattenedCombo(LongCombo, "Longest Combo");
			}

			// Build a list of the combos that have the shortest path to cover all combos
			// found in all groups.
			BuildShortestPathSequences(StartingComboGroup, null);

			// Make a copy of the flattened combos list, sorted by the DisplaySequence.
			DisplaySequenceComparer Comparer = new DisplaySequenceComparer();
			List<FlattenedCombo> LookupList = new List<FlattenedCombo>(FlattenedCombos);
			LookupList.Sort(Comparer);
			
			// Attempt to find each shortest path combo in the lookup list and add an
			// aspect to it for the "Combo Specialist" goal.
			foreach (FlattenedCombo Combo in ShortestPathCombos) {
				int i = LookupList.BinarySearch(Combo, Comparer);
				if (i >= 0) {
					AddAspectToFlattenedCombo(LookupList[i], "Combo Specialist");
				}
			}

			// Sort the assigned aspects list.
			AssignedAspects.Sort();
		}

		/// <summary>
		/// Gets an icon from the application's resource dictionary by its key.
		/// </summary>
		/// <param name="resourceKey">The key of the icon resource to retrieve.</param>
		/// <returns>Returns a Drawing representing the icon found by the resource key.</returns>
		private Drawing GetIconFromResourceDictionary(string resourceKey) {
			Drawing Icon = Application.Current.FindResource(resourceKey) as Drawing;
			return Icon;
		}

		/// <summary>
		/// Gets an existing combo group by name if it exists; otherwise, a new one is created and returned.
		/// </summary>
		/// <param name="name">The name of a combo group to find or create.</param>
		/// <returns>Returns a new or existing combo group with the specified name.</returns>
		private ComboGroup GetNewOrExistingComboGroup(string name) {
			// Attempt to find an existing combo group with the same name.
			ComboGroup Result = null;
			if (ComboGroups.ContainsKey(name)) {
				Result = ComboGroups[name];
			} else {
				// Create a new combo group and add it to the list.
				Result = new ComboGroup(name);
				ComboGroups.Add(name, Result);
			}

			return Result;
		}

		/// <summary>
		/// Reads the configuration from a combo definitions file.
		/// </summary>
		/// <param name="definitionsDocument">The XML document containing the combo definitions.</param>
		private void ReadConfiguration(XmlDocument definitionsDocument) {
			// Read the defined buttons.
			XmlNodeList CommandNodes = definitionsDocument.SelectNodes("ComboDefinitions/Commands/Command");
			foreach (XmlNode CommandNode in CommandNodes) {
				// Create the defined command and add it to the list of available commands.
				Command DefinedCommand = new Command(
					CommandNode.Attributes["id"].Value,
					CommandNode.Attributes["name"].Value
				);
				AvailableCommands.Add(DefinedCommand.Id, DefinedCommand);
			}

			// Read the platform/button mappings.
			XmlNodeList PlatformNodes = definitionsDocument.SelectNodes("ComboDefinitions/DefaultButtonMappings/Platform");
			foreach (XmlNode PlatformNode in PlatformNodes) {
				// Add the button mapping by platform.
				string Platform = PlatformNode.Attributes["name"].Value;
				
				// Read the button mappings for this platform.
				XmlNodeList ButtonMappingNodes = PlatformNode.SelectNodes("ButtonMapping");
				foreach (XmlNode ButtonMappingNode in ButtonMappingNodes) {
					string CommandId = ButtonMappingNode.Attributes["command"].Value;
					if (!string.IsNullOrEmpty(CommandId)) {
						// Find the command by its ID.
						Command Command = AvailableCommands[CommandId];
						if (Command != null) {
							string ButtonId = ButtonMappingNode.Attributes["button"].Value;
							ButtonMappings.Add(string.Format(ButtonMappingKeyFormat, Platform, CommandId), ButtonId);
						}
					}
				}
			}

			// Read the defined combo groups.
			XmlNodeList ComboGroupNodes = definitionsDocument.SelectNodes("ComboDefinitions/ComboGroups/ComboGroup");
			foreach (XmlNode ComboGroupNode in ComboGroupNodes) {
				// Create the defined combo group and add it to the list, if it isn't already.
				ComboGroup DefinedComboGroup = GetNewOrExistingComboGroup(ComboGroupNode.Attributes["name"].Value);
				
				// Read the defined attack combos for this group.
				XmlNodeList AttackComboNodes = ComboGroupNode.SelectNodes("AttackCombo");
				foreach (XmlNode AttackComboNode in AttackComboNodes) {
					// Create the defined attack combo.
					AttackCombo DefinedAttackCombo = new AttackCombo();

					// Parse the command sequence and build a list for this attack combo.
					string DefinedCommandSequence = AttackComboNode.Attributes["commandSequence"].Value;
					string[] CommandsInSequence = DefinedCommandSequence.Split(',');
					if (CommandsInSequence != null) {
						foreach (string CommandId in CommandsInSequence) {
							// Find a matching command by Id.
							if (AvailableCommands.ContainsKey(CommandId)) {
								DefinedAttackCombo.CommandSequence.Add(AvailableCommands[CommandId]);
							} else {
								throw new ArgumentOutOfRangeException(
									string.Format(
										"Command, '{0}', not found for defined combo, '{1}', in combo group, '{2}'",
										CommandId,
										DefinedCommandSequence,
										DefinedComboGroup.Name
									)
								);
							}
						}

						// Read the next group in the combo chain and set it accordingly for this attack combo.
						XmlAttribute NextGroupInChainNode = AttackComboNode.Attributes["nextGroupInChain"];
						if (NextGroupInChainNode != null && !string.IsNullOrEmpty(NextGroupInChainNode.Value)) {
							DefinedAttackCombo.NextGroupInChain = GetNewOrExistingComboGroup(NextGroupInChainNode.Value);
						}

						// Add the attack combo to the group.
						DefinedComboGroup.AttackCombos.Add(DefinedAttackCombo);
					} else {
						throw new ArgumentException(
							string.Format(
								"There is a problem with the command seqence, '{0}', in combo group, '{1}'",
								DefinedCommandSequence,
								DefinedComboGroup.Name
							)
						);
					}
				}
			}

			// Read the starting combo group; the root group that all combos start from.
			XmlNode StartingComboGroupNode = definitionsDocument.SelectSingleNode("ComboDefinitions/StartingComboGroup");
			if (StartingComboGroupNode != null) {
				StartingComboGroup = ComboGroups[StartingComboGroupNode.InnerText];
				if (StartingComboGroup == null) {
					throw new ArgumentNullException("StartingComboGroup value does not identify a defined combo group.");
				}
			} else {
				throw new ArgumentNullException("Missing required StartingComboGroup node in combo definitions.");
			}
		}
		
		/// <summary>
		/// Tests a combo against any existing long combos to see if it is the same length
		/// or longer. The combo is added to a "longest" list as needed.
		/// </summary>
		/// <param name="comboToTest">The FlattenedCombo to test.</param>
		private void TestComboForLongest(FlattenedCombo comboToTest) {
			if (LongestCombos.Count > 0) {
				// Compare the sequence length of the combo to one already in the list.
				int TestCount = comboToTest.CommandSequence.Count;
				int ExistingCount = LongestCombos[0].CommandSequence.Count;
				if (TestCount > ExistingCount) {
					// Clear the list and add the new longest combo to it.
					LongestCombos.Clear();
					LongestCombos.Add(comboToTest);
				} else if (TestCount == ExistingCount) {
					// Add the combo to the list.
					LongestCombos.Add(comboToTest);
				}
			} else {
				// Add the combo to the empty list.
				LongestCombos.Add(comboToTest);
			}
		}
		
	}

}