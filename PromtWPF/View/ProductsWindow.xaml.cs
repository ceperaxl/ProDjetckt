using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PromtWPF.ModelDB; // Указываем правильное пространство имен для контекста и классов сущностей!

namespace PromtWPF.View
{
    public partial class ProductsWindow : Window
    {
        private ObservableCollection<Products> _products;
        private int _userRoleId;

        public ProductsWindow(int userRoleId)
        {
            InitializeComponent();
            _userRoleId = userRoleId;
            LoadProducts();

            // Disable buttons for non-Admin users
            if (_userRoleId != 1) // Assuming RoleId 1 is Admin
            {
                AddButton.IsEnabled = true;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void LoadProducts()
        {
            using (var db = new PromtEntities1()) // <--- Используем сгенерированный контекст
            {
                // Загрузка данных из базы данных
                _products = new ObservableCollection<Products>(db.Products.ToList());
                ProductsDataGrid.ItemsSource = _products;

                // Для обновления DataGrid в реальном времени (необязательно)
                CollectionViewSource itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
                itemCollectionViewSource.Source = _products;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow(null); // Null for new product
            if (editProductWindow.ShowDialog() == true)
            {
                LoadProducts(); // Refresh the DataGrid
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Products selectedProduct = ProductsDataGrid.SelectedItem as Products;
            if (selectedProduct != null)
            {
                EditProductWindow editProductWindow = new EditProductWindow(selectedProduct);
                if (editProductWindow.ShowDialog() == true)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Products selectedProduct = ProductsDataGrid.SelectedItem as Products;
            if (selectedProduct != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using (var db = new PromtEntities1())  // <--- Создаем контекст внутри using
                    {
                        //Надо найти продукт в БД, а потом его удалить
                        // Вариант 1: Используем Find (если Id - первичный ключ)
                        Products productToDelete = db.Products.Find(selectedProduct.Id);

                        // Вариант 2: Используем FirstOrDefault (если Id - не первичный ключ,
                        // или Find не работает)
                        //Product productToDelete = db.Products.FirstOrDefault(p => p.Id == selectedProduct.Id);

                        if (productToDelete != null)
                        {
                            db.Products.Remove(productToDelete);
                            db.SaveChanges();
                            LoadProducts();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти продукт для удаления.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.");
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}