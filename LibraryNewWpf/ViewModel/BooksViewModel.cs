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
        public ICommand DeleteButton { get; set; }       

        public BooksViewModel()
        {
            AddButton = new CommandVM(() =>
            {
                EditAddWindow editAddWindow = new EditAddWindow();
                editAddWindow.Show();
            }, () => true);

            EditButton = new CommandVM(() =>
            {
                EditAddWindow editAddWindow = new EditAddWindow();
                editAddWindow.Show();
            }, () => true);

            DeleteButton = new CommandVM(() =>
            {
                BookDB.GetDb().Remove(SelectedBook);
                SelectAll();
            }, () => SelectedBook != null);


        }

        private void SelectAll()
        {
           Books = new List<Book>(BookDB.GetDb().SelectAll());
        }
    }
}
