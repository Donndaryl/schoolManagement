using System.Data.SQLite;
namespace schoolManagement.Models
{
    public class DB
    {
        protected string connectionString = "Data Source=DB.sqlite";
        protected SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
