using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.models;
using Persistence.datacontext;
using Persistence.unitOfWork;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureRaceDataWebAPI.Tests.Repository
{
    [TestClass]
    public class MeetingsRepositoryTest
    {
        private bool dbTest = false;
        private List<Meeting> mockmeetings;

        [TestInitialize]
        public void Init()
        {
            MockMeetings mock = new MockMeetings();
            mockmeetings = mock.Meetings;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public async Task GetMeetings_Query_Get()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Setup
                await PopulateData(uow);

                IEnumerable<Meeting> actual = uow.MeetingsRepository.Query().Get().ToList();

                Assert.IsInstanceOfType(actual, typeof(IEnumerable<Meeting>));
                Assert.AreEqual(2, actual.Count());
                Assert.AreEqual(actual.FirstOrDefault().VenueName, mockmeetings.FirstOrDefault().VenueName);
                Assert.AreEqual(actual.FirstOrDefault().NumberOfRace, mockmeetings.FirstOrDefault().NumberOfRace);
                Assert.AreEqual(actual.FirstOrDefault().Coverages.Count(), mockmeetings.FirstOrDefault().Coverages.Count());
                Assert.AreEqual(actual.FirstOrDefault().RaceStarts.Count(), mockmeetings.FirstOrDefault().RaceStarts.Count());
            }
        }

        [TestMethod]
        public async Task GetMeetings_Query_GetAsync()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Setup
                await PopulateData(uow);

                IEnumerable<Meeting> actual = await uow.MeetingsRepository.Query().GetAsync();

                Assert.IsInstanceOfType(actual, typeof(IEnumerable<Meeting>));
                Assert.AreEqual(2, actual.Count());
                Assert.AreEqual(actual.FirstOrDefault().VenueName, mockmeetings.FirstOrDefault().VenueName);
                Assert.AreEqual(actual.FirstOrDefault().NumberOfRace, mockmeetings.FirstOrDefault().NumberOfRace);
                Assert.AreEqual(actual.FirstOrDefault().Coverages.Count(), mockmeetings.FirstOrDefault().Coverages.Count());
                Assert.AreEqual(actual.FirstOrDefault().RaceStarts.Count(), mockmeetings.FirstOrDefault().RaceStarts.Count());
            }
        }

        [TestMethod]
        public async Task FindAsyncWithCancellationtoken()
        {
            Assert.Fail("FindAsyncWithCancellationtoken Not tested yet");
        }

        [TestMethod]
        public async Task SqlQuery()
        {
            Assert.Fail("SqlQuery Not tested yet");
        }
        

        [TestMethod]
        public async Task FindMeetingById()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                await PopulateData(uow);

                var meeting = uow.MeetingsRepository.Find(1);

                Assert.IsNotNull(meeting);
                Assert.AreEqual(meeting.Id, 1);
            }
        }

        [TestMethod]
        public async Task FindMeetingByIdAsync()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                await PopulateData(uow);

                var meeting = await uow.MeetingsRepository.FindAsync(1);

                Assert.IsNotNull(meeting);
                Assert.AreEqual(meeting.Id, 1);
            }
        }

        [TestMethod]
        public async Task UpdateMeeting()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                await PopulateData(uow);

                Meeting updatedEntity = mockmeetings.ElementAt(0);
                updatedEntity.VenueName += updatedEntity.VenueName + " Updated";

                uow.MeetingsRepository.Update(updatedEntity);

                var meeting = await uow.MeetingsRepository.FindAsync(0);

                Assert.IsNotNull(meeting);
                Assert.AreEqual(meeting.Id, 0);
                Assert.AreEqual(meeting.VenueName, updatedEntity.VenueName);
            }
        }

        [TestMethod]
        public async Task DeleteMeetingById()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                await PopulateData(uow);

                uow.MeetingsRepository.Delete(1);

                await uow.CommitAsync();

                var meeting = uow.MeetingsRepository.Find(1);

                Assert.IsNull(meeting);
            }
        }

        [TestMethod]
        public async Task DeleteMeetingByMeeting()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                await PopulateData(uow);

                uow.MeetingsRepository.Delete(mockmeetings.ElementAt(0));

                await uow.CommitAsync();

                var meeting = uow.MeetingsRepository.Find(0);

                Assert.IsNull(meeting);
            }
        }

        private IDataContext GetDataContext()
        {
            if (dbTest)
            {
                return new DBContextBase();
            }
            else
            {
                return new Fakes.RaceDataFakeContext();
            }

        }

        private async Task PopulateData(IRaceDataUnitOfWork uow)
        {
            //clear previously added data from DB
            var ms = uow.MeetingsRepository.Query().Get();

            foreach (var m in ms)
            {
                uow.MeetingsRepository.Delete(m.Id);
            }
            var x = await uow.CommitAsync();

            uow.MeetingsRepository.Create(mockmeetings.ElementAt(0));
            uow.MeetingsRepository.Create(mockmeetings.ElementAt(1));

            await uow.CommitAsync();
        }

        //[TestMethod]
        //public void DeepLoadProductWithSupplier()
        //{
        //    using (IDataContext northwindFakeContext = new NorthwindFakeContext())
        //    using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
        //    {
        //        unitOfWork.Repository<Supplier>().Insert(new Supplier { SupplierID = 1, CompanyName = "Nokia", City = "Tampere", Country = "Finland", ContactName = "Stephen Elop", ContactTitle = "CEO" });
        //        unitOfWork.Repository<Product>().Insert(new Product { ProductID = 2, Discontinued = true, ProductName = "Nokia Lumia 1520", SupplierID = 1, ObjectState = ObjectState.Added });

        //        unitOfWork.Save();

        //        var product = unitOfWork.Repository<Product>().Find(2);

        //        Assert.IsNotNull(product);
        //    }
        //}
    }
}
