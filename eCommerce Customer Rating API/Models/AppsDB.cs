namespace eCommerce_Customer_Rating_API.Models
{
    public class AppsDB
    {
        public List<Apps> GetUsers()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data
            List<Apps> userList = new List<Apps>();
            userList.Add(new Apps()
            {
                ID = 101,
                UserName = "app1",
                Password = "123456"
            });
            userList.Add(new Apps()
            {
                ID = 101,
                UserName = "app2",
                Password = "abcdef"
            });
            return userList;
        }
    }
}
