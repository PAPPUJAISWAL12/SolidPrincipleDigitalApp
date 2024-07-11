using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudents();
        Student GetStdById(int id);
        void AddStd(Student std);
        void UpdateStd(Student std);
        void DeleteStd(int id);
    }
}
