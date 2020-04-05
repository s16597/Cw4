using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cw4.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IndexNumber="s11326",FirstName="Kinga",LastName="Malecka"},
                new Student{IndexNumber="s12497",FirstName="Robert",LastName="Rak"},
                new Student{IndexNumber="s13987",FirstName="Antoni",LastName="Pasek"}
            };
        }
        public IEnumerable<Student> GetStudent()
        {
            return _students;
        }
    }
}