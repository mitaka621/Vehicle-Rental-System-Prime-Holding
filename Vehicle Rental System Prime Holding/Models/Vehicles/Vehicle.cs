using System.Text;
using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Models.Vehicles
{
	public abstract class Vehicle : IVehicle
    {
        public Vehicle(
            string vehicleLicensePlate,
            string brand,
            string model,
            double vehicleValue
            )
        {
            VehicleLicensePlate = vehicleLicensePlate;
            Brand = brand;
            Model = model;
            VehicleValue = vehicleValue;
            RentalPeriodInDays = rentalPeriodInDays;
        }

        private string licencePlate = null!;
        public string VehicleLicensePlate
        {
            get => licencePlate;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, nameof(VehicleLicensePlate)));
                }
                licencePlate = value;
            }
        }

        private string brand = null!;
        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, nameof(Brand)));
                }
                brand = value;
            }
        }

        private string model = null!;
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, nameof(Model)));
                }
                model = value;
            }
        }

        private double vehicleValue;
        public double VehicleValue
        {
            get => vehicleValue;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(VehicleValue)));
                }
                vehicleValue = value;
            }
        }

        private int rentalPeriodInDays;
        public int RentalPeriodInDays
        {
            get => rentalPeriodInDays;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(rentalPeriodInDays)));
                }
                rentalPeriodInDays = value;
            }
        }

		public int? ActualRentalPeriod => ActualReturnDate?.DayNumber - ReservationStartDate.DayNumber;

		public DateOnly ReservationStartDate { get; private set; }

        public DateOnly ReservationEndDate => ReservationStartDate.AddDays(RentalPeriodInDays);

        public DateOnly? ActualReturnDate { get; private set; }

		public bool IsRented { get; private set; }

		public double GetRentalCost()
        {
            int period = ActualRentalPeriod ?? RentalPeriodInDays;

            if (period<=7)
            {
                switch (GetType().Name)
                {
                    case "Car":
                        return 20;
                    case "Motorcycle":
                        return 15;
                    case "CargoVan":
                        return 50;

					default:
                        throw new NotImplementedException();
                }
            }

			switch (GetType().Name)
			{
				case "Car":
					return 15;
				case "Motorcycle":
					return 10;
				case "CargoVan":
					return 30;
				default:
					throw new NotImplementedException();
			}
		}

		public abstract double GetInsuranceCost();

		public abstract double GetInsuranceCostChanges();

		public virtual bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam=null)
		{
            if (IsRented)
            {
                return false;
            }

            IsRented = true;

            RentalPeriodInDays = rentalPeriodInDays;
            ReservationStartDate= reservationStartDate;

			return true;
		}

		public bool ReturnVehicle()
		{
            if (IsRented)
            {
                ActualReturnDate = DateOnly.FromDateTime(DateTime.Now);
                IsRented = false;

				return true;
			}

            return false;
        }


        public override string ToString()
		{
		    StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Rented Vehicle: {Brand} {Model}{Environment.NewLine}");
			sb.AppendLine($"Reservation start date: {ReservationStartDate.ToString(Utilities.DateOnlyFormat)}");
			sb.AppendLine($"Reservation end date: {ReservationEndDate}");
			sb.AppendLine($"Reserved rental days: {RentalPeriodInDays}{Environment.NewLine}");
			sb.AppendLine($"Actual Return date: "+ ActualReturnDate==null?"--not yet returned--":ActualReturnDate?.ToString(Utilities.DateOnlyFormat));
			sb.AppendLine($"Actual rental days: " + ActualRentalPeriod == null ? "--not yet returned--" : ActualRentalPeriod.ToString());
            sb.AppendLine($"{Environment.NewLine}Rental cost per day: ${GetRentalCost():F2}");
            sb.AppendLine($"Initial insurance per day: ${GetInsuranceCost():F2}");

            if (GetInsuranceCostChanges()<0)
            {
				sb.AppendLine($"Insurance subtraction per day: ${GetInsuranceCost():F2}");
			}
            else if(GetInsuranceCostChanges() > 0)
            {
				sb.AppendLine($"Insurance addition per day: ${GetInsuranceCost():F2}");
			}

			sb.AppendLine($"Insurance per day: ${GetInsuranceCost()+GetInsuranceCostChanges():F2}");

            double totalRent = GetRentalCost() * ActualRentalPeriod ?? RentalPeriodInDays;
            double totalInsurance = (GetInsuranceCost() + GetInsuranceCostChanges()) * ActualRentalPeriod ?? RentalPeriodInDays;

			sb.AppendLine($"{Environment.NewLine}Total rent: ${totalRent:F2}");
			sb.AppendLine($"Total Insurance: ${totalInsurance:F2}");
            sb.AppendLine($"Total: {totalRent + totalInsurance}");
            sb.AppendLine("XXXXXXXXXXXXX");

            return sb.ToString().Trim();
		}	
	}
}
