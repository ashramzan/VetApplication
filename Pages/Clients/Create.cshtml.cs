using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        { 
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            clientInfo.animal = Request.Form["animal"];
            clientInfo.animal_name = Request.Form["animal_name"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0 || clientInfo.animal.Length == 0 || clientInfo.animal_name.Length == 0)
            {
                errorMessage = "Please fill out all the fields";
                return;
            }

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Users\\ashal\\source\\repos\\store\\DB\\Store.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                                 "(name, email, phone, address, animal, animal_name) VALUES " +
                                 "(@name, @email, @phone, @address, @animal, @animal_name);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);
                        command.Parameters.AddWithValue("email", clientInfo.email);
                        command.Parameters.AddWithValue("phone", clientInfo.phone);
                        command.Parameters.AddWithValue("address", clientInfo.address);
                        command.Parameters.AddWithValue("animal", clientInfo.animal);
                        command.Parameters.AddWithValue("animal_name", clientInfo.animal_name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.phone = ""; clientInfo.email = ""; clientInfo.address = ""; clientInfo.animal = ""; clientInfo.animal_name = "";
            successMessage = "New Client added successfully.";

            Response.Redirect("/Clients/Index");
        }
    }
}
