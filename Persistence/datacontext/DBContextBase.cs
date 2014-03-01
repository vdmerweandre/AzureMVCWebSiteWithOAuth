using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Data.Entity.Validation;

namespace Persistence.datacontext
{
    public class DBContextBase: DbContext, IDataContext
    {
        public DBContextBase()
            : base("name=appdb")
        {

        }

        //public DBContextBase(string nameOrConnectionString) 
        //    : base(nameOrConnectionString)
        //{

        //}

        public void Setup()
        {
            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            //get the applicants
            base.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            base.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            base.Configuration.ValidateOnSaveEnabled = false;

            //context.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        //public void ApplyStateChanges()
        //{
        //    foreach(DbEntityEntry dbEntityEntry in ChangeTracker.Entries())
        //    {
        //        var entityState = dbEntityEntry.Entity as IObjectState;
        //        if (entityState == null)
        //            throw new InvalidCastException("All entites must implement the IObjectState interface, " +
        //                                       "this interface must be implemented so each entites state can explicitely determined when updating graphs.");
        //        dbEntityEntry.State = StateHelper.ConvertState(entityState.State);
        //    }
        //}

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            //ApplyStateChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            //ApplyStateChanges();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //ApplyStateChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public DbEntityEntry Entry(object o)
        {
            return base.Entry(o); 
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return base.GetValidationErrors();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new MeetingsConfig());
            modelBuilder.Configurations.Add(new RaceConfig());
            modelBuilder.Configurations.Add(new PoolConfig());
            modelBuilder.Configurations.Add(new RunnerConfig());
            modelBuilder.Configurations.Add(new RaceStartItemConfig());
            modelBuilder.Configurations.Add(new CoverageItemConfig());
            modelBuilder.Configurations.Add(new DateTimeRecordConfig());
        }
    }

    public class RunnerConfig : EntityTypeConfiguration<Persistence.models.Runner>
    {
        public RunnerConfig()
        {
            // Relationships.
            //Runner has relation with race.
            //race has many runners (one race has many runners )
            //forign key on skill is RaceID
            this.HasRequired(t => t.Race)
                .WithMany(g => g.Runners)
                .HasForeignKey(d => d.RaceID);
        }
    }

    public class PoolConfig : EntityTypeConfiguration<Persistence.models.Pool>
    {
        public PoolConfig()
        {
            // Relationships.
            //Pool has relation with race.
            //race has many pools (one race has many pools )
            //forign key on skill is PoolID
            this.HasRequired(t => t.Race)
                .WithMany(g => g.Pools)
                .HasForeignKey(d => d.RaceID);
        }
    }

    public class RaceConfig : EntityTypeConfiguration<Persistence.models.Race>
    {
        public RaceConfig()
        {
            // Relationships
            //Race has relation with meeting.
            //Meeting has many races (one meeting has many races )
            //forign key on Race is MeetingID
            //this.HasRequired(t => t.Meeting)
            //    .WithMany(g => g.Races)
            //    .HasForeignKey(d => d.MeetingID);
        }
    }

    public class RaceStartItemConfig : EntityTypeConfiguration<Persistence.models.RaceStartItem>
    {
        public RaceStartItemConfig()
        {
            // Relationships
            //StringRecord has relation with meeting.
            //Meeting has many RaceStarts (one meeting has many races )
            //forign key on Race is MeetingID
            this.HasRequired(t => t.Meeting)
                .WithMany(g => g.RaceStarts)
                .HasForeignKey(d => d.MeetingID)
                .WillCascadeOnDelete(true);
        }
    }

    public class CoverageItemConfig : EntityTypeConfiguration<Persistence.models.CoverageItem>
    {
        public CoverageItemConfig()
        {
            // Relationships
            //CoverageItem has relation with meeting.
            //Meeting has many Coverages (one meeting has many races )
            //forign key on Race is MeetingID
            this.HasRequired(t => t.Meeting)
                .WithMany(g => g.Coverages)
                .HasForeignKey(d => d.MeetingID)
                .WillCascadeOnDelete(true);
        }
    }

    public class DateTimeRecordConfig : EntityTypeConfiguration<Persistence.models.DateTimeRecord>
    {
        public DateTimeRecordConfig()
        {
            // Relationships
            //DateTimeRecord has relation with JumpTimeHistory.
            //JumpTimeHistory has many JumptimeUpdates (one meeting has many races )
            //forign key on Race is JumpTimeHistoryID
            this.HasRequired(t => t.JumpTimeHistory)
                .WithMany(g => g.JumptimeUpdates)
                .HasForeignKey(d => d.JumpTimeHistoryID)
                .WillCascadeOnDelete(true);
        }
    }

    public class MeetingsConfig : EntityTypeConfiguration<Persistence.models.Meeting>
    {
        public MeetingsConfig()
        {

        }
    }

    // EF follows a Code based Configration model and will look for a class that
    // derives from DbConfiguration for executing any Connection Resiliency strategies
    public class EFConfiguration : DbConfiguration
    {
        public EFConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
