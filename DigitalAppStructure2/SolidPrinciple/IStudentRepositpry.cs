using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public interface IStudentRepositpry
    {
        IEnumerable<UserList> GetList();
        UserList GetStudentById(int id);

        void AddStudent(UserList std);
        void UpdateStudent(UserList std);
        void DeleteStudent(int id);
    }
}
