// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;

namespace Espresso {
    public class ParseException : Exception {
        Location location;

        public ParseException(Location loc, string message)
            : base(message) {
            this.location = loc;
        }

        public Location Location {
            get { return this.location; }
        }

        public override string ToString() {
            return string.Format("({0},{1}): {2}", this.location.Line, this.location.Column, this.Message);
        }
    }
}