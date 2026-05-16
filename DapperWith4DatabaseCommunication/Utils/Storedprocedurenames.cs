namespace DapperWith4DatabaseCommunication.Utils
{
    //whenever the no of objects creations are increased automatically perfomence of application decresed.
    //maximum if you write any code try to avoid the object creation .
    //if you use static clas ,no need to create object for static class.
    //because whenever compiler see the static keyword it loads all static related variables and mehods.
    //we can call the static variables using(static classname.variablename) and methods using(staticclassname.methodname())
    //Here to call the static related things no need to create object,due to that perfomance also increased.
    public static class Storedprocedurenames
    {//#region is used to grouping the code at one place  for better organization and readability.(singleline statement for interview purpose)
     //============================================================================================
     // #region is used to group related code together and make it easier to navigate and understand the structure of the code.
     // It allows you to collapse and expand sections of code in an IDE, making it easier to focus on specific parts of the codebase.
     // In this case, we are using #region to group the stored procedure names for each entity (Department, Order, Employee, Restaurant) together for better organization and readability.

        #region Department stored procedures
        public static string AddDepartment = "Usp_AddDepartment";
        public static string UpdateDepartment = "Usp_UpdateDepartment";
        public static string DeleteDepartment = "Usp_DeleteDepartment";
        public static string GetDepartment = "Usp_GetDepartment";
        public static string GetDepartmentByDeptId = "Usp_GetDepartmentById";
        #endregion

        #region order stored procedures
        public static string AddOrder = "Usp_AddOrder";
        public static string UpdateOrder = "Usp_UpdateOrder";
        public static string DeleteOrder = "Usp_DeleteOrder";
        public static string GetOrder = "Usp_GetOrder";
        public static string GetOrderByOrderId = "Usp_GetOrderById";
        #endregion

        #region Employee storedprocedures
        public static string AddEmployee = "Usp_AddEmployeeReturn";
        public static string UpdateEmployee = "Usp_UpdateEmployee";
        public static string DeleteEmployee = "Usp_DeleteEmployee";
        public static string GetEmployee = "Usp_GetEmployee";
        public static string GetEmployeeByEmpid = "Usp_GetEmployeeId";
        #endregion

        #region restaurant storedprocedures
        public static string AddRestaurant = "Usp_AddRestaurant";
        public static string UpdateRestaurant = "Usp_UpdateRestaurant";
        public static string DeleteRestaurant = "Usp_DeleteRestaurant";
        public static string GetRestaurant = "Usp_GetRestaurant";
        public static string GetRestaurantById = "Usp_GetRestaurantById";
        #endregion

        #region Logging storedprocedures
        public static string AddLoggingMessages = "Usp_ProjectLevelLog";
        #endregion

        #region ErrorLog storedprocedures
        public static string AddProjectLevelErrorlog = "Usp_AddProjectLevelErrorlog";
        #endregion

        #region TokenBasedAuthentication storedprocedures
        public static readonly string GetUserRolesInformation = "Usp_GetUserRolesInformation";

        public static readonly string SignIn = "Usp_LoginCheck";

        public static readonly string Usp_UserResgistration = "Usp_UserResgistration";

        public static readonly string Usp_RolesResgistration = "Usp_RolesResgistration";

        public static readonly string Usp_UserRolesMapping = "Usp_UserRolesMapping";

        #endregion
    }
}
