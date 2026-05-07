using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class Program
    {
        public class Movie
        {
            private string _name;
            private int _duration;
            private int[] _review;
            public string Name => _name;
            public int Duration => _duration;
            public int[] Review => _review.ToArray();
            public Movie(string name, int duration)
            {
                _name = name;
                _duration = duration;
                _review = new int[0];
            }
            public void AddReview(int stars)
            {
                Array.Resize(ref _review, _review.Length+1);
                _review[_review.Length-1] = stars;
            }
        }

        public class MovieDTO
        {
            // свойства с публичными сеттарами
            public string MovieType {  get; set; }
            public string Name { get; set; }
            public int Duration { get; set; }
            public int[] Review { get; set; }
            // конструктор без параметров
            public MovieDTO()
            {

            }
            // Movie => MpvieDTO
            //public MovieDTO(string name, int duration)
            //{
            //    Name = name;
            //    Duration = duration;
            //}

            public MovieDTO(Movie movie)// movie.GetType() = typeof(Movie)
                                        // movie.GetType().Name = nameof(Movie)
            {
                MovieType = movie.GetType().Name;
                Name = movie.Name;
                Duration = movie.Duration;
                Review = movie.Review;
            }
        }

        static void Main(string[] args)
        {
            Movie moviel = new Movie("Harry Potter", 120);
            moviel.AddReview(5);
            moviel.AddReview(3);
            moviel.AddReview(2);
            moviel.AddReview(4);

            MovieDTO movielDTOl = new MovieDTO(moviel);
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(folderPath, "movie.xml");

            var serializer = new XmlSerializer(typeof(MovieDTO));
            //Серриализация
            using (var writer  = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, movielDTOl);
            }
            //Диссериализация
            MovieDTO movielDTO2;
            using ( var reader = new StreamReader(filePath))
            {
               movielDTO2 =(MovieDTO)serializer.Deserialize(reader);
            }

            Movie moviel2 = new Movie(movielDTO2.Name, movielDTO2.Duration);
            
            foreach(int star in movielDTO2.Review)
            {
                moviel2.AddReview(star);
            }

            if (CompareMovie(moviel, moviel2))
            {
                Console.WriteLine( "Success");
            }
            else
            {
                Console.WriteLine("Not success");
            }
        }

        public static bool CompareMovie(Movie m1, Movie m2)
        {

            if (m1.Name != m2.Name) return false;
            if (m1.Duration != m2.Duration) return false;
            if (m1.Review != m2.Review) return false;
            return true;
        }
    }
}
