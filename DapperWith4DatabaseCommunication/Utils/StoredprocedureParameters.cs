using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DapperWith4DatabaseCommunication.Utils
{
    public static class StoredprocedureParameters
    {
        #region Department Parameters
        public static string DeptId = "@deptid";
        public static string DeptName = "@deptname";
        public static string DeptLocation = "@deptlocation";
        public static string DeptInsertedvariable = "@insertedvalue";
        #endregion

        #region Order Parameters
        public static string OrderId = "@orderid";  //@orderid
        public static string OrderName = "@ordername";
        public static string OrderLocation = "@orderlocation";
        public static string OrderInsertedvariable = "@insertedvalue";
        #endregion

        #region Employee Parameters
        public static string EmployeeID = "@empid";
        public static string EmployeeName = "@empname";
        public static string EmployeeSalary = "@empsalary";
        public static string Insertedvariable = "@insertvalue";
        #endregion

        #region Restaurant Parameters
        public static string ID = "@Id";
        public static string RestaurantName = "@RestaurantName";
        public static string RestaurantLocation = "@RestaurantLocation";
        public static string CreationDate = "@CreationDate";
        #endregion

        #region Logging Parameters

        public static string Logging_UserName = "@username";
        public static string Logging_LogLevel = "@LogLevel";
        public static string Logging_MessageTemplate = "@MessageTemplate";
        #endregion

        #region ErrorLog Parameters
        public static string ErrorLog_StatusCode = "@StatusCode";
        public static string ErrorLog_ErrorMessage = "@ErrorMessage";
        public static string ErrorLog_StackTraceError = "@StackTraceError";
        public static string ErrorLog_InnerExceptionError = "@InnerExceptionError";
        public static string ErrorLog_UserName = "@username";
        #endregion
    }
}



