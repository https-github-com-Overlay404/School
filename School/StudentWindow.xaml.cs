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

            using (SchoolEntities bd = new SchoolEntities())
            {

                var query = bd.Student.Where(student => student.id == IdUSer.Id);

                var queryClass = from classStudent in bd.Class
                                 from student in query
                                 where student.IdClass == classStudent.id
                                 select classStudent;

                var queryLesson = from less in bd.Lesson
                                  from visitLeson in less.VisitLeson
                                  from student in query
                                  where visitLeson.IdStudent == student.id
                                  select less;

                var queryLessonEmployee = (from employee in bd.Employee
                                          from lessonEmployee in employee.LessonEmployee
                                          from lessonStudent in bd.VisitLeson
                                          from studen in bd.Student
                                          where studen.id == studen.id
                                          select employee).Distinct();

                foreach (var entity in query)
                {
                    NameStudent.Text = entity.Name + " " + entity.Surname;
                }
                foreach (var entity in queryClass)
                {
                    ClassStudent.Text += entity.Name + "\n";
                }
                foreach(var entity in queryLessonEmployee)
                {
                    ClassStudent.Text += entity.Name + " " + entity.Surname + " " + entity.Lastname + "\n";
                }
                foreach (var entity in queryLesson)
                {
                    ListLesson.Items.Add(entity.Name);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e) => new MainWindow().Show();

    }
}
