namespace ToroDabEditor

open System

type Segment(offset:int, length:int) = 
    let mutable _offset = offset
    let mutable _length = length

    member this.Offset with get () = _offset
                       and set value = _offset <- value
    
    member this.Length with get () = _length
                       and set value = _length <- value

          
type LineSegment(offset:int, endpos:int, delimiterLength:int) =
    let mutable _offset = offset
    let mutable _length =  endpos - offset + 1
    let mutable _delimiterLength = delimiterLength

    member this.Offset with get () = _offset
                       and set value = _offset <- value
    
    member this.Length with get () = _length - delimiterLength

    member this.TotalLength with get () = _length
                            and set value = _length <- value

    member this.DelimiterLength with get () = _delimiterLength
                                and set value = _delimiterLength <- value
                        
    
and
    LineManagerEventHandler 
        (
            //_document : IDocument,
            _lineStart : int,
            _lineMoved : int) = 
        inherit System.EventArgs()
        
        //member this.Document = _document     
        member this.LineStart = _lineStart
        member this.LineMoved = _lineMoved

//Tracks lines in each file -> will be implemented side by side with the TextBuffer
type ITextLineManager =
    abstract LineSegments : System.Collections.Generic.List<Segment>
    abstract TotalLineNumbers : int
    // abstract Insert : int  -> string -> unit
    abstract SetContent : string -> unit
    //abstract LineCountChanged : LineManagerEventHandler

type ISegment =
    abstract Offset : int 
    abstract Length : int

