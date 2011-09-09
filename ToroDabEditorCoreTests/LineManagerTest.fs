// Learn more about F# at http://fsharp.net

module LineManagerTests

open System
open Xunit
open ToroDabEditor

[<Fact>]
let FirstText() = 
    let xx = new ToroDabEditor.TextLineManager()
   // let result = xx.CreateTheLines ("SomeContect", 0,0)
    let max = 1
    let min = 1

    Assert.Equal(min, max);

[<Fact>]
let GetDelimiterSegmentsFromText() =
    let sut = new ToroDabEditor.TextLineManager()
    sut.GetDelimiterSegmentsFromText("My \r\nName is \r\nAdedayo \r\nAbdulrahaman \r\nBaruwa")
    
    Assert.Equal(4,  sut.Segments.Length)

[<Fact>]
let LineSegmentShouldContainCorrectLineDefinitions() = 
    let sut = new ToroDabEditor.TextLineManager()
    sut.GetDelimiterSegmentsFromText("My \r\nName is \r\nAdedayo \r\nAbdulrahaman \r\nBaruwa")
    sut.GetLinesSegmentsFromDelimiterSegments()
    Assert.Equal(4,  sut.LineSegments.[0].TotalLength)

[<Fact>]
let ShouldGetSegmentForAGivenOffset() =
    let sut = new ToroDabEditor.TextLineManager()
    sut.GetDelimiterSegmentsFromText("My \r\nName is \r\nAdedayo \r\nAbdulrahaman \r\nBaruwa")
    sut.GetLinesSegmentsFromDelimiterSegments()
    let result = sut.FindLine(6, sut.LineSegments)
    Assert.Equal(1, result)

    let result = sut.FindLine(1, sut.LineSegments)
    Assert.Equal(0,result)

    let result = sut.FindLine(16, sut.LineSegments)
    Assert.Equal(2,result)

    let result = sut.FindLine(26, sut.LineSegments)
    Assert.Equal(3,result)

    let result = sut.FindLine(41, sut.LineSegments)
    Assert.Equal(3,result)

