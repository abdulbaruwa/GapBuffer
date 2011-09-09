module DocumentTest

open System
open Xunit
open ToroDabEditor

[<Fact>]
let InsertDocumentAndVerifyTextBufferAndLineManagerAreUpdated() = 
    //Arrange (buffer and line manager pre-constructed to be passed to Document)
    let textBuffer = new TextGapBuffer(10)
    let lineManager = new TextLineManager()

    let document = new Document(textBuffer, lineManager)
    let Idocument = document :> IDocument
    Idocument.InsertAt(0, "Hello world")
    let preCursor = Idocument.TextBuffer.PreCursor
    Assert.Equal(11, preCursor)

[<Fact>]
let ShouldSetContentGivenText() =
    let textBuffer = new TextGapBuffer(10)
    let lineManager = new TextLineManager()

    let document = new Document(textBuffer, lineManager)
    let Idocument = document:>IDocument

    //Insert some text first
    Idocument.Insert("Hello world")
    Assert.Equal("Hello world", Idocument.TextContent)
    //Idocument.TextContent <- "New text content to replace existing"
     

    

