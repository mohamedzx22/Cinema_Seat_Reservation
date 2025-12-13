namespace Cinema_Seat_Reservation

module TicketReserve =

    open System
    open Cinema_Seat_Reservation.Db

    // عرض الكراسي على شكل 2D array
    let displaySeats hallId =
        let seats = Db.getSeats hallId
        // تحديد عدد الصفوف والأعمدة
        let maxRow = seats |> List.maxBy (fun (_,r,_,_) -> r) |> fun (_,r,_,_) -> r
        let maxCol = seats |> List.maxBy (fun (_,_,c,_) -> c) |> fun (_,_,c,_)-> c

        // إنشاء مصفوفة 2D للكراسي
        let arr = Array2D.create (maxRow+1) (maxCol+1) "O" // O = Free

        // تحديث المصفوفة حسب المقاعد المحجوزة
        seats |> List.iter (fun (_,r,c,isBooked) ->
            if isBooked then arr.[r,c] <- "X"
        )

        // طباعة القاعة في Console
        printfn "\nSeats Layout (O=Free, X=Booked):"
        for r in 0..maxRow do
            for c in 0..maxCol do
                printf "%s " arr.[r,c]
            printfn ""
        arr

    // دالة للحجز بناءً على اختيار المستخدم
    let reserveSeat userId showId hallId =
        let arr = displaySeats hallId

        // إدخال الصف والعمود من المستخدم
        printf "\nEnter Row Number: "
        let row = Console.ReadLine() |> int
        printf "Enter Column Number: "
        let col = Console.ReadLine() |> int

        // إيجاد SeatId من Db
        let seat = Db.getSeats hallId |> List.tryFind (fun (_,r,c,_) -> r=row && c=col)
        match seat with
        | Some (seatId,_,_,isBooked) ->
            if isBooked then
                printfn "\n❌ Seat already booked!"
            else
                // حجز المقعد وتوليد Ticket ID
                Db.bookSeat userId showId seatId
        | None ->
            printfn "\n❌ Invalid seat selected!"
