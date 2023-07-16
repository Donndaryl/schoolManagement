using System.Data.SQLite;
namespace schoolManagement.Models
{
    public class Student : DB
    {
        private int id;
        private String firstName, lastName, adress, city, zipCode, country;
        public Student(int id, String firstName, String lastName, String adress, String city, String zipCode, String country) 
        { 
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.adress = adress;
            this.city = city;
            this.zipCode = zipCode;
            this.country = country;

        }
        public Student() { }
        public int Id { get{ return id; } set{ id = value; } }
        public String FirstName { get { return firstName; } set { firstName = value; } }
        public String LastName { get { return lastName;} set { lastName = value; } }
        public String Adress { get { return adress; } set { adress = value; } }
        public String City { get { return city; } set { city = value; } }
        public String ZipCode { get {  return zipCode; } set {  zipCode = value; } }
        public String Country { get { return country; } set { country = value; } }

        public Student createStudent(SQLiteDataReader reader)
        {
            int ? id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : (int?)null;

            String firstname  = reader["firstname"] != DBNull.Value ? Convert.ToString(reader["firstname"]) : null;
            String lastname  = reader["lastname"] != DBNull.Value ? Convert.ToString(reader["lastName"]) : null;
            String adress  = reader["adress"] != DBNull.Value ? Convert.ToString(reader["adress"]) : null;
            String city  = reader["city"] != DBNull.Value ? Convert.ToString(reader["city"]) : null;
            String zipCode = reader["zipCode"] != DBNull.Value ? Convert.ToString(reader["zipCode"]) : null;
            String country = reader["country"] != DBNull.Value ? Convert.ToString(reader["country"]) : null;
            return new Student
            {
                id = id.HasValue ? (int)id : 0,
                firstName = firstname,
                lastName = lastname,
                adress = adress,
                city = city,
                zipCode = zipCode,
                country = country,
            };
        }

        public List<Student> getStudents()
        {
            var student = new List<Student>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Student";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Add(createStudent(reader));

                        }
                    }
                }

            }
            return student;
        }

        public bool addStudent(Student s) 
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "insert into Student (firstName,lastName,adress,city,zipCode,country) VALUES(@firstName,@lastName,@adress,@city,@zipCode,@country)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", s.FirstName);
                    command.Parameters.AddWithValue("@lastName", s.LastName);
                    command.Parameters.AddWithValue("@adress", s.Adress);
                    command.Parameters.AddWithValue("@city", s.City);
                    command.Parameters.AddWithValue("@zipCode", s.ZipCode);
                    command.Parameters.AddWithValue("@country", s.Country);
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
        public List<Student> getStudentOfClass(int classId)
        {

            var student = new List<Student>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "select * from student st inner join studentClass stc on st.id = stc.studentId inner join class cl on cl.id = stc.classId where cl.id = " + classId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Add(createStudent(reader));

                        }
                    }
                }

            }
            return student;
        }

        public Dictionary<string, string> getGradeOfStudentOfSubject(int subjectId, int studentId)
        {
            Dictionary<string, string> grade = new Dictionary<string, string>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "select mark from courseNote where subjectId = " + subjectId+" and studentId = "+ studentId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            grade.Add(Convert.ToString( reader["name"]), Convert.ToString(reader["maxPoint"]));
                            break;

                        }
                    }
                }

            }
            return grade;
        }
        public bool modifyDetailsOfStudent(int studentId, Student s)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "update student set firstName = @firstName,lastName = @lastName,adress = @adress,city = @city,zipCode = @zipCode,country = @country where id = "+studentId;
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", s.FirstName);
                    command.Parameters.AddWithValue("@lastName", s.LastName);
                    command.Parameters.AddWithValue("@adress", s.Adress);
                    command.Parameters.AddWithValue("@city", s.City);
                    command.Parameters.AddWithValue("@zipCode", s.ZipCode);
                    command.Parameters.AddWithValue("@country", s.Country);
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

        public bool ChangeClassOfStudent(Dictionary<string, string> infos)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "UPDATE studentClass SET classId = @classId WHERE studentId = @studentId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@classId", infos["classId"]);
                    command.Parameters.AddWithValue("@studentId", infos["studentId"]);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool updateGrateOfStudent(Dictionary<string, string> infos)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "UPDATE courseNote SET mark = @mark WHERE studentId = @studentId and subjectId = @subjectId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@mark", infos["mark"]);
                    command.Parameters.AddWithValue("@studentId", infos["studentId"]);
                    command.Parameters.AddWithValue("@subjectId", infos["subjectId"]);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool removeStudentFromClass(Dictionary<string, string> infos)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "delete from studentClass where studentId = " + infos["studentId"];
                using (var command = new SQLiteCommand(query, connection))
                {
                    return (int)command.ExecuteNonQuery() > 0;
                }
            }
        }

    }


}
