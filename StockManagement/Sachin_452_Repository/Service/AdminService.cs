using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Repository.Service
{
    public class AdminService : IAdminInterface
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();

        public ProductModel addProduct(ProductModel productModel)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@productName" , productModel.productName),
                    new SqlParameter("@ProductDescription" , productModel.ProductDescription),
                    new SqlParameter("@ProductType" , productModel.ProductType),
                    new SqlParameter("@Quantity" , productModel.Quantity),
                    new SqlParameter("@Price" , productModel.Price),
                };

                ProductModel products = _DBContext.Database.SqlQuery<ProductModel>("exec SP_AddProduct  @productName,@ProductDescription,@ProductType,@Quantity,@Price" , sqlParameters).FirstOrDefault();
                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ProductModel getProductDetails(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("id " , id)
                };
                ProductModel productModel = _DBContext.Database.SqlQuery<ProductModel>("exec getproductdetails @id", sqlParameters).FirstOrDefault();
                return productModel;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ProductModel> GetProductList()
        {
            try
            {
                List<ProductModel> productList = _DBContext.Database.SqlQuery<ProductModel>("exec SP_GetProductList").ToList();
                return productList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Products updateProduct(ProductModel productModel)
        {
            try
            {
                    Products _product = _DBContext.Products.Find(productModel.ProductId);
                        if (_product != null)
                        {
                            _DBContext.Products.Attach(_product);
                            _product.productName = productModel.productName;
                            _product.ProductDescription = productModel.ProductDescription;
                            _product.Quantity = productModel.Quantity;
                            _product.ProductType = productModel.ProductType;
                            _product.Price = productModel.Price;
                            _DBContext.Entry(_product).State = EntityState.Modified;
                            _DBContext.SaveChanges();
                        }
                        return _product;
    
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
