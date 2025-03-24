using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNewWpf.Model
{
    public class Book
    {
        public string StatusText
        {
            get
            {
                if (IsAvailable == true)
                {
                    return "Доступна";
                }
                else
                {
                    return "Взята";
                }
            }
        }
        public string AuthorText
        {
            get
            {
                if (AuthorId == Author.Id)
                {
                    return Author.LastName + " " + Author.FirstName[0] + "." + Author.Patronymic[0];
                }
                else
                {
                    return "Неизвестный автор";
                }
            }
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int YearPublished { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
    }
}
