using System;
using Server.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using System.Linq;

namespace Server.BusinessLogic
{
    //internal class BusinessLogic dont do this, name conflicts
    internal class TaskLogic
    {
        private WLMDbContext cn;
        private DateTime[] HollydayListTmp;

        public TaskLogic(WLMDbContext context)
        {
            cn = context;
        }

        public TaskDTO[] GetTasks()
        {
            //Init HollydayListTmp
            HollydayListTmp = GetHollydayLists(GetTeam());

            //Process

            //DateTime startDate = cn.BusinessDays.Where(b=>b.Id==GetBusinessDayId()).SingleOrDefault().Select(b=>b.StartDate);
            int? i = GetBusinessDayId();
            if(i is null)
                throw new Exception("Error in GetTasks");

            BDT? bdt = GetBusinessDayT(i);
            if(bdt is null)
                throw new Exception("Error2 in GetTasks");
            
            TaskDTO[] myTasks = GetTasksFromT(GetTeam().Id,(BDT)bdt);

            HollydayListTmp = null;

            return myTasks;
        }

        private TaskDTO[] GetTasksFromT(int teamId, BDT businessDayT)
        {
            // return cn
            //     .Tasks
            //     .Where(t=>t.TeamId==teamId)
            //     .Where(t=>t.BusinessDT==businessDayT.TPlus)
            //     .Where(t=>t.BusinessDT==businessDayT.TMinus)
            //     .ToArray(); wrong, we can write multiple where but not on same property why?

            // return cn
            //     .Tasks
            //     .Where(t=>t.TeamId==teamId)
            //     .Where(t=>t.BusinessDT==businessDayT.TPlus || t.BusinessDT==businessDayT.TMinus)
            //     .ToArray();//this woen iswerite:
        
            // return cn
            //     .Tasks
            //     .Where(t=>t.TeamId==teamId)
            //     .Where(t=>t.BusinessDT==businessDayT.TPlus || t.BusinessDT==businessDayT.TMinus)
            //     .ToArray().Select(tsk=>new 
            //     {
            //         tsk.Priority,
            //         tsk.Title,
            //         tsk.Description,
            //         tsk.BusinessDT,
            //         tsk.PrimaryOwner,
            //         tsk.SecondaryOwner
            //     }).Cast<TaskDTO>().ToArray();not working
            //  return cn
            //                 .Tasks
            //                 .Where(t=>t.TeamId==teamId)
            //                 .Where(t=>t.BusinessDT==businessDayT.TPlus || t.BusinessDT==businessDayT.TMinus)
            //                 .ToArray().Select(tsk=>new TaskDTO
            //                 {
            //                     Priority = tsk.Priority.ToString(),
            //                     Title=tsk.Title,
            //                     Description=tsk.Description,
            //                     BusinessDT=tsk.BusinessDT,
            //                     PrimaryOwner=tsk.PrimaryOwner.UserId,
            //                     SecondaryOwner=tsk.SecondaryOwner.UserId
            //                 }).Cast<TaskDTO>().ToArray();working
            return cn
                .Tasks
                .Where(t=>t.TeamId==teamId)
                .Where(t=>t.BusinessDT==businessDayT.TPlus || t.BusinessDT==businessDayT.TMinus)
                .Select(tsk=>new TaskDTO
                {
                    Priority = Enum.GetName(typeof(Priorities),tsk.Priority),
                    Title = tsk.Title,
                    Description = tsk.Description,
                    BusinessDT = tsk.BusinessDT,
                    PrimaryOwner = tsk.PrimaryOwner.UserId,
                    SecondaryOwner = tsk.SecondaryOwner.UserId
                }).ToArray();
        }

        private BDT? GetBusinessDayT(int? businessdayId)
        {
            //BDT? bdt = new BDT();
            BDT bdt = new BDT{TPlus=0,TMinus=0,TotalTs=0};                

            DateTime startDate = cn.BusinessDays
             .Where(b=>b.Id==businessdayId)
             .Select(b=>b.StartDate)
             .SingleOrDefault();

            DateTime endDate = cn.BusinessDays
             .Where(b=>b.Id==businessdayId)
             .Select(b=>b.EndDate)
             .SingleOrDefault();

            DateTime dateForIteration = startDate;
            DateTime nowDate = DateTime.Now;
            
            while (dateForIteration<endDate)//last date is T and henc e <
            {
                bool isHollyday = IsHollydayinHollydayList(dateForIteration);
                if(isHollyday)
                    dateForIteration=dateForIteration.AddDays(1d);//dateForIteration.AddDays(1);
                else
                {
                    dateForIteration=dateForIteration.AddDays(1d);//dateForIteration.AddDays(1); this is bad, i got stuck
                    bdt.TotalTs+=1;
                    if (dateForIteration<=nowDate)
                    {
                        bdt.TPlus+=1;
                    }                
                }
            }

            bdt.TMinus=-(bdt.TotalTs-bdt.TPlus);

            return bdt;
        }

        private DateTime[] GetHollydayLists(Team team)
        {
            return cn.HollydayTeam.Where(ht=>ht.TeamId==team.Id)
                .Join(cn.HollyDays,
                    ht=>ht.HollydayId,
                    h=>h.Id,
                    (ht,h) => h.Date
                )
                .ToArray();
        }

        private bool IsHollydayinHollydayList(DateTime date)//,int teamId)
        {
            if(HollydayListTmp.Contains(date))
                return true;
            else
                {
                    if (date.DayOfWeek==DayOfWeek.Saturday || date.DayOfWeek==DayOfWeek.Sunday)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }

        internal int? GetBusinessDayId()
        {
            // BusinessDay bday = cn.BusinessDays.Where(b=>
                
            //     {b.TeamId==GetTeam().Id;}
            
            // ).SingleOrDefault();

            BusinessDay bday = cn.BusinessDays.Where(b=>b.TeamId==GetTeam().Id)
                .Where(b=>b.MonthId==GetMonthId()).SingleOrDefault();//side effect

            return bday.Id;//very bad, you gonna take back//kya karu me
        }

        int GetMonthId()
        {
            return DateTime.Now.Month;
        }

        IQueryable<DateTime> GetMonthStartDate()
        {
            return cn.BusinessDays
                .Where(b=>b.MonthId==DateTime.Now.Month)
                .Select(b=>b.StartDate);
        }

        IQueryable<DateTime> GetMonthEndDate()
        {
            return cn.BusinessDays
                .Where(b=>b.MonthId==DateTime.Now.Month)
                .Select(b=>b.EndDate);//.SingleOrDefault()
        }

        Team GetTeam()
        {
            
            Team team = cn.Teams.Where(t=>t.Id==GetEmployee().TeamId).SingleOrDefault();
            return team;
        }

        public Employee GetEmployee()
        {
            string userId = Environment.GetEnvironmentVariable("username");
            Employee emp = cn.Employees.Where(e=>e.UserId==userId).FirstOrDefault();
            return emp;
        }
    }

}