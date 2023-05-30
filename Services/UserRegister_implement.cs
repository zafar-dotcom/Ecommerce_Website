using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using OnLineShop.IServices;
using OnLineShop.Models;

namespace OnLineShop.Services
{
    public class UserRegister_implement : IUserRegister
    {
        public string str;
        public UserRegister_implement()
        {
            str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";

        }
        public bool InsertUSer(UserRegister userRegister)
        {
           UserRegister obj=new UserRegister();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("spuserregistration", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_firstname", userRegister.FirstName);
                        cmd.Parameters.AddWithValue("_lastname", userRegister.LastName);
                        cmd.Parameters.AddWithValue("_password", userRegister.Password);
                        cmd.Parameters.AddWithValue("_email", userRegister.Email);
                        cmd.Parameters.AddWithValue("_phone", userRegister.PhoneNumber);
                        cmd.Parameters.AddWithValue("_securityanswer", userRegister.SecurityAnwser);
                        cmd.Parameters.AddWithValue("_gender", userRegister.Gender);
                        cmd.Parameters.AddWithValue("_status", "insert");
                        int rowsaffected = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (rowsaffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Login(UserRegister user)
        {
            using(MySqlConnection conn = new MySqlConnection(str))
            {
                conn.Open();
                string query = "select count(*) from UserRegistration where Email=@email && Password=@Password";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        int result =Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                        if (result > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
