using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Persistence.models;
using Persistence.datacontext;
using Logging;
using Persistence.unitOfWork;
using System.Web.Http.Cors;

namespace AzureRaceDataWebAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Persistence.models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Meeting>("Meetings");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */

    //AsyncEntitySetController
    //http://msdn.microsoft.com/en-us/library/jj890498(v=vs.111).aspx
    //http://localhost:22279/odata/Meetings()?$expand=RaceStarts,Coverages&$top=1

    [EnableCors(origins: "http://localhost:6435", headers: "*", methods: "*")]
    public class MeetingsController : ODataController
    {
        private IRaceDataUnitOfWork _uow;

        public MeetingsController()
        {
            //Empty - needed by Unity
        }

        public MeetingsController(IRaceDataUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET odata/Meetings
        /// <summary>
        /// An async endpoint too retrieve all meetings from
        /// </summary>
        /// <returns>OkNegotiatedContentResult<IEnumerable<Meeting>></returns>
        [Queryable]
        public async Task<IHttpActionResult> Get()
        {
            IEnumerable<Meeting> meetings = await _uow.MeetingsRepository.Query().GetAsync();
            return Ok(meetings);
        }

        // GET odata/Meetings(5)
        [Queryable]
        public async Task<IHttpActionResult> Get([FromODataUri] int key)
        {
            IEnumerable<Meeting> meetings = await _uow.MeetingsRepository.Query().Filter(m => m.Id == key).GetAsync();
            
            if (meetings == null)
                return NotFound();

            return Ok(meetings);
        }

        // POST odata/Meetings
        public async Task<IHttpActionResult> Post(Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.MeetingsRepository.Create(meeting);
            await _uow.CommitAsync();

            Uri uri = new Uri("http://localhost:1234/odata/meetings/" + meeting.Id.ToString());

            return Created(uri, meeting);
        }

        // PUT odata/Meetings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _uow.MeetingsRepository.Update(meeting);
                await _uow.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.MeetingsRepository.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(meeting);
        }

        // PATCH odata/Meetings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Meeting> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    

            Meeting meeting = await _uow.MeetingsRepository.FindAsync(key);
            if (meeting == null)
            {
                return NotFound();
            }
            
            patch.Patch(meeting);

            try
            {
                _uow.MeetingsRepository.Update(meeting);
                await _uow.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.MeetingsRepository.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(meeting);
        }

        // DELETE odata/Meetings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Meeting meeting = await _uow.MeetingsRepository.FindAsync(key);
            if (meeting == null)
            {
                return NotFound();
            }

            _uow.MeetingsRepository.Delete(meeting.Id);
            await _uow.CommitAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> HandleUnmappedRequest(ODataPath odataPath)
        {
            //TODO: add logic and proper return values
            return NotFound();
        }

        //#region Links
        //// Create a relation from Meeting to Category or Supplier, by creating a $link entity.
        //// POST <controller>(key)/$links/Category
        //// POST <controller>(key)/$links/Supplier
        ///// <summary>
        ///// Handle POST and PUT requests that attempt to create a link between two entities.
        ///// </summary>
        ///// <param name="key">The key of the entity with the navigation property.</param>
        ///// <param name="navigationProperty">The name of the navigation property.</param>
        ///// <param name="link">The URI of the entity to link.</param>
        ///// <returns>A Task that completes when the link has been successfully created.</returns>
        //[AcceptVerbs("POST", "PUT")]
        //public override async Task CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        //{
        //    var entity = await _uow.MeetingsRepository.FindAsync(key);

        //    if (entity == null)
        //        throw Request.EntityNotFound();

        //    switch (navigationProperty)
        //    {
        //        case "Category":
        //            var categoryKey = Request.GetKeyValue<int>(link);
        //            var category = await _uow.Repository<Category>().FindAsync(categoryKey);

        //            if (category == null)
        //                throw Request.EntityNotFound();

        //            entity.Category = category;
        //            break;

        //        case "Supplier":
        //            var supplierKey = Request.GetKeyValue<int>(link);
        //            var supplier = await _uow.Repository<Supplier>().FindAsync(supplierKey);

        //            if (supplier == null)
        //                throw Request.EntityNotFound();

        //            entity.Supplier = supplier;
        //            break;

        //        default:
        //            await base.CreateLink(key, navigationProperty, link);
        //            break;
        //    }
        //    await _uow.SaveAsync();
        //}

        //// Remove a relation, by deleting a $link entity
        //// DELETE <controller>(key)/$links/Category
        //// DELETE <controller>(key)/$links/Supplier
        ///// <summary>
        ///// Handle DELETE requests that attempt to break a relationship between two entities.
        ///// </summary>
        ///// <param name="key">The key of the entity with the navigation property.</param>
        ///// <param name="relatedKey">The key of the related entity.</param>
        ///// <param name="navigationProperty">The name of the navigation property.</param>
        ///// <returns>Task.</returns>
        //public override async Task DeleteLink([FromODataUri] int key, string relatedKey, string navigationProperty)
        //{
        //    var entity = await _uow.Repository<Meeting>().FindAsync(key);

        //    if (entity == null)
        //        throw Request.EntityNotFound();

        //    switch (navigationProperty)
        //    {
        //        case "Category":
        //            entity.Category = null;
        //            break;

        //        case "Supplier":
        //            entity.Supplier = null;
        //            break;

        //        default:
        //            await base.DeleteLink(key, relatedKey, navigationProperty);
        //            break;
        //    }

        //    await _uow.SaveAsync();
        //}

        //// Remove a relation, by deleting a $link entity
        //// DELETE <controller>(key)/$links/Category
        //// DELETE <controller>(key)/$links/Supplier
        ///// <summary>
        ///// Handle DELETE requests that attempt to break a relationship between two entities.
        ///// </summary>
        ///// <param name="key">The key of the entity with the navigation property.</param>
        ///// <param name="navigationProperty">The name of the navigation property.</param>
        ///// <param name="link">The URI of the entity to remove from the navigation property.</param>
        ///// <returns>Task.</returns>
        //public override async Task DeleteLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        //{
        //    var entity = await _uow.Repository<Meeting>().FindAsync(key);

        //    if (entity == null)
        //        throw Request.EntityNotFound();

        //    switch (navigationProperty)
        //    {
        //        case "Category":
        //            entity.Category = null;
        //            break;

        //        case "Supplier":
        //            entity.Supplier = null;
        //            break;

        //        default:
        //            await base.DeleteLink(key, navigationProperty, link);
        //            break;
        //    }

        //    await _uow.SaveAsync();
        //}
        //#endregion Links

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
