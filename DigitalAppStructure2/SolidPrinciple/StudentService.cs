using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositpry _repo;
        public StudentService(IStudentRepositpry repo) {
          _repo=repo;
        }

        public IEnumerable<UserList> GetStudents() => _repo.GetList();

        public UserList GetStdById(int id)
        {
            return _repo.GetStudentById(id);
        }

        public void AddStd(UserList std)
        {
            _repo.AddStudent(std);
        }
        public void DeleteStd(int id) { }
        public void UpdateStd(UserList s) { }
    }
}
