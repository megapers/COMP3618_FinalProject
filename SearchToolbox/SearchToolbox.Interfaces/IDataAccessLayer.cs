using SearchToolbox.Classes;
using System.Collections.Generic;

namespace SearchToolbox.Interfaces
{
    public interface IDataAccessLayer
    {
        #region Movie Search
        int GetMovieMatches(string searchFor);
        List<Movie> ReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize);
        #endregion

        #region Movie CRUD
        Movie ReadMovie(string code);
        bool MovieExists(string code);
        bool UpdateMovie(Movie movie);
        bool AddMovie(Movie movie);
        bool DeleteMovie(string code);
        #endregion
    }
}
