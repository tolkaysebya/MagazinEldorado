using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProductShop.Pages;

namespace ProductShop.Components
{
    public partial class Product
    {
        public string NewCost
        {
            get
            {
                if (Discount == 0) return $"{Cost.ToString("0.00")} ₽";
                else return $"{(Cost - Cost * (decimal)Discount / 100).ToString("0.00")} ₽";
            }
        }
        public Visibility costVisibility
        {
            get
            {
                if (Discount == 0) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }
    }
}

