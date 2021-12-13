using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdDbLIb
{
    public class MajorsController
    {

        private SqlConnection sqlConn { get; set; }
        public MajorsController(Connection connection)
        {
            sqlConn = connection.SqlConnection;  // this is all refactored code^^  vv
        }

        //public int Remove(int Id)
        //{
        //    var major = GetByPk(Id);
        //    if (major == null)
        //    {
        //        throw new Exception("Major row not found!");
        //    }
        //    var sql = " DELETE Major  " +
        //             $" Where Id = {9}; ";
        //    var cmd = new SqlCommand(sql, sqlConn);
        //    var rowsAffected = cmd.ExecuteNonQuery();      // check SQL Syntax via SSMS before listing here. (pump that through ssms to make sure your sql statements are syntactically correct
        //    return rowsAffected;
        //}

        public int Change(Major major)
        {
            var sql = " UPDATE Major Set " +
                     $" Code = Code," +
                     $" Description = major.Description," +
                     $" MinSAT = MinSAT " +
                     $" Where Id = Id" ;
            var cmd = new SqlCommand(sql, sqlConn);
            cmd.Parameters.AddWithValue("@Code", major.Code);
            cmd.Parameters.AddWithValue("@Description", major.Description);
            cmd.Parameters.AddWithValue("@MinSAT", major.MinSAT);
            cmd.Parameters.AddWithValue("@Id", major.Id);

            var rowsAffected = cmd.ExecuteNonQuery();      // check SQL Syntax via SSMS before listing here. (pump that through ssms to make sure your sql statements are syntactically correct
            return rowsAffected;
        }

        public int Create(Major major)  //  **********************NEW******************************
        {
            var sql = "INSERT Major (Code, Description, MinSAT) " +
                     $" VALUES ('{major.Code}', '{major.Description}', {major.MinSAT}); ";
            var cmd = new SqlCommand(sql, sqlConn);
            var rowsAffected = cmd.ExecuteNonQuery();      // check SQL Syntax via SSMS before listing here. (pump that through ssms to make sure your sql statements are syntactically correct
            return rowsAffected;
        }   
        public List<Major> GetAll()
        {

            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed to open!");
            }
            //connection opened fine! Now use sql statements

            var sql = $"Select * from Major";
            var cmd = new SqlCommand(sql, sqlConn); // 
          

            var majors = new List<Major>();
            var reader = cmd.ExecuteReader();  
            while (reader.Read())
            {
                var major = new Major()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    MinSAT = Convert.ToInt32(reader["MinSAT"])
                };
                majors.Add(major);
            }

            reader.Close();
            return majors;



        }
        public Major? GetByPk(int Id)
        {
            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed to open!");

            }
            var sql = $"Select * from Major where Id = @Id;";
            var cmd = new SqlCommand(sql, sqlConn);
            cmd.Parameters.AddWithValue("@Id", Id);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows) 
            {
                reader.Close();
                sqlConn.Close();
                return null;   // this is if we don't find the one we're looking for
            }
            reader.Read();
            var major = new Major()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString(),
                Description = reader["Description"].ToString(),
                MinSAT = Convert.ToInt32(reader ["MinSat"])
            };
            reader.Close();
            sqlConn.Close();
            return major;
        }
    }
}

