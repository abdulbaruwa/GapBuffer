using System;
using System.Text;

namespace GapBuffer
{
    public class DocumentBuffer
    {
        private char[] _buffer = new char[0];
        private int _gapPreSize = 0;
        private int _gapPostSize = 0;
        private int _maxGapLength = 5;
        private int _minGapLength = 2;


        public void LoadContent(string text)
        {
            if(text == null) text = string.Empty;
            _buffer = text.ToCharArray();
        }

        public int Length
        {
            get { return _buffer.Length + GapLength; }
        }

        public int GapLength
        {
            get
            {
                return _gapPostSize - _gapPreSize;
            }
        }

        public char GetCharAt(int offSet)
        {
            if(offSet < _gapPreSize)
            {
                return _buffer[offSet];
            }
            return _buffer[offSet + GapLength];
        }

        
        public string GetText(int offSet, int length)
        {
            int endposition = offSet + length;
            if (endposition < _gapPreSize)
            {
                return new string(_buffer, offSet, length);
            }

            if(offSet > _gapPreSize)
            {
                return  new string(_buffer,offSet + GapLength,length);
            }

            StringBuilder buf = new StringBuilder();
            buf.Append(_buffer, offSet, _gapPreSize - offSet);
            buf.Append(_buffer, _gapPostSize, endposition- _gapPreSize);
            return buf.ToString();
        }

        public void PlacGap(int offSet, int length)
        {
            int deltaLength = GapLength - length;
            //If the gap has the right length, move the chars between offSet and gap
            if (_minGapLength <= deltaLength && deltaLength <= _maxGapLength)
            {
                int delta = _gapPreSize - offSet;
                //Is the gap already in place?
                if (offSet == _gapPreSize)
                {
                    return;
                }
                else if (offSet < _gapPreSize)
                {
                    int gapLength = _gapPostSize - _gapPreSize;
                    Array.Copy(_buffer, offSet, _buffer, offSet + gapLength, delta);
                }
                else
                {
                    Array.Copy(_buffer, _gapPostSize, _buffer, _gapPreSize, -delta);
                }
                _gapPreSize -= delta;
                _gapPostSize -= delta;
                return;
            }

            // the gap has not the right length so
            // create new Buffer with new size and copy
            int oldLength = GapLength;
            int newLength = _maxGapLength + length;
            int newGapEndOffset = offSet + newLength;
            char[] newBuffer = new char[_buffer.Length + newLength - oldLength];

            if (oldLength == 0)
            {
                Array.Copy(_buffer, 0, newBuffer, 0, offSet);
                Array.Copy(_buffer, offSet, newBuffer, newGapEndOffset, newBuffer.Length - newGapEndOffset);
            }
            else if (offSet < _gapPreSize)
            {
                int delta = _gapPreSize - offSet;
                Array.Copy(_buffer, 0, newBuffer, 0, offSet);
                Array.Copy(_buffer, offSet, newBuffer, newGapEndOffset, delta);
                Array.Copy(_buffer, _gapPostSize, newBuffer, newGapEndOffset + delta, _buffer.Length - _gapPostSize);
            }
            else
            {
                int delta = offSet - _gapPreSize;
                Array.Copy(_buffer, 0, newBuffer, 0, _gapPreSize);
                Array.Copy(_buffer, _gapPostSize, newBuffer, _gapPreSize, delta);
                Array.Copy(_buffer, _gapPostSize + delta, newBuffer, newGapEndOffset, newBuffer.Length - newGapEndOffset);
            }

            _buffer = newBuffer;
            _gapPreSize = offSet;
            _gapPostSize = newGapEndOffset;
        }

    }
}