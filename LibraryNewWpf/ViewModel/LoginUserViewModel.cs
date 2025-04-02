using LibraryNewWpf.Model;
using LibraryNewWpf.View;
using LibraryNewWpf.VMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryNewWpf.ViewModel
{
    public class LoginUserViewModel : BaseVM
    {
        private User user;
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
                if (Users.Contains())


            }, () => string.IsNullOrWhiteSpace(User.Username) || string.IsNullOrWhiteSpace(User.Password));
        }

        private void SelectAll()
        {
            Users = new List<User>(UserDB.GetDb().SelectAll());
        }
    }
}
