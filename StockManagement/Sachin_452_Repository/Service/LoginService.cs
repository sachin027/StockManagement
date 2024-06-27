using Sachin_452_Helpers;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Repository.Service
{
    public class LoginService : ILoginInterface
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="user"></param>
        public UserModel LoginAccount(LoginModel user)
        {
            try
            {
                UserModel userModel = new UserModel();
               
                Users isUserExist = _DBContext.Users.FirstOrDefault(x => x.EmailID == user.EmailID && x.Password == user.Password);
                if (isUserExist != null)
                {
                        userModel.EmailID = isUserExist.EmailID;
                        userModel.Password = isUserExist.Password;
                        userModel.USerName = isUserExist.USerName;
                        userModel.Role = isUserExist.Role;
                    return userModel;
                }
                else
                {
                    return userModel;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// New user register service
        /// </summary>
        /// <param name="userModel"></param>
        public Users RegisterUser(UserModel userModel)
        {
            try
            {
                var IsEmailExist = _DBContext.Users.Any(x => x.EmailID == userModel.EmailID);

                if (!IsEmailExist)
                {
                    userModel.Role = "Users";
                    Users users = LoginHelper.ConvertCustomModelToUserContext(userModel);
                    _DBContext.Users.Add(users);
                    _DBContext.SaveChanges();
                    return users;
                }
                else
                {
                    return LoginHelper.ConvertCustomModelToUserContext(userModel);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
