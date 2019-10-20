using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Models.Contexts
{
    public class WLMDbContext : DbContext
    {
        #region DBSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSnapShot> TaskSnapShots { get; set; }
        public DbSet<BusinessDay> BusinessDays { get; set; }
        public DbSet<Hollyday> HollyDays { get; set; }
        public DbSet<Server.Models.HollydayTeam> HollydayTeam { get; set; }
        
        #endregion

        #region

        public WLMDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=AG624599;Database=Test2;Trusted_Connection=True");
            optionsBuilder.UseSqlServer(@"Server=ShriKrishnaPC\SSDevInstance;Database=Tst;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
            
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TEAM
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Team>().HasMany(t => t.Employees).WithOne(e => e.Team).HasForeignKey(e => e.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Team>().HasMany(t => t.Tasks).WithOne(task => task.Team).HasForeignKey(task => task.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Team>().HasMany(team => team.HollydayTeams).WithOne(ht => ht.Team).HasForeignKey(ht => ht.TeamId).OnDelete(DeleteBehavior.ClientSetNull);

            //EMPLOYEE
            modelBuilder.Entity<Employee>().HasKey(emp => emp.Id);
            modelBuilder.Entity<Employee>().HasOne(emp => emp.Team).WithMany(team => team.Employees).HasPrincipalKey(team => team.Id).HasForeignKey(emp => emp.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
            //modelBuilder.Entity<Employee>().HasOne(emp => emp.Senior).WithMany(emp => emp.Juniors).HasPrincipalKey(emp => emp.Id).HasForeignKey(emp => emp.SeniorId);
            //This gave error on create at runtime migration or not?
            modelBuilder.Entity<Employee>().HasOne(emp => emp.Senior).WithMany(emp => emp.Juniors)
                .OnDelete(DeleteBehavior.ClientSetNull).HasPrincipalKey(emp => emp.Id).HasForeignKey(emp => emp.SeniorId);
            //modelBuilder.Entity<Employee>().HasMany(emp => emp.Juniors).WithOne(emp => emp.Senior).HasForeignKey(emp => emp.SeniorId);//needed
            modelBuilder.Entity<Employee>().HasMany(emp => emp.TasksOfWhichIAmPrimaryOwner)
                .WithOne(task => task.PrimaryOwner).HasForeignKey(task => task.PrimaryOwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Employee>().HasMany(emp => emp.TasksOfWhichIAmSecondaryOwner)
                .WithOne(task => task.SecondaryOwner).HasForeignKey(task => task.SecondaryOwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            //modelBuilder.Entity<Employee>().HasMany(emp => emp.TasksOfWhichIAmReviewer)
            //    .WithOne(task => task.Reviewer).HasForeignKey(task => task.ReviewerId);
            //TASK
            modelBuilder.Entity<Task>().HasKey(tsk => tsk.Id);
            modelBuilder.Entity<Task>().HasOne(tsk => tsk.PrimaryOwner).WithMany(poemp => poemp.TasksOfWhichIAmPrimaryOwner)
                .HasPrincipalKey(poemp => poemp.Id).HasForeignKey(tsk => tsk.PrimaryOwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Task>().HasOne(tsk => tsk.SecondaryOwner).WithMany(soemp => soemp.TasksOfWhichIAmSecondaryOwner)
                .HasPrincipalKey(soemp => soemp.Id).HasForeignKey(tsk => tsk.SecondaryOwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Task>().HasOne(tsk => tsk.Reviewer).WithMany(rvremp => rvremp.TasksOfWhichIAmReviewer)
                .HasPrincipalKey(rvremp => rvremp.Id).HasForeignKey(tsk => tsk.ReviewerId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Task>().HasOne(task => task.Team).WithMany(team => team.Tasks)
                .HasPrincipalKey(team => team.Id).HasForeignKey(task => task.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Task>().HasMany(task => task.TaskSnapshots).WithOne(tss => tss.Task)
                .HasForeignKey(tss => tss.TaskId).OnDelete(DeleteBehavior.ClientSetNull);
            //TASKSNAPSHOT
            modelBuilder.Entity<TaskSnapShot>().HasKey(tss => tss.Id);
            //modelBuilder.Entity<TaskSnapShot>().HasOne(tss => tss.Task).WithMany(t => t.TaskSnapshots)
            //    .HasPrincipalKey(t => t.Id).HasForeignKey(tss => tss.TaskId).OnDelete(DeleteBehavior.ClientSetNull);
            //
            modelBuilder.Entity<TaskSnapShot>().HasOne(sn => sn.AssignedBy).WithMany(em => em.SnapshotsAssignedByMe)
                .HasPrincipalKey(em => em.Id).HasForeignKey(sn => sn.AssignedById).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<TaskSnapShot>().HasOne(sn => sn.AssignedTo).WithMany(em => em.SnapshotsAssignedToMe)
                .HasPrincipalKey(em => em.Id).HasForeignKey(sn => sn.AssignedToId).OnDelete(DeleteBehavior.ClientSetNull);
            //BUSINESSDAY
            modelBuilder.Entity<BusinessDay>().HasOne(bd => bd.Team).WithMany(t => t.BusinessDays)
                   .HasPrincipalKey(t => t.Id).HasForeignKey(bd => bd.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
            //HOLLYDAY
            modelBuilder.Entity<Hollyday>().HasKey(h => h.Id);
            modelBuilder.Entity<BusinessDay>().HasKey(bd => bd.Id);
            modelBuilder.Entity<Hollyday>().HasOne(h => h.CreatedBy).WithMany(cbemp => cbemp.CreatedByMeHollydays)
                .HasPrincipalKey(cbemp => cbemp.Id).HasForeignKey(h => h.CreatedById).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Hollyday>().HasMany(h => h.HollydayTeams).WithOne(ht => ht.Hollyday)
                .HasPrincipalKey(ht => ht.Id).HasForeignKey(ht => ht.HollydayId).OnDelete(DeleteBehavior.ClientSetNull);
            //oops
            //HOLLYDYTEM
            modelBuilder.Entity<HollydayTeam>().HasKey(ht => ht.Id);
            modelBuilder.Entity<HollydayTeam>().HasOne(ht => ht.Hollyday).WithMany(h => h.HollydayTeams)
                .HasPrincipalKey(h => h.Id).HasForeignKey(ht => ht.HollydayId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<HollydayTeam>().HasOne(ht => ht.Team).WithMany(t => t.HollydayTeams)
                .HasPrincipalKey(t => t.Id).HasForeignKey(ht => ht.TeamId).OnDelete(DeleteBehavior.ClientSetNull);

            //base.OnModelCreating(modelBuilder);
        }
        
        #endregion
    }
}
