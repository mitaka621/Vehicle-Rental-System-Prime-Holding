namespace Vehicle_Rental_System_Prime_Holding.Models.Contracts
{
	public interface IVehicle
    {
        string VehicleLicensePlate { get;}

        string Brand { get; }

        string Model { get; }

        double VehicleValue { get; }

        int RentalPeriodInDays { get; }

		int? ActualRentalPeriod { get; }

		DateOnly ReservationStartDate { get; }

		DateOnly ReservationEndDate { get; }

		DateOnly? ActualReturnDate { get; }

        bool IsRented { get; }

        double GetRentalCost();

        abstract double GetInsuranceCost();

		abstract double GetInsuranceCostChanges();

        bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null);

        bool ReturnVehicle();
	}
}
