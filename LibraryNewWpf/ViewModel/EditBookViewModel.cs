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
        private string title;
        private Author author;
        private int yearPublished;
        private string genre;
        private bool isAvailable;
        private bool isTaken;
        private List<Author> authors;
        private List<string> genres;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Signal();
            }
        }

        public Author Author
        {
            get => author;
            set
            {
                author = value;
                Signal();
            }
        }
        public int YearPublished
        {
            get => yearPublished;
            set
            {
                yearPublished = value;
                Signal();
            }
        }
        public string Genre
        {
            get => genre;
            set
            {
                genre = value;
                Signal();
            }
        }
        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
                Signal();
            }
        }
        public bool IsTaken
        {
            get => isTaken;
            set
            {
                isTaken = value;
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

        public List<string> Genres 
        { 
            get => genres; 
            set
            {
                genres = value;
                Signal();
            }
        }

        public ICommand Save { get; set; }

        public EditBookViewModel(EditBookWindow window, Book selectedBook)
        {
            Genres = new List<string>() {"Роман", "Фантастика", "Романтика", "Драма", "Детектив", "Комедия"};
            
            SelectAll();

            Save = new CommandVM(() =>
            {
                if (selectedBook.Id == 0)
                {
                    Book book = new Book
                    {
                        Title = Title,
                        Author = Author,
                        YearPublished = YearPublished,
                        Genre = Genre,
                        AuthorId = Author.Id,
                        IsAvailable = IsAvailable
                    };
                    BookDB.GetDb().Insert(book);
                }
                else
                {
                    selectedBook.Title = Title;
                    selectedBook.Author = Author;
                    selectedBook.YearPublished = YearPublished;
                    selectedBook.Genre = Genre;
                    selectedBook.AuthorId = Author.Id;
                    selectedBook.IsAvailable = IsAvailable;
                    BookDB.GetDb().Update(selectedBook);
                }

                window.Close();               
            }, () => string.IsNullOrWhiteSpace(Title) || Author != null || YearPublished != 0 || Genre != null || IsAvailable == true);
        }
        private void SelectAll()
        {
            Authors = new List<Author>(AuthorDB.GetDb().SelectAll());
        }



    }

}
