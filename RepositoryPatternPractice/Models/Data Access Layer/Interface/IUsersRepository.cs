using RepositoryPatternPractice.Models.Business_Objet;
namespace RepositoryPatternPractice.Models.Data_Access_Layer.Interface

{
    public interface IUsersRepository
    {

        public void CreateUser(Users users) { } //implemented

        public string loginAsync(PersonLogin pl) { return ""; } //implemented

        public void SignOut() { } //implemented

        public void ProvideRole() { }

        public void checkExistingUser() { }

        public void updateUser() { }    

        

        //public async Task<bool> loginAsync(PersonLogin pl) { return false; }

        //public async Task<Boolean> loginAsync(PersonLogin pl) { return false; }

    }
}
