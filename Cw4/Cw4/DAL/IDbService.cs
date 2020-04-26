using System.Collections.Generic;
using Cw4.Models;


namespace Cw4.DAL

{
    public interface IDbService
    {
        public IEnumerable<Student> getStudents();
    }
}
