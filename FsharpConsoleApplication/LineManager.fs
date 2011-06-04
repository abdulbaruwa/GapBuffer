module LineManager

type Segment() = 
    let mutable _offset = 0
    let mutable _length = 0 

    member this.Offset with get () = _offset
                       and set newOffset = _offset <- newOffset
    
    member this.Length with get () = _length
                       and set newlength = _length <- newlength
                        
type ITextLineManager =
    abstract LineSegments : List<Segment>
    abstract TotalLineNumbers : int
