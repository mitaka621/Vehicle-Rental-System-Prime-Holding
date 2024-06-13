namespace Vehicle_Rental_System_Prime_Holding.Repositories.Contracts
{
	public interface IRepository<T>
	{
		IReadOnlyCollection<T> Models();

		T? GetModelByIdentificator(string id);

		void AddNew(T model);
	}
}
