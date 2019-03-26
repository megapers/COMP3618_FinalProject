using System;

namespace SearchToolbox.Classes
{
    /// <summary>
    /// Class used to hold movie information
    /// </summary>
    [Serializable]
    public class Movie
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Movie()
        {
        }

        /// <summary>
        /// Override constructor
        /// </summary>
        /// <param name="code">Movie code</param>
        /// <param name="titleType">Title type</param>
        /// <param name="primaryTitle">Primary title</param>
        /// <param name="originalTitle">Original title</param>
        /// <param name="isAdult">Flag indicating if the movie is n adult movie or not</param>
        /// <param name="startYear">Start year</param>
        /// <param name="endYear">End year</param>
        /// <param name="runtimeMinutes">Runtime (minutes)</param>
        /// <param name="genres">Genres (comma separated list)</param>
        public Movie(string code, string titleType, string primaryTitle,
            string originalTitle, bool isAdult, short? startYear, short? endYear,
            int? runtimeMinutes, string genres)
        {
            Code = code;
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RuntimeMinutes = runtimeMinutes;
            Genres = genres;
        }
        #endregion

        /// <summary>
        /// Movie code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Title type
        /// </summary>
        public string TitleType { get; set; }

        /// <summary>
        /// Primary title
        /// </summary>
        public string PrimaryTitle { get; set; }

        /// <summary>
        /// Original title
        /// </summary>
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Flag indicating if the movie is n adult movie or not
        /// </summary>
        public bool IsAdult { get; set; }

        /// <summary>
        /// Start year
        /// </summary>
        public short? StartYear { get; set; }

        /// <summary>
        /// End year
        /// </summary>
        public short? EndYear { get; set; }

        /// <summary>
        /// Runtime (minutes)
        /// </summary>
        public int? RuntimeMinutes { get; set; }

        /// <summary>
        /// Genres (comma separated list)
        /// </summary>
        public string Genres { get; set; }

        /// <summary>
        /// Override of the 'ToString' method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Code: {Code}\nTitle Type: {TitleType}\nPrimary Title: {PrimaryTitle}\n" +
                $"Original Title: {OriginalTitle}\nIs Adult: {IsAdult}\n" +
                $"Start Year: {((StartYear == null) ? string.Empty : StartYear.ToString())}\n" +
                $"End Year: {((EndYear == null) ? string.Empty : EndYear.ToString())}\n" +
                $"Runtime Minutes: {((RuntimeMinutes == null) ? string.Empty : RuntimeMinutes.ToString())}\n" +
                $"Genres: {Genres}";
        }
    }
}