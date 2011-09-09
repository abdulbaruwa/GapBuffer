namespace ToroDabEditor


open System

type IDocument = 
    abstract TextBuffer : TextGapBuffer with get, set
    abstract TextContent : string with get
    abstract TextLineManager : TextLineManager with get, set
    abstract member InsertAt : int * string -> unit
    abstract member Insert : string -> unit
    abstract member Remove : int * int -> unit

