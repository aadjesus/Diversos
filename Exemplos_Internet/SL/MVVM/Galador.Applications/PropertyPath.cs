using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Galador.Applications
{
	public class PropertyPath : INotifyPropertyChanged, IDisposable
	{
		#region static Create<..>(..)

		public static PropertyPath Create<TProperty>(Expression<Func<TProperty>> e)
		{
			object root;
			var mp = e.GetPath(out root);
			return new PropertyPath(mp) { Source = root };
		}

		public static PropertyPath Create<TSource, TProperty>(Expression<Func<TSource, TProperty>> e)
		{
			object root;
			var mp = e.GetPath(out root);
			return new PropertyPath(mp) { Source = root };
		}

		public static PropertyPath Create<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> e)
		{
			return new PropertyPath(e.GetPath()) { Source = source };
		}

		#endregion

		readonly WeakPathElement[] elements;

		public PropertyPath(params MemberInfo[] propertyPath)
			: this((IEnumerable<MemberInfo>)propertyPath)
		{

		}
		public PropertyPath(IEnumerable<MemberInfo> ePropertyPath)
		{
			var propertyPath = ePropertyPath.ToList();
			elements = new WeakPathElement[propertyPath.Count];
			for (int i = 0; i < propertyPath.Count; i++)
			{
				// check chain validity!
				if (i > 0 && propertyPath[i - 1].GetValueType() != propertyPath[i].DeclaringType)
					throw new ArgumentException(string.Format("{0}.{1} is not a {2}", propertyPath[i - 1].GetValueType().Name, propertyPath[i - 1].Name, propertyPath[i].DeclaringType));

				int myindex = i; // beware, lambda use variable capture, as opposed to value capture
				elements[i] = new WeakPathElement(propertyPath[i], i);
				elements[i].ValueChanged += delegate { HandleValueChanged(myindex); };
			}

			// in case the 1st property is static, set up the value chain!
			if (elements.Length > 1)
				elements[1].Source = elements[0].Value;
		}

		public object Source
		{
			get { return elements[0].Source; }
			set
			{
				if (value == Source)
					return;
				elements[0].Source = value;
				OnPropertyChanged("Source");
			}
		}

		public Type SourceType { get { return elements[0].MemberInfo.DeclaringType; } }

		public object Value
		{
			get { return elements[elements.Length - 1].Value; }
			set { elements[elements.Length - 1].Value = value; }
		}

		public Type ValueType { get { return elements[elements.Length - 1].MemberInfo.GetValueType(); } }

		void HandleValueChanged(int index)
		{
			if (index == elements.Length - 1)
				OnPropertyChanged("Value");
			else
				elements[index + 1].Source = elements[index].Value;
		}

		void OnPropertyChanged(string name)
		{
			var e = PropertyChanged;
			if (e != null)
				e(this, new PropertyChangedEventArgs(name));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region IDisposable Members

		public void Dispose()
		{
			this.PropertyChanged = null;
			foreach (var item in elements)
				item.Source = null;
		}

		#endregion
	}

	#region WeakValue, WeakPathElement

	class WeakValue
	{
		public object Value
		{
			get { return isValueType ? mValueTypeValue : mReferenceValue.Target; }
			set
			{
				if (value == null || value.GetType().IsValueType)
				{
					isValueType = true;
					mValueTypeValue = value;
					mReferenceValue.Target = null;
				}
				else
				{
					isValueType = false;
					mValueTypeValue = null;
					mReferenceValue.Target = value;
				}
			}
		}
		bool isValueType;
		object mValueTypeValue;
		WeakReference mReferenceValue = new WeakReference(null);
	}

	class WeakPathElement
	{
		// make sure the source don't prevent garbage collection of property path
		MemberInfo mInfo;
		int mIndex;
		WeakValue mSource = new WeakValue();
		WeakValue mValue = new WeakValue();

		public WeakPathElement(MemberInfo pi, int index)
		{
			this.mInfo = pi;
			this.mIndex = index;
			this.mValue.Value = GetValue();
		}

		public MemberInfo MemberInfo { get { return mInfo; } }

		public object Source
		{
			get { return mSource.Value; }
			set
			{
				var oldS = mValue.Value;
				if (value == oldS)
					return;

				if (oldS is INotifyPropertyChanged)
					((INotifyPropertyChanged)oldS).PropertyChanged -= HandlePropertyChanged;
				if (value is INotifyPropertyChanged)
					((INotifyPropertyChanged)value).PropertyChanged += HandlePropertyChanged;

				mSource.Value = value;

				var newvalue = GetValue();
				if (newvalue != mValue.Value)
				{
					mValue.Value = newvalue;
					OnChanged();
				}
			}
		}

		void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == mInfo.Name)
			{
				if (mIsInValueSet)
					return;
				mValue.Value = GetValue();
				OnChanged();
			}
		}

		public object Value
		{
			get { return this.mValue.Value; }
			set
			{
				if (mIsInValueSet)
					return;
				if (Equals(value, mValue.Value))
					return;

				try
				{
					mIsInValueSet = true;
					SetValue(value);
					mValue.Value = GetValue();
				}
				finally
				{
					mIsInValueSet = false;
				}
				OnChanged();
			}
		}
		bool mIsInValueSet;

		void OnChanged()
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}
		public event EventHandler ValueChanged;

		#region GetValue(), SetValue()

		object GetValue()
		{
			object newval = null;
			var ms = mSource.Value;
			if (mInfo is PropertyInfo)
			{
				var pi = (PropertyInfo)mInfo;
				if (pi.CanRead)
				{
					var r = pi.GetGetMethod(true);
					if (ms != null || r.IsStatic)
						newval = r.Invoke(ms, null);
				}
			}
			else if (mInfo is FieldInfo)
			{
				var fi = (FieldInfo)mInfo;
				if (ms != null || fi.IsStatic)
					newval = fi.GetValue(ms);
			}
			return newval;
		}

		void SetValue(object o)
		{
			var ms = mSource.Value;
			if (mInfo is PropertyInfo)
			{
				var pi = (PropertyInfo)mInfo;
				if (pi.CanWrite)
				{
					var r = pi.GetSetMethod();
					if (ms != null || r.IsStatic)
						r.Invoke(ms, new[] { o });
				}
			}
			else if (mInfo is FieldInfo)
			{
				var fi = (FieldInfo)mInfo;
				if (ms != null || fi.IsStatic)
					fi.SetValue(ms, o);
			}
		}

		#endregion
	}

	#endregion
}
