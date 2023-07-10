using System.Data.SQLite;
namespace schoolManagement.Models
{
    public class DB
    {
        protected string connectionString = "Data Source=Borrowers.sqlite";
        protected SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
