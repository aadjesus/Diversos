using System;

namespace MyLib
{
	/// <summary>Represents a persitency service, retrieving entities from a persitent store.</summary>
	public interface IPersistenceService
	{
		/// <summary>Retrieve entities from the store.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityID"></param>
		/// <returns></returns>
		T LoadEntity<T>(int entityID);

		/// <summary>Updates an entity in the store.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		void Update<T>(T entity);
	}
}