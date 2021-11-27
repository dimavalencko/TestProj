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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DgridUsers.ItemsSource = laboratoryEntities.GetContext().users.ToList();
        }

        private void UpdateCloth()
        {
            var currentCloth = laboratoryEntities.GetContext().users.ToList();//Тут надо поменять название фраемворка и таблицы в которой надо искать данные

            currentCloth = currentCloth.Where(p => p.name.ToString().Contains(tbsearch.Text.ToString())).ToList();//Здесь надо поменять Артикул_Ткани на свое название колонки по которой нужно искать и надо поменять название TBSearch на свое, куда вводится значение для поиска.
            DgridUsers.ItemsSource = currentCloth.ToList();//Тут меняем название DGridOstatok на название своего грида.
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCloth();
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            edit frm = new edit((sender as Button).DataContext as users);
            this.Hide();
            frm.Show();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            laboratoryEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DgridUsers.ItemsSource = laboratoryEntities.GetContext().users.ToList();
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var ClothForRemoving = DgridUsers.SelectedItems.Cast<users>().ToList();//Здесь надо поменять DGridOstatok на название своего грида, тут Cast<Склад_ткани>() вместо Склад_Ткани указать таблицу из которой идёт удаление

            if (MessageBox.Show($"Вы действительно хотите удалить выбранные {ClothForRemoving.Count()} строк?", "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    laboratoryEntities.GetContext().users.RemoveRange(ClothForRemoving);//тут меняем на свой фраемворк и Склад_Ткани на название соей таблицы.
                    laboratoryEntities.GetContext().SaveChanges();//Здес только свой фраемворк.
                    MessageBox.Show("Данные удалены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            DgridUsers.ItemsSource = laboratoryEntities.GetContext().users.ToList();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            edit frm = new edit(null);
            frm.Show();
        }
    }
}