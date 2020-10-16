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

namespace mini_projekt
{
    /// <summary>
    /// Interakční logika pro NewStudentForm.xaml
    /// </summary>
    public partial class NewStudentForm : Window
    {
       
        DBworker db;
        List<Subject> subjectsOfStudent = new List<Subject>();

        public NewStudentForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student s = new Student();
            s.Name = txt_Name.Text;
            s.Surname = txt_Surname.Text;
            s.Subjects = subjectsOfStudent;
            db.SaveStudent(s);
            this.Close();
        }

        private void btn_addSubject_Click(object sender, RoutedEventArgs e)
        {
            if(lsbox_Subjects.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)lsbox_Subjects.SelectedItem;
                Subject subject = (Subject)item.Tag;
                subjectsOfStudent.Add(subject);
                upateSubject();
                updateStudentSubjects();
            }
        }

        private void updateStudentSubjects()
        {
            lstbox_SubjectsOfStudents.Items.Clear();
            foreach(Subject s in subjectsOfStudent)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = s.Name;
                item.Tag = s;
                lstbox_SubjectsOfStudents.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fbada\Documents\projekty\miniprojekt\mini_projekt\mini_projekt\Database1.mdf;Integrated Security=True";
            db = new DBworker(conn);
            upateSubject();
        }

        private void upateSubject()
        {
            lsbox_Subjects.Items.Clear();
            List<Subject> subjects = db.getAllSubjects();
            foreach (Subject s in subjects)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = s.Name;
                item.Tag = s;
                lsbox_Subjects.Items.Add(item);
            }
        }
    }
}
