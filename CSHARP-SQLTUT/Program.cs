
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using EdDbLIb;

namespace CSHARP_SQLTUT
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var connection = new Connection(connectionString);
            connection.Open();
            var majorsCtrl = new MajorsController(connection);

            var majors = majorsCtrl.GetByPk(1);  // this selects by the primary key instead of getting all majors as seen below
            Console.WriteLine(majors);  // this displays the line that you made a change to
            //majors = majorsCtrl.GetByPk(11111);
            //Console.WriteLine(majors);


            //majors.Description = "UWBW - By Dan";
            //var rowsAffected = majorsCtrl.Remove(8);    // this all points to major controllers DELETE   to target the row you want to delete, change pk on GetByPk and Remove
            //if (rowsAffected != 1)
            //{
            //    Console.WriteLine("Change failed!");
            //}

            majors.Description = "UWBW - By Dan";
          var rowsAffected = majorsCtrl.Change(majors);  // this all points to major controllers CHANGE
            if (rowsAffected != 1)
            {
                Console.WriteLine("Change failed!");
            }

            //rowsAffected = majorsCtrl.Remove(majors.Id);
               

            var major = majorsCtrl.GetAll();
            foreach (var maj in major)
            {
                Console.WriteLine(maj);
            }


            //// now we're doing an Insert. nothing will happen before instance of our Major. When we insert, the user has to have completed an instance of major with data filled in.  so... 
            //// **************************** NEW *****************************************************
            //var newMajor = new Major()
            //{
            //    Id = 0,
            //    Code = "UWBW",
            //    Description = "Basket Weaving - Underwater",
            //    MinSAT = 1590
            //};
            //var rowsAffected = majorsCtrl.Create(newMajor);
            //if (rowsAffected != 1)
            //{
            //    Console.WriteLine("Create failed!");
            //}
            connection.Close();
        }
            static void x() { 
            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";    // likely use another version of sql
            var sqlConn = new SqlConnection(connStr); // sets up a connection from c# to SQL - does the same things as the login/connect prompt in SSMS (SQL) - close connections!
            sqlConn.Open(); // actually creates the connection, use if statement because it likely wont be open
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return;
            }
            Console.WriteLine("Connection opened.");
            // continue sql here

            var sql = " Select * from Student " +
                " where GPA between 2.5 and 3.5 " +
                " order by SAT desc;";
            var cmd = new SqlCommand( sql, sqlConn);   //first sql, then sql connection
            var reader = cmd.ExecuteReader();
            var students = new List<Student>();
            while (reader.Read())
            {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);  // Id = Object,  Objects     cant be null
                student.Firstname = reader["Firstname"].ToString();    // turns whatever it is, into string using tostring
                student.Lastname = reader["Lastname"].ToString();
                student.StateCode = reader["StateCode"].ToString();
                student.SAT = reader["SAT"].Equals(DBNull.Value)
                    ? (int?)null   // allows it to be any int or allow null, if its true do this, the type has to be a nullable int
                    : Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                student.MajorId = reader["MajorId"].Equals(DBNull.Value)
                    ? (int?)null
                    : Convert.ToInt32(reader["MajorId"]);
                Console.WriteLine(student);
                students.Add(student);
            }
            reader.Close();
            sqlConn.Close();  // make sure you close the application when you are done with it

        }
    }
}
