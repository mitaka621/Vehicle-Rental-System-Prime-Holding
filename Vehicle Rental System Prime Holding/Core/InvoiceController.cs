using System.Reflection;
using Vehicle_Rental_System_Prime_Holding.Core.Contracts;
using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Models.Vehicles;
using Vehicle_Rental_System_Prime_Holding.Repositories;
using Vehicle_Rental_System_Prime_Holding.Repositories.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Core
{
	public class InvoiceController : IController
	{
		IRepository<IVehicle> vehicles = new VehicleRepository();
		IRepository<IClient> clients = new ClientsRepository();

		public void AddVehicle(
			string typeName,
			string vehicleLicensePlate,
			string brand,
			string model,
			double vehicleValue,
			int? safetyRating = null)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();

			Type baseType = typeof(Vehicle);

			List<Type> derivedTypes = assembly.GetTypes()
			   .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType))
			   .ToList();

			if (!derivedTypes.Any(x => x.Name == typeName))
			{
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidClassTypeName, typeName));
			}

			var vehicleType = derivedTypes.First(x => x.Name == typeName);

			if (vehicleType.Name == nameof(Car))
			{
				if (safetyRating == null)
				{
					throw new ArgumentNullException(nameof(safetyRating));
				}

				vehicles.AddNew(new Car(vehicleLicensePlate, brand, model,vehicleValue, safetyRating.Value));

				return;
			}

			vehicles.AddNew((Activator.CreateInstance(vehicleType, vehicleLicensePlate, brand, model, vehicleValue) as Vehicle)!);

			Console.WriteLine(string.Format(SuccessMessages.RegisteredVehicle, $"{brand} {client.LastName}", $"{car.Brand} {car.Model}"));
		}

		public void RegisterClient(string firstName, string lastName, int? age=null, int? experience=null)
		{
			clients.AddNew(new Client(firstName, lastName, age, experience));
		}

		public void RentACar(string vehicleLicensePlate,int userId, int rentPeriod, DateOnly startDate)
		{
			var client = clients.GetModelByIdentificator(userId.ToString());

            if (client==null)
            {
				throw new ArgumentNullException(nameof(client));
            }

            var car = vehicles.GetModelByIdentificator(vehicleLicensePlate);

            if (car==null||car.GetType().Name!=nameof(Car))
            {
				throw new ArgumentNullException(nameof(car));
			}

            if (car.IsRented)
            {
				throw new ArgumentException(ExceptionMessages.TheCarIsRented);
			}

			car.RentVehicle(rentPeriod, startDate, client.Age);

			client.AddCarToColection(car);

            Console.WriteLine(string.Format(SuccessMessages.AddedCarToPerson, $"{client.FirstName} {client.LastName}",$"{car.Brand} {car.Model}"));
        }
	}
}
