using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BookList.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> listBooks = new List<BookInfo>();
        
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSqlLocalDB;Initial Catalog=mybook;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from books"; /* заменить на clients если шо */
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookInfo bookInfo = new BookInfo();
                                bookInfo.id = "" + reader.GetInt32(0);
                                bookInfo.name = reader.GetString(1);
                                bookInfo.genre = reader.GetString(2);
                                bookInfo.created_ad = reader.GetDateTime(3).ToString();

                                listBooks.Add(bookInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class BookInfo
    {
        public String id;
        public String name;
        public String genre;
        public String created_ad;
    }
}
