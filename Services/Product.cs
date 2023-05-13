using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using OnLineShop.IServices;
using OnLineShop.Models;
using System.Data;

namespace OnLineShop.Services
{
    public class Product : IProduct
    { private readonly string str;
        public Product()
        {
            str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";

        }
        public async Task AddProducts (Products product)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    //conn.Open();
                    //string query1 = "insert into producttype (producttype)values(@producttype)";
                    //MySqlCommand cmd1 = new MySqlCommand(query1, conn);                              
                    //cmd1.Parameters.AddWithValue("@producttype", product.ProductType_prop.Producttype);
                    //cmd1.ExecuteNonQuery();
                    //long prdtypeid = cmd1.LastInsertedId;
                    //cmd1.Dispose();
                    //conn.Close();

                    //try select for product type
                   
                    //    Product_Type objptype = new Product_Type();
                    //    conn.Open();
                    //    string query1 = "select * from producttype where Id=@id";
                    //    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                    //    cmd1.Parameters.AddWithValue("@id", product.ProductTypeId);
                    //    MySqlDataReader dr = cmd1.ExecuteReader();
                    //    DataTable tbl = new DataTable();
                    //    tbl.Load(dr);

                    //    foreach (DataRow rows in tbl.Rows)
                    //    {
                    //        objptype = new Product_Type()
                    //        {
                    //            Producttype = rows["producttype"].ToString()
                    //        };

                    //    }
                    //    cmd1.Dispose();
                    //    conn.Close();

                    
                   

                    ////try select for special tag

                    //SpecialTag_model objstag = new SpecialTag_model();
                    //conn.Open();
                    //string qery2 = "select * from specialtag where tag_id=@tag_id";
                    //MySqlCommand cmd2 = new MySqlCommand(qery2, conn);
                    //cmd2.Parameters.AddWithValue("@tag_id", product.SpecialTagId);
                    //MySqlDataReader dr2 = cmd2.ExecuteReader();
                    //DataTable tbl2 = new DataTable();
                    //tbl2.Load(dr2);

                    //foreach (DataRow rows in tbl2.Rows)
                    //{
                    //    objstag = new SpecialTag_model()
                    //    {
                    //        Spectag = rows["specialtag"].ToString()
                    //    };

                    //}
                    //cmd2.Dispose();
                    //conn.Close();
                    ////conn.Open();
                    //string query2 = "insert into specialtag (specialtag)values(@specialtag)";
                    //MySqlCommand cmd2 = new MySqlCommand(query2, conn);                    
                    //cmd2.ExecuteNonQuery();
                    //long specialtagid = cmd2.LastInsertedId;
                    //cmd2.Dispose();
                    //conn.Close();

                    conn.Open();
                    string query3  = "insert into product (product_name,product_price,product_image,product_color,isavailable,producttypeid,specialtagid)" +
                                   "values(@product_name,@product_price,@product_image," +
                                  "@product_color,@isavailable,@ProductTypeId,@SpecialTagId)";

