using Microsoft.EntityFrameworkCore.Storage;

namespace FINSTAR_Test_Task.Infrastructure.Context
{
	/// <summary>
	/// Репозиторий данных.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Добавить коллекцию данных в БД.
		/// </summary>
		/// <typeparam name="T">Тип данных.</typeparam>
		/// <param name="models">Коллекция данных.</param>
		Task AddModelsAsync<T>(ICollection<T> models) where T : class;
		/// <summary>
		/// Получить транзакцию.
		/// </summary>
		/// <returns></returns>
		IDbContextTransaction GetTransaction();
	}
}
