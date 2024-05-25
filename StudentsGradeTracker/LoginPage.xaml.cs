using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Windows;
using static StudentsGradeTracker.HelperMethod; // Assuming EncryptPassword is a static method in HelperMethod
using static StudentsGradeTracker.Class1;

namespace StudentsGradeTracker
{
    public partial class LoginPage : Window
    {
        DBAccess objDBAccess = new DBAccess();
        DataTable dtStudent = new DataTable();
        DataTable dtTeacher = new DataTable();
        DataTable dtAdmin = new DataTable();

        public static int PublicID;
        public static string PublicDepartment;

        Student student = new Student();
        Teacher teacher = new Teacher();
        Admin admin = new Admin();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if (!int.TryParse(txtUserID.Text, out ID))
            {
                MessageBox.Show("Invalid user ID. Please enter a valid numeric ID.");
                return;
            }

            string Password = txtPassword.Password;
            string query;
            string decryptedPassword;

            if (txtPassword.Password != string.Empty)
            {
                if (ID < 1999)
                {
                    // student
                    dtStudent.Clear();
                    query = "SELECT * FROM Students WHERE StudentID = '" + ID + "' ";
                    objDBAccess.readDatathroughAdapter(query, dtStudent);
                    if (dtStudent.Rows.Count == 1)
                    {
                        student.Password = dtStudent.Rows[0]["Password"].ToString();
                        decryptedPassword = EncryptPassword(Password);

                        if (decryptedPassword == student.Password)
                        {
                            PublicID = ID;
                            objDBAccess.closeConn();
                            StudentPage studentPage = new StudentPage();
                            studentPage.Show();
                            dtStudent.Clear();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password for student.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student not found with the provided ID.");
                    }
                }
                else if (ID <= 2999 && ID >= 2000)
                {
                    // teacher
                    dtTeacher.Clear();
                    query = "SELECT * FROM Teachers WHERE TeacherID = '" + ID + "' ";
                    objDBAccess.readDatathroughAdapter(query, dtTeacher);
                    if (dtTeacher.Rows.Count == 1)
                    {
                        teacher.Password = dtTeacher.Rows[0]["Password"].ToString();
                        decryptedPassword = EncryptPassword(Password);

                        if (decryptedPassword == teacher.Password)
                        {
                            PublicID = ID;
                            objDBAccess.closeConn();
                            TeacherPage teacherPage = new TeacherPage();
                            teacherPage.Show();
                            dtTeacher.Clear();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password for teacher.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Teacher not found with the provided ID.");
                    }
                }
                else if (ID >= 3000)
                {
                    // admin
                    dtAdmin.Clear();
                    query = "SELECT * FROM Admins WHERE ID = '" + ID + "' ";
                    objDBAccess.readDatathroughAdapter(query, dtAdmin);
                    if (dtAdmin.Rows.Count == 1)
                    {
                        admin.Password = dtAdmin.Rows[0]["Password"].ToString();
                        decryptedPassword = EncryptPassword(Password);

                        if (decryptedPassword == admin.Password)
                        {
                            PublicID = ID;
                            objDBAccess.closeConn();
                            AdminPage adminPage = new AdminPage();
                            adminPage.Show();
                            dtAdmin.Clear();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password for admin.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Admin not found with the provided ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }
        }
    }
}
