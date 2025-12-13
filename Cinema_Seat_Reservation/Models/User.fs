module Models.User

type User = {
    UserId : int
    Name : string
    Email : string
    Password : string
    Role : bool   // false = admin true = user
}
