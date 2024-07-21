using DigitalAppStructure2.Models;

namespace DigitalAppStructure2.SolidPrinciple
{
    public class StudentRepository : IStudentRepositpry
    {
        private readonly CrudDigitalAppContext _appContext;
       public StudentRepository(CrudDigitalAppContext appContext)
       {
            _appContext = appContext;
       }
        public IEnumerable<UserList> GetList()
        {
            var u = _appContext.UserLists.ToList();
            return u;
        }

        public void AddStudent(UserList user)
        {
            _appContext.Add(user);
            _appContext.SaveChanges();
        }

        public UserList GetStudentById(int id)
        {
            var a= _appContext.UserLists.Where(predicate: x => x.UserId == id).First();
            return a;
                
        }
        public void UpdateStudent(UserList user)
        {
            _appContext.Update(user);
            _appContext.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            var user = _appContext.UserLists.Where(x => x.UserId == id).FirstOrDefault();
            _appContext.UserLists.Remove(user!);
            _appContext.SaveChanges();
        }

    }
}
