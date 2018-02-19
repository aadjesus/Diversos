using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Web.UI.Design;

using Mvc.Components.Design;

namespace Mvc.Components.Controller
{
	/// <summary>
	/// This class represents a link between the view and the model.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ViewInfo
	{
		#region Internal members, overrides & ctors
		/// <summary>
		/// Provides a means to link this view information with the parent controller.
		/// </summary>
		internal BaseController Controller;

		/// <summary>
		/// Provides a friendly string representation.
		/// </summary>
		public override string ToString()
		{
			if (_controlproperty == String.Empty && 
				_model == String.Empty && _modelproperty == String.Empty)
				return "No mapping configured.";
			else
				return _model + "." + _modelproperty + " -> " + _controlid + "." + _controlproperty;
		}

		public ViewInfo()
		{
		}

		public ViewInfo(BaseController parentController, string controlId)
		{
			Controller = parentController;
			_controlid = controlId;
		}

		public ViewInfo(string controlId, string controlProperty, string model, string modelProperty)
		{
			_controlid = controlId;
			_controlproperty = controlProperty;
			_model = model;
			_modelproperty = modelProperty;
		}

		/// <summary>
		/// The control ID.
		/// </summary>
		internal string ControlID
		{
			get { return _controlid; }
			set { _controlid = value; }
		} string _controlid = String.Empty;

		/// <summary>
		/// Indicates whether the class has already been hooked to property change handlers.
		/// </summary>
		internal bool IsHooked
		{
			get { return _ishooked; }
			set { _ishooked = value; }
		} bool _ishooked = false;

		#endregion

		#region Public properties

		/// <summary>
		/// The name of the model in use.
		/// </summary>
		[TypeConverter(typeof(ModelNameConverter))]
		public string Model
		{
			get { return _model; }
			set { _model = value; }
		} string _model = String.Empty;
		
		/// <summary>
		/// The model property used by this mapping.
		/// </summary>
		[DefaultValue("")]
		[TypeConverter(typeof(ModelPropertyConverter))]
		public string ModelProperty
		{
			get { return _modelproperty; }
			set { _modelproperty = value; }
		} string _modelproperty = String.Empty;

		/// <summary>
		/// The control property updated by this mapping.
		/// </summary>
		[DefaultValue("")]
		[TypeConverter(typeof(ControlPropertyConverter))]
		public string ControlProperty
		{
			get { return _controlproperty; }
			set { _controlproperty = value; }
		} string _controlproperty = String.Empty;

		#endregion
	}
}
