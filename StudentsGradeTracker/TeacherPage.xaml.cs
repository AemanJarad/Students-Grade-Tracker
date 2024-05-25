using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static StudentsGradeTracker.Class1;

namespace StudentsGradeTracker
{
    /// <summary>
    /// Interaction logic for TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Window
    {
        DBAccess objDBAccess = new DBAccess();
        DataTable dtStudent = new DataTable();
        Student student = new Student();
        Teacher teacher = new Teacher();
        public TeacherPage()
        {
            InitializeComponent();
        }
        private void TeacherPage_Load(object sender, RoutedEventArgs e)
        {
            dtStudent.Clear();
            student.StudentClass = Convert.ToInt32(SelectClass.Text);
            DGstudent.ItemsSource = HelperMethod.GetClassroomDT(dtStudent, student.StudentClass).DefaultView;
           
        }

        private void SelectClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtStudent.Clear();
            // Check if SelectClass.SelectedItem is not null before trying to convert
            if (SelectClass.SelectedItem != null)
            {
                student.StudentClass = Convert.ToInt32((SelectClass.SelectedItem as ComboBoxItem)?.Content);
                DGstudent.ItemsSource = HelperMethod.GetClassroomDT(dtStudent, student.StudentClass).DefaultView;
            }
        }

        private void DGstudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Save changes made to the dtUsersBook DataTable back to the "Books" table

                string query = "Select StudentID, Name, Surname, Email, StudentClass, Mathematics, Physics, English, Biology, Note FROM Students";
                objDBAccess.executeDataAdapter(dtStudent, query);

            
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            dtStudent.Clear();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }


        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            student.Name = txtStudentName.Text;
            student.Surname = txtStudentSurname.Text;
            student.Email = txtStudentEmail.Text;
            student.Password = txtStudentPassword.Password;
            student.StudentClass = Convert.ToInt32(cbxStudentClass.Text);

            if (!string.IsNullOrWhiteSpace(student.Name) &&
                !string.IsNullOrWhiteSpace(student.Surname) &&
                !string.IsNullOrWhiteSpace(student.Email) &&
                !string.IsNullOrWhiteSpace(student.Password))
            {
                student.Password = HelperMethod.EncryptPassword(student.Password);

                SqlCommand insertCommand = new SqlCommand("INSERT INTO Students (Name,Surname, Email, Password, StudentClass) VALUES (@Name,@Surname, @Email, @Password, @StudentClass)");
                insertCommand.Parameters.AddWithValue("@Name", student.Name); //Record user information in the database
                insertCommand.Parameters.AddWithValue("@Surname", student.Surname);
                insertCommand.Parameters.AddWithValue("@Email", student.Email);
                insertCommand.Parameters.AddWithValue("@Password", student.Password);
                insertCommand.Parameters.AddWithValue("@StudentClass", student.StudentClass);

                int rowsAffected = objDBAccess.executeQuery(insertCommand);

                if (rowsAffected == 1)
                {
                    dtStudent.Clear();
                    student.StudentClass = Convert.ToInt32(SelectClass.Text);
                    DGstudent.ItemsSource = HelperMethod.GetClassroomDT(dtStudent, student.StudentClass).DefaultView;
                    ClearInputFields();

                }
                else
                {
                    MessageBox.Show("Error Occurred. Try Again.");
                }

            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }
        }

        private void ClearInputFields()
        {
            txtStudentName.Clear();
            txtStudentSurname.Clear();
            txtStudentEmail.Clear();
            txtStudentPassword.Clear();
        }
    }
}
