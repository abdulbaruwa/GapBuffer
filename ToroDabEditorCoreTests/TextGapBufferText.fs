module TextGapBufferText

open System
open System.Text
open Xunit
open ToroDabEditor


[<Fact>]
let AbleToInsertStringTextIntoBuffer() =
    let sut = new TextGapBuffer(5)
    let insertString = "Abdulrasheed Baruwa"
    sut.InsertString insertString
    let length = insertString.Length
    Assert.Equal(0, sut.PostCursor)
    Assert.Equal(length, sut.PreCursor)

[<Fact>]
let MoveCursorShouldMoveCursor() =
    let sut = new TextGapBuffer(10)
    let insertString = "Abdulrasheed Baruwa"
    sut.InsertString insertString
    let length = insertString.Length    
    sut.MoveCursorBackwards()
    Assert.Equal(length-1,sut.PreCursor)




