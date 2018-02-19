// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace Espresso {

    public struct Token {
        TokenId id;
        Location location;
        object value;
        public Token(TokenId id, Location location) {
            this.id = id;
            this.location = location;
            this.value = null;
        }
        public Token(TokenId id, Location location, object value) {
            this.id = id;
            this.location = location;
            this.value = value;
        }      
        public TokenId Id {
            get { return this.id; }
        }  
        public Location Location {
            get { return this.location; }
        }
        public object Value {
            get { return this.value; }
        }
        public string Text {
            get { return this.value.ToString(); }
        }
        public override string ToString() {
            switch (this.id) {
                case TokenId.Identifier:
                case TokenId.Literal:
                case TokenId.Character:
                case TokenId.Keyword:
                    return string.Format("{0}: {1}", this.id, this.value);
                default:
                    return id.ToString();
            }
        }
    }
    
    public enum TokenId {
        Unknown = 0,
        
        // punctuation
        Squiggle,
        BackTick,
        ExclamationPoint,
        AtSymbol,
        HashMark,
        Dollar,
        Percent,
        Hat,
        Ampersand,
        Asterisk,
        OpenParen,
        CloseParen,
        Underscore,
        Minus,
        Plus,
        Equals,
        OpenBracket,
        CloseBracket,
        OpenBrace,
        CloseBrace,
        Bar,
        BackSlash,
        Colon,
        Semicolon,
        DoubleQuote,
        SingleQuote,
        OpenAngle,
        Comma,
        CloseAngle,
        Dot,
        QuestionMark,
        Slash,
        
        // common combinations
        DoubleBar,
        DoubleColon,
        DoubleAmpersand,
        DoubleQuestion,
        DoubleEqual,
        OpenAngleEqual,
        CloseAngleEqual,
        ExclamationEqual,
        OpenCloseAngle,       
        
        // common meta tokens
        Identifier,
        Keyword,
        Character,
        Literal,
        Comment,
        Whitespace
    }
    
    internal class Lexer {
        List<Token> tokens;
        int iToken;
        
        internal Lexer(Scanner scanner) {
            this.tokens = new List<Token>();
            ScanTokens(scanner);
        }
        
        public Token Current {
            get {
                if (this.HasTokens())
                    return this.tokens[this.iToken];
                return default(Token);
            }
        }
        
        public Token Peek(int n) {
            if (this.iToken + n < this.tokens.Count) {
                return this.tokens[this.iToken + n];
            }
            return default(Token);
        }
        
        public void MoveNext() {
            if (this.HasTokens())
                this.iToken++;
        }
        
        public bool HasTokens() {
            return this.iToken < this.tokens.Count;
        }
        
        private void ScanTokens(Scanner scanner) {
            this.tokens.Clear();
            this.iToken = 0;
            while(true) {
                Token token = this.ScanToken(scanner);
                if (token.Id == TokenId.Unknown)
                    break;
                this.tokens.Add(token);
            }
        }
        
        protected virtual Token ScanToken(Scanner scanner) {
            Token token;
            Location loc = scanner.Location;
            switch(scanner.Current) {
                case '~':
                    token = new Token(TokenId.Squiggle, loc);
                    scanner.MoveNext();
                    break;
                case '`':
                    token = new Token(TokenId.BackTick, loc);
                    scanner.MoveNext();
                    break;
                case '!':
                    scanner.MoveNext();
                    if (scanner.Current == '=') {
                        token = new Token(TokenId.ExclamationEqual, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.ExclamationPoint, loc);
                    break;
                case '@':
                    token = new Token(TokenId.AtSymbol, loc);
                    scanner.MoveNext();
                    break;
                case '#':
                    token = this.ScanDateTimeLiteral(scanner);
                    break;
                case '$':
                    token = new Token(TokenId.Dollar, loc);
                    scanner.MoveNext();
                    break;
                case '%':
                    token = new Token(TokenId.Percent, loc);
                    scanner.MoveNext();
                    break;
                case '^':
                    token = new Token(TokenId.Hat, loc);
                    scanner.MoveNext();
                    break;
                case '&':
                    scanner.MoveNext();
                    if (scanner.Current == '&') {
                        token = new Token(TokenId.DoubleAmpersand, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.Ampersand, loc);
                    break;
                case '*':
                    token = new Token(TokenId.Asterisk, loc);   
                    scanner.MoveNext();
                    break;
                case '(':
                    token = new Token(TokenId.OpenParen, loc);   
                    scanner.MoveNext();
                    break;
                case ')':
                    token = new Token(TokenId.CloseParen, loc);   
                    scanner.MoveNext();
                    break;
                case '_':
                    token = new Token(TokenId.Underscore, loc);   
                    scanner.MoveNext();
                    break;
                case '-':
                    token = new Token(TokenId.Minus, loc);   
                    scanner.MoveNext();
                    break;
                case '+': 
                    token = new Token(TokenId.Plus, loc);   
                    scanner.MoveNext();
                    break;
                case '=':
                    scanner.MoveNext();
                    if (scanner.Current == '=') {
                        token = new Token(TokenId.DoubleEqual, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.Equals, loc);
                    break;
                case '{':
                    token = new Token(TokenId.OpenBrace, loc);   
                    scanner.MoveNext();
                    break;
                case '}':
                    token = new Token(TokenId.CloseBrace, loc);
                    scanner.MoveNext();
                    break;
                case '[':
                    token = new Token(TokenId.OpenBracket, loc);
                    scanner.MoveNext();
                    break;
                case ']':
                    token = new Token(TokenId.CloseBracket, loc);
                    scanner.MoveNext();
                    break;
                case '|':
                    scanner.MoveNext();
                    if (scanner.Current == '|') {
                        token = new Token(TokenId.DoubleBar, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.Bar, loc);
                    break;
                case '\\':
                    token = new Token(TokenId.BackSlash, loc);
                    scanner.MoveNext();
                    break;                
                case ':':
                    scanner.MoveNext();
                    if (scanner.Current == ':') {
                        token = new Token(TokenId.DoubleColon, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.Colon, loc);
                    break;
                case ';':
                    token = new Token(TokenId.Semicolon, loc);
                    scanner.MoveNext();
                    break;                
                case '"':
                    token = this.ScanStringLiteral(scanner, '"');
                    break;                
                case '\'':
                    //token = this.ScanCharLiteral(scanner);
                    token = this.ScanStringLiteral(scanner, '\'');
                    break;                
                case '<':
                    scanner.MoveNext();
                    if (scanner.Current == '=') {
                        token = new Token(TokenId.OpenAngleEqual, loc);
                        scanner.MoveNext();
                        break;
                    }
                    else if (scanner.Current == '>') {
                        token = new Token(TokenId.OpenCloseAngle, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.OpenAngle, loc);
                    break;
                case '>':
                    scanner.MoveNext();
                    if (scanner.Current == '=') {
                        token = new Token(TokenId.CloseAngleEqual, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.CloseAngle, loc);
                    break;
                case ',':
                    token = new Token(TokenId.Comma, loc);
                    scanner.MoveNext();
                    break;                
                case '.':
                    token = new Token(TokenId.Dot, loc);
                    scanner.MoveNext();
                    break;                
                case '?':
                    scanner.MoveNext();
                    if (scanner.Current == '?') {
                        token = new Token(TokenId.DoubleQuestion, loc);
                        scanner.MoveNext();
                        break;
                    }
                    token = new Token(TokenId.QuestionMark, loc);
                    break;
                case '/':
                    token = new Token(TokenId.Slash, loc);
                    scanner.MoveNext();
                    break;                
                default:
                    char c = scanner.Current;
                    if (Char.IsWhiteSpace(c)) {
                        scanner.SkipWhitespace();
                        return this.ScanToken(scanner);
                    }
                    else if (Char.IsLetter(c)) {
                        token = this.ScanIdentifier(scanner);
                    }
                    else if (char.IsDigit(c)) {
                        token = this.ScanNumericLiteral(scanner);
                    }
                    else if (scanner.HasChars()) {
                        token = new Token(TokenId.Character, loc, scanner.Current);
                        scanner.MoveNext();
                    }
                    else {
                        token = default(Token);
                    }
                    break;
            }             
            return token;           
        }
        
        protected virtual Token ScanIdentifier(Scanner scanner) {
            Location loc = scanner.Location;
            while (Char.IsLetterOrDigit(scanner.Current)) {
                scanner.MoveNext();
            }
            string id = scanner.GetText(loc, scanner.Location);
            return new Token(TokenId.Identifier, loc, id);
        }
        
        protected void Assert(Scanner scanner, char c) {
            if (scanner.Current != c)
                throw new ParseException(scanner.Location, string.Format("Expected {0}", c));
        }

        protected void AssertDigit(Scanner scanner) {
            if (!Char.IsDigit(scanner.Current))
                throw new ParseException(scanner.Location, "Digit expected");
        }
        
        private bool IsHexDigit(char c) {
            return Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }        
        
        protected virtual Token ScanStringLiteral(Scanner scanner, char quoteMark) {
            return new Token(TokenId.Literal, scanner.Location, this.ScanStringValue(scanner, quoteMark));
        }
        
        protected virtual string ScanStringValue(Scanner scanner, char quoteMark) {
            Assert(scanner, quoteMark);
            scanner.MoveNext();            
            Location start = scanner.Location;
            scanner.SkipToChar(quoteMark);
            string value = scanner.GetText(start, scanner.Location);
            scanner.MoveNext();
            return value;
        }
        
        protected virtual Token ScanCharLiteral(Scanner scanner) {
            return new Token(TokenId.Literal, scanner.Location, this.ScanCharValue(scanner, '\''));
        }
        
        protected virtual char ScanCharValue(Scanner scanner, char quoteMark) {
            Assert(scanner, quoteMark);
            scanner.MoveNext();
            char c = this.ScanChar(scanner);
            Assert(scanner, quoteMark);
            scanner.MoveNext();
            return c;            
        }
        
        protected virtual char ScanChar(Scanner scanner) {
            char c = scanner.Current;
            if (c == '\\' && scanner.HasChars()) {
                scanner.MoveNext();
                c = scanner.Current;
                switch (c) {
                    case 'r': c = '\r'; break;
                    case 'n': c = '\n'; break;
                    case '0': c = '\0'; break;
                    case '\\': c = '\\'; break;
                }
            }
            scanner.MoveNext();
            return c;
        }

        protected virtual Token ScanNumericLiteral(Scanner scanner) {
            return new Token(TokenId.Literal, scanner.Location, this.ScanNumericValue(scanner));
        }
        
        protected virtual object ScanNumericValue(Scanner scanner) {
            this.AssertDigit(scanner);
            Location loc = scanner.Location;
            bool hasDot = false;
            bool hasExponent = false;
            while (true) {
                char c = scanner.Current;
                if (c == '.' && (!hasDot && !hasExponent)) {
                    hasDot = true;
                    scanner.MoveNext();
                    AssertDigit(scanner);
                    scanner.MoveNext();
                }
                else if ((c == 'E' || c == 'e') && !hasExponent) {
                    hasExponent = true;
                    scanner.MoveNext();
                    c = scanner.Current;
                    if (c == '+' || c == '-') {
                        scanner.MoveNext();
                        c = scanner.Current;
                    }
                    AssertDigit(scanner);
                    scanner.MoveNext();
                }
                else if (c == 'U') {
                    if (scanner.Peek(1) == 'L') {
                        UInt64 value = UInt64.Parse(scanner.GetText(loc, scanner.Location));
                        scanner.Skip(2);
                        return value;
                    }
                    else {
                        UInt32 value = UInt32.Parse(scanner.GetText(loc, scanner.Location));
                        scanner.MoveNext();
                        return value;
                    }
                }
                else if (c == 'L') {
                    Int64 value = Int64.Parse(scanner.GetText(loc, scanner.Location));
                    scanner.MoveNext();
                    return value;
                }
                else if (c == 'f') {
                    Single value = Single.Parse(scanner.GetText(loc, scanner.Location));
                    scanner.MoveNext();
                    return value;                    
                }
                else if (c == 'M' && !hasExponent) {
                    Decimal value = Decimal.Parse(scanner.GetText(loc, scanner.Location));
                    scanner.MoveNext();
                    return value;
                }
                else if (Char.IsDigit(c)) {
                    scanner.MoveNext();
                }
                else {
                    break;
                }
            }
            if (hasDot) {
                return Double.Parse(scanner.GetText(loc, scanner.Location));
            }
            else {
                return Int32.Parse(scanner.GetText(loc, scanner.Location));
            }
        }
        
        protected virtual Token ScanDateTimeLiteral(Scanner scanner) {
            return new Token(TokenId.Literal, scanner.Location, this.ScanDateTimeValue(scanner, '#'));
        }

        protected virtual DateTime ScanDateTimeValue(Scanner scanner, char quoteMark) {
            string text = this.ScanStringValue(scanner, quoteMark);
            return DateTime.Parse(text);
        }        
    }
}
