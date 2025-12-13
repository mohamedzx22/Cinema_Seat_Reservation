module Repositories.HallRepository

open System.Data.SqlClient
open Database
open Models.Hall

let getAllHalls () =
    use conn = getConnection()
    use cmd = new SqlCommand("SELECT HallId, Name, Rows, Columns FROM Halls", conn)
    use reader = cmd.ExecuteReader()
    [ while reader.Read() do
        { HallId = reader.GetInt32(0)
          Name = reader.GetString(1)
          Rows = reader.GetInt32(2)
          Columns = reader.GetInt32(3) } ]

let getHallsByMovie movieId =
    use conn = getConnection()
    use cmd = new SqlCommand("
        SELECT DISTINCT h.HallId, h.Name, h.Rows, h.Columns
        FROM Halls h
        INNER JOIN ShowTimes s ON h.HallId = s.HallId
        WHERE s.MovieId=@movieId
    ", conn)
    cmd.Parameters.AddWithValue("@movieId", movieId) |> ignore
    use reader = cmd.ExecuteReader()
    [ while reader.Read() do
        { HallId = reader.GetInt32(0)
          Name = reader.GetString(1)
          Rows = reader.GetInt32(2)
          Columns = reader.GetInt32(3) } ]
