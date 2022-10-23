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
using School;

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

            var query = DBConnect.db.Student.Where(student => student.id == IdUSer.Id);

            var queryClass = from classStudent in DBConnect.db.Class
                             from student in query
                             where student.IdClass == classStudent.id
                             select classStudent;

            var queryLesson = (from less in DBConnect.db.Lesson
                               from visitLeson in less.VisitLeson
                               from student in query
                               where visitLeson.IdStudent == student.id
                               select less).Distinct();

            var queryLessonEmployee = (from employee in DBConnect.db.Employee
                                       from lessonEmployee in employee.LessonEmployee
                                       from lessonStudent in DBConnect.db.StudentLesson
                                       from studen in DBConnect.db.Student
                                       where studen.id == studen.id
                                       select employee).Distinct();

            foreach (var entity in query)
                NameStudent.Text = entity.Name + " " + entity.Surname;

            foreach (var entity in queryClass)
                ClassStudent.Text += entity.Name + "\n";

            foreach (var entity in queryLessonEmployee)
                ClassStudent.Text += entity.Name + " " + entity.Surname + " " + entity.Lastname + "\n";

            foreach (var entity in queryLesson)
                ListLesson.Items.Add(entity.Name);
        }

        private void Window_Closed(object sender, EventArgs e) => new MainWindow().Show();

        private void ListLesson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListVisit.Items.Clear();
            InitializeComponent();

            string lessons = "";

            foreach (var item in e.AddedItems)
            {
                lessons += item.ToString();
            }

            var query = from lesson in DBConnect.db.Lesson
                        from visitLesson in lesson.VisitLeson
                        where lesson.Name == lessons
                        select visitLesson;

            foreach (var entity in query)
            {
                ListVisit.Items.Add(entity.DateVisitLessons);
            }
        }
    }
}
