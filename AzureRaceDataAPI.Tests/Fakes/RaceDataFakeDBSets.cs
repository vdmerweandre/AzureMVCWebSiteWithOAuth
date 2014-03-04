using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Persistence.datacontext;
using AzureRaceDataWebAPI.Models;

namespace AzureRaceDataWebAPI.Tests.Fakes
{
    public class MeetingDBSet: FakeDBSet<Meeting>
    {
        public override Meeting Find(params object[] keyValues)
        {
            return this.Local.SingleOrDefault(t => t.Id == (int) keyValues.FirstOrDefault());
        }

        public override Task<Meeting> FindAsync(params object[] keyValues)
        {
            return Task<Meeting>.Run(() => Find(keyValues));
        }

        public override Task<Meeting> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task<Meeting>.Run(() => Find(keyValues));
        }
    }

    public class RaceDBSet : FakeDBSet<Race>
    {
        public override Race Find(params object[] keyValues)
        {
            return this.Local.SingleOrDefault(t => t.Id == (int)keyValues.FirstOrDefault());
        }

        public override Task<Race> FindAsync(params object[] keyValues)
        {
            return Task<Race>.Run(() => Find(keyValues));
        }

        public override Task<Race> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task<Race>.Run(() => Find(keyValues));
        }
    }

    public class RunnerDBSet : FakeDBSet<Runner>
    {
        public override Runner Find(params object[] keyValues)
        {
            return this.Local.SingleOrDefault(t => t.Id == (int)keyValues.FirstOrDefault());
        }

        public override Task<Runner> FindAsync(params object[] keyValues)
        {
            return Task<Runner>.Run(() => Find(keyValues));
        }

        public override Task<Runner> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task<Runner>.Run(() => Find(keyValues));
        }
    }

    public class PoolDBSet : FakeDBSet<Pool>
    {
        public override Pool Find(params object[] keyValues)
        {
            return this.Local.SingleOrDefault(t => t.Id == (int)keyValues.FirstOrDefault());
        }

        public override Task<Pool> FindAsync(params object[] keyValues)
        {
            return Task<Pool>.Run(() => Find(keyValues));
        }

        public override Task<Pool> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task<Pool>.Run(() => Find(keyValues));
        }
    }
}
