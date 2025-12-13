module Services.MovieService

open System
open System.Text
open Repositories.MovieRepository
open Models.Movie

let private formatRange (s: DateTime) (e: DateTime) =
    sprintf "%s → %s" (s.ToString("yyyy-MM-dd HH:mm")) (e.ToString("yyyy-MM-dd HH:mm"))

let addMovieWithShowTime (title:string) (durationMinutes:int) (hallId:int) (startTime:DateTime) =
    let endTime = startTime.AddMinutes(float durationMinutes)
    let overlaps = getOverlappingShowTimes hallId startTime endTime
    if overlaps.Length > 0 then
        let sb = StringBuilder()
        sb.AppendLine("Conflict: the requested time overlaps with existing show(s).") |> ignore
        for s in overlaps do
            sb.AppendLine(sprintf "- ShowId %d : %s" s.ShowId (formatRange s.StartTime s.EndTime)) |> ignore
        Error (sb.ToString())
    else
        let movieId = insertMovie title durationMinutes
        insertShowTime movieId hallId startTime endTime
        Ok (sprintf "Movie '%s' added with duration %d minutes, ShowTime: %s" title durationMinutes (formatRange startTime endTime))


let getAllMoviesService () : Movie list = 
    getAllMovies ()

let getShowTimesByMovieService (movieId:int) =
    getShowTimesByMovie movieId

let getShowTimeByIdService (showId:int) =
    getShowTimeById showId


let addShowTimeForExistingMovie (movieId:int) (hallId:int) (startTime:DateTime) =
    let movieOpt = getMovieById movieId
    match movieOpt with
    | None -> Error "Movie not found."
    | Some movie ->
        let duration = movie.Duration
        let endTime = startTime.AddMinutes(float duration)

        let overlaps = getOverlappingShowTimes hallId startTime endTime
        if overlaps.Length > 0 then
            let sb = StringBuilder()
            sb.AppendLine("Conflict: the requested time overlaps with existing show(s).") |> ignore
            for s in overlaps do
                sb.AppendLine(sprintf "- ShowId %d : %s → %s"
                    s.ShowId
                    (s.StartTime.ToString("yyyy-MM-dd HH:mm"))
                    (s.EndTime.ToString("yyyy-MM-dd HH:mm"))
                ) |> ignore
            Error (sb.ToString())
        else
            insertShowTime movieId hallId startTime endTime
            Ok (sprintf "ShowTime added successfully for MovieId %d from %s to %s"
                    movieId
                    (startTime.ToString("yyyy-MM-dd HH:mm"))
                    (endTime.ToString("yyyy-MM-dd HH:mm")))