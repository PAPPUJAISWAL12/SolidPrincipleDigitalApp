using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public interface IStudentService
    {
        IEnumerable<UserList> GetStudents();
        UserList GetStdById(int id);
        void AddStd(UserList std);
        void UpdateStd(UserList std);
        void DeleteStd(int id);
    }
}
