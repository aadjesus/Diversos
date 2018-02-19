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
using System.IO;
using Istrib.Sound.Formats;

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// Adds a RIFF header (like in WAV files stored on disk) to the Sound Stream.
    /// Header is added when the streaming starts, but it has to be modified each time
    /// the streaming stops (file length is written in the header). That's why the output
    /// stream attached to this filter MUST SUPPORT SEEKING (it's usually a file stream).
    /// </summary>
    public class RiffFilter
        : SoundFilter
    {
        private bool headerWritten = false;
        private InputSplittingStream input = new InputSplittingStream();
        private BinaryWriter writer;

        public override System.IO.Stream Input
        {
            get 
            {
                return input;
            }
        }

        /// <summary>
        /// Raw PCM.
        /// </summary>
        public override Type RequiredInputFormatType
        {
            get 
            {
                return typeof(PcmSoundFormat);
            }
        }

        /// <summary>
        /// Parameters of raw PCM written into Input stream.
        /// </summary>
        public new PcmSoundFormat InputFormat
        {
            get
            {
                return (PcmSoundFormat)base.InputFormat;
            }
        }

        /// <summary>
        /// RIFF of the same parameters as input PCM.
        /// </summary>
        public override ISoundFormat OutputFormat
        {
            get 
            {
                return new RiffSoundFormat(InputFormat); 
            }
        }

        private void ProcessInputDataChunk(object sender, InputSplittingStream.ChunkWrittenEventArgs e)
        {
            WriteHeaderIfNeeded();
            Output.Write(e.Chunk, 0, e.Chunk.Length);
        }

        private void WriteHeaderIfNeeded()
        {
            if (!headerWritten)
            {
                this.writer = new BinaryWriter(Output);

                if (Output == null || !Output.CanSeek)
                {
                    throw new ArgumentException("The output stream for RiffFilter must support seeking. '" + Output + "' does not.");
                }

                /**************************************************************************
        			 
			        Here is where the file will be created. A
			        wave file is a RIFF file, which has chunks
			        of data that describe what the file contains.
			        A wave RIFF file is put together like this:
        			 
			        The 12 byte RIFF chunk is constructed like this:
			        Bytes 0 - 3 :	'R' 'I' 'F' 'F'
			        Bytes 4 - 7 :	Length of file, minus the first 8 bytes of the RIFF description.
							        (4 bytes for "WAVE" + 24 bytes for format chunk length +
							        8 bytes for data chunk description + actual sample data size.)
			        Bytes 8 - 11:	'W' 'A' 'V' 'E'
        			
			        The 24 byte FORMAT chunk is constructed like this:
			        Bytes 0 - 3 :	'f' 'm' 't' ' '
			        Bytes 4 - 7 :	The format chunk length. This is always 16.
			        Bytes 8 - 9 :	File padding. Always 1.
			        Bytes 10- 11:	Number of channels. Either 1 for mono,  or 2 for stereo.
			        Bytes 12- 15:	Sample rate.
			        Bytes 16- 19:	Number of bytes per second.
			        Bytes 20- 21:	Bytes per sample. 1 for 8 bit mono, 2 for 8 bit stereo or
							        16 bit mono, 4 for 16 bit stereo.
			        Bytes 22- 23:	Number of bits per sample.
        			
			        The DATA chunk is constructed like this:
			        Bytes 0 - 3 :	'd' 'a' 't' 'a'
			        Bytes 4 - 7 :	Length of data, in bytes.
			        Bytes 8 -...:	Actual sample data.
        			
		        ***************************************************************************/

                // Set up file with RIFF chunk info.
                char[] ChunkRiff = { 'R', 'I', 'F', 'F' };
                char[] ChunkType = { 'W', 'A', 'V', 'E' };
                char[] ChunkFmt = { 'f', 'm', 't', ' ' };
                char[] ChunkData = { 'd', 'a', 't', 'a' };

                short shPad = 1; // File padding
                int nFormatChunkLength = 0x10; // Format chunk length.
                int nLength = 0; // File length, minus first 8 bytes of RIFF description. This will be filled in later.
                short shBytesPerSample = 0; // Bytes per sample.

                // Figure out how many bytes there will be per sample.
                if (8 == InputFormat.BitsPerSample && 1 == InputFormat.Channels)
                    shBytesPerSample = 1;
                else if ((8 == InputFormat.BitsPerSample && 2 == InputFormat.Channels) || (16 == InputFormat.BitsPerSample && 1 == InputFormat.Channels))
                    shBytesPerSample = 2;
                else if (16 == InputFormat.BitsPerSample && 2 == InputFormat.Channels)
                    shBytesPerSample = 4;

                // Fill in the riff info for the wave file.
                writer.Write(ChunkRiff);
                writer.Write(nLength);
                writer.Write(ChunkType);

                // Fill in the format info for the wave file.
                writer.Write(ChunkFmt);
                writer.Write(nFormatChunkLength);
                writer.Write(shPad);
                writer.Write(InputFormat.Channels);
                writer.Write(InputFormat.SampleRate);
                writer.Write(InputFormat.AverageBytesPerSecond);
                writer.Write(shBytesPerSample);
                writer.Write(InputFormat.BitsPerSample);

                // Now fill in the data chunk.
                writer.Write(ChunkData);
                writer.Write((int)0);	// The sample length will be written in later.

                writer.Flush();

                headerWritten = true;
            }
        }

        protected internal override void OnStreamingStoppedSuspended(SoundStreamingStatus streamingStatus)
        {
            base.OnStreamingStoppedSuspended(streamingStatus);

            if (Output != null && writer != null)
            {
                Output.Flush();
                //Set the sample lenght. It may be later overriden if the streaming is again resumed.

                long curPos = Output.Position;

                writer.Seek(4, SeekOrigin.Begin); // Seek to the length descriptor of the RIFF file.
                writer.Write((int)(streamingStatus.BytesPassed + 36));	// Write the file length, minus first 8 bytes of RIFF description.
                writer.Seek(40, SeekOrigin.Begin); // Seek to the data length descriptor of the RIFF file.
                writer.Write(streamingStatus.BytesPassed); // Write the length of the sample data in bytes.

                //Return to the oryginal position (we're assuming that more data can be appended).
                //But if not, if the stream is closed then the above assures correct header values.
                Output.Position = curPos;

                Output.Flush();
            }
        }

        public RiffFilter()
        {
            input.ChunkWritten += new EventHandler<InputSplittingStream.ChunkWrittenEventArgs>(ProcessInputDataChunk);
        }
    }
}
