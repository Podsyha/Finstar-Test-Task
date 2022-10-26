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

        public async Task AddModelsAsync<T>(ICollection<T> models) where T : class
        {
            _assert.EmptyCollection(models);

            foreach (var model in models)
                await _dbContext.AddAsync(model);
        }

        /// <summary>
        /// Сохранить модифицированные сущности в контексте БД
        /// </summary>
        protected async Task SaveChangeAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}