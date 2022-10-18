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
        public ObservableCollection<string> Items1 { get; }
        public SchoolEntities bd = new SchoolEntities();
        public static List<int> idStudent = new List<int>();
        public Teacher()
        {
            Items1 = new ObservableCollection<string>();

            InitializeComponent();

            var queryListLesson = (from lesson in bd.Lesson
                                   from lessonEmployee in lesson.LessonEmployee
                                   where lessonEmployee.IdEmployees == IdUSer.Id
                                   select lesson).Distinct();
            foreach (var entity in queryListLesson)
            {
                selectLessen.Items.Add(entity.Name.ToString());
            }

            titleList.Text += DateTime.Today.ToString("d");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            new MainWindow().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int maxId = 0;

            foreach (var entity in bd.VisitLeson.Where(lesson => lesson.id > lesson.id - 1))
                if (entity.id > maxId) maxId = entity.id;

            foreach (var entity in idStudent)
            {
                maxId++;
                VisitLeson visitLesson = new VisitLeson
                {
                    id = maxId,
                    IdLesson = 2,
                    IdStudent = entity,
                    DateVisitLessons = DateTime.Today,
                    Presence = true
                };
                bd.VisitLeson.Add(visitLesson);
                bd.SaveChanges();
            }



        }

        private void selectLessen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var queryStudent = (from lesson in bd.Lesson
                                from studentLesson in lesson.StudentLesson
                                from employeeLesson in lesson.LessonEmployee
                                from student in bd.Student
                                where employeeLesson.id == IdUSer.Id
                                select student).Distinct();


            foreach (var entity in queryStudent)
            {
                MessageBox.Show(entity.Name);
                Items1.Add(entity.Name + " " + entity.Surname + " " + entity.Lastname + "\n");
                idStudent.Add(entity.id);
            }

            InitializeComponent();
            DataContext = this;
        }
    }
}
