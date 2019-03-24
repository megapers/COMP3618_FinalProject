using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using System;
using System.Collections.Generic;

namespace SearchToolbox.BLL
{
    public class Utilities : IBusinessLogicLayer
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        private bool disposed = false;

        #region Constructor
        public Utilities(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        #endregion

        #region Movie Search
        public int GetSearchMatches(string searchFor)
        {
            return _dataAccessLayer.GetMovieMatches(searchFor);
        }

        public List<Movie> ReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize)
        {
            return _dataAccessLayer.ReadMatchingMovies(searchFor, codeGreaterThan, blockSize);
        }
        #endregion

        #region Movie CRUD
        public bool AddMovie(Movie movie)
        {
            ValidateMovie(movie);

            if (_dataAccessLayer.MovieExists(movie.Code))
            {
                throw new ArgumentException($"A movie with the code '{movie.Code}' already exits.");
            }

            return _dataAccessLayer.AddMovie(movie);
        }

        public Movie ReadMovie(string code)
        {
            if (code.Trim().Length == 0)
            {
                throw new ArgumentException(@"You have not specified a code.");
            }

            return _dataAccessLayer.ReadMovie(code);
        }

        public bool UpdateMovie(Movie movie)
        {
            ValidateMovie(movie);

            if (!_dataAccessLayer.MovieExists(movie.Code))
            {
                throw new ArgumentException($"A movie with the code '{movie.Code}' does not exits.");
            }

            return _dataAccessLayer.UpdateMovie(movie);
        }

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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
