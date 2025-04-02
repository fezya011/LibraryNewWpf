using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryNewWpf.Model
{
    public class UserDB
    {
        ConnectionDB connection;

        private UserDB(ConnectionDB db)
        {
            this.connection = db;
        }

        public bool Insert(User user)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Users` Values (0, @Username, @Password, @FirstName, @LastName, @Surname);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Username", user.Username));
                cmd.Parameters.Add(new MySqlParameter("Password", user.Password));
                cmd.Parameters.Add(new MySqlParameter("FisrtName", user.FirstName));
                cmd.Parameters.Add(new MySqlParameter("LastName", user.LastName));
                cmd.Parameters.Add(new MySqlParameter("Surname", user.Surname));
                try
                {

                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        user.Id = id;
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

        internal List<User> SelectAll()
        {
            List<User> users = new List<User>();
            if (connection == null)
                return users;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `Id`, `Username`, `Password`, `FirstName`, `LastName`, `Surname` ` from `Users`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);

                        string userName = string.Empty;
                        if (!dr.IsDBNull(1))
                            userName = dr.GetString("Username");

                        string password = string.Empty;
                        if (!dr.IsDBNull(2))
                            password = dr.GetString("Password");

                        string firstName = string.Empty;
                        if (!dr.IsDBNull(1))
                            firstName = dr.GetString("FirstName");

                        string lastName = string.Empty;
                        if (!dr.IsDBNull(1))
                            lastName = dr.GetString("LastName");

                        string surname = string.Empty;
                        if (!dr.IsDBNull(1))
                            surname = dr.GetString("Surname");

                        users.Add(new User
                        {
                            Id = id,
                            Username = userName,
                            Password = password,
                            FirstName = firstName,
                            LastName = lastName,
                            Surname = surname,
                        });

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return users;
        }

        internal bool Update(User edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Users` set `Username`=@Username, `Password`=@Password, `FirstName`=@FirstName, `LastName`=@LastName, `Surname`=@Surname,`Id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("Username", edit.Username));
                mc.Parameters.Add(new MySqlParameter("Password", edit.Password));
                mc.Parameters.Add(new MySqlParameter("LastName", edit.LastName));
                mc.Parameters.Add(new MySqlParameter("FirstName", edit.FirstName));
                mc.Parameters.Add(new MySqlParameter("Surname", edit.Surname));

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


        internal bool Remove(User remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Users` where `Id` = {remove.Id}");
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

        static UserDB db;
        public static UserDB GetDb()
        {
            if (db == null)
                db = new UserDB(ConnectionDB.GetDbConnection());
            return db;
        }
    }
}
        
