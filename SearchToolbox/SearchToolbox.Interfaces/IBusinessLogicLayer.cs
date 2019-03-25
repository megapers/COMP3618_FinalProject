using SearchToolbox.Classes;
using System.Collections.Generic;

namespace SearchToolbox.Interfaces
{
    public interface IBusinessLogicLayer
    {
        #region Movie Search
        int GetSearchMatches(string searchFor);
        List<Movie> ReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize);
        #endregion

        #region Movie CRUD
        bool AddMovie(Movie movie);
        Movie ReadMovie(string code);
        bool UpdateMovie(string code, Movie movie);
        bool DeleteMovie(string code);
        #endregion

    }
}
