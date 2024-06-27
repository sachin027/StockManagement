using Newtonsoft.Json;
using Sachin_452.CustomFilter;
using Sachin_452.WebHelper;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{
    [CustomUserAuthorize]
    public class UserController : Controller
    {
        // GET: User
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        public ActionResult UserDashboard()
        {
            return View();
        }

        public async Task<ActionResult> ProductList()
        {
            try
            {
                string url = "api/AdminAPI/ProductList";
                string response = await WebHelpers.HttpRequestResponce(url);
                List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(response);
                return View(products);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult AddToCart(int id)
        {
            try
            {
                int userId = SessionHelper.SessionHelper.UserID;
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@ProductId" , id),
                    new SqlParameter("@supplierId" , userId)
                };
                _DBContext.Database.ExecuteSqlCommand("exec AddToCart @ProductId , @supplierId", sqlParameters);
                TempData["success"] = "Added to cart !";
                return RedirectToAction("ProductList","User");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult cartlist()
        {
            try
            {
                ViewBag.email = SessionHelper.SessionHelper.EmailID;
                int supplierid = SessionHelper.SessionHelper.UserID;
                SqlParameter[] sqlparameters = new SqlParameter[]
                    {
                    new SqlParameter("@supplierid",supplierid)
                    };

                List<CartModel> _cartmodel = _DBContext.Database.SqlQuery<CartModel>("exec getordermanagement @supplierid", sqlparameters).ToList();

                return View(_cartmodel);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpPost]
        public JsonResult CartList(List<OrderModel> _OrderModel)
        {
            ViewBag.Email = SessionHelper.SessionHelper.EmailID;

            try
            {
                if (_OrderModel != null)
                {
                    int SupplierId = SessionHelper.SessionHelper.UserID;

                    foreach (var orderModel in _OrderModel)
                    {
                        SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                        new SqlParameter("@Price", orderModel.Price),
                        new SqlParameter("@ProductId", orderModel.ProductId),
                        new SqlParameter("@TotalPrice", orderModel.TotalPrice),
                        new SqlParameter("@Quantity",orderModel.Quantity),
                        new SqlParameter("@SupplierId", SupplierId)
                        };


                        int a = _DBContext.Database.SqlQuery<int>("exec AddOrder @Price,@ProductId,@TotalPrice,@SupplierId,@Quantity", sqlParameters).FirstOrDefault();
                        if (a == 0)
                        {
                            break;
                        }
                    }

                    TempData["Success"] = "Order Added SuccessFully";
                    return Json(new { success = true, redirectTo = Url.Action("CartList", "User") });
                }
                else
                {
                    return Json(new { success = false, error = "No order data received." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult IncreaseQuantity(IncreaseQuantityModel _IncreaseQuantityModel)
        {
            ViewBag.Email = SessionHelper.SessionHelper.EmailID;
            SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@Qunatity",_IncreaseQuantityModel.actualQuantity),
                    new SqlParameter("@Product",_IncreaseQuantityModel.itemId)

                };

            int a = _DBContext.Database.SqlQuery<int>("exec IncreaseStock @Qunatity,@Product", sqlParameters).FirstOrDefault();
            return Json(new { a = a }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveItemFromCart(int id)
        {
            try
            {
                var data = _DBContext.cart.FirstOrDefault(x => x.ProductId == id);
                _DBContext.cart.Remove(data);
                _DBContext.SaveChanges();
                TempData["error"] = "Item Removed From cart!";
                return RedirectToAction("CartList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult UserOrders() {
            List<orders> _OrderTable = new List<orders>();
            int id = SessionHelper.SessionHelper.UserID;
            _OrderTable = _DBContext.orders.Where(x=>x.UserId == id).ToList();
            ViewBag.totalcount = _OrderTable.Count();
            return View(_OrderTable);
        }
    }
}