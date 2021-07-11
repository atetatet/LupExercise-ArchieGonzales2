using LupExercise_ArchieGonzales.Controllers.Base;
using LupExercise_ArchieGonzales.DAL;
using LupExercise_ArchieGonzales.DAL.Entities;
using LupExercise_ArchieGonzales.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace LupExercise_ArchieGonzales.Controllers
{
    [EnableCors("*", "*", "*")]
    [Route("api/events")]
    [ApiController]
    public class EventsController : AbstractAPIController
    {
        private readonly UserManager<Users> _userManager;
        public EventsController(ApplicationDbContext dataContext, ILogger<EventsController> logger, UserManager<Users> userManager) : base(dataContext, logger)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous] // Testing only
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Events>> Get()
        {
            var items = await _dataContext.Events.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{eventId}")]
        [AllowAnonymous] // Testing only
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Events>> GetById(int eventId)
        {
            var items = await _dataContext.Events.Where(x => x.EventId == eventId).FirstOrDefaultAsync();
            return Ok(items);
        }

        [HttpPost]
        [AllowAnonymous] // Testing only
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Insert(EventsViewModel items)
        {
            string message = "Something went wrong, please try again";

            DateTime? startDate = items.StartDate;
            DateTime? endDate = items.EndDate;

            // Check required fields
            if (string.IsNullOrEmpty(items.EventName))
            {
                message = "Event name is Required";
                return BadRequest(message);
            }
            else if (!startDate.HasValue)
            {
                message = "Start date is required";
                return BadRequest(message);
            }
            else if (!endDate.HasValue)
            {
                message = "End date is required";
                return BadRequest(message);
            }

            // Check start date and end date
            if(items.StartDate > items.EndDate)
            {
                message = "The start and end dates must not overlap (i.e., the end date must be after or equal to the start date)";
                return BadRequest(message);
            }

            // Check event name is existing
            var events = await _dataContext.Events.Where(x => x.EventName == items.EventName).FirstOrDefaultAsync();

            if(events != null)
            {
                message = "Event name must be unique";
                return BadRequest(message);
            }

            try
            {
                var model = new Events
                {
                    EventName = items.EventName,
                    EventDescription = items.EventDescription,
                    EventTimezone = items.EventTimezone,
                    StartDate = items.StartDate.ToUniversalTime(),
                    EndDate = items.EndDate.ToUniversalTime(),
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    ModifiedDate = DateTime.Now.ToUniversalTime()
                };

                _dataContext.Events.Add(model);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message + ex);
                return BadRequest(message);
            }

            return Ok();
        }

        [HttpPut("{eventId}")]
        [AllowAnonymous] // Testing only
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update(int eventId, EventsViewModel items)
        {
            string message = "Something went wrong, please try again";

            var eventEntity = await _dataContext.Events.Where(x => x.EventId == eventId).FirstOrDefaultAsync();

            if(eventEntity == null)
            {
                message = "Event does not exists";
                return BadRequest(message);
            }

            DateTime? startDate = items.StartDate;
            DateTime? endDate = items.EndDate;

            // Check required fields
            if (string.IsNullOrEmpty(items.EventName))
            {
                message = "Event name is Required";
                return BadRequest(message);
            }
            else if (!startDate.HasValue)
            {
                message = "Start date is required";
                return BadRequest(message);
            }
            else if (!endDate.HasValue)
            {
                message = "End date is required";
                return BadRequest(message);
            }

            // Check start date and end date
            if (items.StartDate > items.EndDate)
            {
                message = "The start and end dates must not overlap (i.e., the end date must be after or equal to the start date)";
                return BadRequest(message);
            }

            // Check event name is existing
            var events = await _dataContext.Events.Where(x => x.EventName == items.EventName).FirstOrDefaultAsync();

            if (events != null)
            {
                message = "Event name must be unique";
                return BadRequest(message);
            }

            try
            {
                eventEntity.EventName = items.EventName;
                eventEntity.EventDescription = items.EventDescription;
                eventEntity.EventTimezone = items.EventTimezone;
                eventEntity.StartDate = items.StartDate.ToUniversalTime();
                eventEntity.EndDate = items.EndDate.ToUniversalTime();
                eventEntity.CreatedDate = DateTime.Now.ToUniversalTime();
                eventEntity.ModifiedDate = DateTime.Now.ToUniversalTime();

                _dataContext.Events.Update(eventEntity);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message + ex);
                return BadRequest(message);
            }

            return Ok();
        }

        [HttpDelete("{eventId}")]
        [AllowAnonymous] // Testing only
        //[Authorize(Roles = "Admin,Worker,Customer")]
        public async Task<IActionResult> UpdateAcceptBooking(int eventId)
        {
            string message = "Something went wrong, please try again";
            var eventEntity = await _dataContext.Events.Where(x => x.EventId == eventId).FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                message = "Event does not exists";
                return BadRequest(message);
            }

            try
            {
                _dataContext.Events.Remove(eventEntity);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message + ex);
                return BadRequest(message);
            }

            return Ok();
        }
    }
}
