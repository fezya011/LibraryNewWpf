using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryNewWpf.Model
{
    public class BookDB
    {
        ConnectionDB connection;

        private BookDB(ConnectionDB db)
        {
            this.connection = db;
        }


        public bool Insert(Book book)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Books` Values (0, @Title, @AuthorId, @YearPublished ,@Genre ,@IsAvailable);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Title", book.Title));
                cmd.Parameters.Add(new MySqlParameter("AuthorId", book.AuthorId));
                cmd.Parameters.Add(new MySqlParameter("YearPublished", book.YearPublished));
                cmd.Parameters.Add(new MySqlParameter("Genre", book.Genre));
                cmd.Parameters.Add(new MySqlParameter("IsAvailable", book.IsAvailable));

                try
                {

                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        book.Id = id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }       

        internal List<Book> SelectAll()
        {
            List<Book> books = new List<Book>();
            if (connection == null)
                return books;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select b.Id, `Title`, `YearPublished`, `Genre` ,`IsAvailable`, `FirstName`, `Patronymic`, `LastName`, `AuthorId`, `Birthday`  from `Books` `b` JOIN Authors  WHERE AuthorId = Authors.Id ");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);

                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");

                        int yearPublished = 0;
                        if (!dr.IsDBNull(2))
                            yearPublished = dr.GetInt32("YearPublished");

                        string genre = string.Empty;
                        if (!dr.IsDBNull(3))
                            genre = dr.GetString("Genre");

                        bool isAvailable = true;
                        if (!dr.IsDBNull(4))
                            isAvailable = dr.GetBoolean("IsAvailable");

                        string firstName = string.Empty;
                        if (!dr.IsDBNull(5))
                            firstName = dr.GetString("FirstName");

                        string patronymic = string.Empty;
                        if (!dr.IsDBNull(6))
                            patronymic = dr.GetString("Patronymic");

                        string lastName = string.Empty;
                        if (!dr.IsDBNull(7))
                            lastName = dr.GetString("LastName");

                        int authorId = 0;
                        if (!dr.IsDBNull(8))
                            authorId = dr.GetInt32("AuthorId");

                        DateOnly birthday = new DateOnly();
                        if (!dr.IsDBNull(9))
                            birthday = dr.GetDateOnly("Birthday");

                        Author author = new Author();
                        author.Id = authorId;
                        author.FirstName = firstName;
                        author.Patronymic = patronymic;
                        author.LastName = lastName;

                        books.Add(new Book
                        {
                            Id = id,
                            Title = title,
                            YearPublished = yearPublished,
                            Genre = genre,
                            IsAvailable = isAvailable,                        
                            AuthorId = authorId,
                            Author = author
                        });

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return books;
        }

        internal bool Update(Book edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Books` set `Title`=@Title, `AuthorId`=@AuthorId, `YearPublished`=@YearPublished, `Genre`=@Genre, `IsAvailable`=@IsAvailable where `Id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("Title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("AuthorId", edit.AuthorId));
                mc.Parameters.Add(new MySqlParameter("YearPublished", edit.YearPublished));
                mc.Parameters.Add(new MySqlParameter("Genre", edit.Genre));
                mc.Parameters.Add(new MySqlParameter("IsAvailable", edit.IsAvailable));
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }


        internal bool Remove(Book remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Books` where `Id` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        static BookDB db;
        public static BookDB GetDb()
        {
            if (db == null)
                db = new BookDB(ConnectionDB.GetDbConnection());
            return db;
        }
    }
}

