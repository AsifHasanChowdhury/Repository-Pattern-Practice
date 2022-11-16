using RepositoryPatternPractice.Models.Business_Objet;
namespace RepositoryPatternPractice.Models.Data_Access_Layer.Interface

{
    public interface IUsersRepository
    {

        public void CreateUser(Users users) { } //implemented

        public void ProvideRole() { }

        public void checkExistingUser() { }

        public void updateUser() { }    

        public Boolean login(PersonLogin pl) { return false; }


    }
}
