using System;
using System.Windows;

namespace XamlIoc
{
	/// <summary>Stand alone resource.</summary>
	/// <remarks>Used by <see cref="ObjectFactory"/>.</remarks>
	internal class StandAloneResource
	{
		private Uri uri;
		private bool isShared;
		private object singletonInstance;

		/// <summary>Construct a stand alone resource.</summary>
		/// <param name="uri"></param>
		/// <param name="isShared"></param>
		public StandAloneResource(Uri uri, bool isShared)
		{
			this.uri = uri;
			this.isShared = isShared;
		}

		/// <summary>Returns the object corresponding to the URI given at construction.</summary>
		/// <remarks>
		/// If the resource isn't shared, the object is created at every request.
		/// If the resource is shared, the object is created on the first request only (lazy init).
		/// </remarks>
		/// <returns></returns>
		public object GetObject()
		{
			if (isShared)
			{
				if (singletonInstance == null)
				{
					lock (this)
					{
						if (singletonInstance == null)
						{	//	Lazy init
							singletonInstance = CreateObject();
						}
					}
				}

				return singletonInstance;
			}
			else
			{
				return CreateObject();
			}
		}

		private object CreateObject()
		{
			return Application.LoadComponent(uri);
		}
	}
}