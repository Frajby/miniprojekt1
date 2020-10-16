using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.IO;
using System.Data;

namespace mini_projekt
{
    class DBworker
    {
        public string ConnectionString { get; set; }
        SqlConnection conn;
        public DBworker(string connectionString)
        {
            ConnectionString = connectionString;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public List<Student> searchStudent(string s)
        {
            string q = String.Format("SELECT * FROM Students WHERE Students.name LIKE '{0}%' OR Students.surname LIKE '{0}%';",s);
            SqlCommand command = new SqlCommand(q, conn);
            conn.Open();
            List<Student> students = new List<Student>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Student student = new Student();
                student.ID = Convert.ToInt32(reader["Id"]);
                student.Name = reader["name"].ToString();
                student.Surname = reader["surname"].ToString();
                students.Add(student);
            }
            conn.Close();
            return students;
        }

        public List<Subject> getStudentsSubject(int id)
        {
            string q = String.Format("SELECT Subjects.name, Teachers.name, Teachers.surname FROM Students INNER JOIN((Teachers INNER JOIN (Subjects INNER JOIN Teaching ON Subjects.Id = Teaching.Id_Subject) ON Teachers.Id = Teaching.Id_Teacher) INNER JOIN StudSubjects ON Subjects.Id = StudSubjects.Id_Subject) ON Students.Id = StudSubjects.Id_Student WHERE(((Students.Id) = {0})); ",id);
            SqlCommand command = new SqlCommand(q, conn);
            conn.Open();
            List<Subject> subjects = new List<Subject>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Subject subject = new Subject();
                subject.Name = reader[0].ToString();
                subject.Teacher = reader[1].ToString();
                subject.Teacher += " " + reader[2].ToString();
                subjects.Add(subject);
            }
            conn.Close();
            return subjects;
        }

        public List<Subject> getAllSubjects()
        {
            string q = "SELECT * FROM Subjects";
            SqlCommand command = new SqlCommand(q, conn);
            conn.Open();
            List<Subject> subjects = new List<Subject>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Subject subject = new Subject();
                subject.ID = (int)reader["Id"];
                subject.Name = reader["name"].ToString();
                subjects.Add(subject);
               
            }
            conn.Close();
            return subjects;
        }

        public void SaveStudent(Student student)
        {
            string q = String.Format("INSERT INTO Students (name,surname) VALUES ('{0}','{1}')", student.Name, student.Surname);
            SqlCommand command = new SqlCommand(q, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            int lastId = getLasIdFromTable("Students");

            foreach (Subject s in student.Subjects)
            {
                string qs = String.Format("INSERT INTO StudSubjects (Id_Student, Id_Subject) VALUES ({0},{1})", lastId, s.ID);
                conn.Open();
                SqlCommand commandS = new SqlCommand(qs, conn);
                commandS.ExecuteNonQuery();
                conn.Close();
            }

        }

        public int getLasIdFromTable(string tableName)
        {
            int lastId = -1;
            string q = String.Format("SELECT TOP 1 Id FROM {0} ORDER BY Id DESC;", tableName);
            SqlCommand command = new SqlCommand(q, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lastId = (int)reader[0];
            }
            conn.Close();
            return lastId;
        }

    }
}
