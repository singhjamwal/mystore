using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace mystore.Pages.mypages
{
    public class addnewModel : PageModel
    {
        public mydata data = new mydata();
        public string errormessage = "", successmessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            data.Name = Request.Form["name"];
            data.Mobile = Request.Form["mobile"];
            data.Email = Request.Form["email"];
            data.Address = Request.Form["address"];

            if(data.Name == "" || data.Mobile == "" || data.Email == "" || data.Address == "")
            {
                errormessage = "Please Enter All Fields";
                return;
            }
            try
            {
                string connectionstring = "Data Source=VIRENDER\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    string query = "insert into storetable " + "(name,mobile,email,address,createdat) values " + "(@name,@mobile,@email,@address,@createdat);";
                    using(SqlCommand cmd = new SqlCommand(query,conn)) {
                        cmd.Parameters.AddWithValue("@name", data.Name);
                        cmd.Parameters.AddWithValue("@mobile", data.Mobile);
                        cmd.Parameters.AddWithValue("@email", data.Email);
                        cmd.Parameters.AddWithValue("@address", data.Address);
                        cmd.Parameters.AddWithValue("@createdat", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                } ;
            }
            catch (Exception ex)
            {
                errormessage= ex.Message;
                return;
            }

            data.Name = ""; data.Mobile = ""; data.Email = ""; data.Address = "";
            successmessage = "Data Submitted Successfully";

        }
    }
}
