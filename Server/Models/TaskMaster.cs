using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    class TaskMaster
    {

        #region POINTS

        //1. TaskAssignment will have a logic and it defers from the Owner
        //2. Point 1 is not for use. Owner is for 

        #endregion

        #region DATA

        //---Constants---//
        //---Variables---//
        //---Properties---//
        //->    Sole
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCommissioned { get; set; }
        //->    4 Nav
        public int PrimaryOwnerId { get; set; }
        public int SecondaryOwnerId { get; set; }
        public int ReviewerId { get; set; }
        //->    Nav
        //public ICollection<Employee> SMEsorSHs { get; set; }

        #endregion

        #region BEHAVIOURS

        //---Constructors---//
        public TaskMaster()
        {
            //For Sake of EF
        }

        //---Methods---//

        #endregion

    }
}
