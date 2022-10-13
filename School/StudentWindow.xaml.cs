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
using static System.Net.Mime.MediaTypeNames;

namespace School
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow()
        {
            InitializeComponent();

            MainWindow mainWindow = new MainWindow();
            MessageBox.Show("" + IdUSer.Id);
            using (School1Entities bd = new School1Entities())
            {
                var query = bd.Student.Where(student => student.id == IdUSer.Id);
                foreach (var entity in query)
                {
                   
                    NameStudent.Text = entity.id + " " + entity.Name + " " + entity.Surname + "\n" + entity.Login + " " + entity.Password;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Update_lessens(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
