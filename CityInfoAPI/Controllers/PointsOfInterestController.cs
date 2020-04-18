 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfoAPI.Controllers
{[ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            try
            {
                throw new Exception("example Ex");


            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city",ex);
                return StatusCode(500, "Something wrong happened");
            }
            return Ok(city.PointsOfInterest);
        }





        [HttpGet("{id}", Name = "GetPointOfAction")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (point == null)
                return NotFound();

            return Ok(point);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody]  PointOfInterestForCreation pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var maxIdPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            if (city == null)
                return NotFound();
            var newPoint = new PointOfInterestDto
            {
                Id = ++maxIdPointOfInterest,
                Description = pointOfInterest.Description,
                Name = pointOfInterest.Name,
            };

            city.PointsOfInterest.Add(newPoint);
            return CreatedAtRoute("GetPointOfAction", new { cityId = city.Id, id = newPoint.Id }, newPoint);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, [FromBody]PointOfInterestForUpdate pointOfIntrerest, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return BadRequest();
            }
            var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (point == null)
            {
                return BadRequest();
            }

            point.Name = pointOfIntrerest.Name;
            point.Description = pointOfIntrerest.Description;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody]JsonPatchDocument<PointOfInterestForUpdate> patchDocument)
        {

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
                return NotFound();
            var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (point == null)
                return NotFound();

            var pointToPatch = new PointOfInterestForUpdate()
            {
                Name = point.Name,
                Description = point.Description
            };


            patchDocument.ApplyTo(pointToPatch, ModelState);
            //luam formatul json din body, ii facem apply pe un model si apoi setam valorile
            point.Name = pointToPatch.Name;
            point.Description = pointToPatch.Description;
            if (!ModelState.IsValid)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{id}")]
         public IActionResult DeletePointOFinterest(int cityId,int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return BadRequest();
            city.PointsOfInterest.Remove(city.PointsOfInterest.FirstOrDefault(p => p.Id == id));
            return NoContent();

        }
    }
}