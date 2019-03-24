using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using SearchTooolbox.EF;
using System.Collections.Generic;
using System.Linq;

namespace SearchToolbox.DAL
{
    public class Utilities : IDataAccessLayer
    {
        private readonly IMDB _context;

        #region Constructor
        public Utilities()
        {
            _context = new IMDB();
        }
        #endregion

        #region Movie Search
        public int GetMovieMatches(string searchFor)
        {
            string searchCode = searchFor.ToUpper();

            return (_context.Titles
                        .Where(t => t.Code.ToUpper().Contains(searchCode))
                        .Count());
        }

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
        public Movie ReadMovie(string code)
        {
            string searchCode = code.ToUpper();

            return ConvertTitleToMovie(_context.Titles
                        .Where(t => t.Code.ToUpper() == searchCode)
                        .FirstOrDefault());
        }

        public bool MovieExists(string code)
        {
            string searchCode = code.ToUpper();

            return (_context.Titles
                        .Where(t => t.Code.ToUpper() == searchCode)
                        .Count() > 0);
        }

        public bool UpdateMovie(Movie movie)
        {
            string searchCode = movie.Code.Trim().ToUpper();
            bool result = false;

            Title editTitle = _context.Titles
                    .Where(t => t.Code.ToUpper() == searchCode)
                  .FirstOrDefault();

            if (editTitle != null)
            {
                _context.Entry(editTitle).CurrentValues.SetValues(ConvertMovieToTitle(movie));

                result = (_context.SaveChanges() > 0);
            }

            return result;
        }

        public bool AddMovie(Movie movie)
        {
            _context.Titles.Add(ConvertMovieToTitle(movie));
            return (_context.SaveChanges() > 0);
        }

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
