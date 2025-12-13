module Services.UserService

open Models.User
open Repositories.UserRepository
open BCrypt.Net

let register (name:string) (email:string) (password:string) : Result<int,string> =
    try
        let hash = BCrypt.HashPassword(password)
        let user = { UserId = 0; Name = name; Email = email; Password = hash; Role = true }
        let userId = addUser user  
        Ok userId
    with ex ->
        Error ex.Message


let printUserInfo userId =
    match getUserById userId with
    | Some user -> printfn "User: %s, Email: %s" user.Name user.Email
    | None -> printfn "User not found"

let login (email:string) (password:string) =
    match getUserByEmail email with
    | Some user ->
        if BCrypt.Verify(password, user.Password) then
            Ok user
        else
            Error "Incorrect password"
    | None -> Error "User not found"
