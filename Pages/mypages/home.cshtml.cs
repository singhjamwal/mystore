using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mystore.Pages.mypages
{
    public class homeModel : PageModel
    {
        public List<mydata> storelist = new List<mydata>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=VIRENDER\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string sqlquery = "select *, Replace(convert(varchar,createdat,6),' ','-') as createat from storetable";
                    int i = 0;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sqlquery;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mydata mydata = new mydata();
                                mydata.Id = reader.GetInt32(reader.GetOrdinal("id"));
                                mydata.Name = reader.GetString(reader.GetOrdinal("name"));
                                mydata.Address = reader.GetString(reader.GetOrdinal("address"));
                                mydata.Mobile = reader.GetString(reader.GetOrdinal("mobile"));
                                mydata.Email = reader.GetString(reader.GetOrdinal("email"));
                                mydata.Createdate = reader.GetString(reader.GetOrdinal("createat"));
                                storelist.Add(mydata);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    public class mydata
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Createdate { get; set; }

    }
}
