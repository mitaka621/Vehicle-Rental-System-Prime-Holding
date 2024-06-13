using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Repositories.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Repositories
{
	public class VehicleRepository : IRepository<IVehicle>
	{
		private List<IVehicle> cars = new List<IVehicle>();

		public void AddNew(IVehicle model)
		{
			cars.Add(model);
		}

		public IVehicle? GetModelByIdentificator(string licensePlate)
		{
			return cars.FirstOrDefault(x=>x.VehicleLicensePlate== licensePlate);			
		}

		public IReadOnlyCollection<IVehicle> Models() => cars;
	}
}
