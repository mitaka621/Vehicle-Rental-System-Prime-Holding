using System.Text;
using Vehicle_Rental_System_Prime_Holding.Messages;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Models
{
	public class Client : IClient
	{
        public Client(string firstName, string lastName, int? age, int? experience)
        {
            FirstName = firstName;
			LastName = lastName;
			Age = age;
			Experience = experience;
        }

        private string firstName=null!;
		public string FirstName
		{
			get => firstName;
			set
			{
                if (string.IsNullOrEmpty(value))
                {
					throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, nameof(FirstName)));
                }

				firstName = value;
            }
		}

		private string lastName=null!;
		public string LastName
		{
			get => lastName;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException(string.Format(ExceptionMessages.PropertyNullOrEmpty, nameof(LastName)));
				}

				lastName = value;
			}
		}

		private int? age;
		public int? Age
		{
			get => age;
			set
			{
                if (value !=null && value<0)
                {
					throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(Age)));
                }

				age = value;
            }
		}

		private int? experience;
		public int? Experience
		{
			get => experience;
			set
			{
				if (value != null && value < 0)
				{
					throw new ArgumentException(string.Format(ExceptionMessages.ValueCannotBeNegative, nameof(Experience)));
				}

				experience = value;
			}
		}

		private List<IVehicle> vehicles = new();
		public IReadOnlyCollection<IVehicle> Vehicles => vehicles;

		public void AddCarToColection(IVehicle model)
		{
			vehicles.Add(model);
		}

		public override string ToString()
		{
			StringBuilder sb=new StringBuilder();
			if (vehicles.Count == 0)
			{
				sb.AppendLine("XXXXXXXXXXXXXX");
				sb.AppendLine("Date: " + DateTime.Now.ToString(Utilities.DateOnlyFormat));
				sb.AppendLine($"Customer Name: {FirstName} {LastName}");         
				sb.AppendLine("--No Rented Cars--");
				sb.AppendLine("XXXXXXXXXXXXXX");

				return sb.ToString().Trim();
			}
            
			foreach (var vehicle in vehicles)
			{
				sb.AppendLine("XXXXXXXXXXXXXX");
				sb.AppendLine("Date: " + DateTime.Now.ToString(Utilities.DateOnlyFormat));
				sb.AppendLine($"Customer Name: {FirstName} {LastName}");
				sb.AppendLine(vehicle.ToString()+Environment.NewLine);
			}
			
			return sb.ToString().TrimEnd();
		}
	}
}
