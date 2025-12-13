module Repositories.TicketRepository

open System.Data.SqlClient
open Database
open Models.Ticket

let insertTicket (userId:int) (showId:int) (seatId:int) =
    use conn = getConnection()
    use insertCmd =
        new SqlCommand(
            "INSERT INTO Tickets (UserId, ShowId, SeatId, IsValid, BookingTime) 
             VALUES (@uId, @shId, @sId, 1, GETDATE());
             SELECT CAST(SCOPE_IDENTITY() AS INT);", conn)
    insertCmd.Parameters.AddWithValue("@uId", userId) |> ignore
    insertCmd.Parameters.AddWithValue("@shId", showId) |> ignore
    insertCmd.Parameters.AddWithValue("@sId", seatId) |> ignore
    insertCmd.ExecuteScalar() |> unbox<int>

let updateTicketId reservationId ticketId =
    use conn = getConnection()
    use updateCmd =
        new SqlCommand("UPDATE Tickets SET TicketID=@tId WHERE ReservationId=@rId", conn)
    updateCmd.Parameters.AddWithValue("@tId", ticketId) |> ignore
    updateCmd.Parameters.AddWithValue("@rId", reservationId) |> ignore
    updateCmd.ExecuteNonQuery() |> ignore

let ticketExists ticketId userId =
    use conn = getConnection()
    use cmd =
        new SqlCommand("SELECT COUNT(*) FROM Tickets WHERE TicketID=@tId AND UserId=@uId AND IsValid=1", conn)
    cmd.Parameters.AddWithValue("@tId", ticketId) |> ignore
    cmd.Parameters.AddWithValue("@uId", userId) |> ignore
    cmd.ExecuteScalar() |> unbox<int> > 0

let invalidateTicket ticketId userId =
    use conn = getConnection()
    use cmd = new SqlCommand("UPDATE Tickets SET IsValid=0 WHERE TicketID=@tId AND UserId=@uId", conn)
    cmd.Parameters.AddWithValue("@tId", ticketId) |> ignore
    cmd.Parameters.AddWithValue("@uId", userId) |> ignore
    cmd.ExecuteNonQuery() |> ignore

let resetSeatBooking ticketId =
    use conn = getConnection()
    use cmd =
        new SqlCommand("
            UPDATE sb
            SET IsBooked = 0
            FROM SeatBookings sb
            INNER JOIN Tickets t2 ON sb.SeatId = t2.SeatId AND sb.ShowId = t2.ShowId
            WHERE t2.TicketID=@tId
        ", conn)
    cmd.Parameters.AddWithValue("@tId", ticketId) |> ignore
    cmd.ExecuteNonQuery() |> ignore

let invalidateExpiredTicketsInDB () =
    use conn = getConnection()
    use updateTicketsCmd =
        new SqlCommand("
            UPDATE t
            SET IsValid = 0
            FROM Tickets t
            INNER JOIN ShowTimes s ON t.ShowId = s.ShowId
            WHERE s.EndTime < GETDATE() AND t.IsValid = 1
        ", conn)
    updateTicketsCmd.ExecuteNonQuery() |> ignore

    use updateSeatsCmd =
        new SqlCommand("
            UPDATE sb
            SET IsBooked = 0
            FROM SeatBookings sb
            INNER JOIN Tickets t2 ON sb.SeatId = t2.SeatId AND sb.ShowId = t2.ShowId
            INNER JOIN ShowTimes s2 ON t2.ShowId = s2.ShowId
            WHERE s2.EndTime < GETDATE() AND t2.IsValid = 0
        ", conn)
    updateSeatsCmd.ExecuteNonQuery() |> ignore

let getUserTicketsFromDB userId =
    use conn = getConnection()
    use cmd =
        new SqlCommand("
            SELECT t.TicketID, m.Title, h.Name, s.RowNumber, s.ColNumber, st.StartTime, st.EndTime
            FROM Tickets t
            INNER JOIN ShowTimes st ON t.ShowId = st.ShowId
            INNER JOIN Movies m ON st.MovieId = m.MovieId
            INNER JOIN Halls h ON st.HallId = h.HallId
            INNER JOIN Seats s ON t.SeatId = s.SeatId
            WHERE t.UserId=@uId AND t.IsValid=1
            ORDER BY st.StartTime
        ", conn)
    cmd.Parameters.AddWithValue("@uId", userId) |> ignore

    use reader = cmd.ExecuteReader()
    let tickets = ResizeArray<_>()
    while reader.Read() do
        tickets.Add(
            {| TicketID = reader.GetString(0)
               MovieTitle = reader.GetString(1)
               HallName = reader.GetString(2)
               Row = reader.GetInt32(3)
               Column = reader.GetInt32(4)
               StartTime = reader.GetDateTime(5)
               EndTime = reader.GetDateTime(6) |}
        )
    tickets |> List.ofSeq
