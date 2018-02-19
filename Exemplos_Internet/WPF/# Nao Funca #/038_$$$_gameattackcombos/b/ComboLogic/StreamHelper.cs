using System;
using System.IO;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// A helper class for stream-related tasks.
	/// </summary>
	public static class StreamHelper {

		/// <summary>
		/// Copies the specified Stream to a byte array.
		/// </summary>
		/// <param name="sourceData">The source byte array to copy from.</param>
		/// <param name="targetStream">The target Stream to copy to.</param>
		public static void CopyArrayToStream(byte[] sourceData, Stream targetStream) {
			const int BufferSize = 1 << 16;

			if (sourceData == null) {
				throw new ArgumentNullException("sourceData");
			}
			if (targetStream == null) {
				throw new ArgumentNullException("targetSource");
			}

			// Create a buffer for copying in chunks.
			byte[] Buffer = new byte[BufferSize];

			// Read bytes from the source data and write them to the target stream until all are read.
			int i = 0;
			int BytesToRead = Math.Min(BufferSize, sourceData.Length);
			while (BytesToRead > 0) {
				targetStream.Write(sourceData, i, BytesToRead);
				i += BytesToRead;

				// Calculate the number of bytes to read next.
				BytesToRead = Math.Min(BufferSize, sourceData.Length - i);
			}
		}

		/// <summary>
		/// Copies the data from the source file to the target stream.
		/// </summary>
		/// <param name="sourceFilePath">The path to the source file to copy from.</param>
		/// <param name="targetStream">The Stream to copy to.</param>
		public static void CopyFileToStream(string sourceFilePath, Stream targetStream) {
			if (!string.IsNullOrEmpty(sourceFilePath)) {
				if (File.Exists(sourceFilePath)) {
					// Open the source file and copy it to the target stream.
					using (FileStream Source = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
						CopyStream(Source, targetStream);
					}
				} else {
					throw new FileNotFoundException("The source file cannot be found.", sourceFilePath);
				}
			} else {
				throw new ArgumentNullException("sourceFilePath");
			}
		}

		/// <summary>
		/// Copies the data from the source stream to the target stream.
		/// </summary>
		/// <remarks>
		/// Both streams must already be opened.
		/// </remarks>
		/// <param name="source">The Stream to copy from.</param>
		/// <param name="target">The Stream to copy to.</param>
		public static void CopyStream(Stream source, Stream target) {
			const int BufferSize = 1 << 16;

			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (target == null) {
				throw new ArgumentNullException("target");
			}

			// Create a buffer for copying in chunks.
			byte[] Buffer = new byte[BufferSize];

			// Read bytes from the source stream until all are read.
			source.Position = 0;
			int BytesRead = 0;
			try {
				while ((BytesRead = source.Read(Buffer, 0, BufferSize)) > 0) {
					// Write the exact number of bytes read from source to target.
					target.Write(Buffer, 0, BytesRead);
				}
			} finally {
				// Flush the target stream when done copying.
				target.Flush();
			}
		}

		/// <summary>
		/// Copies the specified Stream to a byte array.
		/// </summary>
		/// <param name="sourceStream">The source Stream to copy from.</param>
		/// <returns>A byte array containing a copy of the source stream data.</returns>
		public static byte[] CopyStreamToArray(Stream sourceStream) {
			const int BufferSize = 1 << 16;

			if (sourceStream == null) {
				throw new ArgumentNullException("sourceStream");
			}

			// Initialize the target array.
			byte[] Result =  new byte[sourceStream.Length];

			// Read bytes from the source stream and write them to the target array until all are copied.
			sourceStream.Position = 0;
			int i = 0;
			int BytesRead = 0;
			do {
				BytesRead = sourceStream.Read(Result, i, Math.Min(BufferSize, Result.Length - i));
				i += BytesRead;
			} while (BytesRead > 0);

			return Result;
		}

	}

}