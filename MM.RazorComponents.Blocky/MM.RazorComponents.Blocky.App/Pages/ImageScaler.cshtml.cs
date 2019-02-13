using Microsoft.AspNetCore.Components;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.RazorComponents.Blocky.App
{

	public class Pixels1Base : ComponentBase
	{
		public const double MAXDIM = 80;
		public List<Colors> Pixels;
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
			await Task.Delay(0);

			var filepath = @"wwwroot\images\dotnetbot.jpg";
			var bytes = await Task.FromResult(System.IO.File.ReadAllBytes(filepath));
			Console.WriteLine($"Got some bytes: {bytes.Length} {bytes.ToString()}");
			using (var stream = new SKMemoryStream(bytes))
			{
				using (SKBitmap imageInitial = SKBitmap.Decode(stream))
				{
					Pixels = new List<Colors>();
					PixelSize = 2;
					SKBitmap image;
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
						var newInfo = imageInitial.Info.WithSize((int)(newWidth * scale), (int)(newHeight * scale));
						image = imageInitial.Resize(newInfo, SKFilterQuality.High);
					}
					else
					{
						image = imageInitial;
					}
					PictureWidth = image.Width;
					PictureHeight = image.Height;
					Console.WriteLine($"Got some dimensions: {PictureWidth} x {PictureHeight}");

					for (int y = 0; y < image.Height; y++)
					{
						for (int x = 0; x < image.Width; x++)
						{
							SKColor sKColor = image.GetPixel(x, y);
							sKColor.ToHsl(out float h, out float s, out float l);
							Pixels.Add(new Colors(h, s, l, sKColor.Alpha));
						}
						Progress++;
						await Invoke(StateHasChanged);
						await Task.Delay(1);
					}
					Console.WriteLine($"Finished pixel processing: {Progress}");
				}
			}
			IsLoading = false;
			await Invoke(StateHasChanged);
			await Task.Delay(0);
		}
	}
}

