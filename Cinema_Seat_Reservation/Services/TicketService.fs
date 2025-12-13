module Services.TicketService

open Repositories.TicketRepository

let createTicket userId showId seatId =
    let reservationId = insertTicket userId showId seatId

    let ticketId = sprintf "TK%03d" reservationId

    updateTicketId reservationId ticketId

    ticketId


let cancelTicket ticketId userId =
    if not (ticketExists ticketId userId) then
        printfn "❌ Ticket not found or doesn't belong to user."
        false
    else
        invalidateTicket ticketId userId
        resetSeatBooking ticketId
        printfn "✅ Ticket cancelled successfully!"
        true

let invalidateExpiredTickets () =
    invalidateExpiredTicketsInDB ()

let getUserTickets userId =
    getUserTicketsFromDB userId
