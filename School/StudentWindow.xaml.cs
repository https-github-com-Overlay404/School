using System;
using System.Collections;
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
            ArrayList indexLesson = new ArrayList();
            using (SchoolEntities bd = new SchoolEntities())
            {
                var query = bd.Student.Where(student => student.id == IdUSer.Id);
                foreach (var entity in query)
                {
                    NameStudent.Text = entity.Name + " " + entity.Surname;
                }

                var queryLesson = from less in bd.Lesson
                                  from visitLeson in less.VisitLeson
                                  from student in query
                                  where visitLeson.IdLesson == student.id
                                  select less;
                foreach (var entity in queryLesson)
                {
                    ListLesson.Items.Add(entity.Name);
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
