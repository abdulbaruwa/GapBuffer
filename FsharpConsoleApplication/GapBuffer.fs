open System
open System.IO

type AbGapBuffer(size : int) =
    let mutable _buffer = Array.zeroCreate size
    let mutable _size = size
    let mutable _preCursor = 0
    let mutable _postCursor = 0
    
    member this.MoveCursorForward() =
        if _postCursor > 0 then
            _buffer.[_preCursor] <- _buffer.[_postCursor - size]
            _buffer.[_postCursor - size] <- new char()   
            
            _postCursor <- _postCursor + 1
            _preCursor <- _preCursor - 1

    member this.InsertCharacter(character : char) = 
        if (_preCursor + _postCursor) = _size then
            this.InflateBuffer()
        _buffer.[_preCursor] <- character
        _preCursor <- _preCursor + 1

    member this.InsertString(text : string) = 
        for index in 0..text.Length-1 do 
            let character = text.Chars index
            this.InsertCharacter character

    member this.MoveCursorBackwards() = 
        if _preCursor > 0 then
           let character = _buffer.[_preCursor-1]
           _buffer.[_size - _postCursor-1] <- character
           _buffer.[_preCursor-1] <- new char()
           _postCursor <- _postCursor + 1
           _preCursor <- _preCursor - 1
    
    member this.MoveCursorFor(duration : int, forward : bool) =
        if forward then
            for _ in 0..duration do
                this.MoveCursorForward()
        else
            for _ in 0..duration do
                this.MoveCursorBackwards()

    member this.InflateBuffer() =
        let newSize = _size*2
        let mutable newBuffer : char array = Array.zeroCreate newSize
        for index in 0 .._preCursor-1 do
            newBuffer.[index] <- _buffer.[index]
            _buffer.[index] <- new char()

        for index in 0.._postCursor do
            newBuffer.[newSize-index-1] <- _buffer.[_size-index-1]
            _buffer.[_size-index-1] <- new char()

        _buffer <- newBuffer
        _size <- newSize

    member this.PrintChars() = 
        for index in 0.._buffer.Length - 1 do
            _buffer.[index].ToString() |> printfn "character %s"
        do _preCursor |> printfn "_preCursor: = %d"
        do _postCursor |> printfn "_postCursor: = %d";;

            

//Execute
let document = 
    let doc = new AbGapBuffer(10)
    doc.InsertString "Abdulrahaman Adedayo"
    doc.InsertString " Baruwa"
    doc.PrintChars()
    doc.MoveCursorBackwards()
    doc.MoveCursorBackwards()
    doc.InflateBuffer()
    doc.PrintChars()
    doc.MoveCursorFor(3, false)
    doc.PrintChars()
    Console.ReadKey;;

