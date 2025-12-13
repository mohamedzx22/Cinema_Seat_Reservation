module Services.ShowService

open Repositories.ShowRepository
open Models.Show

let getShowTimes hallId movieId =
    getShowTimesByHallAndMovie hallId movieId
