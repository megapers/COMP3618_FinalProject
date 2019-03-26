using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using SearchTooolbox.EF;
using System.Collections.Generic;
using System.Linq;

namespace SearchToolbox.DAL
{
    /// <summary>
    /// Class for the data access layer
    /// </summary>
    public class Utilities : IDataAccessLayer
    {
        private readonly IMDB _context;

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Utilities()
        {
            _context = new IMDB();
        }
        #endregion

        #region Movie Search
        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <returns>Count of the number of movies that meet the search criteria</returns>
        public int GetMovieMatches(string searchFor)
        {
            string searchCode = searchFor.ToUpper();

            return (_context.Titles
                        .Where(t => t.Code.ToUpper().Contains(searchCode))
                        .Count());
        }

        /// <summary>
        /// Gets the next block of movies meeting the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <param name="codeGreaterThan">Last movie code of the previous search block</param>
        /// <param name="blockSize">Number of results to return in the result block</param>
        /// <returns>List of movies neeting the search criteria of the 'Movie'</returns>
        public List<Movie> ReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize)
        {
            string searchCode = searchFor.ToUpper();

            return (_context.Titles
                        .Where(t => t.Code.ToUpper().Contains(searchCode))
                        .Where(tl => tl.Code.CompareTo(codeGreaterThan) > 0)
                        .Select(r => new Movie()
                        {
                            Code = r.Code,
                            TitleType = r.TitleType,
                            PrimaryTitle = r.PrimaryTitle,
                            OriginalTitle = r.OriginalTitle,
                            IsAdult = r.IsAdult,
                            StartYear = r.StartYear,
                            EndYear = r.EndYear,
                            RuntimeMinutes = r.RuntimeMinutes,
                            Genres = r.Genres
                        })
                        .OrderBy(r => r.Code)
                        ).Take(blockSize).ToList();
        }
        #endregion

        #region Movie CRUD
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        public bool AddMovie(Movie movie)
        {
            _context.Titles.Add(ConvertMovieToTitle(movie));
            return (_context.SaveChanges() > 0);
        }

        /// <summary>
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        public Movie ReadMovie(string code)
        {
            string searchCode = code.ToUpper();

            return ConvertTitleToMovie(_context.Titles
                        .Where(t => t.Code.ToUpper() == searchCode)
                        .FirstOrDefault());
        }

        /// <summary>
        /// Checks to see if a movie with the specified code already exists
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Flag indicating whether or not a movie with the specified code exists</returns>
        public bool MovieExists(string code)
        {
            string searchCode = code.ToUpper();

            return (_context.Titles
                        .Where(t => t.Code.ToUpper() == searchCode)
                        .Count() > 0);
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="code">Movie code to update</param>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        public bool UpdateMovie(string code, Movie movie)
        {
            string searchCode = code.Trim().ToUpper();
            bool result = false;

            Title editTitle = _context.Titles
                    .Where(t => t.Code.ToUpper() == searchCode)
                  .FirstOrDefault();

            if (editTitle != null)
            {
                movie.Code = code;
                _context.Entry(editTitle).CurrentValues.SetValues(ConvertMovieToTitle(movie));

                result = (_context.SaveChanges() > 0);
            }

            return result;
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="code">Movie code to delete</param>
        /// <returns>Flag indicating success / failure</returns>
        public bool DeleteMovie(string code)
        {
            string searchCode = code.Trim().ToUpper();

            Title title = _context.Titles
                .Where(t => t.Code.Trim().ToUpper() == searchCode)
                .FirstOrDefault();

            _context.Titles.Remove(title);

            return (_context.SaveChanges() > 0);
        }
        #endregion

        #region Helper Methods
        private static Movie ConvertTitleToMovie(Title titles)
        {
            Movie movie = null;

            if (titles != null)
            {
                movie = new Movie(titles.Code, titles.TitleType, titles.PrimaryTitle,
                    titles.OriginalTitle, titles.IsAdult, titles.StartYear, titles.EndYear,
                    titles.RuntimeMinutes, titles.Genres);
            }

            return movie;
        }

        private static Title ConvertMovieToTitle(Movie movie)
        {
            Title title = null;

            if (movie != null)
            {
                title = new Title()
                {
                    Code = movie.Code,
                    TitleType = movie.TitleType,
                    PrimaryTitle = movie.PrimaryTitle,
                    OriginalTitle = movie.OriginalTitle,
                    IsAdult = movie.IsAdult,
                    StartYear = movie.StartYear,
                    EndYear = movie.EndYear,
                    RuntimeMinutes = movie.RuntimeMinutes,
                    Genres = movie.Genres
                };
            }

            return title;
        }
        #endregion
    }
}
