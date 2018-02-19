using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Istrib.Sound.Formats
{
	public partial struct Mp3BitRate
		: IEquatable<Mp3BitRate>, IComparable<Mp3BitRate>, IComparable
	{
		public static readonly Mp3BitRate BitRate8 = new Mp3BitRate(8, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate16 = new Mp3BitRate(16, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate24 = new Mp3BitRate(24, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate32 = new Mp3BitRate(32, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate40 = new Mp3BitRate(40, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate48 = new Mp3BitRate(48, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate56 = new Mp3BitRate(56, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate64 = new Mp3BitRate(64, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate80 = new Mp3BitRate(80, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate96 = new Mp3BitRate(96, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate112 = new Mp3BitRate(112, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate144 = new Mp3BitRate(144, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate128 = new Mp3BitRate(128, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate160 = new Mp3BitRate(160, MpegVersion.Mpeg1, MpegVersion.Mpeg2);
		public static readonly Mp3BitRate BitRate192 = new Mp3BitRate(192, MpegVersion.Mpeg1);
		public static readonly Mp3BitRate BitRate224 = new Mp3BitRate(224, MpegVersion.Mpeg1);
		public static readonly Mp3BitRate BitRate256 = new Mp3BitRate(256, MpegVersion.Mpeg1);
		public static readonly Mp3BitRate BitRate320 = new Mp3BitRate(320, MpegVersion.Mpeg1);

		private static IEnumerable<Mp3BitRate> cachedAllValues = null;

		public static IEnumerable<Mp3BitRate> AllValues
		{
			get
			{
				if (cachedAllValues == null)
				{
					lock (typeof(Mp3BitRate))
					{
						if (cachedAllValues == null)
						{
							cachedAllValues = GetAllValuesInstance();
						}
					}
				}

				return cachedAllValues;
			}
		}

		private static IEnumerable<Mp3BitRate> GetAllValuesInstance()
		{
			return new ReadOnlyCollection<Mp3BitRate>(
				new Mp3BitRate[]
				{
					BitRate8, BitRate16, BitRate24, BitRate144,
					BitRate32, BitRate40, BitRate48, BitRate56, 
					BitRate64, BitRate80, BitRate96, BitRate112,
					BitRate128, BitRate160, BitRate192, BitRate224,
					BitRate256, BitRate320
				});
		}
		
		public IEnumerable<PcmSoundFormat> CompatibleSourceFormats
		{
			get
			{
				Dictionary<PcmSoundFormat, PcmSoundFormat> result = new Dictionary<PcmSoundFormat, PcmSoundFormat>();
				foreach (MpegVersion mpegVersion in CompatibleMpegVersions)
				{
					foreach (PcmSoundFormat sourceFormat in mpegVersion.CompatibleSourceFormats)
					{
						result[sourceFormat] = sourceFormat;
					}
				}

				return result.Keys;
			}
		}

		private uint value;
		private IEnumerable<MpegVersion> mpegVersions;

		/// <summary>
		/// Value in kbit/s.
		/// </summary>
		public uint Value
		{
			get
			{
				return value;
			}
		}

		public IEnumerable<MpegVersion> CompatibleMpegVersions
		{
			get
			{
				return mpegVersions;
			}
		}


		public string Description
		{
			get
			{
				return string.Format("{0} kbit/sec", value);
			}
		}

		public static implicit operator uint(Mp3BitRate bitRate)
		{
			return bitRate.value;
		}

		public static implicit operator Mp3BitRate(uint value)
		{
			return new Mp3BitRate(value);
		}

		public static explicit operator int(Mp3BitRate bitRate)
		{
			return (int)bitRate.value;
		}

		public static explicit operator Mp3BitRate(int value)
		{
			return new Mp3BitRate((uint)value);
		}

		public static bool operator >(Mp3BitRate bitRate1, Mp3BitRate bitRate2)
		{
			return bitRate1.CompareTo(bitRate2) > 0;
		}

		public static bool operator <(Mp3BitRate bitRate1, Mp3BitRate bitRate2)
		{
			return bitRate1.CompareTo(bitRate2) < 0;
		}

		public static bool operator ==(Mp3BitRate bitRate1, Mp3BitRate bitRate2)
		{
			return bitRate1.Equals(bitRate2);
		}

		public static bool operator !=(Mp3BitRate bitRate1, Mp3BitRate bitRate2)
		{
			return !(bitRate1 == bitRate2);
		}

		#region IEquatable<Mp3BitRate> Members

		public bool Equals(Mp3BitRate other)
		{
			return value == other.value;
		}

		#endregion

		public override bool Equals(object obj)
		{
			return CompareTo(obj) == 0;
		}

		#region IComparable<Mp3BitRate> Members

		public int CompareTo(Mp3BitRate other)
		{
			return value.CompareTo(other.value);
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (obj is Mp3BitRate)
			{
				return CompareTo((Mp3BitRate)obj);
			}
			else
			{
				uint value2;
				if (uint.TryParse(obj.ToString(), out value2))
				{
					return value.CompareTo(value2);
				}
				else
				{
					return 1;
				}
			}
		}

		#endregion

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override string ToString()
		{
			return Description;
		}

		private Mp3BitRate(uint value, params MpegVersion[] mpegVersions)
		{
			this.mpegVersions = new ReadOnlyCollection<MpegVersion>(mpegVersions);
			this.value = value;
		}
	}
}
