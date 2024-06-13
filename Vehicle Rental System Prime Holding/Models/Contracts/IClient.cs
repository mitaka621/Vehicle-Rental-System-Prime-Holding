namespace Vehicle_Rental_System_Prime_Holding.Models.Contracts
{
	public interface IClient
	{
        string FirstName { get; }

		string LastName { get; }

		int? Age { get; }

		int? Experience { get; }

		IReadOnlyCollection<IVehicle> Vehicles { get; }

		void AddCarToColection(IVehicle model);
	}
}
