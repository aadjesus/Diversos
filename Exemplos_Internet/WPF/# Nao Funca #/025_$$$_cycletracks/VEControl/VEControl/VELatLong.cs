namespace VEControl
{
    public class VELatLong
    {
        double lat;
        double lon;
        double alt;
        string altMode;

        public VELatLong(double lat, double lon)
            : this ( lat, lon, 0 )
        {
            this.lat = lat;
            this.lon = lon;
        }

        public VELatLong(double lat, double lon, double alt)
            : this( lat, lon, alt, null )
        {
        }

        public VELatLong(double lat, double lon, double alt, string altMode)
        {
            this.lat = lat;
            this.lon = lon;
            this.alt = alt;
            this.altMode = altMode;
        }

        public double Latitude
        {
            get { return this.lat; }
            set { this.lat = value; }
        }

        public double Longitude
        {
            get { return this.lon; }
            set { this.lon = value; }
        }

        public double Altitude
        {
            get { return this.alt; }
            set { this.alt = value; }
        }

        public string AltMode
        {
            get { return this.altMode; }
            set { this.altMode = value; }
        }

        public override string ToString()
        {
            return this.lat + "|" + this.lon;
        }
    }
}
