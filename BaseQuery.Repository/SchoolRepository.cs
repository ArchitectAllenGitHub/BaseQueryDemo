using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using BaseQueryDemo.Model.Input;
using SqlSugar;

namespace BaseQueryDemo.Repository
{
    /// <summary>
    /// 学校仓储
    /// </summary>
    public class SchoolRepository : BaseRepository<SchoolEntity, SchoolAddInput, SchoolModifyInput, SchoolQueryInput>, ISchoolRepository
    {
        public SchoolRepository(ISqlSugarClient db) : base(db)
        {

        }
    }
}