using schoolManagement.Models;
using System.Data.SQLite;

namespace schoolManagement.Models
{
    public class ClassR : DB
    {
        private int id,schoolId;
        private String name;

        public String Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
        public int SchoolId {get { return schoolId; } set { schoolId = value; } }
        public ClassR( String name, int schoolId) { 
        this.name = name; this.schoolId = schoolId;
        }
        public ClassR() { }

        public ClassR createdClass(SQLiteDataReader reader)
        {
            int? id = reader["id"] != DBNull.Value ? Convert.ToInt32(Convert.ToInt32(reader["id"])) : (int?)null;
            int? schoolId = reader["schoolId"] != DBNull.Value ? Convert.ToInt32(Convert.ToInt32(reader["schoolId"])) : (int?)null;
            String? name = reader["name"] != DBNull.Value ? reader["name"].ToString() : null;

            return new ClassR
            {
                id = id.HasValue ? (int)id : 0,
                name = name,
                schoolId = schoolId.HasValue ? (int)schoolId : 0,
            };
        }

        public bool addClass(ClassR c)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "insert into Class (name,schoolId) VALUES(@name,@schoolId)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", c.Name);
                    command.Parameters.AddWithValue("@schoolId", c.SchoolId);
                    
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }


            }
        }

        public List<ClassR> getAllClass()
        {
            var classR = new List<ClassR>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Class";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classR.Add(createdClass(reader));

                        }
                    }
                }

            }
            return classR;
        }

        public bool updateClass(int id,ClassR c)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "update Class set name = @name,schoolId = @schoolId where id = " + id;
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", c.Name);
                    command.Parameters.AddWithValue("@schoolId", c.SchoolId);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public bool deleteClass(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "delete from Class where id = " + id;
                using (var command = new SQLiteCommand(query, connection))
                {
                    
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public List<ClassR> getClassOfSchool(int schoolId)
        {

            var classR = new List<ClassR>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Class where schoolId = "+schoolId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classR.Add(createdClass(reader));

                        }
                    }
                }

            }
            return classR;
        }
    }
}
