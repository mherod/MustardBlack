using MustardBlack.Pipeline;

namespace MustardBlack.Results
{
	sealed class FileStreamResultExecutor : ResultExecutor<FileStreamResult>
	{
		public override void Execute(PipelineContext context, FileStreamResult result)
		{
			context.Response.SetCacheHeaders(result);
			context.Response.StatusCode = result.StatusCode;
			context.Response.ContentType = result.ContentType;

			SetLinkHeaders(context, result);
			SetTempData(context, result.TempData);

			if (!string.IsNullOrEmpty(result.ContentDisposition))
				context.Response.Headers.Add("Content-Disposition", result.ContentDisposition);

			using (result.FileStream)
			{
				var buffer = new byte[0x1000];
				while (true)
				{
					int bytesRead = result.FileStream.Read(buffer, 0, 0x1000);
					if (bytesRead == 0)
						return;

					context.Response.OutputStream.Write(buffer, 0, bytesRead);
				}
			}
		}
	}
}