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

namespace Laboratory
{
    /// <summary>
    /// Логика взаимодействия для edit.xaml
    /// </summary>
    public partial class edit : Window
    {
        private users newUser = new users();
        public edit(users currUser)
        {
            InitializeComponent();
            CbGender.ItemsSource = laboratoryEntities.GetContext().gender.ToList();

            if (currUser != null)
            {
                newUser = currUser;
            }
            DataContext = newUser;
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            if (newUser.id == 0)
                laboratoryEntities.GetContext().users.Add(newUser);

            try
            {
                laboratoryEntities.GetContext().SaveChanges();
                MessageBox.Show("Saved!");
                this.Close();
                MainWindow frm = new MainWindow();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
