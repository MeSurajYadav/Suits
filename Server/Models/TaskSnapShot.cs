using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class TaskSnapShot //: TaskMaster
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
        public DateTime TimeStamp { get; set; }
        public Statuses Status { get; set; }
        public int PercentageOfWorkCompleted { get; set; }
        //->    4 Nav
        public int AssignedToId { get; set; }
        public int AssignedById { get; set; }
        public int TaskId { get; set; }
        //->    Nav
        public Employee AssignedTo { get; set; }
        public Employee AssignedBy { get; set; }
        public Task Task { get; set; }

        #endregion

        #region BEHAVIOURS

        //---Constructors---//

        public TaskSnapShot()
        {

        }

        //---Methods---//

        #endregion
    }
}
