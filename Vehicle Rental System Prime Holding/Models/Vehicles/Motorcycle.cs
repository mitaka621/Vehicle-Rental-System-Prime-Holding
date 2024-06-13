using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Models.Vehicles
{
	public class Motorcycle : Vehicle, IMotorcycle
	{
		public Motorcycle(
			string vehicleLicensePlate,
			string brand,
			string model,
			double vehicleValue)
			: base(vehicleLicensePlate, brand, model, vehicleValue)
		{
		}

		private int age;
		public int Age 
		{
			get => age;
			set 
			{
                if (age<0)
                {
					throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(Age)));
                }

				age=value;
            }
		}


		public override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)
		{
            if (optParam==null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, "driver age (optParam)"));
            }

			Age=optParam.Value;

            return base.RentVehicle(rentalPeriodInDays, reservationStartDate, optParam);
		}

		public override double GetInsuranceCost()=> VehicleValue * 0.02;

		public override double GetInsuranceCostChanges()
		{
			if (Age < 25)
			{
				return GetInsuranceCost() + GetInsuranceCost() * 0.2;
			}

			return GetInsuranceCost();
		}
	}
}
