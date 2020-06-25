using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using NETCoreSSO.Pages;
using MySql.Data;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETCoreSSO.Controllers
{
    public class FormController : Controller
    {
        static string connStr = "{Sensative Info}";
        MySqlConnection conn = new MySqlConnection(connStr);
        [HttpPost]
        public IActionResult Deposit()
        {
            Console.WriteLine("Deposit form data recieved");
            var a = HttpContext.Request.Form["DBox.DepositValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = HttpContext.Request.Form["CurrBal"];
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal + SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    
                }
            }
            
            
            return Redirect("~/Orderrecieved");
        }
       [HttpPost]
       public IActionResult Withdrawl()
        {
            Console.WriteLine("Withdrawl form data recieved");
            var a = HttpContext.Request.Form["WBox.WithdrawlValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = HttpContext.Request.Form["CurrBal"];
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal - SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    
                }
            }
            return Redirect("~/Orderrecieved");
        }

    }
}
