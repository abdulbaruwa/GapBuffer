// Learn more about F# at http://fsharp.net

module gapBuffer

open System
open ToroDabEditor
open System.Text

let x =
        printfn "My \r\nName is \r\nAdedayo \r\nAbdulrahaman \r\nBaruwa"
        let y = 0
        let yx = 1
        let sut = new ToroDabEditor.TextLineManager()
        let content =  "My \r\nName is \r\nAdedayo \r\nAbdulrahaman \r\nBaruwa"
        sut.GetDelimiterSegmentsFromText(content)
        let lineSegments = sut.GetLinesSegmentsFromDelimiterSegments()
        
        for i in 0 .. sut.LineSegments.Length - 1 do
            let linestring = content.Substring(sut.LineSegments.[i].Offset, sut.LineSegments.[i].TotalLength + 1)
            printfn "*** %s" linestring

        sut.PrintSegements()
        Console.ReadKey()

         

