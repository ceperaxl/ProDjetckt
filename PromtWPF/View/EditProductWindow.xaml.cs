using System;
using System.Windows;
using PromtWPF.ModelDB;
namespace PromtWPF.View
{
    public partial class EditProductWindow : Window
    {
        private Products _product; 

        public EditProductWindow(Products product)
        {
            InitializeComponent();
            _product = product;

            if (_product != null)
            {
                NameTextBox.Text = _product.Name;
                DescriptionTextBox.Text = _product.Description;
                PriceTextBox.Text = _product.Price.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                decimal price = decimal.Parse(PriceTextBox.Text);

                using (var db = new PromtEntities1())
                {
                    if (_product == null)
                    {
                        // Создание нового товара
                        _product = new Products();
                        db.Products.Add(_product);
                    }
                    else
                    {
                        // Если продукт редактируется, нужно сначала получить его из контекста,
                        // чтобы Entity Framework отслеживал изменения.
                        _product = db.Products.Find(_product.Id);
                    }

                    if (_product != null)
                    {
                        _product.Name = NameTextBox.Text;
                        _product.Description = DescriptionTextBox.Text;
                        _product.Price = price;

                        db.SaveChanges();
                    }
                }

                DialogResult = true; // Indicate success
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат цены или ошибка: " + ex.Message);
            }
        }
    }
}