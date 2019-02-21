using Microsoft.AspNetCore.Components;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.RazorComponents.Blocky.Shared
{

	public class Pixels1Base : ComponentBase
	{
		public const double MAXDIM = 120;
		public List<String> Pixels;
		public int PixelSize = 1;
		public int PictureWidth = 0;
		public int PictureHeight = 0;
		public bool IsLoading = true;
		public int Progress;
		Task Loader;

		protected override void OnInit()
		{
			Loader = LoadImage();
		}
		async Task LoadImage()
		{
			await Invoke(StateHasChanged);
			await Task.Delay(1);

			var filepath = @"wwwroot\images\dotnetbot.jpg";
			var bytes = await Task.FromResult(System.IO.File.ReadAllBytes(filepath));
			Console.WriteLine($"Got some bytes: {bytes.Length} {bytes.ToString()}");

			Image<Rgba32> imageInitial = Image.Load(bytes);

			Pixels = new List<string>();
			PixelSize = 2;
			Image<Rgba32> image;
			if (imageInitial.Width > MAXDIM || imageInitial.Height > MAXDIM)
			{
				var newWidth = imageInitial.Width;
				var newHeight = imageInitial.Height;
				double scale = 1;
				if (newWidth > newHeight)
				{
					scale = (MAXDIM / (double)newWidth);
				}
				else
				{
					scale = (MAXDIM / (double)newHeight);
				}
				image = imageInitial.Clone(c => c.Resize((int)(newWidth * scale), (int)(newHeight * scale)));
			}
			else
			{
				image = imageInitial;
			}
			PictureWidth = image.Width;
			PictureHeight = image.Height;

			await Invoke(StateHasChanged);
			await Task.Delay(1);
			ReadPixels(image);

			IsLoading = false;
			await Invoke(StateHasChanged);
			await Task.Delay(1);
		}

		void ReadPixels(Image<Rgba32> image)
		{
			for (int y = 0; y < image.Height; y++)
			{
				Span<Rgba32> sKColor = image.GetPixelRowSpan(y);
				for (int x = 0; x < image.Width; x++)
				{
					Pixels.Add($"#{sKColor[x].ToHex()}");
				}
				Progress++;
				Invoke(StateHasChanged);
				Invoke(async ()=> await Task.Delay(1));
			}
		}
	}
}

