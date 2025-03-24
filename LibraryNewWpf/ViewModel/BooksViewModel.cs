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
    public class BooksViewModel : BaseVM
    {
        private Book selectedBook;
        private List<Book> books;

        public Book SelectedBook 
        { 
            get => selectedBook;
            set
            {
                selectedBook = value;
                Signal();
            }
        }

        public List<Book> Books 
        { 
            get => books; 
            set
            {
                books = value;
                Signal();
            }
        }

        public ICommand AddButton { get; set; }
        public ICommand EditButton { get; set; }
        public CommandVM DeleteButton { get; set; }
        public ICommand AddAuthor { get; set; }
        
        ConnectionDB db;

        public BooksViewModel()
        {
            SelectAll();

            AddButton = new CommandVM(() =>
            {
                EditBookWindow editAddWindow = new EditBookWindow(SelectedBook);
                editAddWindow.ShowDialog();
                SelectAll();
            }, () => true);

            EditButton = new CommandVM(() =>
            {
                EditBookWindow editAddWindow = new EditBookWindow(SelectedBook);
                editAddWindow.ShowDialog();
                SelectAll();
            }, () => SelectedBook != null);

            DeleteButton = new CommandVM(() =>
            {
                BookDB.GetDb().Remove(SelectedBook);
                SelectAll();
            }, () => SelectedBook != null);

            AddAuthor = new CommandVM(() =>
            {
                AuthorWindow authorWindow = new AuthorWindow();
                authorWindow.ShowDialog();
                SelectAll();
            }, () => true);
        }
        
        private void SelectAll()
        {
           Books = new List<Book>(BookDB.GetDb().SelectAll());
        }
    }
}
