using System.Data.SQLite;
using System.Xml.Linq;

namespace schoolManagement.Models
{
    public class CourseNote : DB
    {
        private int id,subjectId, studentId;
        private Double mark;
        public CourseNote(int id, int subjectId, int studentId, Double mark) 
        { 
            this.id = id;
            this.subjectId = subjectId;
            this.studentId = studentId; this.mark = mark;
        }
        public CourseNote() { }
        public int Id { get { return id; } set { id = value; } }
        public int SubjectId { get {  return subjectId; } set {  subjectId = value; } }
        public int StudentId { get { return studentId; } set {  studentId = value; } }
        public double Mark { get { return mark; }
            set
            {
                mark = value;
            } }

        public CourseNote createCourseNote(SQLiteDataReader reader)
        {

            int? id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : (int?)null;
            int? subjectId = reader["subjectId"] != DBNull.Value ? Convert.ToInt32(reader["subjectId"]) : (int?)null;
            int? studentId = reader["studentId"] != DBNull.Value ? Convert.ToInt32(reader["studentId"]) : (int?)null;
            Double mark = (Double) reader["mark"];


            return new CourseNote
            {
                id = id.HasValue ? (int)id : 0,
                subjectId = subjectId.HasValue ? (int)subjectId : 0,
                studentId = studentId.HasValue ? (int)studentId : 0,
                mark = mark
            };
        }

        public bool adcourseNote(CourseNote c)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "insert into CourseNote (mark,subjectId,studentId) VALUES(@mark,@subjectId,@studentId)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@mark", c.Mark);
                    command.Parameters.AddWithValue("@subjectId", c.StudentId);
                    command.Parameters.AddWithValue("@studentId", c.StudentId);
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

        public List<CourseNote> GetCourseNotes()
        {
            var courseNote = new List<CourseNote>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM CourseNote";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courseNote.Add(createCourseNote(reader));

                        }
                    }
                }
            }
            return courseNote;
        }

        

        public bool deleteGradeOfStudent(Dictionary<string, string> infos)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "delete from courseNote where subjectId = " + infos["subjectId"] +" and studentId = " + infos["studentId"];
                using (var command = new SQLiteCommand(query, connection))
                {
                    return (int)command.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool deleteAllGradeOfStudent(Dictionary<string, string> infos)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "delete from courseNote where studentId = " + infos["studentId"];
                using (var command = new SQLiteCommand(query, connection))
                {
                    return (int)command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
