using System.Data.SQLite;
namespace schoolManagement.Models
{
    public class Subject: DB
    {
        private int id,classId, maxPoint;
        private string name;

        public Subject(int id, int classId,int maxPoint, String name) 
        { 
            this.id = id;
            this.classId = classId;
            this.maxPoint = maxPoint; this.name = name;
        }
        public int Id { get { return id; } set { id = value; } }
        public int ClassId { get { return classId; } set { classId = value; } }
        public int MaxPoint { get { return maxPoint; } set { maxPoint = value; } }
        public String Name { get { return name; }
            set
            {
                name = value;
            } }

        public Subject() { }

        public Subject createSubject(SQLiteDataReader reader)
        {
            int? id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : (int?)null;
            int? classId = reader["classId"] != DBNull.Value ? Convert.ToInt32(reader["classId"]) : (int?)null;
            int? maxPoint = reader["maxPoint"] != DBNull.Value ? Convert.ToInt32(reader["maxPoint"]) : (int?)null;

            String? name = reader["name"] != DBNull.Value ? Convert.ToString(reader["name"]) : null;

            return new Subject
            {
                id = id.HasValue ? (int)id : 0,
                classId = classId.HasValue ? (int)classId : 0,
                maxPoint = maxPoint.HasValue ? (int)maxPoint : 0,
                name = name
            };
        }

        public List<Subject> getSubject()
        {
            var subject = new List<Subject>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Subject";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subject.Add(createSubject(reader));

                        }
                    }
                }

            }
            return subject;
        }

        public bool addSubject(Subject s)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "insert into Subject (name,maxPoint,classId) VALUES(@name,@maxPoint,@classId)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", s.Name);
                    command.Parameters.AddWithValue("@maxPoint", s.MaxPoint);
                    command.Parameters.AddWithValue("@classId", s.ClassId);
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
        public List<Subject> getSubjectOfClass(int classId)
        {
            var subject = new List<Subject>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Subject where classId = "+classId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subject.Add(createSubject(reader));

                        }
                    }
                }

            }
            return subject;
        }
        public bool upadteSubject(Subject s)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "update Subject set name = @name,maxPoint = @maxPoint,classId = @classId where id = "+s.Id;
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", s.Name);
                    command.Parameters.AddWithValue("@maxPoint", s.MaxPoint);
                    command.Parameters.AddWithValue("@classId", s.ClassId);

                    return (int)command.ExecuteNonQuery() > 0;

                }


            }
        
        }

        public bool deleteSubject(int subjectId)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "delete from subject where id = " + subjectId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    return (int)command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
