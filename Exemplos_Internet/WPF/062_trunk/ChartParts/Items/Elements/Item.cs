// <copyright file="Item.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. Abstract base element class of all Chart items.</summary>
// <revision>$Id: Item.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Abstract base element class of all Chart items.
	/// </summary>
	public abstract class Item : FrameworkElement, ISupportInitialize
	{
		/// <summary>
		/// Contained Item Visuals which do all rendering.
		/// </summary>
		protected VisualCollection visuals;

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class.
		/// </summary>
		public Item()
		{
			visuals = new VisualCollection(this);
		}

		#region Dependency properties
		#region ItemDataView
		/// <summary>
		/// Identifies the ItemDataView dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemDataViewProperty
			= DependencyProperty.Register("ItemDataView", typeof(ItemDataView), typeof(Item)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						, ItemDataViewPropertyChanged
					)
				);
		/// <summary>
		/// Gets or sets the ItemDataView property.
		/// </summary>
		/// <value>ItemData value</value>
		public ItemDataView ItemDataView
		{
			get { return (ItemDataView)GetValue(ItemDataViewProperty); }
			set { SetValue(ItemDataViewProperty, value); }
		}
		/// <summary>
		/// Resets PropertyChanged event handler and bindings to ItemDataView.
		/// ItemDataViewProperty changed event hadler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void ItemDataViewPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Item item = sender as Item;
			if (item != null)
			{
				ItemDataView oldItem = e.OldValue as ItemDataView;
				if (oldItem != null)
					oldItem.PropertyChanged -= item.ItemDataViewChanged;

				ItemDataView newItem = e.NewValue as ItemDataView;
				if (newItem != null)
					newItem.PropertyChanged += item.ItemDataViewChanged;
			}
		}

		/// <summary>
		/// Handles the PropertyChanged event of the ItemDataView.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		private void ItemDataViewChanged(object sender, PropertyChangedEventArgs e)
		{
			// TODO Manage Mouse Enter/Leave ...

			// Notify derived classes on ItemDataView Change.
			OnItemDataViewChanged(e);

			if (e.PropertyName == "VisualCue")
				RaiseEvent(new RoutedEventArgs(VisualCueChangedEvent));

			RenderVisuals();
			InvalidateVisual();
			InvalidateMeasure();
		}
		/// <summary>
		/// Notifies derived classes on ItemDataView Change.
		/// </summary>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		protected virtual void OnItemDataViewChanged(PropertyChangedEventArgs e) { }
		#endregion ItemDataView
		#endregion Dependency properties

		#region Routed events
		#region Click
		/// <summary>
		/// ClickEvent signals that the Item Visual is clicked with some mouse button.
		/// </summary>
		public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
			RoutingStrategy.Bubble, typeof(ClickEventHandler), typeof(Item));
		/// <summary>
		/// ClickEvent signals that the Item Visual is clicked with some mouse button.
		/// </summary>
		public event ClickEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}
		#endregion Click

		#region MouseEnterItem
		/// <summary>
		/// MouseEnterItemEvent signals that the Mouse is entered the Item's contained Visual.
		/// </summary>
		public static readonly RoutedEvent MouseEnterItemEvent = EventManager.RegisterRoutedEvent("MouseEnterItem",
			RoutingStrategy.Bubble, typeof(MouseItemEventHandler), typeof(Item));
		/// <summary>
		/// MouseEnterItemEvent signals that the Mouse is entered the Item's contained Visual.
		/// </summary>
		public event MouseItemEventHandler MouseEnterItem
		{
			add { AddHandler(MouseEnterItemEvent, value); }
			remove { RemoveHandler(MouseEnterItemEvent, value); }
		}
		#endregion MouseEnterItem

		#region MouseLeaveItem
		/// <summary>
		/// MouseLeaveItemEvent signals that the Mouse is leaved the Item's contained Visual.
		/// </summary>
		public static readonly RoutedEvent MouseLeaveItemEvent = EventManager.RegisterRoutedEvent("MouseLeaveItem",
			RoutingStrategy.Bubble, typeof(MouseItemEventHandler), typeof(Item));
		/// <summary>
		/// MouseEnterItemEvent signals that the Mouse is entered the Item's contained Visual.
		/// </summary>
		public event MouseItemEventHandler MouseLeaveItem
		{
			add { AddHandler(MouseLeaveItemEvent, value); }
			remove { RemoveHandler(MouseLeaveItemEvent, value); }
		}
		#endregion MouseLeaveItem

		#region VisualCueChanged
		/// <summary>
		/// VisualCueChanged Event signals that the ItemDataView.VisualCue property changed.
		/// </summary>
		public static readonly RoutedEvent VisualCueChangedEvent 
			= EventManager.RegisterRoutedEvent("VisualCueChanged", RoutingStrategy.Bubble
				, typeof(RoutedEventHandler), typeof(Item));
		/// <summary>
		/// VisualCueChanged Event signals that the ItemDataView.VisualCue property changed.
		/// </summary>
		public event RoutedEventHandler VisualCueChanged
		{
			add { AddHandler(VisualCueChangedEvent, value); }
			remove { RemoveHandler(VisualCueChangedEvent, value); }
		}
		#endregion VisualCueChanged
		#endregion Routed events

		/// <summary>
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method 
		/// is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set 
		/// to true internally.
		/// </summary>
		/// <remarks>
		/// Renders all visuals.
		/// </remarks>
		/// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
		protected override void OnInitialized(EventArgs e)
		{
			RenderVisuals();
		}

		/// <summary>
		/// Renders all visuals.
		/// </summary>
		void RenderVisuals()
		{
			foreach (Visual visual in visuals)
			{
				ItemVisual itemVisual = visual as ItemVisual;
				Debug.Assert(itemVisual != null, "itemVisual != null");
				itemVisual.Render();
			}
		}

		/// <summary>
		/// Item's visual HitTest.
		/// </summary>
		/// <param name="pt">The point.</param>
		/// <returns>Hit visual or null.</returns>
		object ItemHitTest(Point pt)
		{
			foreach (Visual visual in visuals)
			{
				ItemVisual itemVisual = visual as ItemVisual;
				Debug.Assert(itemVisual != null, "itemVisual != null");
				HitTestResult result = VisualTreeHelper.HitTest(itemVisual, pt);
				if (result != null && (result.VisualHit is ItemVisual || result.VisualHit is ChartPointVisual))
					return result.VisualHit;
			}
			return null;
		}

		#region Raise Click Event
		/// <summary>
		/// Raises the Click Event.
		/// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseUp"/> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the mouse button was released.</param>
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);

			object itemClicked = ItemHitTest(e.GetPosition(this));
			if (itemClicked != null)
				RaiseEvent(new ClickEventArgs(itemClicked, e));
		}
		#endregion Raise Click Event

		#region Raise MouseEnterItem/MouseLeaveItem Events
		/// <summary>
		/// The Item's Visual under the Mouse.
		/// </summary>
		private object objectUnderMouse;

		/// <summary>
		/// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseEnter"/> attached 
		/// event is raised on this element. Implement this method to add class handling for this event.
		/// </summary>
		/// <remarks>
		/// Sets the Item's Visual under Mouse.
		/// </remarks>
		/// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains 
		/// the event data.</param>
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			Debug.Assert(objectUnderMouse == null, "objectUnderMouse == null");

			objectUnderMouse = ItemHitTest(e.GetPosition(this));
			Debug.Assert(objectUnderMouse != null, "objectUnderMouse != null");
			if (ItemDataView.RaiseMouseEnterLeaveItemEvents)
				RaiseEvent(new MouseItemEventArgs(MouseEnterItemEvent, objectUnderMouse, e));
		}

		/// <summary>
		/// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseLeave"/> attached 
		/// event is raised on this element. Implement this method to add class handling for this event.
		/// </summary>
		/// <remarks>
		/// Clears the Item's Visual under Mouse.
		/// </remarks>
		/// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains the 
		/// event data.</param>
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			//Debug.Assert(objectUnderMouse != null, "objectUnderMouse != null");
			object obj = objectUnderMouse;
			objectUnderMouse = null;
			if (ItemDataView.RaiseMouseEnterLeaveItemEvents)
				RaiseEvent(new MouseItemEventArgs(MouseLeaveItemEvent, obj, e));
		}

		/// <summary>
		/// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseMove"/> attached 
		/// event reaches an element in its route that is derived from this class. Implement this 
		/// method to add class handling for this event.
		/// </summary>
		/// <remarks>
		/// Resets the Item's Visual under Mouse.
		/// </remarks>
		/// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains 
		/// the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Debug.Assert(objectUnderMouse != null, "objectUnderMouse != null");
			object obj = ItemHitTest(e.GetPosition(this));
			if (objectUnderMouse != obj)
			{
				if (ItemDataView.RaiseMouseEnterLeaveItemEvents)
					RaiseEvent(new MouseItemEventArgs(MouseLeaveItemEvent, objectUnderMouse, e));
				objectUnderMouse = obj;
				if (ItemDataView.RaiseMouseEnterLeaveItemEvents)
					RaiseEvent(new MouseItemEventArgs(MouseEnterItemEvent, objectUnderMouse, e));
			}
		}
		#endregion Raise MouseEnterItem/MouseLeaveItem Events

		#region OnContextMenu Overrides
		/// <summary>
		/// Attaches Item's Visual as the Tag to ContextMenu.
		/// Invoked whenever an unhandled <see cref="E:System.Windows.FrameworkElement.ContextMenuOpening"/> 
		/// routed event reaches this class in its route. Implement this method to add class handling 
		/// for this event.
		/// </summary>
		/// <remarks>
		/// Attaches Item's Visual as the Tag to ContextMenu.
		/// </remarks>
		/// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
		protected override void OnContextMenuOpening(ContextMenuEventArgs e)
		{
			ContextMenu mnu = ContextMenu;
			if (mnu != null)
				mnu.Tag = ItemHitTest(new Point(e.CursorLeft, e.CursorTop));
		}

		/// <summary>
		/// Invoked whenever an unhandled <see cref="E:System.Windows.FrameworkElement.ContextMenuClosing"/> 
		/// routed event reaches this class in its route. Implement this method to add class handling 
		/// for this event.
		/// </summary>
		/// <remarks>
		/// Detaches the Tag from ContextMenu.
		/// </remarks>
		/// <param name="e">Provides data about the event.</param>
		protected override void OnContextMenuClosing(ContextMenuEventArgs e)
		{
			ContextMenu mnu = ContextMenu;
			if (mnu != null)
				mnu.Tag = null;
		}
		#endregion OnContextMenu Overrides

		#region Visual Tree Overrides
		/// <inheritdoc />
		protected override int VisualChildrenCount
		{
			get { return visuals.Count; }
		}

		/// <inheritdoc />
		protected override Visual GetVisualChild(int index)
		{
			if (index >= visuals.Count)
				throw new ArgumentOutOfRangeException("index");

			return visuals[index];
		}
		#endregion Visual Tree Overrides

		#region Layout Overrides
		/// <inheritdoc />
		protected override Size MeasureOverride(Size availableSize)
		{
			Rect rect = Rect.Empty;
			foreach (Visual visual in visuals)
			{
				ItemVisual itemVisual = visual as ItemVisual;
				Debug.Assert(itemVisual != null, "itemVisual != null");

				rect.Union(itemVisual.ContentBounds);
				rect.Union(itemVisual.DescendantBounds);
			}

			if (rect.IsEmpty || double.IsInfinity(rect.Width) || double.IsInfinity(rect.Height))
				return new Size(0, 0);
			return new Size(rect.Width, rect.Height);
		}

		/// <inheritdoc />
		protected override Size ArrangeOverride(Size finalSize)
		{
			return finalSize;
		}
		#endregion Layout Overrides

		#region ISupportInitialize Members
		/// <exclude />
		protected bool initializing = false;
		/// <inheritdoc />
		public override void BeginInit()
		{
			base.BeginInit();
			initializing = true;
		}

		/// <inheritdoc />
		public override void EndInit()
		{
			base.EndInit();
			initializing = false;
			RenderVisuals();
			InvalidateVisual();
			InvalidateMeasure();
		}
		#endregion ISupportInitialize Members
	}

	#region Click Event support types
	/// <summary>
	/// Click Event handler.
	/// </summary>
	public delegate void ClickEventHandler(Object sender, ClickEventArgs e);

	/// <summary>
	/// Click Event arguments.
	/// </summary>
	public class ClickEventArgs : MouseButtonEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClickEventArgs"/> class.
		/// </summary>
		/// <param name="itemClicked">Clicked item's contained visual.</param>
		/// <param name="args">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> 
		/// instance containing the base event data.</param>
		public ClickEventArgs(object itemClicked, MouseButtonEventArgs args)
			: base(args.MouseDevice, args.Timestamp, args.ChangedButton, args.StylusDevice)
		{
			RoutedEvent = Item.ClickEvent;
			ItemClicked = itemClicked;
		}

		/// <summary>
		/// Gets or sets the clicked item's contained visual.
		/// </summary>
		/// <value>The item's contained visual clicked.</value>
		public object ItemClicked { get; private set; }

		/// <summary>
		/// Invokes event handlers in a type-specific way, which can increase event system 
		/// efficiency.
		/// </summary>
		/// <param name="genericHandler">The generic handler to call in a type-specific way.</param>
		/// <param name="genericTarget">The target to call the handler on.</param>
		protected override void InvokeEventHandler(Delegate genericHandler, Object genericTarget)
		{
			((ClickEventHandler)genericHandler).Invoke(genericTarget, this);
		}
	}
	#endregion Click Event support types

	#region Mouse Enter/Leave Item Events support types
	/// <summary>
	/// Click Event handler.
	/// </summary>
	public delegate void MouseItemEventHandler(Object sender, MouseItemEventArgs e);

	/// <summary>
	/// MouseItem Event arguments.
	/// </summary>
	public class MouseItemEventArgs : MouseEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MouseItemEventArgs"/> class.
		/// </summary>
		/// <param name="routedEvent">The routed event.</param>
		/// <param name="mouseItem">Related to Mouse item's contained visual.</param>
		/// <param name="args">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
		/// instance containing the base event data.</param>
		public MouseItemEventArgs(RoutedEvent routedEvent, object mouseItem, MouseEventArgs args)
			: base(args.MouseDevice, args.Timestamp, args.StylusDevice)
		{
			RoutedEvent = routedEvent;
			MouseItem = mouseItem;
		}

		/// <summary>
		/// Gets or sets the item's contained visual.
		/// </summary>
		/// <value>The item's contained visual.</value>
		public object MouseItem { get; private set; }

		/// <summary>
		/// Invokes event handlers in a type-specific way, which can increase event system 
		/// efficiency.
		/// </summary>
		/// <param name="genericHandler">The generic handler to call in a type-specific way.</param>
		/// <param name="genericTarget">The target to call the handler on.</param>
		protected override void InvokeEventHandler(Delegate genericHandler, Object genericTarget)
		{
			((MouseItemEventHandler)genericHandler).Invoke(genericTarget, this);
		}
	}
	#endregion Mouse Enter/Leave Item Events support types
}
