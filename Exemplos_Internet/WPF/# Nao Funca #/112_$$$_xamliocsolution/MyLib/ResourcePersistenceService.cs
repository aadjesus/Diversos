using System;
using XamlIoc;

namespace MyLib
{
	/// <summary>Dummy persistance service, picking entities from the resources.</summary>
	public class ResourcePersistenceService : IPersistenceService
	{
		#region IPersistenceService Members
		T IPersistenceService.LoadEntity<T>(int entityID)
		{
			return ObjectFactory.GetObject<T>(entityID.ToString());
		}

		void IPersistenceService.Update<T>(T entity)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}