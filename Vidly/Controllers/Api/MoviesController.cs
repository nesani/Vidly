using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext dbContext;

        public MoviesController()
        {
            dbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// GET api/movies
        /// </summary>
        /// <returns>List of Movies</returns>
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
          return dbContext.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }
        /// <summary>
        /// Get a single Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Movie</returns>
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        /// <summary>
        /// Creates a Movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns>Ok if movie is created</returns>
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var dbMovie = Mapper.Map<MovieDto, Movie>(movieDto);
            dbContext.Movies.Add(dbMovie);
            dbContext.SaveChanges();

            movieDto.Id = dbMovie.Id;

            return Created(new Uri(Request.RequestUri + "/" + dbMovie.Id), movieDto);
            
        }


        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            dbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            dbContext.Movies.Remove(movieInDb);
            dbContext.SaveChanges();

            return Ok();

        }

    }
}
