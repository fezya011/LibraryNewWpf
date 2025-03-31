using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNewWpf.Model
{
    public class SearchTable
    {
        private readonly ConnectionDB myConnection;

        public List<Book> SearchBook(string search)
        {
            List<Book> result = new();
            List<Author> authors = new();

            string query = $"SELECT Books.ID AS 'bookid', Title, YearPublished, Genre, IsAvailable, AuthorID, a.LastName, a.FirstName, a.Patronymic, a.Birthday FROM Books \r\nJOIN Authors a ON Books.AuthorID = a.ID\r\nWHERE Title LIKE @search OR a.LastName LIKE @search";

            if (myConnection.OpenConnection())
            {
                using (var mc = myConnection.CreateCommand(query))
                {
                    
                    mc.Parameters.Add(new MySqlParameter("search", $"%{search}%"));
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            
                            var book = new Book();
                            book.Id = dr.GetInt32("bookid");
                            book.YearPublished = dr.GetInt32("YearPublished");
                            book.Title = dr.GetString("Title");
                            book.AuthorId = dr.GetInt32("AuthorID");
                            book.Genre = dr.GetString("Genre");
                            book.IsAvailable = dr.GetBoolean("IsAvailable");

                            
                            var author = authors.FirstOrDefault(s => s.Id == book.AuthorId);
                            if (author == null)
                            {
                                
                                author = new Author();
                                author.Id = book.AuthorId;
                                author.FirstName = dr.GetString("FirstName");
                                author.LastName = dr.GetString("LastName");
                                author.Patronymic = dr.GetString("Patronymic");
                                author.Birthday = dr.GetDateTime("Birthday");
                               
                                authors.Add(author);
                            }
                           
                            author.Books.Add(book);
                            
                            book.Author = author;

                            
                            result.Add(book);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

       
        static SearchTable table;
        private SearchTable(ConnectionDB myConnection)
        {
            this.myConnection = myConnection;
        }
        public static SearchTable GetTable()
        {
            if (table == null)
                table = new SearchTable(ConnectionDB.GetDbConnection());
            return table;
        }
    }
}
