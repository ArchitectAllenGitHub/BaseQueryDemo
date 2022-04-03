using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using BaseQueryDemo.Model.Input;
using BaseQueryDemo.Model.Response;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseQueryDemo.Repository
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : BaseRepository<StudentEntity, StudentAddInput, StudentModifyInput, StudentQueryInput>, IStudentRepository
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
            var r = this.GetQueryableWithSchool(input);
            var sql = r.ToSql();
            return await r.ToListAsync();
        }

        /// <summary>
        /// 获取分页基础集合关联表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StudentEntity>> GetPageListAsync(PageEntity<StudentQueryInput> input)
        {
            var ex = this.GetQueryableWithSchool(input.Data);

            if (!string.IsNullOrEmpty(input.OrderField))
                ex.OrderBy(input.OrderField);

            return await ex.ToListAsync();
        }

        /// <summary>
        /// 获取分页自定义集合关联表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StudentResponse>> GetResponsePageListAsync(PageEntity<StudentQueryInput> input)
        {
            var ex = this.GetQueryableWithSchool(input.Data);

            if (!string.IsNullOrEmpty(input.OrderField))
                ex.OrderBy(input.OrderField);

            var r = ex.Select((a, b) => new StudentResponse { Id = a.Id, Name = a.Name, SchoolName = b.Name });
            var sql = r.ToSql();
            return await r.ToListAsync();
        }

        /// <summary>
        /// [多表]获取关联表达式目录树
        /// </summary>
        /// <returns></returns>
        private ISugarQueryable<StudentEntity, SchoolEntity> GetQueryableWithSchool(StudentQueryInput input)
        {
            var res = GetExpression(input);

            return _db.Queryable<StudentEntity>()
                   .LeftJoin<SchoolEntity>((a, b) => a.SchoolId == b.Id)
                   .WhereIF(!string.IsNullOrEmpty(input.SchoolName), (a, b) => b.Name == input.SchoolName)
                   .WhereIF(res.Condition, res.Expression);
        }
        //后面还可以写不同表和Student的关联..根据需求自由拆分或合并
    }
}
