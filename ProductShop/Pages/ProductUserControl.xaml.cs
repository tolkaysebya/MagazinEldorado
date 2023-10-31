using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public Product product;
        public ProductUserControl(Product _product)
        {
            InitializeComponent();
            product = _product;
            var FeedList = App.db.Feedback.ToList();
            var feed = FeedList.Where(x => x.ProductId == product.Id).ToList();
            int count = 0;
            double rate = 0;

            if (App.isAdmin == true) BuyButt.Visibility = Visibility.Collapsed;
            else
            {
                EditButt.Visibility = Visibility.Collapsed;
                DeleteButt.Visibility = Visibility.Collapsed;
            }

            foreach (var item in feed)
            {
                count++;
                rate += item.Evaluation;
            }

            TitleTb.Text = product.Title;
            RatingTB.Text = $"★ {(rate / count).ToString("0.00")}";
            RatingAmount.Text = $"{count} отзывов";
            NewCostTB.Text = product.NewCost;
            OldCostTB.Text = $"{product.Cost.ToString("0.00")} ₽";
            OldCostTB.Visibility = product.costVisibility;
            ImageImg.Source = GetImageSources(product.MainImage);
            DiscoTb.Text = $"-{product.Discount.ToString()}%!";
            DiscoTb.Visibility = product.costVisibility;

        }

        private BitmapImage GetImageSources(byte[] byteImage)
        {
                MemoryStream byteStream = new MemoryStream(byteImage);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                return image;
        }

        private void EditButt_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent(new AddEditPage(product)));
        }

        private void DeleteButt_Click(object sender, RoutedEventArgs e)
        {

            App.db.Product.Remove(product);
            App.db.SaveChanges();
            MessageBox.Show("Удалено: " + product.Title);
            Navigation.NextPage(new PageComponent(new ProductListPage()));

        }
    }
}
