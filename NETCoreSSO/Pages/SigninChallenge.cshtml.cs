using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace NETCoreSSO.Pages
{
    
    public class DatabaseFunctions
    {
        string connStr = "{Sensative Info}";
        MySqlConnection conn;
        
        public DatabaseFunctions()
        {
            conn = new MySqlConnection(connStr);
            
            
        }
        public int GetCurrentBal(string UID)
        {
            int Val = 0;
            conn.Open();
            string sql = $"SELECT Balance FROM Users WHERE Username = '{UID}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);
                Val = (int)rdr[0];
                
            }
            rdr.Close();
            conn.Close();
            return Val;
        }
        public void CheckForPreviousEntry(string UID)
        {
            conn.Open();
            string sql = $"SELECT * FROM Users WHERE Username = '{UID}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {

                string sql2 = $"INSERT INTO Users Values ('{UID}', 100);";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                rdr.Close();
                cmd2.ExecuteNonQuery();
            }
            
            conn.Close();
            return;
        }
    }
    public class DepositBox
    {
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public int DepositValue { get; set; }
    }
    public class WithdrawBox
    {
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public int WithdrawlValue { get; set; }
    }
   
    [Authorize]
    public class SigninChallengeModel : PageModel
    {
        private readonly ILogger<SigninChallengeModel> _logger;

        public SigninChallengeModel(ILogger<SigninChallengeModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            Console.WriteLine("User {0} has signed in.", HttpContext.User.FindFirst("cognito:username").ToString());
        }
        
    }
}

