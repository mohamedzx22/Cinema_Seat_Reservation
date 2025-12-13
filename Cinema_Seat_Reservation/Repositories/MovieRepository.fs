module Repositories.MovieRepository

open System
open System.Data.SqlClient
open System.Text
open Database
open Models.Movie

let insertMovie title duration =
    use conn = getConnection()
    use cmd = new SqlCommand("
        INSERT INTO Movies (Title, Duration)
        VALUES (@title, @duration);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ", conn)
    cmd.Parameters.AddWithValue("@title", title) |> ignore
    cmd.Parameters.AddWithValue("@duration", duration) |> ignore

    match cmd.ExecuteScalar() with
    | :? int as id -> id
    | :? System.Decimal as d -> int d
    | _ -> failwith "Failed to retrieve inserted MovieId."


let insertShowTime movieId hallId startT endT =
    use conn = getConnection()
    use cmd = new SqlCommand("
        INSERT INTO ShowTimes (MovieId, HallId, StartTime, EndTime)
        VALUES (@m,@h,@s,@e)", conn)
    cmd.Parameters.AddWithValue("@m", movieId) |> ignore
    cmd.Parameters.AddWithValue("@h", hallId) |> ignore
    cmd.Parameters.AddWithValue("@s", startT) |> ignore
    cmd.Parameters.AddWithValue("@e", endT) |> ignore
    cmd.ExecuteNonQuery() |> ignore


let getAllMovies () =
    use conn = getConnection()
    use cmd = new SqlCommand("SELECT MovieId, Title, Duration FROM Movies", conn)
    use reader = cmd.ExecuteReader()
    let buf = ResizeArray<Movie>()
    while reader.Read() do
        buf.Add({
            MovieId = reader.GetInt32(0)
            Title = reader.GetString(1)
            Duration = reader.GetInt32(2)
        })
    buf |> List.ofSeq


let getOverlappingShowTimes hallId startT endT =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT ShowId, MovieId, HallId, StartTime, EndTime
        FROM ShowTimes
        WHERE HallId = @h
          AND NOT (EndTime <= @start OR StartTime >= @end)
    ", conn)
    cmd.Parameters.AddWithValue("@h", hallId) |> ignore
    cmd.Parameters.AddWithValue("@start", startT) |> ignore
    cmd.Parameters.AddWithValue("@end", endT) |> ignore

    use reader = cmd.ExecuteReader()
    let buf = ResizeArray<ShowTime>()
    while reader.Read() do
        buf.Add({
            ShowId = reader.GetInt32(0)
            MovieId = reader.GetInt32(1)
            HallId = reader.GetInt32(2)
            StartTime = reader.GetDateTime(3)
            EndTime = reader.GetDateTime(4)
        })
    buf |> List.ofSeq


let getShowTimesByMovie movieId =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT ShowId, MovieId, HallId, StartTime, EndTime 
        FROM ShowTimes WHERE MovieId = @mid", conn)
    cmd.Parameters.AddWithValue("@mid", movieId) |> ignore

    use reader = cmd.ExecuteReader()
    let buf = ResizeArray<ShowTime>()
    while reader.Read() do
        buf.Add({
            ShowId = reader.GetInt32(0)
            MovieId = reader.GetInt32(1)
            HallId = reader.GetInt32(2)
            StartTime = reader.GetDateTime(3)
            EndTime = reader.GetDateTime(4)
        })
    buf |> List.ofSeq


let getShowTimeById showId =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT ShowId, MovieId, HallId, StartTime, EndTime 
        FROM ShowTimes WHERE ShowId=@id", conn)
    cmd.Parameters.AddWithValue("@id", showId) |> ignore
    use reader = cmd.ExecuteReader()
    if reader.Read() then
        Some {
            ShowId = reader.GetInt32(0)
            MovieId = reader.GetInt32(1)
            HallId = reader.GetInt32(2)
            StartTime = reader.GetDateTime(3)
            EndTime = reader.GetDateTime(4)
        }
    else None


let getMovieById movieId =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT MovieId, Title, Duration
        FROM Movies
        WHERE MovieId = @id
    ", conn)

    cmd.Parameters.AddWithValue("@id", movieId) |> ignore

    use reader = cmd.ExecuteReader()
    if reader.Read() then
        Some {
            MovieId = reader.GetInt32(0)
            Title = reader.GetString(1)
            Duration = reader.GetInt32(2)
        }
    else
        None

