using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Galador.Applications
{
	public enum POCOBindingMode
	{
		TwoWay,
		OneWay,
		OneWayToSource,
	}

	public class POCOBinding : IDisposable
	{
		#region static Create<..>(..)

		public static POCOBinding Create<TProperty>(Expression<Func<TProperty>> getter, Expression<Func<TProperty>> setter)
		{
			return new POCOBinding
			{
				SourceProperty = PropertyPath.Create(getter),
				TargetProperty = PropertyPath.Create(setter),
			};
		}

		public static POCOBinding Create<TProperty>(POCOBindingMode mode, Expression<Func<TProperty>> getter, Expression<Func<TProperty>> setter)
		{
			return new POCOBinding
			{
				Mode = mode,
				SourceProperty = PropertyPath.Create(getter),
				TargetProperty = PropertyPath.Create(setter),
			};
		}

		public static POCOBinding Create<TSource, TTarget, TProperty>(TSource source, Expression<Func<TSource, TProperty>> getter, TTarget target, Expression<Func<TTarget, TProperty>> setter)
			where TSource : class
			where TTarget : class
		{
			return new POCOBinding
			{
				SourceProperty = PropertyPath.Create(source, getter),
				TargetProperty = PropertyPath.Create(target, setter),
			};
		}

		public static POCOBinding Create<TSource, TTarget, TProperty>(POCOBindingMode mode, TSource source, Expression<Func<TSource, TProperty>> getter, TTarget target, Expression<Func<TTarget, TProperty>> setter)
			where TSource : class
			where TTarget : class
		{
			return new POCOBinding
			{
				Mode = mode,
				SourceProperty = PropertyPath.Create(source, getter),
				TargetProperty = PropertyPath.Create(target, setter),
			};
		}

		#endregion

		#region Mode

		public POCOBindingMode Mode
		{
			get { return mMode; }
			set
			{
				if (value == mMode)
					return;
				mMode = value;
				Update();
			}
		}
		POCOBindingMode mMode = POCOBindingMode.TwoWay;

		#endregion

		#region SourceProperty

		public PropertyPath SourceProperty
		{
			get { return mSourceProperty; }
			set
			{
				if (mSourceProperty != null)
					mSourceProperty.PropertyChanged -= mSourceProperty_PropertyChanged;
				mSourceProperty = value;
				if (mSourceProperty != null)
					mSourceProperty.PropertyChanged += mSourceProperty_PropertyChanged;
				Update();
			}
		}
		PropertyPath mSourceProperty;

		void mSourceProperty_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (isUpdatingValue)
				return;
			if (mTargetProperty == null)
				return;
			if (e.PropertyName != "Value")
				return;
			try
			{
				isUpdatingValue = true;
				switch (Mode)
				{
					case POCOBindingMode.TwoWay:
					case POCOBindingMode.OneWay:
						mTargetProperty.Value = mSourceProperty.Value;
						break;
				}
			}
			finally { isUpdatingValue = false; }
		}

		#endregion

		#region TargetProperty

		public PropertyPath TargetProperty
		{
			get { return mTargetProperty; }
			set
			{
				if (mTargetProperty != null)
					mTargetProperty.PropertyChanged -= mTargetProperty_PropertyChanged;
				mTargetProperty = value;
				if (mSourceProperty != null)
					mTargetProperty.PropertyChanged += mTargetProperty_PropertyChanged;
				Update();
			}
		}
		PropertyPath mTargetProperty;

		void mTargetProperty_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (isUpdatingValue)
				return;
			if (mSourceProperty == null)
				return;
			if (e.PropertyName != "Value")
				return;
			try
			{
				switch (Mode)
				{
					case POCOBindingMode.TwoWay:
					case POCOBindingMode.OneWayToSource:
						mSourceProperty.Value = mTargetProperty.Value;
						break;
				}
			}
			finally { isUpdatingValue = false; }
		}

		#endregion

		#region internal: Update()

		void Update()
		{
			if (TargetProperty == null || SourceProperty == null)
				return;
			try
			{
				isUpdatingValue = true;
				switch (Mode)
				{
					case POCOBindingMode.TwoWay:
					case POCOBindingMode.OneWay:
						TargetProperty.Value = SourceProperty.Value;
						break;
					case POCOBindingMode.OneWayToSource:
						TargetProperty.Value = SourceProperty.Value;
						break;
				}
			}
			finally { isUpdatingValue = false; }
		}

		bool isUpdatingValue;

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (SourceProperty != null)
				SourceProperty.Dispose();
			SourceProperty = null;
			if (TargetProperty != null)
				TargetProperty.Dispose();
			TargetProperty = null;
		}

		#endregion
	}
}
