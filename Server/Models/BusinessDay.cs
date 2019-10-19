using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class BusinessDay
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        //->    4 Nav
        public int TeamId { get; set; }
        
        //->    Nav
        public Team Team { get; set; }
        
        #endregion

        #region BEHAVIOURS

        //---Constructors---//

        public BusinessDay()
        {
            //For the Sake of EF
        }

        //---Methods---//

        #endregion
    }
}
