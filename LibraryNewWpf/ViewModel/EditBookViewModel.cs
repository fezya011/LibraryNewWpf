using LibraryNewWpf.Model;
using LibraryNewWpf.View;
using LibraryNewWpf.VMTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryNewWpf.ViewModel
{
    public class EditBookViewModel : BaseVM
    {
        
        private Author author;
        private List<Author> authors;
        private List<string> genres;
        private Book book;

       


        public List<Author> Authors
        {
            get => authors;
            set
            {
                authors = value;
                Signal();
            }
        }

        public List<string> Genres 
        { 
            get => genres; 
            set
            {
                genres = value;
                Signal();
            }
        }
        public Book Book 
        {
            get => book;
            set
            {
                book = value;
                Signal();
            } 
        }



        public ICommand Save { get; set; }

        public EditBookViewModel(EditBookWindow window, Book editBook)
        {
            Book = editBook;    
            Genres = new List<string>() {"Роман", "Фантастика", "Романтика", "Драма", "Детектив", "Комедия"};
            
            SelectAll();

            Save = new CommandVM(() =>
            {
                Book.AuthorId = Book.Author.Id;
                if (Book.Id == 0)
                {
                    BookDB.GetDb().Insert(Book);
                }
                else
                {                    
                    BookDB.GetDb().Update(Book);
                }

                window.Close();               
            }, () => string.IsNullOrWhiteSpace(Book.Title) || Book.Author != null || Book.YearPublished != 0 || Book.Genre != null || Book.IsAvailable == true);
        }

        private void SelectAll()
        {
            Authors = new List<Author>(AuthorDB.GetDb().SelectAll());
            if (Book.Author != null)
            {
                Book.Author = Authors.FirstOrDefault(s => s.Id == Book.Author.Id);
            }
        }
    }

}
