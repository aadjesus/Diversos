using System;
using System.Text;

namespace nBASS
{
	/// <summary>
	/// Summary description for ID3v1Tag.
	/// </summary>
	public class ID3v1Tag //really 1.1 :)
	{
		string TAG; //last 3 bytes
		string songtitle; // 30 characters 
		string artist; // 30 characters 
		string album; //30 characters 
		string year; //4 characters 
		string comment; //28 characters
		byte track; // 1 byte, 0 byte b4 that
		byte genre;// 1 byte

		public ID3v1Tag(byte[] block)
		{
			if (block.Length != 128) throw new Exception("Black must be 128 bytes in size");
			TAG = Encoding.Default.GetString(block, 0, 3);
			songtitle = Encoding.Default.GetString(block, 3, 30);
			artist = Encoding.Default.GetString(block, 33, 30);
			album = Encoding.Default.GetString(block, 63, 30);
			year = Encoding.Default.GetString(block, 93, 4);
			comment = Encoding.Default.GetString(block, 97, 28);
			track = block[126];
			genre = block[127];
		}

		public string SongTitle {get {return songtitle;}}
		public string Artist {get {return artist;}}
		public string Album {get {return album;}}
		public string Year {get {return year;}}
		public string Comment {get {return comment;}}
		public int Track {get {return track;}}
		public string Genre {get {return ((GenreType)genre).ToString();}}

		private enum GenreType :byte
		{
			Blues = 0,
			ClassicRock,
			Country,
			Dance,
			Disco,
			Funk,
			Grunge,
			HipHop,
			Jazz,
			Metal,
			NewAge,
			Oldies,
			Other,
			Pop,
			RnB,
			Rap,
			Reggae,
			Rock,
			Techno,
			Industrial,
			Alternative,
			Ska,
			DeathMetal,
			Pranks,
			Soundtrack,
			EuroTechno,
			Ambient,
			TripHop,
			Vocal,
			JazzFunk,
			Fusion,
			Trance,
			Classical,
			Instrumental,
			Acid,
			House,
			Game,
			SoundClip,
			Gospel,
			Noise,
			AlternRock,
			Bass,
			Soul,
			Punk,
			Space,
			Meditative,
			InstrumentalPop,
			InstrumentalRock,
			Ethnic,
			Gothic,
			Darkwave,
			TechnoIndustrial,
			Electronic,
			PopFolk,
			Eurodance,
			Dream,
			SouthernRock,
			Cult,
			Gangsta,
			Top40,
			ChristianRap,
			PopFunk,
			Jungle,
			NativeAmerican,
			Cabaret,
			NewWave,
			Psychadelic,
			Rave,
			Showtunes,
			Trailer,
			LoFi,
			Tribal,
			AcidPunk,
			AcidJazz,
			Polka,
			Retro,
			Musical,
			RocknRoll,
			HardRock,
			None = 255,
		}

	}
}
