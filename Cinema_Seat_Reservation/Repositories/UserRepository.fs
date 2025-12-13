module Repositories.UserRepository

open Models.User
open Database
open System.Data.SqlClient

let addUser (user: User) : int =
    use conn = getConnection()
    use cmd =
        new SqlCommand(
            "INSERT INTO Users (Name, Email, Password, Role)
             VALUES (@name, @email, @hash, @role);
             SELECT CAST(SCOPE_IDENTITY() AS INT);", conn)

    cmd.Parameters.AddWithValue("@name", user.Name) |> ignore
    cmd.Parameters.AddWithValue("@email", user.Email) |> ignore
    cmd.Parameters.AddWithValue("@hash", user.Password) |> ignore
    cmd.Parameters.AddWithValue("@role", if user.Role then 1 else 0) |> ignore

    cmd.ExecuteScalar() |> unbox<int>

let getUserById (id:int) : User option =
    use conn = getConnection()
    use cmd = new SqlCommand(
        "SELECT UserId, Name, Email, PasswordHash, Role FROM Users WHERE UserId=@id", conn)
    cmd.Parameters.AddWithValue("@id", id) |> ignore
    use reader = cmd.ExecuteReader()
    if reader.Read() then
        Some {
            UserId = reader.GetInt32(0)
            Name = reader.GetString(1)
            Email = reader.GetString(2)
            Password = reader.GetString(3)
            Role = reader.GetInt32(4) = 1
        }
    else None

let getUserByEmail (email:string) : User option =
    use conn = getConnection()
    use cmd = new SqlCommand(
        "SELECT UserId, Name, Email, Password, Role FROM Users WHERE Email=@e", conn)
    cmd.Parameters.AddWithValue("@e", email) |> ignore
    use reader = cmd.ExecuteReader()
    if reader.Read() then
        Some {
            UserId = reader.GetInt32(0)
            Name = reader.GetString(1)
            Email = reader.GetString(2)
            Password = reader.GetString(3)
            Role = reader.GetBoolean(reader.GetOrdinal("Role"))

        }
    else None
