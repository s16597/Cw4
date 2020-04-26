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
                new Student{indexNumber="",firstName="Kinga",lastName="Malecka"},
                new Student{indexNumber="",firstName="Robert",lastName="Rak"},
                new Student{indexNumber="",firstName="Antoni",lastName="Pasek"}
            };
        }
        public IEnumerable<Student> getStudents()
        {
            return _students;
        }
    }
}