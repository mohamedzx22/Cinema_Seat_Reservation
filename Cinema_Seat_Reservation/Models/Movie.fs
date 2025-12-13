module Models.Movie

open System

type Movie = {
    MovieId : int
    Title : string
    Duration : int
}

type ShowTime = {
    ShowId : int
    MovieId : int
    HallId : int
    StartTime : DateTime
    EndTime : DateTime
}
