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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        public Teacher()
        {
            InitializeComponent();

            using (SchoolEntities bd = new SchoolEntities())
            {
                var query = bd.Employee.Where(employee => employee.id == IdUSer.Id);
                foreach (var entity in query)
                {
                    text.Text = entity.id + " " + entity.Name + " " + entity.Surname + "\n" + entity.Login + " " + entity.Password;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
