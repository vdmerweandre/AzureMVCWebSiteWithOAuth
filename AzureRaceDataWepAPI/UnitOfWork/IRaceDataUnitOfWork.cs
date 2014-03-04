using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.repositories;
using AzureRaceDataWebAPI.Models;

namespace AzureRaceDataWebAPI.UnitOfWork
{
    public interface IRaceDataUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        void Commit();

        //Repositories
        IRepository<Meeting> MeetingsRepository { get; }
        IRepository<Race> RacesRepository { get; }
        IRepository<Runner> RunnersRepository { get; }
        IRepository<Pool> PoolsRepository { get; }
    }
}