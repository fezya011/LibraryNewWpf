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
    public class AuthorViewModel : BaseVM
    {
        private List<Author> authors;
        private Author selectedAuthor;

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

        public ICommand AddButton { get; set; }
        public ICommand EditButton { get; set; }
        public ICommand DeleteButton { get; set; }


        public AuthorViewModel()
        {
            SelectAll();

            AddButton = new CommandVM(() =>
            {
                AuthorEditWindow editAddWindow = new AuthorEditWindow(SelectedAuthor);
                editAddWindow.ShowDialog();
                SelectAll();
            }, () => true);

            EditButton = new CommandVM(() =>
            {
                AuthorEditWindow editAddWindow = new AuthorEditWindow(SelectedAuthor);
                editAddWindow.ShowDialog();
                SelectAll();
            }, () => SelectedAuthor != null);

            DeleteButton = new CommandVM(() =>
            {
                AuthorDB.GetDb().Remove(SelectedAuthor);
                SelectAll();
            }, () => SelectedAuthor != null);
        }

        private void SelectAll()
        {
            Authors = new List<Author>(AuthorDB.GetDb().SelectAll());
        }
    }
}
