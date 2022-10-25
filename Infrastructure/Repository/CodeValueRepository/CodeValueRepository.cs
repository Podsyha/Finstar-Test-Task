using FINSTAR_Test_Task.Common.Assert;
using FINSTAR_Test_Task.Infrastructure.Context;
using FINSTAR_Test_Task.Infrastructure.DAO;

namespace FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;

public class CodeValueRepository : AppDbFunc, ICodeValueRepository
{
    public CodeValueRepository(AppDbContext dbContext, IAssert assert)
        : base(dbContext, assert)
    {
        _assert = assert;
    }

    
    private readonly IAssert _assert;


    public async Task ClearTable()
    {
        
    }

    public async Task AddSorted()
    {
        
    }

    private CodeValueEntity ConvertToDao()
    {
        
    }

    private async Task Add()
    {
        
    }

    private async Task SortedByCode()
    {
        
    }
}