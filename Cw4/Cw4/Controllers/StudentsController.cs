using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;


namespace Cw4.Controllers
{
  
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s16597;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudents()
        {

            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var st = new Student();
                    st.IndexNumber = dr["indexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                    st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"].ToString());


                    list.Add(st);

                }

            }

            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student where indexnumber=@index";
                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["indexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                    st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"].ToString());


                    return Ok(st);

                }


            }

            return NotFound();
        }

        [HttpGet("joinTables")]
        public IActionResult GetStudentsJoinTables()
        {

            var list2 = new List<Combo>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName, LastName, BirthDate, Name, Semester From Student st LEFT JOIN Enrollment enr ON st.IdEnrollment = enr.IdEnrollment LEFT JOIN Studies stu ON enr.IdStudy = stu.IdStudy";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var combo = new Combo();

                    combo.FirstName = dr["FirstName"].ToString();
                    combo.LastName = dr["LastName"].ToString();
                    combo.BirthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                    combo.Name = dr["Name"].ToString();
                    combo.Semester = Convert.ToInt32(dr["Semester"].ToString());

                    list2.Add(combo);

                }


            }

            return Ok(list2);
        }

    }
}