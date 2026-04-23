using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using Microsoft.Data.SqlClient;

namespace DapperWith4DatabaseCommunication.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        /*
iF YOU WANT READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE,
WE HAVE ONE PREDEFINE INTEFACE IS AVAILABLE IN .NET CALLED IConfiguration, 
WE CAN INJECT IT IN THE CONSTRUCTOR OF THE CONNECTION FACTORY CLASS AND THEN READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE by using GetSection().
*/
        private readonly IConfiguration _configuration;
        //inject the IConfiguration interface in the constructor of the connection factory class and assign it to the private readonly field of the IConfiguration interface type.
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection HotelmanagementsqlConnectionString()
        { //READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE USING THE IConfiguration INTERFACE AND THEN CREATE A NEW SQL CONNECTION USING THE CONNECTION STRING AND RETURN IT.
            var connectionString = Convert.ToString(_configuration.GetSection(Connectionstringnames.Hotelmanagement_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
        //Here i am reading/getting  the connectionstring and assign to sqlconnection class object and returning the Data.
        public SqlConnection MidLandSqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(Connectionstringnames.Midland_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public SqlConnection Northwind_DBSqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(Connectionstringnames.Northwind_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public SqlConnection RestaurantDBSqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(Connectionstringnames.Restaurant_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
