using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using System;

namespace SearchToolbox.DataTesting
{
    class Program
    {
        private static IBusinessLogicLayer _businessLogicLayer;

        static void Main(string[] args)
        {
            _businessLogicLayer = new BLL.Utilities(new DAL.Utilities());

            //Test serach methods
            Console.WriteLine(@"Testing movie search methods.");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestGetSearchMatches(@"000");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestReadMatchingMovies(@"tt000", @"tt0000005", 5);
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();

            //Test CRUD methods
            Console.WriteLine(@"Testing movie CRUD methods.");
            Console.WriteLine(@"--------------------------------------------");

            TestReadMovie(@"AAAAAAAAAA");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestReadMovie(@"tt0000001");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestUpdateMovie(@"tt0000001");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestAddMovieMovieMovie(@"aa0000001");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestDeleteMovieMovie(@"aa0000001");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            TestReadMovie(@"aa0000001");
            Console.WriteLine(@"--------------------------------------------");
            Console.WriteLine();

            //Wait for user to end console
            Console.ReadLine();
        }

        private static void TestGetSearchMatches(string searchFor)
        {
            Console.WriteLine($"Testing how many movies match code '{searchFor}'.");
            Console.WriteLine($"Matches: { _businessLogicLayer.GetSearchMatches(searchFor):N0}");
        }

        private static void TestReadMatchingMovies(string searchFor, string codeGreaterThan, int blockSize)
        {
            Console.WriteLine($"Testing reading the next batch of {blockSize} movies \nthat match with '{searchFor}' and are greater that '{codeGreaterThan}'.");
            foreach (Movie movie in _businessLogicLayer.ReadMatchingMovies(searchFor, codeGreaterThan, blockSize))
            {
                Console.WriteLine($"Movie code: '{movie.Code}'");
            }
        }

        private static void TestReadMovie(string code)
        {
            Movie movie = null;

            Console.WriteLine($"Testing reading movie '{code}'.");
            movie = _businessLogicLayer.ReadMovie(code);
            if (movie != null)
            {
                Console.WriteLine(movie.ToString());
            }
            else
            {
                Console.WriteLine($"Movie '{code}' does not exist.");
            }
        }

        private static void TestUpdateMovie(string code)
        {
            Movie movie = null;
            string originalTitle = string.Empty;

            Console.WriteLine($"Testing updating movie '{code}'.");
            movie = _businessLogicLayer.ReadMovie(code);
            if (movie != null)
            {
                originalTitle = movie.OriginalTitle;

                movie.OriginalTitle = @"Test Update";
                Console.WriteLine($"Update Success: {_businessLogicLayer.UpdateMovie(code, movie).ToString()}.");

                movie = _businessLogicLayer.ReadMovie(code);
                Console.WriteLine($"New Title: {movie.OriginalTitle}.");

                movie.OriginalTitle = originalTitle;
                Console.WriteLine($"Update Success: {_businessLogicLayer.UpdateMovie(code, movie).ToString()}.");

                movie = _businessLogicLayer.ReadMovie(code);
                Console.WriteLine($"Title set back to: {movie.OriginalTitle}.");
            }
        }

        private static void TestAddMovieMovieMovie(string code)
        {
            Movie movie = null;

            Console.WriteLine($"Testing adding movie '{code}'.");

            movie = new Movie(code, @"Test Title Type", @"Test Prmay Title",
                @"Test Original Title", false, (short?)2019, (short?)2020,
                (int?)67, @"Test Genre");

            Console.WriteLine($"Add Success: {_businessLogicLayer.AddMovie(movie).ToString()}.");

            movie = _businessLogicLayer.ReadMovie(code);
            Console.WriteLine(@"New Movie:");
            Console.WriteLine(movie.ToString());
        }

        private static void TestDeleteMovieMovie(string code)
        {
            Console.WriteLine($"Testing deleting movie '{code}'.");

            Console.WriteLine($"Delete Success: {_businessLogicLayer.DeleteMovie(code).ToString()}.");
        }
    }
}
