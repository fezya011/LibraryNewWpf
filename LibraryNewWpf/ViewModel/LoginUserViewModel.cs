using LibraryNewWpf.Model;
using LibraryNewWpf.View;
using LibraryNewWpf.VMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryNewWpf.ViewModel
{
    public class LoginUserViewModel : BaseVM
    {
        private User user = new User();
        private List<User> users;

        public User User 
        {
            get => user;
            set
            {
                user = value;
                Signal();
            }
        }

        public List<User> Users 
        {
            get => users;
            set
            {
                users = value; 
                Signal();
            }
        }

        public ICommand Login { get; set; }
        public ICommand Register { get; set; }

        public LoginUserViewModel()
        {
            SelectAll();
            Register = new CommandVM(() =>
            {
                RegisterUserWindow registerUserWindow = new RegisterUserWindow();
                registerUserWindow.ShowDialog();
            }, () => true);

            Login = new CommandVM(() =>
            {
                User user = Users.FirstOrDefault(u => u.Username == User.Username && u.Password == User.Password);
                if (user != null)
                {
                    MainWindow booksWindow = new MainWindow(user);
                    booksWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Неправильный ввод");
                }
                


            },() => !string.IsNullOrWhiteSpace(User.Username) && !string.IsNullOrWhiteSpace(User.Password));
        }

        private void SelectAll()
        {
            Users = new List<User>(UserDB.GetDb().SelectAll());
        }
    }
}
