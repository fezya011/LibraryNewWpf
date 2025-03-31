using LibraryNewWpf.Model;
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

        public LoginUserViewModel()
        {
            
        }

        private void SelectAll()
        {
            Users = new List<User>(UserDB.GetDb().SelectAll());
        }
    }
}
