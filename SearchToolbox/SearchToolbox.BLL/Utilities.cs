using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using System;
using System.Collections.Generic;

namespace SearchToolbox.BLL
{
    /// <summary>
    /// Class for the business logic layer
    /// </summary>
    public class Utilities : IBusinessLogicLayer
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        private bool disposed = false;

        #region Constructor
        /// <summary>
        /// Constructor for the business logic layer
        /// </summary>
        /// <param name="dataAccessLayer">Data access layer implemtation</param>
        public Utilities(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        #endregion

        #region Movie Search
        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <returns>Count of the number of movies that meet the search criteria</returns>
        public int GetSearchMatches(string searchFor)
        {
            return _dataAccessLayer.GetMovieMatches(searchFor);
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
            return _dataAccessLayer.ReadMatchingMovies(searchFor, codeGreaterThan, blockSize);
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
            ValidateMovie(movie);

            if (_dataAccessLayer.MovieExists(movie.Code))
            {
                throw new ArgumentException($"A movie with the code '{movie.Code}' already exits.");
            }

            return _dataAccessLayer.AddMovie(movie);
        }

        /// <summary>
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        public Movie ReadMovie(string code)
        {
            if (code.Trim().Length == 0)
            {
                throw new ArgumentException(@"You have not specified a code.");
            }

            return _dataAccessLayer.ReadMovie(code);
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="code">Movie code to update</param>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        public bool UpdateMovie(string code, Movie movie)
        {
            ValidateMovie(movie);

            if (!_dataAccessLayer.MovieExists(code))
            {
                throw new ArgumentException($"A movie with the code '{code}' does not exits.");
            }

            return _dataAccessLayer.UpdateMovie(code, movie);
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="code">Movie code to delete</param>
        /// <returns>Flag indicating success / failure</returns>
        public bool DeleteMovie(string code)
        {
            if (!_dataAccessLayer.MovieExists(code))
            {
                throw new ArgumentException($"A movie with the code '{code}' does not exits.");
            }

            return _dataAccessLayer.DeleteMovie(code);
        }
        #endregion

        #region Helper Methods
        private void ValidateMovie(Movie movie)
        {
            if (movie.Code.Trim() == string.Empty)
            {
                throw new ArgumentException(@"You have not specified a code.");
            }
            if (movie.TitleType.Trim() == string.Empty)
            {
                throw new ArgumentException(@"You have not specified a title type.");
            }
            if (movie.PrimaryTitle.Trim() == string.Empty)
            {
                throw new ArgumentException(@"You have not specified a primay title.");
            }
            if (movie.OriginalTitle.Trim() == string.Empty)
            {
                throw new ArgumentException(@"You have not specified an original title.");
            }
        }
        #endregion

        #region IDisposable Support
        /// <summary>
        /// Dispose mthod
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
        #endregion
    }
}
