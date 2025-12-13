module Database

open System.Data.SqlClient

let connectionString =
    "Server=DESKTOP-BFA16MI\SQLEXPRESS;Database=CinemaBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"

let getConnection () =
    let conn = new SqlConnection(connectionString)
    conn.Open()
    conn



