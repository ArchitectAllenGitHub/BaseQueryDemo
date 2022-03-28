using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using BaseQueryDemo.Model.Input;
using BaseQueryDemo.Model.Response;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseQuery.Repository
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : BaseRepository<StudentEntity, StudentQueryInput, StudentAddInput, StudentModifyInput>, IStudentRepository
    {
        public StudentRepository(ISqlSugarClient db) : base(db)
        {

        }

        /// <summary>
        /// 获取基础集合关联表
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<StudentEntity>> GetListAsync(StudentQueryInput input)
        {
            var res = GetExpression(input);
            var r = _db.Queryable<StudentEntity>()
                   .LeftJoin<SchoolEntity>((a, b) => a.SchoolId == b.Id)
                   .Where((a, b) => b.Name == "小学")
                   .WhereIF(res.Condition, res.Expression);
            var sql = r.ToSql();
            return await r.ToListAsync();
        }

        /// <summary>
        /// 获取分页基础集合关联表
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<StudentEntity>> GetPageListAsync(PageEntity<StudentQueryInput> input)
        {
            var res = GetExpression(input.Data);

            var ex = _db.Queryable<StudentEntity>()
                   .LeftJoin<SchoolEntity>((a, b) => a.SchoolId == b.Id)
                   .Where((a, b) => b.Name == "小学")
                   .WhereIF(res.Condition, res.Expression);
            foreach (var item in GetOrderBy(input.OrderByEntity))
            {
                ex.OrderBy(item.OrderByExpression, item.Ascending ? OrderByType.Asc : OrderByType.Desc);
            }
            var sql = ex.ToSql();
            return await ex.ToListAsync();
        }

        /// <summary>
        /// 获取分页自定义集合关联表
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<StudentResponse>> GetPageList2Async(PageEntity<StudentQueryInput> input)
        {
            var res = GetExpression(input.Data);

            var ex = _db.Queryable<StudentEntity>()
                   .LeftJoin<SchoolEntity>((a, b) => a.SchoolId == b.Id)
                   .Where((a, b) => b.Name == "小学")
                   .WhereIF(res.Condition, res.Expression);
            foreach (var item in GetOrderBy(input.OrderByEntity))
            {
                ex.OrderBy(item.OrderByExpression, item.Ascending ? OrderByType.Asc : OrderByType.Desc);
            }

            var r = ex.Select((a, b) => new StudentResponse { Id = a.Id, Name = a.Name, SchoolName = b.Name });
            var sql = r.ToSql();
            return await r.ToListAsync();
        }
    }
}
