using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ErrorHandler
    {
       public static string ErrorMessage="";
        public static int StatusCode { get; set; } = 500;
        public static void GetError(Exception ex)
        {
            ErrorMessage = "Error - Please contact the site administrator";
            if (ex.GetType() == typeof(DivideByZeroException))
            {
                ErrorMessage = "Divide by zero error";
                StatusCode = 521;
            }
            if(ex.GetType()== typeof(System.Security.Cryptography.CryptographicException))
            {
                ErrorMessage = "Decoding error";
                StatusCode = 522;
            }
            
            
            if (ex.GetType() == typeof(FormatException))
            {
                ErrorMessage = "Format error";
                StatusCode = 523;
            }
            if (ex.GetType() == typeof(SqlException))
            {

                ErrorMessage = "Database error";
                SqlException exsql = ex as SqlException;
                ErrorMessage = GetSqlServerError(exsql);
            }
            if(ex.GetType()==typeof(DbUpdateConcurrencyException))
            {
                ErrorMessage = "The desired information has been changed by another user. Try again";
            }
            if (ex.GetType()==typeof(DbUpdateException))
            {
                SqlException exsql = ex.InnerException as SqlException;
                ErrorMessage = GetSqlServerError(exsql);
            }

           
        }

        private static string GetSqlServerError(SqlException exsql)
        {

            if (exsql.Number == 515)
            {
                ErrorMessage = "Enter Details completely";
                StatusCode = 530;
            }
            if (exsql.Number == 2627)
            {
                ErrorMessage = "Duplicate information";
                StatusCode = 531;
            }
            if (exsql.Number == 547)
            {
                ErrorMessage = " Because the information is dependent on other parts, it cannot be changed ";
                StatusCode = 532;
            }
            if (exsql.Number == 0 || exsql.Number == 2 || exsql.Number == -2)
            {
                ErrorMessage = " The database cannot be accessed ";
                StatusCode = 533;
            }
            return ErrorMessage;
        }
    }

    public class AuthenticatExcpetion:Exception
    {

    }
    public class SelectionException:Exception
    {

    }
}
