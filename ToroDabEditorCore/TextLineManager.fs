namespace ToroDabEditor

open System
open System.Text

type TextLineManager() =
    let mutable _lineSegments : List<LineSegment> = []
    let mutable _segments : List<Segment> = []
    let mutable _textLength = 0
    let mutable _totalLineNumbers = 0
    let mutable _contentArray : char array = Array.zeroCreate 0

    member this.Segments with get () = _segments
    member this.ContentArray with get () = _contentArray
    member this.LineSegments with get () = _lineSegments

    member this.ResetContent(content:string) =
        _lineSegments <- []
        _textLength <- 0
        _totalLineNumbers <- 0
        _contentArray <- Array.zeroCreate 0
        this.GetDelimiterSegmentsFromText(content)
        this.GetLinesSegmentsFromDelimiterSegments()

    member this.GetDelimiterSegmentsFromText(content:string) =
        _contentArray <- content.ToCharArray()

        let delimiterSegments = 
                                [
                                    for i in 0 .. _contentArray.Length - 1 do
                                        let character = _contentArray.[i]
                                        match character with
                                        | '\r' -> 
                                                    let nextChar = _contentArray.[i + 1]
                                                    match nextChar with 
                                                    | '\n' -> 
                                                            let segment = new Segment(i, 2)
                                                            yield segment
                                                    | _ -> yield new Segment(i,1)
                                        | '\n' -> 
                                                    let lastChar = _contentArray.[i - 1]
                                                    match lastChar with
                                                    | '\r' -> ()
                                                    | _ -> yield new Segment(i, 1)
                                        | _ -> () 
                                ]
        _segments <- delimiterSegments

    member this.GetLinesSegmentsFromDelimiterSegments() =
        let lineSegments =
                        [
                            for i in 0 .. _segments.Length - 1 do 
                                let x = _segments.[i]
                                if i = 0 then
                                    //this is the first segment/word
                                    let lineSegment = new LineSegment(0, x.Offset, x.Length)
                                    yield lineSegment
                                else
                                    let previousSegment = _segments.[i - 1]
                                    let nextLineStartPosition = previousSegment.Offset + previousSegment.Length
                                    let lineSegment = new LineSegment(nextLineStartPosition, x.Offset, x.Length)
                                    yield lineSegment
                        ]
        _lineSegments <- lineSegments


    //Gets a the Line from the collectin of LineSegments given an Offset for a character within the line
    member this.FindLine(offset:int, segments:List<LineSegment>) =
        
        let rec FindLineSegment(offset:int, segments:List<LineSegment>, rightIndex:int, leftIndex:int) =
            if leftIndex < rightIndex then
                let middlePivot = (rightIndex + leftIndex) / 2
                let currentLine = segments.[middlePivot]
                if offset < currentLine.Offset then
                    let newRightIndex = middlePivot - 1
                    FindLineSegment(offset, segments, newRightIndex, leftIndex)
                elif offset > currentLine.Offset then
                    let newLeftIndex = middlePivot + 1
                    FindLineSegment(offset, segments, rightIndex, newLeftIndex)
                else
                    middlePivot 
            else
                leftIndex

        let rIndex = segments.Length - 1
        let lIndex = 0
        let index = FindLineSegment(offset, segments, rIndex, lIndex)
        if segments.[index].Offset > offset then
            index - 1
        else
           index 

    member this.PrintSegements() =
        printfn "segment length %d" _segments.Length
        for i in 0.. _segments.Length - 1 do
            printfn "Segment item: %d, Offset:%d, Length:%d" i, _segments.[i].Offset, _segments.[i].Length

