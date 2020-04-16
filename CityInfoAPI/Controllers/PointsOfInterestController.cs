 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoAPI.Controllers
{[ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

         return Ok(city.PointsOfInterest);
        }





        [HttpGet("{id}", Name= "GetPointOfAction")]
        public IActionResult GetPointsOfInterest( int cityId, int id)
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
        public IActionResult CreatePointOfInterest( int cityId,[FromBody]  PointOfInterestForCreation pointOfInterest)
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
    }
}