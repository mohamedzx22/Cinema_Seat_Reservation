module Services.SeatService

open Cinema_Seat_Reservation.Repository.SeatRepository
open TicketService
open Models.Seat

let loadSeatsWithValidationCSharp hallId showId rows cols : bool[,] =
    invalidateExpiredTickets()
    loadSeatsForHall hallId showId rows cols

let getSeatForCSharp hallId showId row col : SeatBookingInfo =
    match getSeatId hallId showId row col with
    | Some seat -> seat
    | None -> failwith $"Seat not found at {row},{col}"

let bookSeatForCSharp seatId showId userId : bool =
    bookSeat seatId showId userId

let createTicketForCSharp userId showId seatId : string =
    createTicket userId showId seatId
