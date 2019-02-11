using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MM.RazorComponents.Blocky.App
{
	public struct Colors
	{
		public double C1;
		public double C2;
		public double C3;
		public double A;
		public Colors(double c1, double c2, double c3, double a)
		{
			C1 = c1;
			C2 = c2;
			C3 = c3;
			A = a;
		}
	}
	public struct Position
	{
		public double X;
		public double Y;

		public Position(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}
	}

	public class PixelDTO
	{
		public Colors Colors;
		public Position Position;
		public double Scale;
		public bool ShouldDie { get; private set; }
		public int Size => (int)Scale;
		public void Die() => ShouldDie = true;
	}
}
