using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Journey.Infrastructure;
using Journey.Persistence.Entities;
using Journey.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Journey.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CityController : ControllerBase
    {
        private readonly JourneyUnit _unit;
        private readonly IMapper _mapper;
        public CityController(JourneyUnit unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CityDTO>> GetCities()
        {
            var cities = _unit.Cities.Get();
            if (cities == default)
            {
                return NotFound();
            }

            var citiesDTO = _mapper.Map<IEnumerable<CityDTO>>(cities);

            return Ok(citiesDTO);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CityDTO> GetCity(string id)
        {
            var guidId = Guid.Parse(id);
            var city = _unit.Cities.GetById(guidId);

            if (city == default)
            {
                return NotFound();
            }

            var cityDTO = _mapper.Map<CityDTO>(city);

            return Ok(cityDTO);
        }

        [HttpGet("{id:guid}/places")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PlaceOfInterestDTO>> GetCityPlaces(string id)
        {
            var guidId = Guid.Parse(id);
            var city = _unit.Cities.GetById(guidId);

            if (city == default)
            {
                return NotFound();
            }

            var places = _unit.IntrestringPlaces.Get(
                p => p.CityOwnerId == guidId,
                orderBy: p => p.Name
            );

            var placesDTO = _mapper.Map<IEnumerable<PlaceOfInterestDTO>>(places);

            return Ok(placesDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<City> CreateCity(CreateCityModel cityModel)
        {
            var city = _mapper.Map<City>(cityModel);

            _unit.Cities.Insert(city);

            try
            {
                _unit.Save();
            }
            catch (Exception)
            {
                // log it
                return BadRequest();
            }

            return Created(
                HttpContext.Request.Path.ToUriComponent(),
                new { id = city.Id.ToString("N") });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCity(string id, UpdateCityModel cityModel)
        {
            var guidId = Guid.Parse(id);
            var existingCity = _unit.Cities.GetById(guidId);

            if (existingCity == default)
            {
                return NotFound();
            }

            _mapper.Map(cityModel, existingCity);

            try
            {
                _unit.Save();
            }
            catch (Exception)
            {
                // log it
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCity(string id)
        {
            var guidId = Guid.Parse(id);
            var existingCity = _unit.Cities.GetById(guidId);

            if (existingCity == default)
            {
                return NotFound();
            }

            _unit.Cities.Delete(guidId);

            try
            {
                _unit.Save();
            }
            catch (Exception)
            {
                // log it
                return BadRequest();
            }

            return NoContent();
        }
    }
}