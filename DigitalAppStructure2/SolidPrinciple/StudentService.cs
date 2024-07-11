using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositpry _repo;
        public StudentService(IStudentRepositpry repo) {
          _repo=repo;
        }

        public IEnumerable<Student> GetStudents() => _repo.GetList();

        public Student GetStdById(int id)
        {
            return _repo.GetStudentById(id);
        }

        public void AddStd(Student std)
        {

        }
        public void DeleteStd(int id) { }
        public void UpdateStd(Student s) { }
    }
}
