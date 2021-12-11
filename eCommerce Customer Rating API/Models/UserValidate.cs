namespace eCommerce_Customer_Rating_API.Models
{
    public class UserValidate
    {
        private List<Apps> _users = new AppsDB().GetUsers();

        //This method is used to check the user credentials
        public static bool Login(string username, string password)
        {
            AppsDB appsDb = new AppsDB();
            var UserLists = appsDb.GetUsers();

            return UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }

        public async Task<Apps> Authenticate(string username, string password)
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details
            return user;
        }
    }
}
