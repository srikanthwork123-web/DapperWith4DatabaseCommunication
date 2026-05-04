using Dapper;//Import the dapper namespace to use the dapper extension methods for executing SQL queries and commands.
using DapperWith4DatabaseCommunication.Data;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;
using DapperWith4DatabaseCommunication.Utils;
using Serilog;
using System.Data;
namespace DapperWith4DatabaseCommunication.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
        }
        public async Task<int> AddEmployes(Employee empdetail)
        {
            Log.Information("EmployeeRepository: AddEmployes method Excution Starts");
            await _loggingFactory.AddLoggingMessages("chandu", "Information", "EmployeeRepository: AddEmployes method Excution Starts");//logg the message in database using custom logging factory
            //we need to store the Sqlconnection object into IDbConnection Refrence Variable to use the dapper extension methods for executing SQL queries and commands.
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                //In Dapper we will use the DynamicParameters class to pass data to the stored procedure input parameters.
                //The DynamicParameters class allows us to define parameters and their values in a flexible way, making it easier to work with stored procedures that require multiple parameters or output parameters.
                //Create object for DynamicParameters class  for Passing  data to Storedprocedure input paramaters..
                //The first argument is the name of the parameter as defined in the stored procedure, and the second argument is the value you want to pass to that parameter.
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredprocedureParameters.EmployeeName, empdetail.empname);
                parameters.Add(StoredprocedureParameters.EmployeeSalary, empdetail.empsalary);
                parameters.Add(StoredprocedureParameters.Insertedvariable, DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(Storedprocedurenames.AddEmployee, parameters, commandType: CommandType.StoredProcedure);
                int inserterdid = parameters.Get<int>(StoredprocedureParameters.Insertedvariable);
                Log.Information("EmployeeRepository: AddEmployes method Excution Ended");
                await _loggingFactory.AddLoggingMessages("chandu", "Information", "EmployeeRepository: AddEmployes method Excution Ended");//logg the message in database using custom logging factory

                Log.Information("EmployeeRepository: AddEmployes method Excution Ended and Insertedrecord is{@Insertedrecord}", inserterdid);
                await _loggingFactory.AddLoggingMessages("chandu", "Information", $"EmployeeRepository: AddEmployes method Excution Ended and Insertedrecord is {inserterdid}");//logg the message in database using custom logging factory
                return inserterdid;
            }
        }
        public async Task<bool> DeleteEmployesById(int empid)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empid);
                await con.ExecuteScalarAsync(Storedprocedurenames.DeleteEmployee, p, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
            
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empid);
                var result = await con.QueryAsync<Employee>(Storedprocedurenames.GetEmployeeByEmpid, p, commandType: CommandType.StoredProcedure);
                Employee emp = result.FirstOrDefault();//FirstOrDefault() it will return the first element of the sequence or a default value if the sequence contains no elements. In this case, it will return the first Employee object from the result set or null if there are no matching records.
                return emp;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (IDbConnection conn = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Employee>(Storedprocedurenames.GetEmployee, CommandType.StoredProcedure);
                List<Employee> res = queryresult.ToList();
                return res;
            }
        }

        public async Task<bool> UpdateEmploye(Employee empdetail)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                //"@empid"  =>we called this is hardcoding string.Don't write like this way.always read it from static class
                var p = new DynamicParameters();
                p.Add(StoredprocedureParameters.EmployeeID, empdetail.empid);
                p.Add(StoredprocedureParameters.EmployeeName, empdetail.empname);
                p.Add(StoredprocedureParameters.EmployeeSalary, empdetail.empsalary);
                await con.ExecuteReaderAsync(Storedprocedurenames.UpdateEmployee, p, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
