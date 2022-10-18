using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> Items { get; }
        public SchoolEntities bd = new SchoolEntities();
        public List<int> idStudent = new List<int>();
        public Teacher()
        {
            Items = new ObservableCollection<string>();

            var queryStudent = (from student in bd.Student
                                from studentLesson in student.VisitLeson
                                from lesson in bd.LessonEmployee
                                where lesson.IdEmployees == IdUSer.Id
                                select student).Distinct();


            foreach (var entity in queryStudent)
            {
                Items.Add(entity.Name + " " + entity.Surname + " " + entity.Lastname + "\n");
                idStudent.Add(entity.id);
            }

            InitializeComponent();
            DataContext = this;

            titleList.Text += DateTime.Today.ToString("d");

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            new MainWindow().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < ListLesson.SelectedItems.Count; i++)
            {
                VisitLeson visitLesson = new VisitLeson { id = 7, IdLesson = 1, IdStudent = idStudent.IndexOf(i), DateVisitLessons = DateTime.Today };
                bd.VisitLeson.Add(visitLesson);
                bd.SaveChanges();
            }

        }
    }
}
