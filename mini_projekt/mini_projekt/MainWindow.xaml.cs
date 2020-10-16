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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mini_projekt
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBworker db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fbada\Documents\projekty\miniprojekt\mini_projekt\mini_projekt\Database1.mdf;Integrated Security=True";
            db = new DBworker(conn);
        }

        private void txt_student_finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            list_students.Items.Clear();
           List<Student> students =  db.searchStudent(txt_student_finder.Text);
            foreach(Student s in students)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = s.Name + " " + s.Surname;
                item.Tag = s;
                list_students.Items.Add(item);
                
            }
        }

        private void list_students_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            list_subjects.Items.Clear();
        
            if(list_students.SelectedItem != null)
            {
                ListBoxItem itemID = (ListBoxItem)list_students.SelectedItem;
                Student sID = (Student)itemID.Tag;
                List<Subject> subjects = db.getStudentsSubject(sID.ID);
                foreach(Subject s in subjects)
                {
                    ListBoxItem item = new ListBoxItem();
                    string contentstr = String.Format("{0} - {1}", s.Name,s.Teacher);
                    item.Content = contentstr;
                    item.Tag = s;
                    list_subjects.Items.Add(item);

                }
            }
        }

        private void btn_newStudent_Click(object sender, RoutedEventArgs e)
        {
            NewStudentForm nsf = new NewStudentForm();
            nsf.Show();
            nsf.InitializeComponent();
        }
    }
}
