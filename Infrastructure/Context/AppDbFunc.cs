using FINSTAR_Test_Task.Common.Assert;
using Microsoft.EntityFrameworkCore.Storage;

namespace FINSTAR_Test_Task.Infrastructure.Context
{
    /// <summary>
    /// Абстрактный класс описания функциональности взаимодействия с БД.
    /// </summary>
    public class AppDbFunc : IRepository
    {
        public AppDbFunc(AppDbContext dbContext, IAssert assert)
        {
            _dbContext = dbContext;
            _assert = assert;
        }


        protected readonly AppDbContext _dbContext;
        private readonly IAssert _assert;


        /// <summary>
        /// Получить транзакцию.
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction GetTransaction() =>
            _dbContext.Database.CurrentTransaction ?? _dbContext.Database.BeginTransaction();

        /// <summary>
        /// Добавить данные в БД.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="model">Модель данных.</param>
        public async Task AddModelAsync<T>(T model)
        {
            _assert.IsNull(model);
            await _dbContext.AddAsync(model);
        }

        /// <summary>
        /// Сохранить модифицированные сущности в контексте БД
        /// </summary>
        protected async Task SaveChangeAsync() =>
            await _dbContext.SaveChangesAsync();

        /// <summary>
        /// Удалить данные из БД. С проверкой на null
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <typeparam name="T">Тип данных</typeparam>
        public void RemoveModel<T>(T model)
        {
            _assert.IsNull(model);
            _dbContext.Remove(model);
        }
    }
}