using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Helpers
{
    public class LoginHelper
    {
        /// <summary>
        /// Convert custom model into users table
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>User Table </returns>
        public static Users ConvertCustomModelToUserContext(UserModel userModel)
        {
            try
            {
                Users user = new Users();
                if(userModel != null)
                {
                    user.UserID = userModel.UserID;
                    user.USerName = userModel.USerName;
                    user.EmailID = userModel.EmailID;
                    user.Password = userModel.Password;
                    user.Role = userModel.Role;
                }
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
    }
}
