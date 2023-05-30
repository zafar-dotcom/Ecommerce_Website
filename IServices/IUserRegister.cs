using OnLineShop.Models;

namespace OnLineShop.IServices
{
    public interface IUserRegister
    {
        bool InsertUSer(UserRegister userRegister);
        int Login(UserRegister user);
    }
}
