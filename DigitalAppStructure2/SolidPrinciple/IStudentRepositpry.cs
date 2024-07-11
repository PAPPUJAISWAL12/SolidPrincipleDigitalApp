using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public interface IStudentRepositpry
    {
        IEnumerable<Student> GetList();
        Student GetStudentById(int id);

        void AddStudent(Student std);
        void UpdateStudent(Student std);
        void DeleteStudent(int id);
    }
}
