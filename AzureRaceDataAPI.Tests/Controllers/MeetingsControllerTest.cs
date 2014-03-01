using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureRaceDataWebAPI;
using AzureRaceDataWebAPI.Controllers;
using Persistence;
using System.Threading.Tasks;
using System.Web.Http.OData.Results;
using System.Web.Http.OData;
using System.Net;
using Persistence.models;
using Persistence.datacontext;
using Persistence.unitOfWork;
using Logging;

namespace AzureRaceDataWebAPI.Tests.Controllers
{
    [TestClass]
    public class MeetingsControllerTest
    {
        private bool dbTest = true;
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
        public async Task Get()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow);

                //Act
                IHttpActionResult response = await controller.Get();
                OkNegotiatedContentResult<IEnumerable<Meeting>> actual = response as OkNegotiatedContentResult<IEnumerable<Meeting>>;

                Meeting mock1 = mockmeetings.ElementAt(0);
                Meeting mock2 = mockmeetings.ElementAt(1);

                //Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(2, actual.Content.Count());
                Assert.AreEqual(actual.Content.ElementAt(0).VenueName, mock1.VenueName);
                Assert.AreEqual(actual.Content.ElementAt(0).NumberOfRace, mock1.NumberOfRace);
                Assert.AreEqual(actual.Content.ElementAt(0).Coverages.Count(), mock1.Coverages.Count());
                Assert.AreEqual(actual.Content.ElementAt(0).RaceStarts.Count(), mock1.RaceStarts.Count());
                Assert.AreEqual(actual.Content.ElementAt(1).VenueName, mock2.VenueName);
                Assert.AreEqual(actual.Content.ElementAt(1).NumberOfRace, mock2.NumberOfRace);
                Assert.AreEqual(actual.Content.ElementAt(1).Coverages.Count(), mock2.Coverages.Count());
                Assert.AreEqual(actual.Content.ElementAt(1).RaceStarts.Count(), mock2.RaceStarts.Count());
            }
        }

        [TestMethod]
        public async Task Get_FromODataURI()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow, false);

                Meeting mock = mockmeetings.ElementAt(1);
                //Act
                IHttpActionResult post_response = await controller.Post(mock);
                CreatedNegotiatedContentResult<Meeting> post_actual = post_response as CreatedNegotiatedContentResult<Meeting>;

                // Assert
                Assert.IsNotNull(post_actual, "A response equal to null is unexpected!");

                //Act
                IHttpActionResult response = await controller.Get(post_actual.Content.Id);
                OkNegotiatedContentResult<IEnumerable<Meeting>> actual = response as OkNegotiatedContentResult<IEnumerable<Meeting>>;

                //Assert
                // Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(actual.Content.ElementAt(0).VenueName, mock.VenueName);
                Assert.AreEqual(actual.Content.ElementAt(0).NumberOfRace, mock.NumberOfRace);
                Assert.AreEqual(actual.Content.ElementAt(0).Coverages.Count(), mock.Coverages.Count());
                Assert.AreEqual(actual.Content.ElementAt(0).RaceStarts.Count(), mock.RaceStarts.Count());
            }
        }

        [TestMethod]
        public async Task Post()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow, false);

                //Act
                IHttpActionResult response = await controller.Post(mockmeetings.ElementAt(1));
                CreatedNegotiatedContentResult<Meeting> actual = response as CreatedNegotiatedContentResult<Meeting>;

                //Assert
                // Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(actual.Content.VenueName, mockmeetings.ElementAt(1).VenueName);
                Assert.AreEqual(actual.Location, "http://localhost:1234/odata/meetings/" + mockmeetings.ElementAt(1).Id.ToString());

                IEnumerable<Meeting> check_meetings = await uow.MeetingsRepository.Query().GetAsync();
                Assert.IsNotNull(check_meetings, "A response equal to null is unexpected!");
                Assert.AreEqual(check_meetings.Count(),1);
            }
        }

        [TestMethod]
        public async Task Put()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow, false);

                var expected = mockmeetings.ElementAt(0);
                expected.VenueName += " Updated!";

                //Act
                IHttpActionResult post_response = await controller.Post(mockmeetings.ElementAt(0));
                CreatedNegotiatedContentResult<Meeting> post_actual = post_response as CreatedNegotiatedContentResult<Meeting>;

                // Assert
                Assert.IsNotNull(post_actual, "A response equal to null is unexpected!");
              
                //Act
                IHttpActionResult response = await controller.Put(post_actual.Content.Id, expected);
                UpdatedODataResult<Meeting> actual = response as UpdatedODataResult<Meeting>;

                //Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(actual.Entity.VenueName, expected.VenueName);
                Assert.AreEqual(actual.Entity.NumberOfRace, expected.NumberOfRace);
                Assert.AreEqual(actual.Entity.Coverages.Count(), expected.Coverages.Count());
                Assert.AreEqual(actual.Entity.RaceStarts.Count(), expected.RaceStarts.Count());  
            }
        }

        [TestMethod]
        public async Task Patch()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow, false);

                // Expected
                Meeting expected = mockmeetings.ElementAt(0);

                Delta<Meeting> delta = new Delta<Meeting>(typeof(Meeting));
                delta.TrySetPropertyValue("VenueName", expected.VenueName + " Patched");

                //Act
                IHttpActionResult post_response = await controller.Post(mockmeetings.ElementAt(0));
                CreatedNegotiatedContentResult<Meeting> post_actual = post_response as CreatedNegotiatedContentResult<Meeting>;

                // Assert
                Assert.IsNotNull(post_actual, "A response equal to null is unexpected!");

                //Act
                IHttpActionResult response = await controller.Patch(post_actual.Content.Id, delta);
                UpdatedODataResult<Meeting> actual = response as UpdatedODataResult<Meeting>;

                // Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(actual.Entity.VenueName, expected.VenueName);
                Assert.AreEqual(actual.Entity.NumberOfRace, expected.NumberOfRace);
                Assert.AreEqual(actual.Entity.Coverages.Count(), expected.Coverages.Count());
                Assert.AreEqual(actual.Entity.RaceStarts.Count(), expected.RaceStarts.Count());
            }
        }

        [TestMethod]
        public async Task Patch_NotExistingEntity()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow);

                // Expected
                Meeting expected = mockmeetings.ElementAt(1);

                Delta<Meeting> delta = new Delta<Meeting>(typeof(Meeting));
                delta.TrySetPropertyValue("VenueName", expected.VenueName + " Patched");

                //Act
                IHttpActionResult response = await controller.Patch(1, delta);

                // Assert
                Assert.IsNotNull(response, "A response equal to null is unexpected!");
                Assert.IsInstanceOfType(response, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            using (IDataContext context = GetDataContext())
            using (IRaceDataUnitOfWork uow = new RaceDataUnitOfWork(context, new Logger()))
            {          
                //Arrange
                MeetingsController controller = new MeetingsController(uow);

                await SetupData(controller, uow);

                // Expected
                Meeting expected = mockmeetings.ElementAt(1);

                //Act
                IHttpActionResult response = await controller.Delete(expected.Id);
                StatusCodeResult actual = response as StatusCodeResult;

                // Assert
                Assert.IsNotNull(actual, "A response equal to null is unexpected!");
                Assert.AreEqual(actual.StatusCode, HttpStatusCode.NoContent);
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

        private async Task SetupData(MeetingsController controller, IRaceDataUnitOfWork uow, bool withNewData = true)
        {
            //clear previously added data from DB
            var ms = uow.MeetingsRepository.Query().Get();

            foreach (var m in ms)
            {
                uow.MeetingsRepository.Delete(m.Id);
            }
            var x = await uow.CommitAsync();

            if (withNewData)
            {
                //Setup
                IHttpActionResult post_response = await controller.Post(mockmeetings.ElementAt(0));
                CreatedNegotiatedContentResult<Meeting> post_actual = post_response as CreatedNegotiatedContentResult<Meeting>;
                // Assert
                Assert.IsNotNull(post_actual, "A response equal to null is unexpected!");
                post_response = await controller.Post(mockmeetings.ElementAt(1));
                post_actual = post_response as CreatedNegotiatedContentResult<Meeting>;
                // Assert
                Assert.IsNotNull(post_actual, "A response equal to null is unexpected!");
            }
        }
    }
}
