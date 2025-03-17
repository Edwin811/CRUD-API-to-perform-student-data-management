using Npgsql;
using percobaan2.Helpers;
using System.Collections.Generic;

namespace percobaan2.Models
{
    public class StudentContext
    {
        private string __constr;

        public StudentContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT * FROM students";
            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student()
                {
                    id_student = (int)reader["id_student"],
                    nama = reader["nama"].ToString(),
                    alamat = reader["alamat"].ToString(),
                    email = reader["email"].ToString(),
                });
            }

            reader.Close();
            cmd.Dispose();

            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = null;
            string query = "SELECT * FROM students WHERE id_student = @id";

            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            try
            {
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student()
                    {
                        id_student = (int)reader["id_student"],
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            cmd.Dispose();

            return student;
        }

        public Student GetStudentByFullName(string nama)
        {
            Student student = null;
            string query = "SELECT * FROM students WHERE LOWER(nama) = LOWER(@nama)";

            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@nama", nama); // Mencari nama persis

            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = new Student()
                {
                    id_student = (int)reader["id_student"],
                    nama = reader["nama"].ToString(),
                    alamat = reader["alamat"].ToString(),
                    email = reader["email"].ToString(),
                };
            }

            reader.Close();
            cmd.Dispose();

            return student;
        }


        public void AddStudent(Student student)
        {
            string query = @"INSERT INTO students (nama, alamat, email) 
                           VALUES (@nama, @alamat, @email)";

            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@nama", student.nama);
            cmd.Parameters.AddWithValue("@alamat", student.alamat);
            cmd.Parameters.AddWithValue("@email", student.email);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }

        public void UpdateStudent(Student student)
        {
            string query = @"UPDATE students SET 
                        nama = @nama, 
                        alamat = @alamat, 
                        email = @email 
                     WHERE id_student = @id";

            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@nama", student.nama);
            cmd.Parameters.AddWithValue("@alamat", student.alamat);
            cmd.Parameters.AddWithValue("@email", student.email);
            cmd.Parameters.AddWithValue("@id", student.id_student);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }

        public void DeleteStudent(int id)
        {
            string query = "DELETE FROM students WHERE id_student = @id";

            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
    }
}
