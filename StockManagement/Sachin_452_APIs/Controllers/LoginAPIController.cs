

using Sachin_452_APIs.JWTAuthentication;
using Sachin_452_Helpers;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using Sachin_452_Repository.Service;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace Sachin_452_APIs.Controllers

{
    public class LoginAPIController : ApiController
    {
        ILoginInterface _loginInterface = new LoginService();


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LoginAPI/LoginAccount")]
        public UserModel LoginAccount(LoginModel login)
        {
            try
            {
                UserModel loginModel = _loginInterface.LoginAccount(login);
                loginModel.Token = Authentication.GenerateJWTAuthetication(loginModel.USerName, loginModel.Role);
                return loginModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LoginAPI/RegisterUser")]
        public Users RegisterUser(UserModel user)
        {
            try
            {
                if(user != null)
                {
                    Users _userContext = _loginInterface.RegisterUser(user);
                    return _userContext;
                }
                else
                {
                    return LoginHelper.ConvertCustomModelToUserContext(user);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
         }

    }
}