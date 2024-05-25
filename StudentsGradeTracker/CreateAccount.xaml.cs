using Microsoft.Data.SqlClient;
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

namespace StudentsGradeTracker
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        DBAccess objDBAccess = new DBAccess();

        public CreateAccount()
        {
            InitializeComponent();
        }
        public class User
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int StudentClass { get; set; } // Additional property for student class
            public string UserRank { get; set; }
            public bool IsTeacher { get; set; } // Flag to indicate whether the user is a teacher
        }


        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtName.Text;
            string Surname = txtSurname.Text;
            string Email = txtEmail.Text;
            string Password = txtPass1.Password;
            //string Password_2 = txtPass2.Password;
            string UserRank = ((ComboBoxItem)AccountType.SelectedItem).Content.ToString();
            int StudentClass = Convert.ToInt32(((ComboBoxItem)StudentGrade.SelectedItem).Content);
            // HelperMethod.ConfirmUserInformation(Name, Surname, Email, Password_1, Password_2, UserRank, StudentClass);
            string encryptedPassword = HelperMethod.EncryptPassword(Password);

            if (AccountType.SelectedIndex == 1) //Teachers
            {
                //SqlCommand insertCommand = new SqlCommand("INSERT INTO Teachers (Name,Surname, Email, Password, Department) VALUES (@Name,@Surname, @Email, @Password, @Department)");
                //insertCommand.Parameters.AddWithValue("@Name", Name); //Record user information in the database
                //insertCommand.Parameters.AddWithValue("@Surname", Surname);
                //insertCommand.Parameters.AddWithValue("@Email", Email);
                //insertCommand.Parameters.AddWithValue("@Password", encryptedPassword);
                //insertCommand.Parameters.AddWithValue("@Department", Department);

                //int rowsAffected = objDBAccess.executeQuery(insertCommand);

                //if (rowsAffected == 1)
                //{
                //    txtName.Text = txtSurname.Text = txtEmail.Text = txtPass1.Password = string.Empty;
                //}
                //else
                //{
                //    MessageBox.Show("Error Occurred. Try Again.");
                //}
            }
            else // Students
            {
                SqlCommand insertCommand = new SqlCommand("INSERT INTO Students (Name,Surname, Email, Password, StudentClass) VALUES (@Name,@Surname, @Email, @Password, @StudentClass)");
                insertCommand.Parameters.AddWithValue("@Name", Name); //Record user information in the database
                insertCommand.Parameters.AddWithValue("@Surname", Surname);
                insertCommand.Parameters.AddWithValue("@Email", Email);
                insertCommand.Parameters.AddWithValue("@Password", encryptedPassword);
                insertCommand.Parameters.AddWithValue("@StudentClass", StudentClass);

                int rowsAffected = objDBAccess.executeQuery(insertCommand);

                if (rowsAffected == 1)
                {
                    txtName.Text = txtSurname.Text = txtPass1.Password = string.Empty;
                    txtEmail.Text = "@gmail.com";
                }
                else
                {
                    MessageBox.Show("Error Occurred. Try Again.");
                }
            }

                
           
        }

    }
}
