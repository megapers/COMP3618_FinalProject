using SearchToolbox.Classes;
using System.Collections.Generic;

namespace SearchToolbox.Interfaces
{
    /// <summary>
    /// Interface for the business logic layer
    /// </summary>
    public interface IBusinessLogicLayer
    {
        #region Movie Search
        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <returns>Count of the number of movies that meet the search criteria</returns>
        int GetSearchMatches(string searchFor);

        /// <summary>
        /// Gets the next block of movies meeting the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <param name="codeGreaterThan">Last movie code of the previous search block</param>
        /// <param name="blockSize">Number of results to return in the result block</param>
        /// <returns>List of movies neeting the search criteria of the 'Movie'</returns>
        List<Movie> ReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize);
        #endregion

        #region Movie CRUD
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        bool AddMovie(Movie movie);

        /// <summary>
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        Movie ReadMovie(string code);

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="code">Movie code to update</param>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        bool UpdateMovie(string code, Movie movie);

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="code">Movie code to delete</param>
        /// <returns>Flag indicating success / failure</returns>
        bool DeleteMovie(string code);
        #endregion
    }
}
