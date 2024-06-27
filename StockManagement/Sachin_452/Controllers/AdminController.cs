using Newtonsoft.Json;
using Rotativa;
using Sachin_452.CustomFilter;
using Sachin_452.WebHelper;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using Sachin_452_Repository.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{

    [CustomAdminAuthorize]
    public class AdminController : Controller
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        IAdminInterface _adminInterface = new AdminService();
        // GET: Admin
        public ActionResult AdminHomepage()
        {
            return View();
        }

        public async Task<ActionResult> ProductList()
        {
            try
            {
                //string url = "api/AdminAPI/ProductList";
                //string response = await WebHelpers.HttpRequestResponce(url);
                List<Products> productList = _DBContext.Products.ToList();

                return View(productList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        public ActionResult AddProduct()
        {
            return View();
        }        
        
        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductModel productModel)
        {
            try
            {
                string url = "api/AdminAPI/AddProduct";
                string content = JsonConvert.SerializeObject(productModel);
                string response = await WebHelpers.HttpRequestResponce(url, content);
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(response);

                if(product != null)
                {
                    TempData["success"] = "Product Successfully added";
                    return RedirectToAction("ProductList");
                }
                else
                {
                    TempData["error"] = "something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> EditProduct(int id)
        {
            try
            {
                string url = $"api/ProductAPI/EditProduct?ProductId={id}" ;
                string response = await WebHelpers.HttpRequestResponce(url);
                // ProductModel products = _adminInterface.getProductDetails(id);
                ProductModel products = JsonConvert.DeserializeObject<ProductModel>(response);
                return View(products);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        public ActionResult EditProduct(ProductModel productModel)
        {
            try
            {
                var updateProduct = _adminInterface.updateProduct(productModel);

                if(updateProduct != null)
                {
                    TempData["success"] = "Product Updated";
                    return RedirectToAction("ProductList");

                }
                return View(productModel);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult SuppilerList()
        {
            try
            {
                List<Users> users = _DBContext.Users.Where(x=>x.Role =="Users").ToList();
                return View(users);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var data = _DBContext.Products.FirstOrDefault(x => x.ProductId == id);
                _DBContext.Products.Remove(data);
                _DBContext.SaveChanges();

                return RedirectToAction("ProductList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult OrdersRecordList()
        {
            try
            {
                ViewBag.Email = SessionHelper.SessionHelper.EmailID;
                List<orders> _OrderTable = new List<orders>();
                _OrderTable = _DBContext.orders.ToList();
                return View(_OrderTable);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult OrdersRecordList(FilterDataOrderModel _FilterDataOrderModel)
        {
            try
            {
                ViewBag.Email = SessionHelper.SessionHelper.EmailID;
                List<orders> _OrderTable = new List<orders>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@startdate",_FilterDataOrderModel.StartDateFormatted),
                        new SqlParameter("@endDate",_FilterDataOrderModel.EndDateFormatted)
                    };
                ViewBag.startDate = _FilterDataOrderModel.StartDateFormatted;
                ViewBag.endDate = _FilterDataOrderModel.EndDateFormatted;
                _OrderTable = _DBContext.Database.SqlQuery<orders>("exec SearchOrders @startdate,@endDate", sqlParameters).ToList();
                ViewBag.totalcount = _OrderTable.Count();
                return View(_OrderTable);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        
        [AllowAnonymous]
        public ActionResult OrdersRecordListPdf(DateTime startDate, DateTime endDate)
        {
            List<orders> orderRecords = _DBContext.orders
              .Where(o => o.orderDate >= startDate && o.orderDate <= endDate)
              .ToList();

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.totalcount = orderRecords.Count;

            return new Rotativa.ViewAsPdf("OrdersRecordListPdf", orderRecords)
            {
                FileName = "OrdersRecordListPdf.pdf",
                PageSize = Rotativa.Options.Size.A4
            };
        }

    }
}