using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentsGradeTracker.CreateAccount;
using System.Security.Cryptography;
using System.Data;
using static StudentsGradeTracker.Class1;
using System.Windows;
using System.Windows.Controls;

namespace StudentsGradeTracker
{

    class HelperMethod
    {
        DBAccess objDBAccess = new DBAccess();

        Teacher teacher = new Teacher();
        Student student = new Student();

        //public static bool ConfirmUserInformation(string Name, string Surname, string Email, string Password_1, string Password_2, string UserRank, int StudentClass)
        //{
        //    string[] inputStrings = { Name, Surname, Email, Password_1, Password_2, UserRank };

        //    // Check if any of the strings is null or empty
        //    foreach (string input in inputStrings)
        //    {
        //        if (string.IsNullOrEmpty(input))
        //        {
        //            return false; // Return false if any string is null or empty
        //        }
        //    }

        //    if( Password_1 != Password_2)
        //    {
        //        return false;
        //    }

        //    //if(email)
        //    return true;
        //}


        public static string EncryptPassword(string password) //Password encryption
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static DataTable GetClassroomDT(DataTable dt,int StudentClass)
        {
            DBAccess objDBAccess = new DBAccess();
           string query = "SELECT StudentID, Name, Surname, Email, StudentClass,Mathematics ,Physics ,English,Biology ,Note  FROM Students where StudentClass = '" + StudentClass + "'";

            objDBAccess.readDatathroughAdapter(query, dt);
            return dt;
        }


        public static string GetStringFromDataRow(DataTable dataTable, string columnName)
        {
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0][columnName] != null)
            {
                object value = dataTable.Rows[0][columnName];
                return value == DBNull.Value ? string.Empty : value.ToString();
            }
            else { return string.Empty; }
            
        }



    }
}
