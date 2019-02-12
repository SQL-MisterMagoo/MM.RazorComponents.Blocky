using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SkiaSharp;

namespace MM.RazorComponents.Blocky.App
{
	
	public class SkiaPictureBase : ComponentBase
	{
		private const double MAXDIM = 200;
		public List<Colors> Pixels;
		public int PixelSize = 1;
		public int PictureWidth = 0;
		public int PictureHeight = 0;
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
					for (int i = 1; i <= 5; i++)
					{
						Pixels = new List<Colors>();
						int newSize = (int)(MAXDIM * i) / 5;
						PixelSize = 5 / i;
						SKBitmap image;
						if (imageInitial.Width > newSize || imageInitial.Height > newSize)
						{
							var newWidth = imageInitial.Width;
							var newHeight = imageInitial.Height;
							double scale = 1;
							if (newWidth > newHeight)
							{
								scale = (newSize / (double)newWidth);
							}
							else
							{
								scale = (newSize / (double)newHeight);
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
						Console.WriteLine($"Image is {PictureWidth} x {PictureHeight}");

						for (int y = 0; y < image.Height; y++)
						{
							for (int x = 0; x < image.Width; x++)
							{
								SKColor sKColor = image.GetPixel(x, y);
								sKColor.ToHsl(out float h, out float s, out float l);
								Pixels.Add(new Colors(h, s, l, sKColor.Alpha));
							}
						}
						await Invoke(StateHasChanged);
						await Task.Delay(1);
					}
				}
			}
		}
	}
}
