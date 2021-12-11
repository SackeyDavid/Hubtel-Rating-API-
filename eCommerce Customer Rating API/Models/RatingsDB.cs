namespace eCommerce_Customer_Rating_API.Models
{
    public class RatingsDB
    {
        private static Random random = new Random();
        public List<Ratings> GetEmployees()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data

            List<Ratings> empList = new List<Ratings>();
            for (int i = 0; i < 10; i++)
            {
                if (i > 5)
                {
                    empList.Add(new Ratings()
                    {
                        RatingsId = i,
                        ItemId = RandomString(100),
                        Comment = "Fantastic, I'm totally blown away by the product.",
                        Stars = 5
                    });
                }
                else
                {
                    empList.Add(new Ratings()
                    {
                        RatingsId = i,
                        ItemId = RandomString(100),
                        Comment = "The service was excellent. I'm good to go. I don't know what else to say.",
                        Stars = 5
                    });
                }
            }
            return empList;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }

    

}
