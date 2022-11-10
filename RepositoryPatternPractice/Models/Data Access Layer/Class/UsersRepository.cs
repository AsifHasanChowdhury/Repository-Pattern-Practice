using RepositoryPatternPractice.Models.Business_Objet;
using RepositoryPatternPractice.Models.Data_Access_Layer.Interface;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class UsersRepository: Interface.IUsersRepository
    {
        public void CreateUser(Users user) { }

        public void ProvideRole(Users user) { }

        public void checkExistingUser(Users user) { }

        public void updateUser(Users user) { }


    }
}
