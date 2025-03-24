﻿using LibraryNewWpf.Model;
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
    /// Логика взаимодействия для AuthorEditWindow.xaml
    /// </summary>
    public partial class AuthorEditWindow : Window
    {
        public AuthorEditWindow(Author selectedAuthor)
        {
            InitializeComponent();
            DataContext = new AuthorEditViewModel(selectedAuthor);
        }
    }
}
