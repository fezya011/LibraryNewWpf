using LibraryNewWpf.Model;
using LibraryNewWpf.VMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNewWpf.ViewModel
{
    public class AuthorEditViewModel : BaseVM
    {
        private Author author;
        private List<Author> authors;
        private Author selectedAuthor;

        public Author Author 
        { 
            get => author; 
            set
            {
                author = value;
                Signal();
            }
        }

        public List<Author> Authors 
        { 
            get => authors; 
            set
            {
                authors = value;
                Signal();
            }
        }

        public Author SelectedAuthor 
        { 
            get => selectedAuthor; 
            set
            {
                selectedAuthor = value;
                Signal();
            }
        }

        public AuthorEditViewModel(Author selectedAuthor)
        {
            SelectAll();

        }

        private void SelectAll()
        {
            Authors = new List<Author>(AuthorDB.GetDb().SelectAll());
        }
    }
}
