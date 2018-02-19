using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Istrib.Sound.Formats
{
	/// <summary>
	/// MP3 MPEG versions.
	/// </summary>
	public struct MpegVersion
		: IEquatable<MpegVersion>
	{
		public static readonly MpegVersion Mpeg1 = new MpegVersion("MPEG-1",
			new PcmSoundFormat(32000, 16, 1),
			new PcmSoundFormat(32000, 16, 2),
			new PcmSoundFormat(44100, 16, 1),
			new PcmSoundFormat(44100, 16, 2),
			new PcmSoundFormat(48000, 16, 1),
			new PcmSoundFormat(48000, 16, 2));
		
		public static readonly MpegVersion Mpeg2 = new MpegVersion("MPEG-2",
			new PcmSoundFormat(16000, 16, 1),
			new PcmSoundFormat(16000, 16, 2),
			new PcmSoundFormat(22050, 16, 1),
			new PcmSoundFormat(22050, 16, 2),
			new PcmSoundFormat(24000, 16, 1),
			new PcmSoundFormat(24000, 16, 2));

		public static readonly MpegVersion None = new MpegVersion("(None)");

		#region All Instances

		private static IEnumerable<MpegVersion> cachedAll = null;

		public static IEnumerable<MpegVersion> All
		{
			get
			{
				if (cachedAll == null)
				{
					lock (typeof(MpegVersion))
					{
						if (cachedAll == null)
						{
							cachedAll = GetAllInstance();
						}
					}
				}

				return cachedAll;
			}
		}

		private static IEnumerable<MpegVersion> GetAllInstance()
		{
			return new ReadOnlyCollection<MpegVersion>(
				new MpegVersion[]
				{
					Mpeg2, Mpeg1
				});
		}

		#endregion

		private string name;
		private IEnumerable<PcmSoundFormat> compatibleSourceFormats;

		public string Name
		{
			get
			{
				return name;
			}
		}

		public IEnumerable<Mp3BitRate> CompatibleBitRates
		{
			get
			{
				foreach (Mp3BitRate bitRate in Mp3BitRate.AllValues)
				{
					foreach (MpegVersion version in bitRate.CompatibleMpegVersions)
					{
						if (version == this)
						{
							yield return bitRate;
						}
					}
				}
			}
		}

		public IEnumerable<PcmSoundFormat> CompatibleSourceFormats
		{
			get
			{
				return compatibleSourceFormats;
			}
		}

		public static bool operator ==(MpegVersion version1, MpegVersion version2)
		{
			return version1.Equals(version2);
		}

		public static bool operator !=(MpegVersion version1, MpegVersion version2)
		{
			return !(version1 == version2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is MpegVersion))
			{
				return false;
			}

			return base.Equals((MpegVersion)obj);
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public bool Equals(MpegVersion other)
		{
			return other.name == name;
		}

		public MpegVersion(string name, params PcmSoundFormat[] compatibleSourceFormats)
		{
			this.name = name;
			this.compatibleSourceFormats = new ReadOnlyCollection<PcmSoundFormat>(compatibleSourceFormats);
		}
	}
}
