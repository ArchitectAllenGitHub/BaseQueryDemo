using BaseQueryDemo.Entities;
using SqlSugar;

namespace BaseQueryDemo.Extensions
{
    public static class SqlSugarExtension
    {
        /// <summary>
        /// 添加SqlSugar
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugar(this IServiceCollection services, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            //SqlSugarScope用单例AddSingleton  单例
            //SqlSugarClient用 AddScoped  每次请求一个实例

            services.AddSingleton<ISqlSugarClient>(_ => new SqlSugarScope(new ConnectionConfig()
            {
                //ConfigId="db01"  多租户用到
                ConnectionString = configuration.GetConnectionString("mysql"),
                DbType = DbType.MySql,
                IsAutoCloseConnection = true//自动释放
            }, db =>
            {
                //单例参数配置，所有上下文生效
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    if (webHostEnvironment.IsDevelopment())
                    {
                        Console.WriteLine($"{sql};\r\n 参数:{string.Join(';', pars.Select(t => $"{t.ParameterName }={t.Value}"))}");//输出sql
                    }
                };
            }));

            return services;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateDatabase(this ISqlSugarClient db)
        {
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(StudentEntity));
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(SchoolEntity));

            if (!db.Queryable<SchoolEntity>().Any())
            {
                SchoolEntity input = new SchoolEntity
                {
                    Name = "XX第一小学",
                    Code = 1,
                    CreateDate = new DateTime(2022, 03, 20, 14, 40, 14),
                    CreateUserId = "123",
                    UpdateDate = new DateTime(2022, 03, 20, 14, 40, 14),
                    UpdateUserId = "123"
                };
                db.Insertable(input).ExecuteCommand();
            }

            if (!db.Queryable<StudentEntity>().Any())
            {
                StudentEntity input = new StudentEntity
                {
                    Name = "Allen",
                    Code = "153",
                    CreateDate = new DateTime(2022, 03, 20, 14, 40, 14),
                    Sex = 1,
                    CreateUserId = "123",
                    UpdateDate = new DateTime(2022, 03, 20, 14, 40, 14),
                    UpdateUserId = "123",
                    SchoolId = 1,
                    Age = 11,
                };
                db.Insertable(input).ExecuteCommand();
            }
        }
    }
}
