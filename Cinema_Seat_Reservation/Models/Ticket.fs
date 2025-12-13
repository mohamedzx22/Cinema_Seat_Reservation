namespace Models.Ticket

open System

type Ticket = {
    ReservationId: int
    TicketID: string
    UserId: int
    ShowId: int
    SeatId: int
    IsValid: bool
    BookingTime: DateTime
}

type TicketView = {
    TicketID: string
    MovieTitle: string
    HallName: string
    Row: int
    Column: int
    StartTime: DateTime
    EndTime: DateTime
}
