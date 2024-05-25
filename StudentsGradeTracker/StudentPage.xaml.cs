using System;
using System.Data;
using System.Windows;

namespace StudentsGradeTracker
{
    public partial class StudentPage : Window
    {
        private DBAccess objDBAccess = new DBAccess();
        private DataTable dtStudentGrades = new DataTable();
        private DataTable dtStudentInfo = new DataTable();
        public StudentPage()
        {
            InitializeComponent();
            LoadStudentGrades();
        }

        private void LoadStudentGrades()
        {
            // Replace 'YourStudentID' with the actual student's identifier
            int studentID = LoginPage.PublicID;
            int total = 0;
            int tempValue;
            string GradesQuery = $"SELECT Mathematics, Physics, English, Biology, Note FROM Students WHERE StudentID = '{studentID}'";
            string InfoQuery = $"SELECT Name, Surname, Email, StudentClass FROM Students WHERE StudentID = '{studentID}'";
            try
            {
                objDBAccess.readDatathroughAdapter(GradesQuery, dtStudentGrades);
                if (dtStudentGrades.Rows.Count > 0)
                {
                    txtMathematics.Text = HelperMethod.GetStringFromDataRow(dtStudentGrades, "Mathematics");
                    txtPhysics.Text = HelperMethod.GetStringFromDataRow(dtStudentGrades, "Physics");
                    txtEnglish.Text = HelperMethod.GetStringFromDataRow(dtStudentGrades, "English");
                    txtBiology.Text = HelperMethod.GetStringFromDataRow(dtStudentGrades, "Biology");
                    textBlockNote.Text = HelperMethod.GetStringFromDataRow(dtStudentGrades, "Note");



                    if (int.TryParse(txtMathematics.Text, out tempValue))
                        total += tempValue;
                    if (int.TryParse(txtPhysics.Text, out tempValue))
                        total += tempValue;
                    if (int.TryParse(txtEnglish.Text, out tempValue))
                        total += tempValue;
                    if (int.TryParse(txtBiology.Text, out tempValue))
                        total += tempValue;

                    total /= 4;
                    txtTotal.Text = total.ToString() + "%";
                    objDBAccess.closeConn();
                }

                objDBAccess.readDatathroughAdapter(InfoQuery, dtStudentInfo);
                if(dtStudentInfo.Rows.Count > 0)
                {
                    txtName.Text = HelperMethod.GetStringFromDataRow(dtStudentInfo, "Name");
                    txtSurname.Text = HelperMethod.GetStringFromDataRow(dtStudentInfo, "Surname");
                    txtEmail.Text = HelperMethod.GetStringFromDataRow(dtStudentInfo, "Email");
                    txtClass.Text = HelperMethod.GetStringFromDataRow(dtStudentInfo, "StudentClass");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                //throw ex;
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            dtStudentGrades.Clear();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
}
