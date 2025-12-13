module SeatServiceWrapper

open Cinema_Seat_Reservation.Repository.SeatRepository
open Models.Seat

module SeatServiceWrapper =

    /// Wrapper for getSeatId: returns SeatBookingInfo or null
    let getSeatIdCSharp (hallId:int) (showId:int) (row:int) (col:int) : SeatBookingInfo =
        match getSeatId hallId showId row col with
        | Some seatInfo -> seatInfo
        | None -> 

    /// Wrapper for bookSeat: returns bool
    let bookSeatCSharp (seatId:int) (showId:int) (userId:int) : bool =
        bookSeat seatId showId userId
