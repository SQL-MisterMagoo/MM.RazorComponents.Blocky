namespace MM.RazorComponents.Blocky.Shared
{
	public struct Colors
	{
		public double C1;
		public double C2;
		public double C3;
		public double A;
		public ColorType ColorType;
		public Colors(double c1, double c2, double c3, double a, ColorType colorType = ColorType.RGBA)
		{
			C1 = c1;
			C2 = c2;
			C3 = c3;
			A = a;
			ColorType = colorType;
		}
	}
	public enum ColorType
	{
		RGBA,
		HSLA
	}
	public struct Vector2D
	{
		public double X;
		public double Y;

		public Vector2D(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}
	}

	public class PixelDTO
	{
		public Colors Colors;
		public Vector2D Position;
		public Vector2D Motion;
		public Vector2D? StartPosition;
		public Vector2D? ArcPosition;
		public Vector2D? ScaleRange;
		public Vector2D? OpacityRange;
		public double Scale;
		public bool ShouldDie { get; private set; }
		public int Size => (int)Scale;
		public void Die() => ShouldDie = true;
	}
}
