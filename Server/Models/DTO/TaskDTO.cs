using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models.DTO
{
    public class TaskDTO
    {
        public string Priority { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }        
        public int BusinessDT { get; set; }
        public string PrimaryOwner { get; set; }
        public string SecondaryOwner { get; set; }
    }
}
