﻿using System.IO;
using FluentAssertions;
using MustardBlack.Hosting.AspNet;
using NUnit.Framework;

namespace MustardBlack.Tests.Compression.gzip
{
	public class EmptyStream : Specification
	{
		Stream gzipStream;
		MemoryStream memoryStream;

		protected override void Given()
		{
			this.memoryStream = new MemoryStream();
			this.gzipStream = new GzipStreamWrapper(this.memoryStream, () =>
			{
				Assert.Fail("Should not execute");
			}, true);
		}

		protected override void When()
		{
			this.gzipStream.Flush();
		}

		[Then]
		public void MemoryStreamShouldBeEmpty()
		{
			this.memoryStream.Length.Should().Be(0);
		}
	}
}