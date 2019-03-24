using System;

namespace SearchToolbox.Classes
{
    [Serializable]
    public class Movie
    {
        #region Constructor
        public Movie()
        {
        }

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

        public string Code { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public short? StartYear { get; set; }
        public short? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string Genres { get; set; }

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