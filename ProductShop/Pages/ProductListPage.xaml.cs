using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProductShop.Components;

namespace ProductShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PoductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            InitializeComponent();

            if (App.isAdmin == false)
            {
                AddButt.Visibility = Visibility.Hidden;
            }

            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<Product> productSortList = App.db.Product;
            if (SortCB.SelectedIndex == 1)
            {
                productSortList = productSortList.OrderBy(x => x.NewCost);
            }
            else if (SortCB.SelectedIndex == 2)
            {
                productSortList = productSortList.OrderByDescending(x => x.NewCost);
            }

            if (SearchTbx.Text != null)
            {
                productSortList = productSortList.Where(x => x.Title.ToLower().Contains(SearchTbx.Text.ToLower()));
            }

            ProductWP.Children.Clear();
            foreach (var product in productSortList)
            {
                ProductWP.Children.Add(new ProductUserControl(product));
            }
        }


        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent(new AddEditPage(new Product())));
        }
    }
}
