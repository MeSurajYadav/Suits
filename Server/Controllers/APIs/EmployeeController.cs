using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Contexts;
using Server.Models.DTO;
using Server.BusinessLogic;
namespace Server.Controllers.APIs
{
    [Route("/api/employee")]
    public class EmployeeController : Controller
    {

        private readonly WLMDbContext _context;

        public EmployeeController(WLMDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCurrentUser")]
        /*without above HTTP get wilkl get error → No webpage was found for the web address: https://localhost:5001/api/Employee/getcurrentuser*/
        public string  GetCurrentUser ()
        {
            string currentUser ;
            TaskLogic t = new TaskLogic(_context);
            currentUser = t.GetEmployee().UserId;
            return currentUser;
            
        }
    }
}