                    MySqlCommand cmd = new MySqlCommand(query3, conn);                                   
                    cmd.Parameters.AddWithValue("@product_name", product.Name);
                    cmd.Parameters.AddWithValue("@product_price", product.Price);
                    cmd.Parameters.AddWithValue("@product_image", product.Image);
                    cmd.Parameters.AddWithValue("@product_color", product.ProductColor);
                    cmd.Parameters.AddWithValue("@isavailable", product.IsAvailable);
                    cmd.Parameters.AddWithValue("@ProductTypeId", product.ProductTypeId);
                    cmd.Parameters.AddWithValue("@SpecialTagId", product.SpecialTagId);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.Dispose();
                    conn.Close();
                    


                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    conn.Open();
                    string deletequery = "DELETE p FROM product p " +
               "INNER JOIN producttype pt ON p.producttypeid = pt.Id " +
               "INNER JOIN specialtag st ON p.specialtagid = st.tag_id " +
               "WHERE p.product_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(deletequery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                      int rowsaffected=  cmd.ExecuteNonQuery();
                        conn.Close();
                        if (rowsaffected > 0)
                            return true;
                        else
                            return false;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Products GetProductById(int id)
        {
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                conn.Open();
                DataTable tbl = new DataTable();
                Products obj = new Products();
                string query = "select p.* ,pt.producttype, st.specialtag from product p " +
                    "inner join producttype pt on p.producttypeid=pt.Id " +
                    "inner join specialtag st on p.specialtagid=st.tag_id "+
                    "where p.product_id=@product_id ";
                using(MySqlCommand cmd=new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@product_id", id);
                    using(MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        tbl.Load(dr);
                        foreach(DataRow rows in tbl.Rows)
                        {
                            obj = new Products()
                            {
                                Id = (int)rows["product_id"],
                                Name = (string)rows["product_name"],
                                Price = (decimal)rows["product_price"],
                                Image = rows["product_image"].ToString(),
                                ProductColor = rows["product_color"].ToString(),
                                IsAvailable = (bool)rows["isavailable"],
                                
                            };
                        }
                    }

 

                }
                return obj;
            }
        }
        //selectitemlisr use to populate dropdown list
        public List<SelectListItem> GetProductTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            string connectionString = str;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id, producttype FROM producttype";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string productType = reader["producttype"].ToString();
                        items.Add(new SelectListItem { Value = id.ToString(), Text = productType });
                    }
                }
            }

            return items;
        }
        public List<SelectListItem> GetProductTypesById(int ptyeid)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            string connectionString = str;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
               // string query = "SELECT Id, producttype FROM producttype where Id=@id";
                string query2 = "select p.* ,pt.Id ,pt.producttype,st.tag_id, st.specialtag  from product p " +
                 "inner join producttype pt on  p.producttypeid=pt.Id " +
                 "inner join specialtag st on p.specialtagid=st.tag_id " +
                 "where p.product_id=@id";
                MySqlCommand command = new MySqlCommand(query2, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", ptyeid);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string productType = reader["producttype"].ToString();
                        items.Add(new SelectListItem { Value = id.ToString(), Text = productType });
                    }
                }
            }

            return items;
        }
        public List<SelectListItem> Getspecialtag()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string connectionString = str;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT tag_id, specialtag FROM specialtag";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["tag_id"];
                        string specialtag = reader["specialtag"].ToString();
                        items.Add(new SelectListItem { Value = id.ToString(), Text = specialtag });
                    }
                }
            }

            return items;
        }
        public List<SelectListItem> GetspecialtagById(int stagid)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string connectionString = str;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
             //   string query = "SELECT tag_id, specialtag FROM specialtag where tag_id=@tag_id";
                string query2 = "select p.* ,pt.producttype,st.tag_id, st.specialtag  from product p " +
                    "inner join producttype pt on  p.producttypeid=pt.Id " +
                    "inner join specialtag st on p.specialtagid=st.tag_id " +
                    "where p.product_id=@id";
                MySqlCommand command = new MySqlCommand(query2, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", stagid);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["tag_id"];
                        string specialtag = reader["specialtag"].ToString();
                        items.Add(new SelectListItem { Value = id.ToString(), Text = specialtag });
                    }
                }
            }

            return items;
        }
        public List<Products> ProductList()
        {
            List<Products> lst = new List<Products>();
            using (MySqlConnection con = new MySqlConnection(str))
            {
                
                DataTable tbl = new DataTable();
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        "SELECT p.*, pt.producttype, st.specialtag\r\nFROM product p\r\nINNER JOIN producttype pt ON p.producttypeid = pt.Id\r\nINNER JOIN specialtag st ON p.specialtagid = st.tag_id;\r\n";

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        tbl.Load(dr);
                        con.Close();
                        foreach (DataRow dar in tbl.Rows)
                        {
                            Products obj = new Products()
                            {
                                Id = (int)dar["product_id"],
                                Name = dar["product_name"].ToString(),
                                Price = (decimal)dar["product_price"],
                                Image = dar["product_image"].ToString(),
                                ProductColor = dar["product_color"].ToString(),
                                IsAvailable = (bool)dar["isavailable"],

                            };
                             obj.ProductType_prop = new Product_Type()
                                {
                                    Id = (int)dar["producttypeid"],
                                    Producttype = dar["producttype"].ToString()

                                };
                            obj.SpecialTag_prop = new SpecialTag_model()
                            {
                                Id = (int)dar["specialtagid"],
                                Spectag = dar["specialtag"].ToString()
                            };
                            lst.Add(obj);
                            };
                        }
                        return lst;
                    }                 
                }
                 
            }

        public async Task<List<Products>> ProductPriceRange(decimal loweramount, decimal highamount)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                DataTable tbl = new DataTable();
                List<Products> lst= new List<Products>();
                conn.Open();
              
                string query = "select p.* ,pt.Id, pt.producttype,st.tag_id, st.specialtag from product p " +
                    "inner join producttype pt on p.producttypeid =pt.Id " +
                    "inner join specialtag st on p.specialtagid =st.tag_id " +
                    "where p.product_price>=@lowerprice AND p.product_price<=@higherprice";
                using(MySqlCommand cmd=new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lowerprice", loweramount);
                    cmd.Parameters.AddWithValue("@higherprice", highamount);
                    using(var dr=await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            Products obj = new Products()
                            {
                                Id = (int)dr["product_id"],
                                Name = dr["product_name"].ToString(),
                                Price = (decimal)dr["product_price"],
                                Image = dr["product_image"].ToString(),
                                ProductColor = dr["product_color"].ToString(),
                                IsAvailable = (bool)dr["isavailable"],

                            };
                            obj.ProductType_prop = new Product_Type
                            {
                                Id = (int)dr["Id"],
                                Producttype = dr["producttype"].ToString()


                            };
                            obj.SpecialTag_prop = new SpecialTag_model
                            {
                                Id = (int)dr["tag_id"],
                                Spectag = dr["specialtag"].ToString()
                            };

                            lst.Add(obj);
                        }

                    }
                    return lst; 
                }



            }

        }

        public async Task<bool> UpdateProduct(Products modl)
        {

            try
            {
                int productTypeId;
                int specialTagId;

                // Check if ProductTypeId is a valid integer


                // Check if SpecialTagId is a valid integer


                // Check if ProductTypeId corresponds to a valid primary key in ProductType table
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    conn.Open();
                    string productTypeQuery = "SELECT COUNT(*) FROM producttype WHERE Id = @productTypeId";
                    using (MySqlCommand cmd = new MySqlCommand(productTypeQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@productTypeId", modl.ProductTypeId);
                        int productTypeCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (productTypeCount == 0)
                        {
                            throw new ArgumentException("ProductTypeId is not a valid primary key in ProductType table.");
                        }
                    }
                }

                // Check if SpecialTagId corresponds to a valid primary key in SpecialTag table
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    conn.Open();
                    string specialTagQuery = "SELECT COUNT(*) FROM specialtag WHERE tag_id = @specialTagId";
                    using (MySqlCommand cmd = new MySqlCommand(specialTagQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@specialTagId", modl.SpecialTagId);
                        int specialTagCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (specialTagCount == 0)
                        {
                            throw new ArgumentException("SpecialTagId is not a valid primary key in SpecialTag table.");
                        }
                    }
                }

                // If input validation passes, perform update operation
                using (MySqlConnection conn = new MySqlConnection(str))
                {
                    conn.Open();

                    string updatequery = "update product p " +
                            "inner join producttype pt on p.producttypeid=pt.Id " +
                            "inner join specialtag st on p.specialtagid=st.tag_id " +
                            "set p.product_name=@product_name ,p.product_price=@product_price" +
                            ", p.product_image=@product_image, " +
                            "p.product_color=@product_color, " +
                            "isavailable=@isavailable, producttypeid=@producttypeid, " +
                            "specialtagid=@specialtagid " +
                                "where p.product_id=@product_id";

                    using (MySqlCommand cmd = new MySqlCommand(updatequery, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", modl.Id);
                        cmd.Parameters.AddWithValue("@product_name", modl.Name);
                        cmd.Parameters.AddWithValue("@product_price", modl.Price);
                        cmd.Parameters.AddWithValue("@product_image", modl.Image);
                        cmd.Parameters.AddWithValue("@product_color", modl.ProductColor);
                        cmd.Parameters.AddWithValue("@isavailable", modl.IsAvailable);
                        cmd.Parameters.AddWithValue("@producttypeid", modl.ProductTypeId);
                        cmd.Parameters.AddWithValue("@specialtagid", modl.SpecialTagId);
                        int res = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (res > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                /* try
                 {
                     using (MySqlConnection conn = new MySqlConnection(str))
                     {
                         conn.Open();
                         string updatequery = "update product p " +
                             "inner join producttype pt on p.producttypeid=pt.Id " +
                             "inner join specialtag st on p.specialtagid=st.tag_id " +
                             "set p.product_name=@product_name ,p.product_price=@product_price" +
                             ", p.product_image=@product_image, "+
                             "p.product_color=@product_color, " +
                             "isavailable=@isavailable, producttypeid=@producttypeid, " +
                             "specialtagid=@specialtagid " +
                               "where p.product_id=@product_id";


                         using (MySqlCommand cmd = new MySqlCommand(updatequery, conn))
                         {
                             cmd.Parameters.AddWithValue("@product_id", modl.Id);
                             cmd.Parameters.AddWithValue("@product_name", modl.Name);
                             cmd.Parameters.AddWithValue("@product_price", modl.Price);
                             cmd.Parameters.AddWithValue("@product_image", modl.Image);
                             cmd.Parameters.AddWithValue("@product_color", modl.ProductColor);
                             cmd.Parameters.AddWithValue("@isavailable", modl.IsAvailable);
                             cmd.Parameters.AddWithValue("@producttypeid", modl.ProductTypeId);
                             cmd.Parameters.AddWithValue("@specialtagid", modl.SpecialTagId);
                            int res= cmd.ExecuteNonQuery();
                             conn.Close();
                             if (res > 0)
                                 return true;
                             else
                                 return false;

                         }

                     }
                 }
                 catch (Exception)
                 {
                     throw;
                 }
                */
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
