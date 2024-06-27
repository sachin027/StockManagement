using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Repository.Interface
{
    public interface ILoginInterface
    {

        UserModel LoginAccount(LoginModel user);
        Users RegisterUser(UserModel userModel);
    }
}
