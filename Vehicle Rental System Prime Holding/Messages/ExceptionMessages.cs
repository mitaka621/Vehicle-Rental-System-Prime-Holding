namespace Vehicle_Rental_System_Prime_Holding.Messages
{
	public static class ExceptionMessages
	{
		public const string PropertyNullOrEmpty = "The property {0} cannot be null or empty";

		public const string ValueCannotBeNegative = "The property {0} cannot be negative (less than zero)";

		public const string OutOfRange = "The property {0} cannot be outside of the following range [{1}, {2}]";

		public const string DateBefore = "The property {0} cannot contain a date before {1}";

		public const string InvalidClassTypeName = "The class type name you have entered has not been implemented- {0}";

		public const string TheCarIsRented = "The car is already rented";

		public const string TheLicensePlateAlreadyExists = "A vehicle with the license plate {0} already exists";
	}
}
