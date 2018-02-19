using System;

namespace MyLib
{
	/// <summary>Class validating <see cref="Person"/> entities.</summary>
	public class PersonValidator
	{
		private const int AGE_THRESHOLD = 100;

		private IPersistenceService persistenceService;
		private IAlertService alertService;

		/// <summary>Exposes the dependancy on a persistence service.</summary>
		public IPersistenceService PersistenceService
		{
			get { return persistenceService; }
			set { persistenceService = value; }
		}

		/// <summary>Exposes the dependancy on an alert service.</summary>
		public IAlertService AlertService
		{
			get { return alertService; }
			set { alertService = value; }
		}

		/// <summary>Validate a list of persons.</summary>
		/// <remarks>Raises an alert if one of the persons is more than 100 years old.</remarks>
		/// <param name="personIDs"></param>
		public void ValidatePersons(params int[] personIDs)
		{
			foreach (int id in personIDs)
			{
				Person person = PersistenceService.LoadEntity<Person>(id);

				if (person.Age > AGE_THRESHOLD)
				{
					AlertService.RaiseAlert(
						"Admin",
						string.Format("The person with ID={0} has invalid age (Age={1})", id, person.Age));
				}
			}
		}
	}
}