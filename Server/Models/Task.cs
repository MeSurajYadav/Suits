using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class Task
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
        public DateTime CreatedOn { get; set; }
        public Priorities Priority { get; set; }        
        public int BusinessDT { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public bool IsCommissioned { get; set; }
        //->    4 Nav
        public int PrimaryOwnerId { get; set; }
        public int SecondaryOwnerId { get; set; }
        public int ReviewerId { get; set; }
        public int TeamId { get; set; }
        //->    Nav
        public Employee PrimaryOwner { get; set; }
        public Employee SecondaryOwner { get; set; }
        public Employee Reviewer { get; set; }
        public Team Team { get; set; }
        public ICollection<TaskSnapShot> TaskSnapshots { get; set; }
        //public Statuses CurrentStatus { get; set; }
        //public int PercentageOfTask { get; set; }
        //public int CurrentPercentageOfWorkCompleted { get; set; }
        //public ICollection<Employee> SMEsorSHs { get; set; }
        #endregion

        #region BEHAVIOURS

        //---Constructors---//
        public Task()
        {
            //EF
        }
        //---Methods---//

        #endregion

    }
}
