using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using OnLineShop.IServices;
using OnLineShop.Models;
using System.Data;

namespace OnLineShop.Services
{
    public class ProductType : IProductTypes
    {
        private readonly string str;

        public ProductType()
        {
            str= "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        }

        public async Task AddProductType(Product_Type producttype)
        {
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                using(MySqlCommand cmd=new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into producttype (producttype) values (@producttype)";
                    cmd.Parameters.AddWithValue("@producttype", producttype.Producttype);
                   await  cmd.ExecuteNonQueryAsync();
                    conn.Close();
                   
                }

            }
        }

        public async Task DeleteProductType(Product_Type modl)
        {
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                conn.Open();
                string query = "delete from producttype where Id=@id";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", modl.Id);
                        await cmd.ExecuteNonQueryAsync();
                        conn.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Product_Type> producttypelist()
        {
            
                List<Product_Type> lst = new List<Product_Type>();
                MySqlConnection con = new MySqlConnection(str);
                MySqlDataReader dr;
                DataTable tbl = new DataTable();
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from producttype";
                dr = cmd.ExecuteReader();
                tbl.Load(dr);
                con.Close();
                foreach (DataRow dar in tbl.Rows)
                {
                    lst.Add(new Product_Type
                    {
                        Id = (int)dar["Id"],
                        Producttype = dar["producttype"].ToString()
                        //Email = !Convert.IsDBNull(dar["email"]) ? (int?)dar["email"] :

                        //Join_date = ((DateTime)dar["join_date"]),
       
                    });

                }

                return lst;
            }

        public Product_Type Product_TypeGetProductTypeById(int? id)
        {
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                Product_Type obj = new Product_Type();
                DataTable tbl = new DataTable();
                MySqlDataReader dr;
                using(MySqlCommand cmd=new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from producttype where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    dr = cmd.ExecuteReader();
                    tbl.Load(dr);
                    foreach(DataRow row in tbl.Rows)
                    {
                   obj=  new Product_Type{
                         Id =(int) row["Id"],
                         Producttype = row["producttype"].ToString()
                     };

                               
                    }

                }
                return obj;
            }
        }

        public async Task UpdateProductType(Product_Type modl)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update producttype set producttype=@producttype where Id=@id ";
                        cmd.Parameters.AddWithValue("@id", modl.Id);
                        cmd.Parameters.AddWithValue("@producttype", modl.Producttype);
                        await cmd.ExecuteNonQueryAsync();
                        conn.Close();

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
