﻿using System;
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
using System.Data.Entity;

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

            foreach (var entity in DBConnect.db.Student.Where(student => student.id == IdUSer.Id))
                NameStudent.Text = entity.Name + " " + entity.Surname + " " + entity.Lastname;

            foreach (var entity in DBConnect.db.Student.Where(s => s.id == IdUSer.Id).Select(c => c.Class))
                ClassStudent.Text += entity.Name + "\n";

            foreach (var entity in DBConnect.db.StudentLesson.Where(c => c.IdStudent == IdUSer.Id).SelectMany(c => c.Lesson.LessonEmployee.Select(e => e.Employee)).Distinct())
                ClassStudent.Text += entity.Name + " " + entity.Surname + " " + entity.Lastname + "\n";

            foreach (var entity in DBConnect.db.Student.Where(s => s.id == IdUSer.Id).SelectMany(c => c.StudentLesson.Select(l => l.Lesson)))
                ListLesson.Items.Add(entity.Name);
        }

        private void Window_Closed(object sender, EventArgs e) => new MainWindow().Show();

        private void ListLesson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListVisit.Items.Clear();
            InitializeComponent();

            string lessons = "";

            foreach (var item in e.AddedItems)
                lessons += item.ToString();

            foreach (var entity in (DBConnect.db.Student.SelectMany(c => c.VisitLeson).Where(c => c.Lesson.Name == lessons && c.Student.id == IdUSer.Id)))
                ListVisit.Items.Add(entity.DateVisitLessons);
        }
    }
}
