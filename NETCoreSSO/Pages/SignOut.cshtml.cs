using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace NETCoreSSO.Pages
{
    public class HTTPTool
    {
        public async Task<string> HTTPGET(string URL)
        {
            HttpClient HC = new HttpClient();
            var ret = await HC.GetAsync(URL);
            return ret.ToString();
        }
    }
    public class SignOutModel : PageModel
    {
        private readonly ILogger<SignOutModel> _logger;

        public SignOutModel(ILogger<SignOutModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
           
        }
    }
}