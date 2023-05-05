using MySql.Data.MySqlClient;
using OnLineShop.IServices;
using OnLineShop.Models;
using System.Data;

namespace OnLineShop.Services
{
    public class SpecialTag_implement :ISpecialTag
    {
        private readonly string str;

        public SpecialTag_implement()
        {
            str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        }

        public async Task AddSpecialTag(SpecialTag_model stag)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into specialtag (specialtag) values (@specialtag)";
                    cmd.Parameters.AddWithValue("@specialtag",stag.Spectag );
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();

                }

            }
        }

        public async Task DeleteSpecialTag(SpecialTag_model modl)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                conn.Open();
                string query = "delete from specialtag where tag_id=@tag_id";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tag_id", modl.Id);
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

        public List<SpecialTag_model> SpecialTagList()
        {

            List<SpecialTag_model> lst = new List<SpecialTag_model>();
            MySqlConnection con = new MySqlConnection(str);
            MySqlDataReader dr;
            DataTable tbl = new DataTable();
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from specialtag";
            dr = cmd.ExecuteReader();
            tbl.Load(dr);
            con.Close();
            foreach (DataRow dar in tbl.Rows)
            {
                lst.Add(new SpecialTag_model
                {
                    Id = (int)dar["tag_id"],
                    Spectag = dar["specialtag"].ToString()
                    //Email = !Convert.IsDBNull(dar["email"]) ? (int?)dar["email"] :

                    //Join_date = ((DateTime)dar["join_date"]),

                });

            }

            return lst;
        }

        public SpecialTag_model Get_specialTag(int? id)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                SpecialTag_model obj = new SpecialTag_model();
                DataTable tbl = new DataTable();
                MySqlDataReader dr;
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from specialtag where tag_id=@tag_id";
                    cmd.Parameters.AddWithValue("@tag_id", id);
                    dr = cmd.ExecuteReader();
                    tbl.Load(dr);
                    foreach (DataRow row in tbl.Rows)
                    {
                        obj = new SpecialTag_model
                        {
                            Id = (int)row["tag_id"],
                            Spectag = row["specialtag"].ToString()
                        };


                    }

                }
                return obj;
            }
        }

        public async Task UpdateSpecialTag(SpecialTag_model modl)
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
                        cmd.CommandText = "update specialtag set specialtag=@specialtag where tag_id=@tag_id ";
                        cmd.Parameters.AddWithValue("@tag_id", modl.Id);
                        cmd.Parameters.AddWithValue("@specialtag", modl.Spectag);
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
