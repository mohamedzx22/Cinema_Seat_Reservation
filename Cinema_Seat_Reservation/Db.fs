namespace Cinema_Seat_Reservation

module Db =

    open System
    open System.Data.SqlClient

    let connectionString = 
        @"Server=DESKTOP-BFA16MI\SQLEXPRESS;Database=CinemaBookingDB;Trusted_Connection=True;"

    let getConnection () =
        let conn = new SqlConnection(connectionString)
        conn.Open()
        conn
    
