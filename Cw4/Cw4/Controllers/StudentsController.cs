using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw4.DAL;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;


namespace Cw4.Controllers
{
  
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s16597;Integrated Security=True";

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("getStudents")]

        public IActionResult GetListStudent(string orderBy)
        {

            return Ok(_dbService.getStudents());

        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT IndexNumber, FirstName, LastName, BirthDate, st.IdEnrollment, Name, Semester FROM Student st JOIN ENROLLMENT enr ON st.IdEnrollment = enr.IdEnrollment JOIN Studies stud on enr.IdStudy = stud.IdStudy";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var st = new Student();
                    st.indexNumber = dr["IndexNumber"].ToString();
                    st.firstName = dr["FirstName"].ToString();
                    st.lastName = dr["LastName"].ToString();
                    st.birthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                    st.idEnrollment = Convert.ToInt32(dr["IdEnrollment"].ToString());
                    st.study = dr["Name"].ToString();
                    st.semester = Convert.ToInt32(dr["Semester"].ToString());



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
                    st.indexNumber = dr["indexNumber"].ToString();
                    st.firstName = dr["FirstName"].ToString();
                    st.lastName = dr["LastName"].ToString();
                    st.birthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                    st.idEnrollment = Convert.ToInt32(dr["IdEnrollment"].ToString());


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

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1, 99999)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            return Ok("Aktualizacja studenta nr " + id + " dokończona.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie studenta nr " + id + " ukończone.");
        }

    }
}