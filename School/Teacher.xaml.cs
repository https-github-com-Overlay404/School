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

        public static List<int> idStudent = new List<int>();
        public Teacher()
        {
            InitializeComponent();
            //Добавление предметов в ComboBox
            foreach (var entity in DBConnect.db.Employee.Where(employee => employee.id == IdUSer.Id).SelectMany(l => l.LessonEmployee.Select(less => less.Lesson)).Distinct())
                selectLessen.Items.Add(entity.Name.ToString());

            titleList.Text += DateTime.Today.ToString("d");
        }


        //Загрузка данных в БД
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var entity in idStudent)
            {
                VisitLeson visitLesson = new VisitLeson
                {
                    IdLesson = DBConnect.db.Lesson.Where(l => l.Name == selectLessen.Text).First().id,
                    IdStudent = entity,
                    DateVisitLessons = DateTime.Today,
                    Presence = true
                };
                DBConnect.db.VisitLeson.Add(visitLesson);
                DBConnect.db.SaveChanges();
            }
        }
        //Отображение учеников в ListBox
        private void selectLessen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeComponent();

            var queryStudent = (
                                from student in DBConnect.db.Student
                                from lessonStudent in student.StudentLesson
                                where lessonStudent.IdLesson ==
                                    (from lesson in DBConnect.db.Lesson
                                     where lesson.Name == selectLessen.Text
                                     select lesson).FirstOrDefault().id
                                select new
                                {
                                    fullName = " " + student.Name + " " + student.Surname + " " + student.Lastname
                                }).Distinct();

            StudentLesson.ItemsSource = queryStudent.ToList();
        }
        //Открытие окна авторизации
        private void Window_Closed(object sender, EventArgs e) => new MainWindow().Show();
    }
}
