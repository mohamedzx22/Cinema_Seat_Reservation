module Services.HallService

open Repositories.HallRepository
open Models.Hall

let getAllHallsService () =
    getAllHalls ()

let getHallsByMovieService movieId =
    getHallsByMovie movieId
