using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BookList.Pages.Books
{
    public class CreateModel : PageModel
    {
        public BookInfo bookInfo = new BookInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            bookInfo.name = Request.Form["name"];
            bookInfo.genre = Request.Form["genre"];

            if (bookInfo.name.Length == 0 || bookInfo.genre.Length == 0)
            {
                errorMessage = "все пол€ об€зательны";
                return;
            }

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSqlLocalDB;Initial Catalog=mybook;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into books " +
                                "(name, genre) values " +
                                "(@name, @genre);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", bookInfo.name);
                        command.Parameters.AddWithValue("@genre", bookInfo.genre);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
            bookInfo.name = ""; bookInfo.genre = "";
            successMessage = "Ќова€ книга успешно добавлена";

            Response.Redirect("/Books/Index");
        }   
    }
}
