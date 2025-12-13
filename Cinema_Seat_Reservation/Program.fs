namespace Cinema_Seat_Reservation

open System
open Models.User
open Models.Hall
open Models.Movie
open Models.Show
open Services.UserService
open Services.MovieService
open Services.HallService
open Services.ShowService
open Services.SeatService
open Services.TicketService

module Program =

    // USER MENU (Booking / Cancel / View)
    let rec userMenu userId =
        printfn "\n===== USER MENU ====="
        printfn "1) Book a ticket"
        printfn "2) Cancel a ticket"
        printfn "3) View My Tickets"
        printfn "0) Logout"
        printf "Choose: "

        match Console.ReadLine() with
        | "1" ->
            let movies = getAllMoviesService()
            printfn "\nAvailable Movies:"
            movies |> List.iter (fun m -> printfn "%d: %s" m.MovieId m.Title)

            printf "Select Movie ID: "
            let movieId = int (Console.ReadLine())

            let halls = getHallsByMovieService movieId
            if List.isEmpty halls then
                printfn "No halls found for this movie."
                userMenu userId
            else
                printfn "\nAvailable Halls:"
                halls |> List.iter (fun h -> printfn "%d: %s" h.HallId h.Name)

                printf "Select Hall ID: "
                let hallId = int (Console.ReadLine())
                let selectedHall = halls |> List.find (fun h -> h.HallId = hallId)

                let shows = getShowTimes selectedHall.HallId movieId
                if List.isEmpty shows then
                    printfn "No shows available for this hall."
                    userMenu userId
                else
                    printfn "\nAvailable Shows for Hall %s:" selectedHall.Name
                    shows |> List.iter (fun s ->
                        printfn "ShowID: %d | %s → %s"
                            s.ShowId
                            (s.StartTime.ToString("yyyy-MM-dd HH:mm"))
                            (s.EndTime.ToString("yyyy-MM-dd HH:mm"))
                    )

                    printf "Select Show ID: "
                    let showId = int (Console.ReadLine())

                    let seats = loadSeatsWithValidationCSharp selectedHall.HallId showId selectedHall.Rows selectedHall.Columns
                    printfn "\nCurrent Seats Status (true=booked, false=available):"
                    for r in 0 .. selectedHall.Rows - 1 do
                        for c in 0 .. selectedHall.Columns - 1 do
                            printf "%b " seats.[r, c]
                        printfn ""

                    printf "\nEnter Row (0-based): "
                    let row = int (Console.ReadLine())

                    printf "Enter Column (0-based): "
                    let col = int (Console.ReadLine())

                    try
                        let seatInfo = getSeatForCSharp selectedHall.HallId showId row col
                        let seatId = seatInfo.SeatId

                        try
                            if bookSeatForCSharp seatId showId userId then
                                let ticketId = createTicketForCSharp userId showId seatId
                                printfn "✅ Ticket booked successfully! TicketID: %s" ticketId
                            else
                                printfn "❌ Seat is already booked!"
                        with ex ->
                            printfn "❌ Error during booking: %s" ex.Message

                    with ex ->
                        printfn "❌ Error retrieving seat: %s" ex.Message

                    userMenu userId

        | "2" ->
            printf "\nEnter Ticket ID to cancel: "
            let ticketId = Console.ReadLine()

            if cancelTicket ticketId userId then
                printfn "✅ Ticket cancelled successfully!"
            else
                printfn "❌ Cancellation failed."

            userMenu userId

        | "3" ->
            let tickets = getUserTickets userId
            if List.isEmpty tickets then
                printfn "You have no valid tickets."
            else
                printfn "\n🎫 Your Tickets:"
                tickets
                |> List.iter (fun t ->
                    printfn "TicketID: %s | Movie: %s | Hall: %s | Seat: R%dC%d | %s → %s"
                        t.TicketID t.MovieTitle t.HallName t.Row t.Column
                        (t.StartTime.ToString("yyyy-MM-dd HH:mm"))
                        (t.EndTime.ToString("yyyy-MM-dd HH:mm"))
                )
            userMenu userId

        | "0" ->
            printfn "Logging out..."

        | _ ->
            printfn "Invalid option."
            userMenu userId

    // ADMIN MENU
    let rec adminMenu () =
        printfn "\n===== ADMIN MENU ====="
        printfn "1) Add Movie With ShowTime"
        printfn "2) Add ShowTime for existing movie"
        printfn "3) View Movies"
        printfn "0) Logout"
        printf "Choose: "

        match Console.ReadLine() with
        | "1" ->
            printf "\nTitle: "
            let title = Console.ReadLine()

            printf "Duration (minutes): "
            let duration = Int32.Parse(Console.ReadLine())

            printf "HallId: "
            let hallId = Int32.Parse(Console.ReadLine())

            printf "Start (yyyy-MM-dd HH:mm): "
            let startStr = Console.ReadLine()
            let startTime = DateTime.ParseExact(startStr, "yyyy-MM-dd HH:mm", Globalization.CultureInfo.InvariantCulture)

            match addMovieWithShowTime title duration hallId startTime with
            | Ok msg -> printfn "Success: %s" msg
            | Error err -> printfn "Error: %s" err

            adminMenu ()

        | "2" ->
            printf "\nMovieId: "
            let movieId = Int32.Parse(Console.ReadLine())

            printf "HallId: "
            let hallId = Int32.Parse(Console.ReadLine())

            printf "Start (yyyy-MM-dd HH:mm): "
            let startStr = Console.ReadLine()
            let startTime = DateTime.ParseExact(startStr, "yyyy-MM-dd HH:mm", Globalization.CultureInfo.InvariantCulture)

            match addShowTimeForExistingMovie movieId hallId startTime with
            | Ok msg -> printfn "Success: %s" msg
            | Error err -> printfn "Error: %s" err

            adminMenu ()

        | "3" ->
            let movies = getAllMoviesService()
            printfn "\n--- Movies List ---"
            movies |> List.iter (fun m -> printfn "%d - %s" m.MovieId m.Title)
            adminMenu ()

        | "0" ->
            printfn "Logging out..."

        | _ ->
            printfn "Invalid option."
            adminMenu ()

    // MAIN PROGRAM
    [<EntryPoint>]
    let main argv =
        printfn "===== CINEMA SYSTEM ====="
        printfn "1) Login"
        printfn "2) Register"
        printfn "0) Exit"
        printf "Choose: "

        match Console.ReadLine() with
        | "1" ->
            printf "Email: "
            let email = Console.ReadLine()

            printf "Password: "
            let password = Console.ReadLine()

            match login email password with
            | Ok user ->
                if not user.Role then
                    printfn "\nWelcome Admin %s!" user.Name
                    adminMenu ()
                else
                    printfn "\nWelcome %s!" user.Name
                    userMenu user.UserId
            | Error msg -> printfn "Login Error: %s" msg

        | "2" ->
            printf "Name: "
            let name = Console.ReadLine()

            printf "Email: "
            let email = Console.ReadLine()

            printf "Password: "
            let password = Console.ReadLine()

            match register name email password with
            | Ok id -> printfn "Registered successfully! Your ID is %d" id
            | Error msg -> printfn "Register Error: %s" msg

        | "0" -> printfn "Exiting..."
        | _ -> printfn "Invalid option."

        0
