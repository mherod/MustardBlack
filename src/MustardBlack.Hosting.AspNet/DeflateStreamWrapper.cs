﻿using System;
using System.IO;
using Ionic.Zlib;

namespace MustardBlack.Hosting.AspNet
{
	sealed class DeflateStreamWrapper : DeflateStream
	{
		readonly Action writeAction;
		bool written;

		public DeflateStreamWrapper(Stream baseStream, Action writeAction, bool leaveOpen = false) : base(baseStream, CompressionMode.Compress, CompressionLevel.Default, leaveOpen)
		{
			this.writeAction = writeAction;
		}
		
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.Write(buffer, offset, count);

			if (!this.written)
			{
				this.written = true;
				this.writeAction();
			}
		}
	}
}