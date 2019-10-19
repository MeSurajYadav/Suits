using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class Employee
    {

        #region POINTS

        //1. Employee has self join for his Senior
        //2. Employee can assign cases to his juniors only

        #endregion

        #region DATA
        //---Constants---//
        //---Variables---//
        //---Properties---//
        //->    Sole
        public EmployeeRole Role { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebPageAddress { get; set; }
        public string Address { get; set; }
        public int MobilePhoneNo { get; set; }
        public int OfficePhoneNo { get; set; }
        public int HomePhoneNo { get; set; }
        public int FaxNo { get; set; }
        //->    4 Nav
        public int TeamId { get; set; }
        public int? SeniorId { get; set; }
        //->    Nav
        public Team Team { get; set; }
        public Employee Senior { get; set; }//Employee?The type 'Employee' must be a non-nullable value type in order to use it as parameter 'T' in the generic type or method 'Nullable<T>' [Allocator]
//Employee? Employee.Senior
        public ICollection<Employee> Juniors { get; set; }
        public ICollection<Task> TasksOfWhichIAmPrimaryOwner { get; set; }
        public ICollection<Task> TasksOfWhichIAmSecondaryOwner { get; set; }
        public ICollection<Task> TasksOfWhichIAmReviewer { get; set; }
        public IEnumerable<Hollyday> CreatedByMeHollydays { get; internal set; }
        public ICollection<TaskSnapShot> SnapshotsAssignedToMe { get; set; }
        public ICollection<TaskSnapShot> SnapshotsAssignedByMe { get; set; }


        //public ICollection<Task> Tasks { get; set; }
        #endregion

        #region BEHAVIOURS

        //---Constructors---//

        public Employee()
        {
            //For the Sake of EF
        }

        //---Methods---//

        #endregion

    }
}

