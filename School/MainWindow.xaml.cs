using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
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
using System.Configuration;
using System.Data.SqlClient;

namespace School
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordBox.Password != "")
            {
                try
                {
                    int s = Convert.ToInt32(LoginTextBox.Text);
                    int d = Convert.ToInt32(PasswordBox.Password);
                }
                catch (FormatException)
                {
                    InfoMessage.Text = "В поле логина и пароля можно ввести только число";
                    LoginTextBox.Clear();
                    PasswordBox.Clear();
                    return;
                }
                #region check login and password
                using (SchoolEntities school1Entities = new SchoolEntities())
                {
                    
                    foreach(var entity in school1Entities.Employee.Where(employee => employee.IdJobTitle == 2))
                    {
                        if(Convert.ToInt32(LoginTextBox.Text) == entity.Login && Convert.ToInt32(PasswordBox.Password) == entity.Password && entity.Activ == true)
                        {
                            IdUSer.Id = entity.id;
                            new Teacher().Show();
                            Hide();
                            return;
                        }
                    }

                    foreach (var entity in school1Entities.Employee.Where(employee => employee.IdJobTitle != 2))
                    {
                        if (Convert.ToInt32(LoginTextBox.Text) == entity.Login && Convert.ToInt32(PasswordBox.Password) == entity.Password && entity.Activ == true)
                        {
                            IdUSer.Id = entity.id;
                            new Administrator().Show();
                            Hide();
                            return;
                        }
                    }

                    foreach (var entity in school1Entities.Student)
                    {
                        if (Convert.ToInt32(LoginTextBox.Text) == entity.Login && PasswordBox.Password == entity.Password && entity.Activ == true)
                        {
                            IdUSer.Id = entity.id;
                            new StudentWindow().Show();
                            Hide();
                            return;
                        }
                    }
                }

                InfoMessage.Text = "Логин или пароль неверный";
                #endregion
            }
            else InfoMessage.Text = "Не введены значения";
        }

        private void Window_Closed(object sender, EventArgs e) => Application.Current.Shutdown();
    }
    public class IdUSer
    {
        public static int Id { get; set; }
    }
}
