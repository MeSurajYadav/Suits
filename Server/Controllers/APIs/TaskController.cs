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
    [Route("/api/task")]
    public class TaskController : Controller
    {
        private readonly WLMDbContext _context;

        public TaskController(WLMDbContext context)
        {
            _context = context;
        }

        /*this isn't working as /api/task/GetTodaysTasks, is it true that if we write attribute route to controller, we got to write the same(attribute route for action as well?)*/
        //[Route()]oops dont know, lets do another scenario
        //https://codeopinion.com/asp-net-core-mvc-attribute-routing/
        [HttpGet("gettodaystasks")]
        public TaskDTO[] GetTodaysTasks()//defualt route is /MainApp/GetTasks
        {
            return new TaskLogic(_context).GetTasks();//restrict what we return            
        }
        // public int? GetBusinessDay(){
        //     return new BusinessLogic(_context).GetBusinessDay();
        // }

        public TaskDTO[] GetTasksOfDate()//above is give specific route, dfolt
        {
            return new TaskLogic(_context).GetTasks();          
        }

    }
}
