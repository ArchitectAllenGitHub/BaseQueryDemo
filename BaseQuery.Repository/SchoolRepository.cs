using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using BaseQueryDemo.Model.Input;
using SqlSugar;

namespace BaseQuery.Repository
{
    /// <summary>
    /// 学校仓储
    /// </summary>
    public class SchoolRepository : BaseRepository<SchoolEntity, SchoolQueryInput, SchoolAddInput, SchoolModifyInput>, ISchoolRepository
    {
        public SchoolRepository(ISqlSugarClient db) : base(db)
        {

        }
    }
}