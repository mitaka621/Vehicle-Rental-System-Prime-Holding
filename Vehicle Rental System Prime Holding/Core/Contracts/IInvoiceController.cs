using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Rental_System_Prime_Holding.Core.Contracts
{
	public interface IInvoiceController
	{
		void RegisterClient(string FirstName, string LastName, int? age = null, int? experience=null);

		void AddVehicle(string typeName,
			string vehicleLicensePlate,
			string brand,
			string model,
			double vehicleValue,
			int? safetyRating = null);

		void RentACar(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate);

		void RentAVan(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate);

		void RentAMotorcycle(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate);

		void ReturnVehicleAndPrintInvoice(string numberPlate, int clientId);

		void PrintInvoicesForAllClients();
	}
}
