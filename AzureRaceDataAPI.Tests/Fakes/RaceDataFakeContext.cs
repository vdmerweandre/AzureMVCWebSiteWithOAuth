using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.datacontext;
using AzureRaceDataWebAPI.Models;

namespace AzureRaceDataWebAPI.Tests.Fakes
{
    public class RaceDataFakeContext: FakeDBContext
    {
        public RaceDataFakeContext()
        {
            AddFakeDbSet<Meeting, MeetingDBSet>();
            AddFakeDbSet<Race, RaceDBSet>();
            AddFakeDbSet<Runner, RunnerDBSet>();
            AddFakeDbSet<Pool, PoolDBSet>();
        }
    }
}
