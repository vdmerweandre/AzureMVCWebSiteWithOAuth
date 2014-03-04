using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.datacontext;
using Persistence.repositories;
using AzureRaceDataWebAPI.Models;
using AzureRaceDataWebAPI.Context;
using Logging;
using System.Data.Entity.Validation;

namespace AzureRaceDataWebAPI.UnitOfWork
{
    /// <summary>
    /// The "Unit of Work"
    ///     1) decouples the repos from the console,controllers,ASP.NET pages....
    ///     2) decouples the context and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a applicant.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data .
    /// http://www.codeproject.com/Articles/615499/Models-POCO-Entity-Framework-and-Data-Patterns
    /// </remarks>
    public class RaceDataUnitOfWork : IRaceDataUnitOfWork, IDisposable
    {
        private IDataContext _context { get; set; }
        private ILogger _logger { get; set; }

        //To be used with EF DB Context
        public RaceDataUnitOfWork(ILogger logger)
        {
            CreateContext();
            _logger = logger;
        }

        //To be used with Fake DB Context 
        public RaceDataUnitOfWork(IDataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Repositories

        private IRepository<Meeting> _meetingsRepository;
        private IRepository<Race> _racesRepository;
        private IRepository<Runner> _runnersRepository;
        private IRepository<Pool> _poolsRepository;

        public IRepository<Meeting> MeetingsRepository
        {
            get
            {
                if (_meetingsRepository == null)
                {
                    _meetingsRepository = new GenericRepository<Meeting>(_context, _logger);
                }
                return _meetingsRepository;
            }
        }

        public IRepository<Race> RacesRepository
        {
            get
            {
                if (_racesRepository == null)
                {
                    _racesRepository = new GenericRepository<Race>(_context, _logger);
                }
                return _racesRepository;
            }
        }

        public IRepository<Runner> RunnersRepository
        {
            get
            {
                if (_runnersRepository == null)
                {
                    _runnersRepository = new GenericRepository<Runner>(_context, _logger);
                }
                return _runnersRepository;
            }
        }

        public IRepository<Pool> PoolsRepository
        {
            get
            {
                if (_poolsRepository == null)
                {
                    _poolsRepository = new GenericRepository<Pool>(_context, _logger);
                }
                return _poolsRepository;
            }
        }

        #endregion

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _context.GetValidationErrors();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected void CreateContext()
        {
            _context = new RaceDataDBContext();

            _context.Setup();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}