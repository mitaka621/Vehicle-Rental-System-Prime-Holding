using System.Reflection;
using System.Text;
using Vehicle_Rental_System_Prime_Holding.Core.Contracts;
using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Models.Vehicles;
using Vehicle_Rental_System_Prime_Holding.Repositories;
using Vehicle_Rental_System_Prime_Holding.Repositories.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Core
{
	public class InvoiceController : IInvoiceController
	{
		private readonly IRepository<IVehicle> vehicles = new VehicleRepository();
		private readonly IRepository<IClient> clients = new ClientsRepository();

		private bool enableHelperMessages;

		public InvoiceController(bool EnableHelperMessages=true)
        {
			enableHelperMessages= EnableHelperMessages;
		}

        public void AddVehicle(
			string typeName,
			string vehicleLicensePlate,
			string brand,
			string model,
			double vehicleValue,
			int? safetyRating = null)
		{
            if (vehicles.Models().Any(x=>x.VehicleLicensePlate== vehicleLicensePlate))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.TheLicensePlateAlreadyExists, vehicleLicensePlate));
            }

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

				vehicles.AddNew(new Car(vehicleLicensePlate, brand, model, vehicleValue, safetyRating.Value));
			}
			else
			{
				vehicles.AddNew((Activator.CreateInstance(vehicleType, vehicleLicensePlate, brand, model, vehicleValue) as Vehicle)!);
			}

            if (enableHelperMessages)
            {
				Console.WriteLine(string.Format(SuccessMessages.RegisteredVehicle, $"{brand} {model}", typeName));
			}         
		}

		public void RegisterClient(string firstName, string lastName, int? age=null, int? experience=null)
		{
			clients.AddNew(new Client(firstName, lastName, age, experience));
            if (enableHelperMessages)
            {
				Console.WriteLine(string.Format(SuccessMessages.RegisteredClient, $"{firstName} {lastName}"));
			}
           
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

			car.RentVehicle(rentPeriod, startDate);

			client.AddCarToColection(car);

            if (enableHelperMessages)
            {
				Console.WriteLine(string.Format(SuccessMessages.AddedCarToPerson, $"{client.FirstName} {client.LastName}", $"{car.Brand} {car.Model}"));
			}          
        }

		public void RentAVan(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)
		{
			var client = clients.GetModelByIdentificator(userId.ToString());

			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

            if (client.Experience==null)
            {
				throw new ArgumentNullException(nameof(client.Experience));
			}

            var van = vehicles.GetModelByIdentificator(vehicleLicensePlate);

			if (van == null || van.GetType().Name != nameof(CargoVan))
			{
				throw new ArgumentNullException(nameof(CargoVan));
			}

			if (van.IsRented)
			{
				throw new ArgumentException(ExceptionMessages.TheCarIsRented);
			}

			van.RentVehicle(rentPeriod, startDate, client.Experience);

			client.AddCarToColection(van);

            if (enableHelperMessages)
            {
				Console.WriteLine(string.Format(SuccessMessages.AddedCarToPerson, $"{client.FirstName} {client.LastName}", $"{van.Brand} {van.Model}"));
			}        
		}

		public void RentAMotorcycle(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)
		{
			var client = clients.GetModelByIdentificator(userId.ToString());

			if (client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}

			if (client.Age == null)
			{
				throw new ArgumentNullException(nameof(client.Age));
			}

			var motorcycle = vehicles.GetModelByIdentificator(vehicleLicensePlate);

			if (motorcycle == null || motorcycle.GetType().Name != nameof(Motorcycle))
			{
				throw new ArgumentNullException(nameof(Motorcycle));
			}

			if (motorcycle.IsRented)
			{
				throw new ArgumentException(ExceptionMessages.TheCarIsRented);
			}

			motorcycle.RentVehicle(rentPeriod, startDate, client.Age);

			client.AddCarToColection(motorcycle);

            if (enableHelperMessages)
            {
				Console.WriteLine(string.Format(SuccessMessages.AddedCarToPerson, $"{client.FirstName} {client.LastName}", $"{motorcycle.Brand} {motorcycle.Model}"));
			}
           
		}

		public void ReturnVehicleAndPrintInvoice(string numberPlate, int clientId)
		{
			var client=clients.GetModelByIdentificator(clientId.ToString());

            if (client==null)
            {
				throw new ArgumentNullException(nameof(client));
			}

			var rentedCar = client.Vehicles.FirstOrDefault(x => x.VehicleLicensePlate == numberPlate);

			if (rentedCar == null) 
			{
				throw new ArgumentNullException(nameof(rentedCar));
			}

            if (rentedCar.ReturnVehicle())
            {
                StringBuilder sb=new StringBuilder();
				sb.AppendLine("XXXXXXXXXXXXXX");
				sb.AppendLine("Date: " + DateTime.Now.ToString(Utilities.DateOnlyFormat));
				sb.AppendLine($"Customer Name: {client.FirstName} {client.LastName}");
				sb.AppendLine(rentedCar.ToString());

                Console.WriteLine(sb.ToString());
            }
           
		}

		public void PrintInvoicesForAllClients()
		{
            foreach (var client in clients.Models())
            {
				Console.WriteLine(client.ToString());
			}
        }
	}
}
