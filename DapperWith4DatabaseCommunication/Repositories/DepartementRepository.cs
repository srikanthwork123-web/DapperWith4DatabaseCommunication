using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using System.Data;

namespace DapperWith4DatabaseCommunication.Repositories
{
    public class DepartementRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public DepartementRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddDeparment(Department deptdetail)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add(StoredprocedureParameters.DeptName, deptdetail.deptname);
                p.Add(StoredprocedureParameters.DeptLocation, deptdetail.deptlocation);
                p.Add(StoredprocedureParameters.DeptInsertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(Storedprocedurenames.AddDepartment, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>(StoredprocedureParameters.DeptInsertedvariable);
                return inserterdid;
            }
        }

        public async Task<string> DeleteDepartmentById(int deptid)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptid);
                var result = await con.QueryAsync<Department>(Storedprocedurenames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                if(dept == null)
                {
                    return $"Department with id {deptid} not found.";
                }
                else
                {
                    var deletedData = $"Deleted Department: ID={dept.deptid}, Name={dept.deptname}, Location={dept.deptlocation}";
                   
                    await con.ExecuteScalarAsync(Storedprocedurenames.DeleteDepartment, p, commandType: CommandType.StoredProcedure);
                    return deletedData;
                }
              
            }
        }

        public async Task<List<Department>> GetDepartMentDetails()
        {
            using (IDbConnection conn = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Department>(Storedprocedurenames.GetDepartment, CommandType.StoredProcedure);
                List<Department> res = queryresult.ToList();
                return res;
            }
        }

        public async Task<Department> GetDepartmentDetailsById(int deptid)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptid);
                var result = await con.QueryAsync<Department>(Storedprocedurenames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                return dept;
            }
        }

        public async Task<string> UpdateDepartment(Department deptdetail)
        {

            using (IDbConnection con = _connectionFactory.Northwind_DBSqlConnectionString())
            {//first featch the data based on id and then delete the data based on id and return the deleted data as string format
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.DeptId, deptdetail.deptid);
                var result = await con.QueryAsync<Department>(Storedprocedurenames.GetDepartmentByDeptId, p, commandType: CommandType.StoredProcedure);
                Department dept = result.FirstOrDefault();
                if (dept == null)
                {
                    return $"Department with id {deptdetail.deptid} not found.";
                }
                else
                {
                    var UpdatedData = $"Updated Department: ID={deptdetail.deptid}, Name={deptdetail.deptname}, Location={deptdetail.deptlocation}";
                    var up = new DynamicParameters();
                    up.Add("@deptid", deptdetail.deptid);
                    up.Add("@deptname", deptdetail.deptname);
                    up.Add("@deptlocation", deptdetail.deptlocation);
                    await con.ExecuteScalarAsync(Storedprocedurenames.UpdateDepartment, up, commandType: CommandType.StoredProcedure);
                    return UpdatedData;
                }

            }
        }
    }
}
