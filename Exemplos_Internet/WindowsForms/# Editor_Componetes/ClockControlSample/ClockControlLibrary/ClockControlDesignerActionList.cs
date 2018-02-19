using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ClockControlLibrary
{
	public class ClockControlDesignerActionList : DesignerActionList
	{
		public ClockControlDesignerActionList(IComponent component)
			: base(component)
		{

			// Automatically display smart tag panel when
			// design-time component is dropped onto the
			// Windows Forms Designer
			this.AutoShow = true;
		}

		public override DesignerActionItemCollection GetSortedActionItems()
		{

			// Create list to store designer action items
			DesignerActionItemCollection actionItems = new DesignerActionItemCollection();

			// Add Appearance category header text
			actionItems.Add(new DesignerActionHeaderItem("Appearance"));

			// Add Appearance category descriptive label
			actionItems.Add(
			  new DesignerActionTextItem(
				"Properties that affect how the ClockControl looks.",
				"Appearance"));

			// Add Face designer action property item
			actionItems.Add(
			  new DesignerActionPropertyItem(
				"Face",
				"Face",
				GetCategory(this.ClockControl, "Face"),
				GetDescription(this.ClockControl, "Face")));

			// Add DigitalTimeFormat designer action property
			actionItems.Add(
			  new DesignerActionPropertyItem(
				"DigitalTimeFormat",
				"Digital Time Format",
				GetCategory(this.ClockControl, "DigitalTimeFormat"),
				GetDescription(this.ClockControl, "DigitalTimeFormat")));

			// EditClockHands designer action method item
			actionItems.Add(
			  new DesignerActionMethodItem(
				this,
				"EditClockHands",
				"Edit Clock HandsX",
				"Appearance",
				"Configure the ClockControl's hour, minute and second hands.",
				true));

			// Add Behavior category header text
			actionItems.Add(new DesignerActionHeaderItem("Behavior"));

			// Add Behavior category descriptive label
			actionItems.Add(
			  new DesignerActionTextItem(
				"Properties that affect how the ClockControl behaves.",
				"Behavior"));

			// Add BackupAlarm designer action property item
			actionItems.Add(
			  new DesignerActionPropertyItem(
				"BackupAlarm",
				"Backup Alarm",
				GetCategory(this.ClockControl, "BackupAlarm"),
				GetDescription(this.ClockControl, "BackupAlarm")));

			// Add PrimaryAlarm designer action property item
			actionItems.Add(
			  new DesignerActionPropertyItem(
				"PrimaryAlarm",
				"Primary Alarm",
				GetCategory(this.ClockControl, "PrimaryAlarm"),
				GetDescription(this.ClockControl, "PrimaryAlarm")));

			// Add Design category header text
			actionItems.Add(new DesignerActionHeaderItem("Design"));

			// Add Design category descriptive label
			actionItems.Add(
			  new DesignerActionTextItem(
				"Properties that affect how the ClockControl acts in the designer.",
				"Design"));

			// ShowBorder designer action property item
			actionItems.Add(
			  new DesignerActionPropertyItem(
				"ShowBorder",
				"Show Border",
				GetCategory(this.Designer, "ShowBorder"),
				GetDescription(this.Designer, "ShowBorder")));

			// Dock/Undock designer action method item
			actionItems.Add(
			  new DesignerActionMethodItem(
				this,
				"ToggleDockStyle",
				GetDockStyleText(),
				"Design",
				"Dock or undock this control in it's parent container.",
				true));

			//// Add Face designer action property item
			//actionItems.Add(
			//  new DesignerActionPropertyItem(
			//    "Face",
			//    "Face"));

			//// Add DigitalTimeFormat designer action property item
			//actionItems.Add(
			//  new DesignerActionPropertyItem(
			//    "DigitalTimeFormat",
			//    "Digital Time Format"));

			//// Add BackupAlarm designer action property item
			//actionItems.Add(
			//  new DesignerActionPropertyItem(
			//    "BackupAlarm",
			//    "Backup Alarm"));

			//// Add PrimaryAlarm designer action property item
			//actionItems.Add(
			//  new DesignerActionPropertyItem(
			//    "PrimaryAlarm",
			//    "Primary Alarm"));

			//// ShowBorder designer action property item
			//actionItems.Add(
			//  new DesignerActionPropertyItem(
			//    "ShowBorder",
			//    "Show Border"));

			////// ShowBorder designer action property item
			////actionItems.Add(
			////  new DesignerActionPropertyItem(
			////    "HourHand",
			////    "Hour Hand"));

			////// ShowBorder designer action property item
			////actionItems.Add(
			////  new DesignerActionPropertyItem(
			////    "MinuteHand",
			////    "Minute Hand"));

			////// ShowBorder designer action property item
			////actionItems.Add(
			////  new DesignerActionPropertyItem(
			////    "SecondHand",
			////    "Second Hand"));

			//// EditClockHands designer action method item
			//actionItems.Add(
			//  new DesignerActionMethodItem(this, "EditClockHands", "Edit Clock Hands...", true));

			//// Dock/Undock designer action method item
			//actionItems.Add(
			//  new DesignerActionMethodItem(
			//    this,
			//    "ToggleDockStyle",
			//    GetDockStyleText()));

			// Return action list to design time item
			return actionItems;
		}

		// Face proxy property
		//[CategoryAttribute("Appearance")]
		//[DescriptionAttribute("Determines the clock face type to display.")]
		//[DisplayNameAttribute("Face")]
		//[Editor(typeof(FaceEditor), typeof(UITypeEditor))]
		public ClockFace Face
		{
			get { return this.ClockControl.Face; }
			set { SetProperty("Face", value); }
		}

		//DigitalTimeFormat proxy property
		//[CategoryAttribute("Appearance")]
		//[DescriptionAttribute("The digital time format, constructed from .NET format specifiers.")]
		//[DisplayNameAttribute("Digital Time Format")]
		[Editor(typeof(DigitalTimeFormatEditor), typeof(UITypeEditor))]
		public string DigitalTimeFormat
		{
			get { return this.ClockControl.DigitalTimeFormat; }
			set { SetProperty("DigitalTimeFormat", value); }
		}

		// EditClockHands designer action method implementation
		//[CategoryAttribute("Appearance")]
		//[DescriptionAttribute("Edit Hour Hand, Minute Hand and Second Hand.")]
		//[DisplayNameAttribute(this.GetDockStyleText())]
		public void EditClockHands()
		{
			// Create form
			HandsEditorForm form = new HandsEditorForm();
			// Set current hand values
			form.HourHand = this.ClockControl.HourHand;
			form.MinuteHand = this.ClockControl.MinuteHand;
			form.SecondHand = this.ClockControl.SecondHand;
			// Update new hand values of OK button w  as pressed
			if (form.ShowDialog() == DialogResult.OK)
			{
				SetProperty("HourHand", form.HourHand);
				SetProperty("MinuteHand", form.MinuteHand);
				SetProperty("SecondHand", form.SecondHand);
			}
		}

		// HourHand proxy property
		public Hand HourHand
		{
			get { return this.ClockControl.HourHand; }
			set { SetProperty("HourHand", value); }
		}

		// MinuteHand proxy property
		public Hand MinuteHand
		{
			get { return this.ClockControl.MinuteHand; }
			set { SetProperty("MinuteHand", value); }
		}

		// SecondHand proxy property
		public Hand SecondHand
		{
			get { return this.ClockControl.SecondHand; }
			set { SetProperty("SecondHand", value); }
		}

		// PrimaryAlarm proxy property
		//[CategoryAttribute("Behavior")]
		//[DescriptionAttribute("Backup alarm for very very late risers.")]
		//[DisplayNameAttribute("Primary Alarm")]
		public DateTime PrimaryAlarm
		{
			get { return this.ClockControl.PrimaryAlarm; }
			set { SetProperty("PrimaryAlarm", value); }
		}

		// BackupAlarm proxy property
		//[CategoryAttribute("Behavior")]
		//[DescriptionAttribute("Primary alarm for late risers.")]
		//[DisplayNameAttribute("Backup Alarm")]
		public DateTime BackupAlarm
		{
			get { return this.ClockControl.BackupAlarm; }
			set { SetProperty("BackupAlarm", value); }
		}

		// ShowBorder proxy property
		//[CategoryAttribute("Design")]
		//[DescriptionAttribute("Show/Hide a border at design-time.")]
		//[DisplayNameAttribute("Show Border")]
		public bool ShowBorder
		{
			get { return this.Designer.ShowBorder; }
			set { this.Designer.ShowBorder = value; }
		}

		// Dock/Undock designer action method implementation
		//[CategoryAttribute("Design")]
		//[DescriptionAttribute("Dock/Undock in parent container.")]
		//[DisplayNameAttribute("Dock/Undock in parent container")]
		public void ToggleDockStyle()
		{

			// Toggle ClockControl's Dock property
			if (this.ClockControl.Dock != DockStyle.Fill)
			{
				SetProperty("Dock", DockStyle.Fill);
			}
			else
			{
				SetProperty("Dock", DockStyle.None);
			}
		}

		// Helper method that returns an appropriate
		// display name for the Dock/Undock property,
		// based on the ClockControl's current Dock 
		// property value
		private string GetDockStyleText()
		{
			if (this.ClockControl.Dock == DockStyle.Fill)
			{
				return "Undock in parent container";
			}
			else
			{
				return "Dock in parent container";
			}
		}

		// Helper property to acquire a ClockControl reference
		private ClockControl ClockControl
		{
			get { return (ClockControl)this.Component; }
		}

		// Helper method to acquire a ClockControlDesigner reference
		private ClockControlDesigner Designer
		{
			get
			{
				IDesignerHost designerHost = (IDesignerHost)this.ClockControl.Site.Container;
				return (ClockControlDesigner)designerHost.GetDesigner(this.ClockControl);
			}
		}

		// Helper method to safely set a component’s property
		private void SetProperty(string propertyName, object value)
		{
			// Get property
			PropertyDescriptor property = TypeDescriptor.GetProperties(this.ClockControl)[propertyName];
			// Set property value
			property.SetValue(this.ClockControl, value);
		}

		// Helper method to return the Category string from a
		// CategoryAttribute assigned to a property exposed by 
		//the specified object
		private string GetCategory(object source, string propertyName)
		{
			PropertyInfo property = source.GetType().GetProperty(propertyName);
			CategoryAttribute attribute = (CategoryAttribute)property.GetCustomAttributes(typeof(CategoryAttribute), false)[0];
			if (attribute == null) return null;
			return attribute.Category;
		}

		// Helper method to return the Description string from a
		// DescriptionAttribute assigned to a property exposed by 
		//the specified object
		private string GetDescription(object source, string propertyName)
		{
			PropertyInfo property = source.GetType().GetProperty(propertyName);
			DescriptionAttribute attribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
			if (attribute == null) return null;
			return attribute.Description;
		}
	}
}
