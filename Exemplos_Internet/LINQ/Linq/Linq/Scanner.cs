// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;

namespace Espresso {
    
    public struct Location {
        int position;
        int line;
        int column;
        internal Location(int position, int line, int column) {
            this.position = position;
            this.line = line;
            this.column = column;
        }
        public int Position {
            get { return this.position; }
        }
        public int Line {
            get { return this.line; }            
        }
        public int Column {
            get { return this.column; }
        }
    }
    
    internal class Scanner {
        string text;
        int iPos;
        int iLine;
        int iOffset;

        internal Scanner(string text) {
            this.text = text;
            this.iLine = 1;
        }

        public Location Location {
            get { return new Location(this.iPos, this.iLine, this.iOffset); }
            set { this.iPos = value.Position; this.iLine = value.Line; this.iOffset = value.Column; }
        }

        public bool HasChars() {
            return this.iPos < this.text.Length;
        }

        public bool HasChars(int n) {
            return this.iPos <= this.text.Length - n;
        }

        public char Current {
            get { 
                if (this.iPos < this.text.Length)
                    return this.text[this.iPos];
                return (char)0;
            }
        }

        public char Peek(int n) {
            if (this.iPos + n < this.text.Length)
                return this.text[this.iPos + n];
            return (char)0;
        }

        public void MoveNext() {
            if (this.Current == '\r') {
                this.iPos++;
                if (this.Current == '\n')
                    this.iPos++;
                this.iLine += 1;
                this.iOffset = 0;
            }
            else if (this.Current == '\n') {
                this.iPos++;
                if (this.Current == '\r')
                    this.iPos++;
                this.iLine++;
                this.iOffset++;
            }
            else {
                this.iPos++;
                this.iOffset++;
            }
        }

        public void Skip(int n) {
            int iStart = this.iPos;
            while(this.HasChars() && (this.iPos - iStart) < n) {
                this.MoveNext();
            }
        }

        public void SkipWhitespace() {
            while (Char.IsWhiteSpace(this.Current)) {
                this.MoveNext();
            }
        }

       public void SkipToChar(char c) {
            while (this.HasChars() && this.Current != c) {
                this.MoveNext();
            }
        }
        
        public void SkipPastChar(char c) {
            this.SkipToChar(c);
            this.MoveNext();
        }

        public string GetText(Location start, Location end) {
            return this.text.Substring(start.Position, end.Position - start.Position);
        }
    }
}