using BaseQueryDemo.Entities;
using BaseQueryDemo.Model.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseQueryDemo.IRepository
{
    /// <summary>
    /// 学校仓储
    /// </summary>
    public interface ISchoolRepository : IRepository<SchoolEntity, SchoolQueryInput, SchoolAddInput, SchoolModifyInput>
    {

    }
}
