using FINSTAR_Test_Task.Common.Assert;
using FINSTAR_Test_Task.Controllers.Models;
using FINSTAR_Test_Task.Infrastructure.Context;
using FINSTAR_Test_Task.Infrastructure.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;

public class CodeValueRepository : AppDbFunc, ICodeValueRepository
{
    public CodeValueRepository(AppDbContext dbContext, IAssert assert)
        : base(dbContext, assert)
    {
        _assert = assert;
    }


    private readonly IAssert _assert;


    public async Task AddSorted(ICollection<CodeValueDto> codeValues)
    {
        await using var transaction = GetTransaction();

        try
        {
            List<CodeValueEntity> codeValuesDao = ConvertToArrayDao(codeValues);
            SortedByCode(codeValuesDao);

            await ClearDataTable();
            await Add(codeValuesDao);

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }
    }

    private async Task ClearDataTable()
    {
        IEntityType entityType = _dbContext.Model.FindEntityType(typeof(CodeValueEntity));
        string tableName = entityType.GetTableName();

        await _dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{tableName}]");
    }

    private static List<CodeValueEntity> ConvertToArrayDao(ICollection<CodeValueDto> codeValues)
    {
        List<CodeValueEntity> codeValuesDao = codeValues.Select(codeValue => new CodeValueEntity()
        {
            Code = codeValue.Code,
            Value = codeValue.Value
        }).ToList();

        return codeValuesDao;
    }

    private async Task Add(ICollection<CodeValueEntity> codeValuesDao)
    {
        await AddModelAsync(codeValuesDao);
        await SaveChangeAsync();
    }

    private void SortedByCode(ICollection<CodeValueEntity> codeValuesDao) =>
        codeValuesDao.OrderBy(x => x.Code).ToList();
}