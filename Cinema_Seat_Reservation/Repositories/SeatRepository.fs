namespace Cinema_Seat_Reservation.Repository

open System.Data.SqlClient
open Database
open Models.Seat

module SeatRepository =

    let loadSeatsForHall hallId showId rows cols =
        let seats = Array2D.create rows cols false

        use conn = getConnection()
        use cmd =
            new SqlCommand("
                SELECT 
                    s.RowNumber, 
                    s.ColNumber, 
                    ISNULL(b.IsBooked, 0) AS IsBooked
                FROM Seats s
                LEFT JOIN SeatBookings b
                    ON s.SeatId = b.SeatId
                    AND b.ShowId = @showId
                WHERE s.HallId = @hallId
            ", conn)

        cmd.Parameters.AddWithValue("@hallId", hallId) |> ignore
        cmd.Parameters.AddWithValue("@showId", showId) |> ignore

        use reader = cmd.ExecuteReader()
        while reader.Read() do
            let row = reader.GetInt32(0)
            let col = reader.GetInt32(1)
            let isBooked = reader.GetBoolean(2)
            seats.[row, col] <- isBooked

        seats

    let getSeatId hallId showId row col =
        use conn = getConnection()
        use cmd =
            new SqlCommand(
                "SELECT s.SeatId, 
                        CASE WHEN sb.SeatBookingId IS NOT NULL THEN 1 ELSE 0 END AS IsBooked
                 FROM Seats s
                 LEFT JOIN SeatBookings sb
                   ON sb.SeatId = s.SeatId AND sb.ShowId = @showId
                 WHERE s.HallId=@h AND s.RowNumber=@r AND s.ColNumber=@c", conn)

        cmd.Parameters.AddWithValue("@h", hallId) |> ignore
        cmd.Parameters.AddWithValue("@r", row) |> ignore
        cmd.Parameters.AddWithValue("@c", col) |> ignore
        cmd.Parameters.AddWithValue("@showId", showId) |> ignore

        use reader = cmd.ExecuteReader()
        if reader.Read() then
            let seatId = reader.GetInt32(reader.GetOrdinal("SeatId"))
            let isBooked = reader.GetInt32(reader.GetOrdinal("IsBooked")) = 1
            Some { SeatId = seatId; IsBooked = isBooked }
        else
            None

    let bookSeat seatId showId userId =
        use conn = getConnection()

        use checkCmd =
            new SqlCommand(
                "SELECT IsBooked FROM SeatBookings WHERE SeatId=@sId AND ShowId=@shId",
                conn
            )
        checkCmd.Parameters.AddWithValue("@sId", seatId) |> ignore
        checkCmd.Parameters.AddWithValue("@shId", showId) |> ignore

        let result = checkCmd.ExecuteScalar()

        match result with
        | :? bool as isBooked ->
            if isBooked then
                false
            else
                use updateCmd =
                    new SqlCommand(
                        "UPDATE SeatBookings SET IsBooked = 1 
                         WHERE SeatId=@sId AND ShowId=@shId",
                        conn
                    )
                updateCmd.Parameters.AddWithValue("@sId", seatId) |> ignore
                updateCmd.Parameters.AddWithValue("@shId", showId) |> ignore
                updateCmd.ExecuteNonQuery() |> ignore
                true

        | _ ->
            use insertCmd =
                new SqlCommand(
                    "INSERT INTO SeatBookings (SeatId, ShowId, IsBooked)
                     VALUES (@sId, @shId, 1)",
                    conn
                )
            insertCmd.Parameters.AddWithValue("@sId", seatId) |> ignore
            insertCmd.Parameters.AddWithValue("@shId", showId) |> ignore
            insertCmd.ExecuteNonQuery() |> ignore
            true
