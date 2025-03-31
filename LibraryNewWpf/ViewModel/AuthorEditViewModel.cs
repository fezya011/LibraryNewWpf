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

        public ICommand Save { get; set; }

        public AuthorEditViewModel(AuthorEditWindow window ,Author editAuthor)
        {
            
            SelectAll();
            Author = editAuthor;
            Save = new CommandVM(() =>
            {
                
                if (Author.Id == 0)
                {
                    Author.Birthday = DateTime.Now;
                    AuthorDB.GetDb().Insert(Author);
                }
                else
                {                  
                    AuthorDB.GetDb().Update(Author);
                }
                window.Close();
            }, () => true);
        }
        

        private void SelectAll()
        {
            Authors = new List<Author>(AuthorDB.GetDb().SelectAll());
        }

        
    }
}
