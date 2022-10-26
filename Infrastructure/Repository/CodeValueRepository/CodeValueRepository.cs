using FINSTAR_Test_Task.Common.Assert;
using FINSTAR_Test_Task.Common.Extensions;
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
            List<CodeValueEntity> codeValuesDao = ConvertToArrayDao(SortedByCode(codeValues));

            await ClearDataTable();
            /*
             Текст ТЗ: Полученный массив необходимо отсортировать по полю code и сохранить в БД. 
             От автора: если имелось ввиду сохранение в базу с таким же порядком, то в случае в EF core сохранять нужно каждую запись отдельно
             */
            await Add(codeValuesDao);

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task<ICollection<CodeValueUi>> GetData(FilteringParams filteringParams)
    {
        IQueryable<CodeValueEntity> query = _dbContext.CodeValue.BuildByFilters(filteringParams);
        ICollection<CodeValueEntity> data = await query.ToListAsync();

        return ConvertToArrayUi(data).ToList();
    }

    private async Task ClearDataTable()
    {
        IEntityType entityType = _dbContext.Model.FindEntityType(typeof(CodeValueEntity));
        string tableName = entityType.GetTableName();
        string query = $"TRUNCATE TABLE \"{tableName}\"";

        await _dbContext.Database.ExecuteSqlRawAsync(query);
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

    private static List<CodeValueUi> ConvertToArrayUi(ICollection<CodeValueEntity> codeValues)
    {
        List<CodeValueUi> codeValuesUi = codeValues.Select(codeValue => new CodeValueUi()
        {
            Code = codeValue.Code,
            Value = codeValue.Value,
            Id = codeValue.Id
        }).ToList();

        return codeValuesUi;
    }

    private async Task Add(ICollection<CodeValueEntity> codeValuesDao)
    {
        await AddModelsAsync(codeValuesDao);
        await SaveChangeAsync();
    }

    private ICollection<CodeValueDto> SortedByCode(ICollection<CodeValueDto> codeValuesDto) =>
        codeValuesDto.OrderBy(x => x.Code).ToList();
}