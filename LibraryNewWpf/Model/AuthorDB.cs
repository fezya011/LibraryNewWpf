using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryNewWpf.Model
{
    public class AuthorDB
    {
        ConnectionDB connection;

        private AuthorDB(ConnectionDB db)
        {
            this.connection = db;
        }

        public bool Insert(Author author)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Authors` Values (0, @FirstName, @Patronymic, @LastName ,@Birthday );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("FirstName", author.FirstName));
                cmd.Parameters.Add(new MySqlParameter("Patronymic", author.Patronymic));
                cmd.Parameters.Add(new MySqlParameter("LastName", author.LastName));
                cmd.Parameters.Add(new MySqlParameter("Birthday", author.Birthday));

                try
                {

                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        author.Id = id;
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

        internal List<Author> SelectAll()
        {
            List<Author> authors = new List<Author>();
            if (connection == null)
                return authors;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `Id`, `FirstName`, `Patronymic`, `LastName` ,`Birthday`  from `Authors`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);

                        string firstName = string.Empty;
                        if (!dr.IsDBNull(1))
                            firstName = dr.GetString("FirstName");

                        string patronymic = string.Empty;
                        if (!dr.IsDBNull(2))
                            patronymic = dr.GetString("Patronymic");

                        string lastName = string.Empty;
                        if (!dr.IsDBNull(3))
                            lastName = dr.GetString("LastName");

                        DateOnly birthday = new DateOnly();
                        if (!dr.IsDBNull(4))
                            birthday = dr.GetDateOnly("Birthday");

                        authors.Add(new Author
                        {
                            Id = id,
                            FirstName = firstName,
                            LastName = lastName,
                            Patronymic = patronymic,
                            Birthday = birthday,
                        });

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return authors;
        }

        internal bool Update(Author edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Authors` set `FirstName`=@FirstName, `Patronymic`=@Patronymic, `LastName`=@LastName, `Birthday`=@Birthday where `Id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("FirstName", edit.FirstName));
                mc.Parameters.Add(new MySqlParameter("Patronymic", edit.Patronymic));
                mc.Parameters.Add(new MySqlParameter("LastName", edit.LastName));
                mc.Parameters.Add(new MySqlParameter("Birthday", edit.Birthday));
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


        internal bool Remove(Author remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Authors` where `Id` = {remove.Id}");
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

        static AuthorDB db;
        public static AuthorDB GetDb()
        {
            if (db == null)
                db = new AuthorDB(ConnectionDB.GetDbConnection());
            return db;
        }
    }
}

