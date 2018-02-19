using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Istrib.Sound.Formats
{
	public static class PcmSoundFormatExtensions
	{
		public static MpegVersion GetCompatibleMpegVersion(this PcmSoundFormat sourceFormat)
		{
			foreach (MpegVersion version in MpegVersion.All)
			{
				foreach (PcmSoundFormat soundFormat in version.CompatibleSourceFormats)
				{
					if (soundFormat == sourceFormat)
					{
						return version;
					}
				}
			}

			return MpegVersion.None;
		}

		public static IEnumerable<Mp3BitRate> GetCompatibleMp3BitRates(this PcmSoundFormat sourceFormat)
		{
			return sourceFormat.GetCompatibleMpegVersion().CompatibleBitRates;
		}
	}
}
