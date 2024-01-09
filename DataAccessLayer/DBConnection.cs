using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Web.Configuration;
using System.IO;


namespace DataAccessLayer
{
    public class DBConnection
    {
        private string dbsource;
        private string path;
        public DBConnection()
        {
            dbsource = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbsource);
        }
        public string DBsource()
        {
            var connectionString = string.Format("DataSource={0};Version=3;", path);
            return connectionString;
        }
    }
}
