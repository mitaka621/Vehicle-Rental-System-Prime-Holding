using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Models.Vehicles
{
	public class Car : Vehicle, ICar
    {
        public Car(
            string vehicleLicensePlate,
            string brand,
            string model,
            double vehicleValue,
            int safetyRating) 
            : base(vehicleLicensePlate,brand, model, vehicleValue)
        {
            SafetyRating = safetyRating;
        }

        private int safetyRating;
        public int SafetyRating
        {
            get => safetyRating;
			private set
			{
                if (value < 1 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(string.Format(ExceptionMessages.OutOfRange, nameof(SafetyRating), 1, 5));
                }

                safetyRating = value;
            }
        }

		public override double GetInsuranceCost()=> VehicleValue * 0.01;

		public override double GetInsuranceCostChanges()
		{
			if (SafetyRating >= 4)
			{
				return GetInsuranceCost() - GetInsuranceCost() * 0.1;
			}

            return GetInsuranceCost();
		}
	}
}
