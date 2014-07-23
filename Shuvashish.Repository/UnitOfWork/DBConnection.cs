using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace Shuvashish.Repository.UnitOfWork
{
    public class DBConnection
    {
        public static string GetConStrIntegrated()
        {
            //var conStrIntegratedSecurity = new EntityConnectionStringBuilder
            //{
            //    Metadata = "res://*/NorthwindModel.csdl|res://*/NorthwindModel.ssdl|res://*/NorthwindModel.msl",
            //    Provider = "System.Data.SqlClient",
            //    ProviderConnectionString = GetConectionString()
            //}.ConnectionString;

            //return conStrIntegratedSecurity;

            return GetConectionString();
        }
       
        public static string GetConectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
            return connectionString;
        }
    }
}