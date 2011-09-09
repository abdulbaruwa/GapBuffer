namespace ToroDabEditor

open System
open System.IO

type Document(textBuffer:TextGapBuffer, lineManager:TextLineManager) = 
    let mutable _textLineManager = lineManager
    let mutable _textGapBuffer = textBuffer
    let mutable _textContent = String.Empty
        
    let SetTextContent(content:string) = 
            _textGapBuffer.InsertString(content)
            _textLineManager.ResetContent(content)
            content
    interface IDocument with
        member this.TextBuffer with get () = _textGapBuffer
                                  and set value = _textGapBuffer <- value

        member this.TextLineManager with get () = _textLineManager 
                                        and set value = _textLineManager <- value 
    

        member this.TextContent with get () = _textGapBuffer.GetText()

        //Inserts a string of characters into the text sequence
        member this.InsertAt(offset, content) = 
                        _textGapBuffer.InsertStringAt(offset, content)
                        _textLineManager.ResetContent(content)

        member this.Insert(content) =
            _textGapBuffer.InsertString(content)
            _textLineManager.ResetContent(content)

        //Remove parts of the sequence
        member this.Remove(offset, length) =    
                        printfn "remove method: Not implemented"

    

        

