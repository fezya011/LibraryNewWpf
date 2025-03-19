using LibraryNewWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryNewWpf.View
{
    /// <summary>
    /// Логика взаимодействия для EditAddWindow.xaml
    /// </summary>
    public partial class EditAddWindow : Window
    {
        public EditAddWindow()
        {
            InitializeComponent();
            DataContext = new EditAddViewModel();
        }
    }
}
