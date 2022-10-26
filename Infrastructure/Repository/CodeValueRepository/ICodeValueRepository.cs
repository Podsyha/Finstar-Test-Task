using FINSTAR_Test_Task.Controllers.Models;

namespace FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;

public interface ICodeValueRepository
{
    /// <summary>
    /// Сохрпанить коллекцию данных в БД
    /// </summary>
    /// <param name="codeValues">DTO модель для общения с БД</param>
    /// <returns></returns>
    Task AddDataCollection(ICollection<CodeValueDto> codeValues);
    /// <summary>
    /// Получить данные из БД
    /// </summary>
    /// <param name="filteringParams">Фильтры на выдачу данных из БД</param>
    /// <returns></returns>
    Task<ICollection<CodeValueUi>> GetData(FilteringParams filteringParams);

}