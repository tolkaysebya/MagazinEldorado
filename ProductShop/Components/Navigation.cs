using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.Entity.Migrations.Model;
using System.Windows;

namespace ProductShop.Components
{
    internal class Navigation
    {
        private static List<PageComponent> components = new List<PageComponent>();
        public static MainWindow mainWindow;
        public static void ClearHistory()
        {
            App.isAdmin = false;
            components.Clear();
        }
        public static void Update(PageComponent pageComponent)
        {
            //mainWindow.BackButt.Visibility = components.Count() > 1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            //mainWindow.ExitButt.Visibility = App.isAdmin ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mainWindow.MainFrame.Navigate(pageComponent.Page);
        }
        public static void NextPage(PageComponent pageComponent)
        {
            components.Add(pageComponent);
            Update(pageComponent);
        }
        public static void BackPage()
        {
            if (components.Count > 1)
            {
                components.RemoveAt(components.Count - 1);
                Update(components[components.Count - 1]);
            }
        }
    }

    class PageComponent
    {
        public Page Page { get; set; }
        public PageComponent(Page page)
        {
            Page = page;
        }
    }
}

