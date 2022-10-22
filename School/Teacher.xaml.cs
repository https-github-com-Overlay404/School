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
        public SchoolEntities db = new SchoolEntities();
        public static List<int> idStudent = new List<int>();
        public Teacher()
        {
            InitializeComponent();

            var queryListLesson = (from lesson in db.Lesson
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
           

            foreach (var entity in idStudent)
            {
                var query = (from lesson in db.Lesson
                             where lesson.Name == selectLessen.Text
                             select lesson).First();
                VisitLeson visitLesson = new VisitLeson
                {
                    IdLesson = query.id,
                    IdStudent = entity,
                    DateVisitLessons = DateTime.Today,
                    Presence = true
                };
                db.VisitLeson.Add(visitLesson);
                db.SaveChanges();
            }
        }

        private void selectLessen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeComponent();

            var queryStudent = (
                                from student in db.Student
                                from lessonStudent in student.StudentLesson
                                where lessonStudent.IdLesson == 
                                    (from lesson in db.Lesson
                                     where lesson.Name == selectLessen.Text
                                     select lesson).FirstOrDefault().id
                                select new
                                {
                                    fullName = " " + student.Name + " " + student.Surname + " " + student.Lastname
                                }).Distinct();

            StudentLesson.ItemsSource = queryStudent.ToList(); 
        }
    }
}
