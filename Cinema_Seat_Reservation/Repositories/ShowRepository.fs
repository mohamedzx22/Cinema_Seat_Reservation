module Repositories.ShowRepository

open System
open System.Data.SqlClient
open Database
open Models.Show

let getShowTimesByHallAndMovie hallId movieId =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT ShowId, MovieId, HallId, StartTime, EndTime
        FROM ShowTimes
        WHERE HallId=@hId AND MovieId=@mId
        ORDER BY StartTime
    ", conn)
    cmd.Parameters.AddWithValue("@hId", hallId) |> ignore
    cmd.Parameters.AddWithValue("@mId", movieId) |> ignore

    use reader = cmd.ExecuteReader()
    let shows = ResizeArray<_>()
    while reader.Read() do
        shows.Add({
            ShowId = reader.GetInt32(0)
            MovieId = reader.GetInt32(1)
            HallId = reader.GetInt32(2)
            StartTime = reader.GetDateTime(3)
            EndTime = reader.GetDateTime(4)
        })
    shows |> List.ofSeq
