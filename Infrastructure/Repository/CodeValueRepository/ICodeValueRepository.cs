using FINSTAR_Test_Task.Controllers.Models;

namespace FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;

public interface ICodeValueRepository
{
    Task AddSorted(ICollection<CodeValueDto> codeValues);
    Task<ICollection<CodeValueUi>> GetAllData();

}