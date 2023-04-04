using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TheMovie.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult GetPopularMovies()
        {
            Models.Movie movie = new Models.Movie();

            using (var client = new HttpClient())
            {
                string urlApi = "https://api.themoviedb.org/3/movie/";
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("popular?api_key=34c2dfa3311eb101ef0ec1d014c9a52f&language=en-ES&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.IdMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Nombre = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }
            }
            return View(movie);
        }

        public ActionResult GetFaviriteMovie()
        {
            Models.Movie movie = new Models.Movie();

            using (var client = new HttpClient())
            {
                string urlApi = "https://api.themoviedb.org/3/account/18694857/favorite/";
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("movies?api_key=34c2dfa3311eb101ef0ec1d014c9a52f&session_id=5a4133177b9f6f1f4351048be370b51a171b76d9&language=en-ES&sort_by=created_at.asc&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.IdMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Nombre = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }
            }
            return View(movie);
        }

        public ActionResult AddFavoriteMovie(Models.Movie movie)
        {
            movie.media_type = "movie";
            movie.media_id = movie.IdMovie;
            movie.favorite = true;

            using (var client = new HttpClient())
            {
                string urlApi = "https://api.themoviedb.org/3/account/18694857/";
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.PostAsJsonAsync("favorite?api_key=34c2dfa3311eb101ef0ec1d014c9a52f&session_id=5a4133177b9f6f1f4351048be370b51a171b76d9",movie);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                }
            }
            return View("GetFaviriteMovie");
        }

        public ActionResult DeleteFavoriteMovie(Models.Movie movie)
        {
            movie.media_type = "movie";
            movie.media_id = movie.IdMovie;
            movie.favorite = false;

            using (var client = new HttpClient())
            {
                string urlApi = "https://api.themoviedb.org/3/account/18694857/";
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.PostAsJsonAsync("favorite?api_key=34c2dfa3311eb101ef0ec1d014c9a52f&session_id=5a4133177b9f6f1f4351048be370b51a171b76d9", movie);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                }
            }
            return View("GetFaviriteMovie");
        }
    }
}