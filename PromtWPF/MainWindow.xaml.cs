using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PromtWPF.View;

namespace PromtWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); 
        }
    }
}