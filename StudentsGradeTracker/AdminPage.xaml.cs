using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using static StudentsGradeTracker.Class1;
using static StudentsGradeTracker.HelperMethod;

namespace StudentsGradeTracker
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        Teacher teacher = new Teacher();
        Student student = new Student();
        User admin = new User();

        DBAccess objDBAccess = new DBAccess();

        DataTable dtTechers = new DataTable();
        DataTable dtStudents = new DataTable();
        DataTable dtAdmins = new DataTable();

        public AdminPage()
        {
            InitializeComponent();
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            dtTechers.Clear();
            string Teacherquery = "SELECT TeacherID , Name, Surname, Email,Department FROM Teachers";
            objDBAccess.readDatathroughAdapter(Teacherquery, dtTechers);
            TeachersGrid.ItemsSource = dtTechers.DefaultView;
            objDBAccess.closeConn();

            dtStudents.Clear();
            string Studentquery = "SELECT StudentID , Name, Surname, Email,StudentClass FROM Students";
            objDBAccess.readDatathroughAdapter(Studentquery, dtStudents);
            StudentGrid.ItemsSource = dtStudents.DefaultView;
            objDBAccess.closeConn();

            dtAdmins.Clear();
            string AdminQuery = "SELECT ID , Name, Surname, Email FROM Admins";
            objDBAccess.readDatathroughAdapter(AdminQuery, dtAdmins);
            AdminGrid.ItemsSource = dtAdmins.DefaultView;
            objDBAccess.closeConn();
        }

        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            teacher.Name = txtTeacherName.Text;
            teacher.Surname = txtTeacherSurname.Text;
            teacher.Email = txtTeacherEmail.Text;
            teacher.Password = txtTeacherPassword.Password;
            teacher.Department = cbxDepartment.Text;

            if (!string.IsNullOrWhiteSpace(teacher.Name) &&
                !string.IsNullOrWhiteSpace(teacher.Surname) &&
                !string.IsNullOrWhiteSpace(teacher.Email) &&
                !string.IsNullOrWhiteSpace(teacher.Password) &&
                !string.IsNullOrWhiteSpace(teacher.Department))
            {
                teacher.Password = HelperMethod.EncryptPassword(teacher.Password);
                // Assuming you have a method in DBAccess to add a new account
                bool success = objDBAccess.AddNewAccount(teacher.Name, teacher.Surname, teacher.Email, teacher.Password, teacher.Department);

                if (success)
                {
                    MessageBox.Show("Account added successfully!");
                    // Optionally, clear the input fields after successful addition
                    ClearInputFields();

                    dtTechers.Clear();
                    string TeacherQuery = "SELECT TeacherID , Name, Surname, Email,Department FROM Teachers";
                    objDBAccess.readDatathroughAdapter(TeacherQuery, dtTechers);
                    TeachersGrid.ItemsSource = dtTechers.DefaultView;
                    objDBAccess.closeConn();
                }
                else
                {
                    MessageBox.Show("Failed to add the account. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }
        }

        private void ClearInputFields()
        {
            txtTeacherName.Clear();
            txtTeacherSurname.Clear();
            txtTeacherEmail.Clear();
            txtTeacherPassword.Clear();

            txtStudentName.Clear();
            txtStudentSurname.Clear();
            txtStudentEmail.Clear();
            txtStudentPassword.Clear();

            txtAdminName.Clear();
            txtAdminSurname.Clear();
            txtAdminEmail.Clear();
            txtAdminPassword.Clear();

            
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
                    dtStudents.Clear();
                    string StudentQuery = "SELECT StudentID , Name, Surname, Email,StudentClass FROM Students";
                    objDBAccess.readDatathroughAdapter(StudentQuery, dtStudents);
                    StudentGrid.ItemsSource = dtStudents.DefaultView;
                    objDBAccess.closeConn();
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

        private void BtnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.Name = txtAdminName.Text;
            admin.Surname = txtAdminSurname.Text;
            admin.Email = txtAdminEmail.Text;
            admin.Password = txtAdminPassword.Password;


 
            if (!string.IsNullOrWhiteSpace(admin.Name) &&
                !string.IsNullOrWhiteSpace(admin.Surname) &&
                !string.IsNullOrWhiteSpace(admin.Email) &&
                !string.IsNullOrWhiteSpace(admin.Password))
            {
                admin.Password = HelperMethod.EncryptPassword(admin.Password);

                SqlCommand insertCommand = new SqlCommand("INSERT INTO Admins (Name,Surname, Email, Password ) VALUES (@Name,@Surname, @Email, @Password)");
                insertCommand.Parameters.AddWithValue("@Name", admin.Name); //Record user information in the database
                insertCommand.Parameters.AddWithValue("@Surname", admin.Surname);
                insertCommand.Parameters.AddWithValue("@Email", admin.Email);
                insertCommand.Parameters.AddWithValue("@Password", admin.Password);

                int rowsAffected = objDBAccess.executeQuery(insertCommand);

                if (rowsAffected == 1)
                {
                    dtAdmins.Clear();
                    string AdminQuery = "SELECT ID , Name, Surname, Email FROM Admins";
                    objDBAccess.readDatathroughAdapter(AdminQuery, dtAdmins);
                    AdminGrid.ItemsSource = dtAdmins.DefaultView;
                    objDBAccess.closeConn();
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

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        private void SaveTeachers_Click(object sender, RoutedEventArgs e)
        {
            // Save changes made to the dtUsers DataTable back to the "Users" table
            string query = "Select TeacherID,Name,Surname,Email,Department from Teachers";
            objDBAccess.executeDataAdapter(dtTechers, query);
        }

        private void SaveStudents_Click(object sender, RoutedEventArgs e)
        {
            // Save changes made to the dtUsers DataTable back to the "Users" table
            string query = "Select StudentID, Name, Surname, Email, StudentClass from Students";
            objDBAccess.executeDataAdapter(dtStudents, query);
        }

        private void SaveAdmins_Click(object sender, RoutedEventArgs e)
        {
            // Save changes made to the dtUsers DataTable back to the "Users" table
            string query = "Select ID, Name, Surname, Emaile from Admins";
            objDBAccess.executeDataAdapter(dtAdmins, query);
        }
    }
}
