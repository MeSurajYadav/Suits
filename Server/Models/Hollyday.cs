using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class Hollyday
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
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        //->    4 Nav        
        //->    Nav
        //public ICollection<Team> Teams { get; set; }
        public ICollection<HollydayTeam> HollydayTeams { get; set; }
        public Employee CreatedBy { get; set; }

        #endregion

        #region BEHAVIOURS

        //---Constructors---//

        public Hollyday()
        {
            //For the Sake of EF
        }

        //---Methods---//

        #endregion
    }
}
