using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public class StudentRepository : IStudentRepositpry
    {
        public List<Student> StudentList()
        {
            List<Student> s = new()
            {
                new Student { Id=1,Name="Hari1",Address="Ktm"},
                new Student { Id=2,Name="Hari2",Address="Ktm"},
                new Student { Id=3,Name="Hari3",Address="Ktm"},
            };
            return s;
        }
        public IEnumerable<Student> GetList()
        {    return StudentList();
        }

        public void AddStudent(Student std)
        {
        }

        public Student GetStudentById(int id)
        {
            return StudentList().Where(x => x.Id == id).First();
        }
        public void UpdateStudent(Student student)
        {
        }
        public void DeleteStudent(int id)
        {
        }

    }
}
