using System.Data.SQLite;

namespace schoolManagement.Models
{
    public class School : DB
    {
        private int id;
        private String city, zipCode, country, name, adress;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public String City
        {
            get { return city; }
            set { city = value; }

        }
        public String ZipCode
        {
            get { return zipCode; }
            set
            {
                zipCode = value;
            }
        }
        public String Country
        {
            get { return country; }
            set
            {
                country = value;
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public String Adress
        {
            get { return adress; }
            set
            {
                adress = value;
            }
        }

        public School(String name,String adress, String zipCode, String city, String country)
        {
            this.name = name;
            this.city = city;
            this.zipCode = zipCode;
            this.country = country;
            this.adress  = adress;
        }

        public School() { }

        public School createSchool(SQLiteDataReader reader) 
        {
            int? id = reader["id"] != DBNull.Value ? Convert.ToInt32(Convert.ToInt32(reader["id"])) : (int?)null;
            String ? name = reader["name"] != DBNull.Value ? reader["name"].ToString() : null;
            String ? country = reader["country"] != DBNull.Value ? reader["country"].ToString() : null;
            String? zipCode = reader["zipCode"] != DBNull.Value ? reader["zipCode"].ToString() : null;
            String ? adress = reader["adress"] != DBNull.Value ? reader["adress"].ToString() : null;
            String? city = reader["city"] != DBNull.Value ? reader["city"].ToString() : null;

            return new School
            {
                id = id.HasValue ? (int)id : 0,
                name = name,
                city = city,
                country = country,
                zipCode = zipCode,
                adress = adress
            };

        }

        public List<School> getAllSchool()
        {
            var school = new List<School>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM School";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            school.Add(createSchool(reader));

                        }
                    }
                }

            }
            return school;
        }

        public bool addSchool(School s)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "insert into School (name,adress,city,zipCode,country) VALUES(@name,@adress,@city,@zipCode,@country)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name",s.Name);
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
    }
}
