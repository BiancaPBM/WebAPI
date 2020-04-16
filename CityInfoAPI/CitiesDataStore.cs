using CityInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New Yorkshire",
                    Description = "beautiful",
                    PointsOfInterest= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {   Id = 1,
                            Name = "First point of interest",
                            Description = "description of first point"
                        },
                          new PointOfInterestDto
                        {  Id = 2,
                            Name = "Second point of interest",
                            Description = "description of second point"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Old Yorkshire",
                    Description = "not so beautiful",
                     PointsOfInterest= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {   Id = 1,
                            Name = "First point of interest",
                            Description = "description of first point"
                        },
                          new PointOfInterestDto
                        {   Id = 2,
                            Name = "Second point of interest",
                            Description = "description of second point"
                        }
                    }
                   
                }

            };
        }
     }
}