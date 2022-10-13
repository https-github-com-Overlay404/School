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

        public string connection = @"Data Source=OVER\SQLSERSTUDY;Initial Catalog=School;Integrated Security=True";
        bool f = false;
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
                catch (System.FormatException)
                {
                    InfoMessage.Text = "В поле логина и пароля можно ввести только число";
                    LoginTextBox.Text = "";
                    PasswordBox.Password= "";
                    return;
                }
                #region check login and password
                using (School1Entities school1Entities = new School1Entities())
                {
                    var query = school1Entities.Employee.Where(employee => employee.IdJobTitle == 2);
                    foreach(var entity in query)
                    {
                        if(Convert.ToInt32(LoginTextBox.Text) == entity.Login && Convert.ToInt32(PasswordBox.Password) == entity.Password)
                        {
                            IdUSer.Id = entity.id;
                            Teacher teacher = new Teacher();
                            teacher.Show();
                            this.Hide();
                            f = true;
                        }
                    }
                    query = school1Entities.Employee.Where(employee => employee.IdJobTitle != 2);
                    foreach (var entity in query)
                    {
                        if (Convert.ToInt32(LoginTextBox.Text) == entity.Login && Convert.ToInt32(PasswordBox.Password) == entity.Password)
                        {
                            IdUSer.Id = entity.id;
                            Administrator administrator = new Administrator();
                            administrator.Show();
                            this.Hide();
                            f = true;
                        }
                    }
                    var queryStudent = school1Entities.Student;
                    foreach (var entity in queryStudent)
                    {
                        if (Convert.ToInt32(LoginTextBox.Text) == entity.Login && PasswordBox.Password == entity.Password)
                        {
                            IdUSer.Id = entity.id;
                            StudentWindow studentWindow = new StudentWindow();
                            studentWindow.Show();
                            this.Hide();
                            f = true;
                        }
                    }
                }
                if (!f)
                {
                    InfoMessage.Text = "Логин или пароль неверный";
                }
                #endregion
            }
            else
            {
                InfoMessage.Text = "Не введены значения";
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    public class IdUSer
    {
        public static int Id { get; set; }
    }
}
