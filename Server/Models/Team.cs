using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class Team
    {
        #region POINTS

        //1. 

        #endregion

        #region DATA

        //---Constants---//
        //---Variables---//
        //---Properties---//
        //->    Sole
        public int Id { get; set; }
        public string Name { get; set; }

        //->    4 Nav
        //->    Nav
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<HollydayTeam> HollydayTeams { get; set; }
        public ICollection<BusinessDay> BusinessDays { get; set; }

        #endregion

        #region BEHAVIOURS

        //---Constructors---//

        public Team()
        {
            //For the Sake of EF
        }

        //---Methods---//

        #endregion
    }
}
