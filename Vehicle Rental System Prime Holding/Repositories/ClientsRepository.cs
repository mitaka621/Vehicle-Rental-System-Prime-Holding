using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Repositories.Contracts;

namespace Vehicle_Rental_System_Prime_Holding.Repositories
{
	public class ClientsRepository : IRepository<IClient>
	{
		private Dictionary<int,IClient> clients = new Dictionary<int,IClient>();

		int idCount=1;

		public void AddNew(IClient model)
		{
			clients[idCount++]=model;
		}

		public IClient? GetModelByIdentificator(string id)
		{
            if (!int.TryParse(id,out int parsedId)||!clients.ContainsKey(parsedId))
            {
				return null;
            }

            return clients[int.Parse(id)];
		}

		public IReadOnlyCollection<IClient> Models() => clients.Values;


	}
}
