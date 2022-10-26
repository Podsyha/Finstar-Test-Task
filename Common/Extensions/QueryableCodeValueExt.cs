using FINSTAR_Test_Task.Controllers.Models;
using FINSTAR_Test_Task.Infrastructure.DAO;

namespace FINSTAR_Test_Task.Common.Extensions;

public static class QueryableCodeValueExt
{
    /// <summary>
    /// Построить запрос получения данных на основе поступающих фильтров
    /// </summary>
    /// <param name="query">Объект запроса</param>
    /// <param name="filteringParams">Фильтры</param>
    /// <returns></returns>
    public static IQueryable<CodeValueEntity> BuildByFilters(this IQueryable<CodeValueEntity> query,
        FilteringParams filteringParams)
    {
        query = filteringParams.OnlyValue == null
            ? query
            : query.Where(x => x.Value == filteringParams.OnlyValue);

        query = filteringParams.OnlyCode == null
            ? query
            : query.Where(x => x.Code == filteringParams.OnlyCode);

        query = filteringParams.MinCode == null
            ? query
            : query.Where(x => x.Code >= filteringParams.MinCode);

        query = filteringParams.MaxCode == null
            ? query
            : query.Where(x => x.Code <= filteringParams.MaxCode);

        query = filteringParams.CodeOrderByDesc is true
            ? query.OrderByDescending(x => x.Code)
            : query;

        query = filteringParams.CodeOrderByAsc is true
            ? query.OrderBy(x => x.Code)
            : query;

        query = filteringParams.CountTakeFirst == null
            ? query
            : query.Take((int)filteringParams.CountTakeFirst);

        return query;
    }
}