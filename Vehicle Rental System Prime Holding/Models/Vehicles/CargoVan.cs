using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Models.Vehicles
{
	public class CargoVan : Vehicle, ICargoVan
	{
        public CargoVan(
			string vehicleLicensePlate,
			string brand,
			string model,
			double vehicleValue) 
			: base(vehicleLicensePlate, brand, model, vehicleValue)
        {
		}

		private int driverExperience;
		public int DriverExperience 
		{
			get=> driverExperience;
			private set
			{
                if (value<0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(DriverExperience)));
                }
				driverExperience = value;
            }
		}

		public override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)
		{
			if (optParam == null)
			{
				throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, "driver experiance (optParam)"));
			}

			DriverExperience = optParam.Value;

			return base.RentVehicle(rentalPeriodInDays, reservationStartDate, optParam);
		}

		public override double GetInsuranceCost() => VehicleValue * (0.03 * 0.01);

		public override double GetInsuranceCostChanges()
		{
			if (DriverExperience > 5)
			{
				return GetInsuranceCost() * 0.15 * -1;
			}

			return 0;
		}
	}
}
