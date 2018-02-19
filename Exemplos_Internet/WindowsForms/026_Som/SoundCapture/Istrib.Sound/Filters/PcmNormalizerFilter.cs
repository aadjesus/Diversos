//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  lukasz@istrib.org
//
//  Copyright (C) 2008-2009 Lukasz Kwiecinski. 
//

using System;
using System.Collections.Generic;
using System.Text;
using Istrib.Sound.Formats;
using System.IO;

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// This filter normalizes raw PCM data gathered within Start()-Stop() streaming
    /// session.
    /// </summary>
    public class PcmNormalizerFilter
        : BufferFilter
    {
        /// <summary>
        /// Raw PCM
        /// </summary>
        public override Type RequiredInputFormatType
        {
            get { return typeof(PcmSoundFormat); }
        }

        /// <summary>
        /// The same as actual input format.
        /// </summary>
        public override Istrib.Sound.Formats.ISoundFormat OutputFormat
        {
            get { return InputFormat; }
        }

        protected override void RewriteData(BinaryReader input, BinaryWriter output)
        {
            PcmSoundFormat format = (PcmSoundFormat)InputFormat;
            if (format.BitsPerSample == 8)
            {
                Normalize8(input, output);
            }
            else if (format.BitsPerSample == 16)
            {
                Normalize16(input, output);
            }
            else
            {
                throw new ArgumentException("Unsupported bits per sample: " + format.BitsPerSample + ".");
            }
        }

        private void Normalize8(BinaryReader input, BinaryWriter output)
        {
            long size = input.BaseStream.Length;

            byte maxValue = 0;
            long index = 0;
            while (index++ < size)
            {
                byte val;
                if ((val = input.ReadByte()) > maxValue)
                {
                    maxValue = val;
                }
            }

            input.BaseStream.Seek(0, SeekOrigin.Begin);

            if (maxValue == 0)
            {
                for (long i = 0; i < size; i++)
                {
                    output.Write((byte)0);
                }
            }
            else
            {
                index = 0;
                while (index++ < size)
                {
                    byte val = input.ReadByte();
                    output.Write((byte)((int)byte.MaxValue * val / maxValue));
                }
            }
        }

        private void Normalize16(BinaryReader input, BinaryWriter output)
        {
            long size = input.BaseStream.Length / 2;

            int maxValue = 0;
            long index = 0;
            while (index++ < size)
            {
                int val;
                if ((val = input.ReadInt16()) > maxValue || -val > maxValue)
                {
                    //val = ((val & 0xFF00) >> 8) | ((val & 0x00FF) << 8);
                    maxValue = val < 0 
                        ? -val 
                        : val;
                }
            }

            input.BaseStream.Seek(0, SeekOrigin.Begin);

            if (maxValue == 0)
            {
                for (long i = 0; i < size; i++)
                {
                    output.Write((Int16)0);
                }
            }
            else
            {
                index = 0;
                while (index++ < size)
                {
                    int val = input.ReadInt16();
                    //val = ((val & 0xFF00) >> 8) | ((val & 0x00FF) << 8);
                    int normalized = Int16.MaxValue * val / maxValue;
                    //normalized = ((normalized & 0xFF00) >> 8) | ((normalized & 0x00FF) << 8);
                    output.Write((Int16)normalized);
                }
            }
        }
    }
}
