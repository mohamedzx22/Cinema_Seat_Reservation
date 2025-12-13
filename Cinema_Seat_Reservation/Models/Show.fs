module Models.Show

open System

type ShowTime = {
    ShowId: int
    MovieId: int
    HallId: int
    StartTime: DateTime
    EndTime: DateTime
}
