using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNewWpf.Model
{
    public class Author
    {
        public string ComboText
        {
            get 
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public List<Book> Books { get; set; } = new();
    }

}
