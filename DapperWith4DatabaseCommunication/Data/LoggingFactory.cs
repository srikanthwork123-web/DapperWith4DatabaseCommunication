using Dapper;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using Microsoft.Data.SqlClient;
using Serilog.Events;
using System.Data;

namespace DapperWith4DatabaseCommunication.Data
{
    public class LoggingFactory: ILoggingFactory
    {
        #region connectionFactory
        private readonly IConnectionFactory _connectionFactory;
        public LoggingFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion

      
        public async Task<bool> AddLoggingMessages(string userName, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                //DynamicParameters used in dapper,to pass the values to storedprocedure parameters.
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.Logging_UserName, userName);
                p.Add(StoredprocedureParameters.Logging_LogLevel, logLevel);
                p.Add(StoredprocedureParameters.Logging_MessageTemplate, messageTemplate);
                await con.ExecuteScalarAsync(Storedprocedurenames.AddLoggingMessages, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }

        public async Task<bool> AddProjectLevelErrorlogAsync(string statusCode, string ErrorMessage, string StackTraceError,string InnerExceptionError)
        {
            using (IDbConnection con = _connectionFactory.HotelmanagementsqlConnectionString())
            {
                //DynamicParameters used in dapper,to pass the values to storedprocedure parameters.
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredprocedureParameters.ErrorLog_StatusCode, statusCode);
                p.Add(StoredprocedureParameters.ErrorLog_ErrorMessage, ErrorMessage);
                p.Add(StoredprocedureParameters.ErrorLog_StackTraceError, StackTraceError);
                p.Add(StoredprocedureParameters.ErrorLog_InnerExceptionError, InnerExceptionError);
                await con.ExecuteScalarAsync(Storedprocedurenames.AddProjectLevelErrorlog, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
    }
}
