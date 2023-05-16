using System;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	internal struct IntPoint : IEquatable<IntPoint>
	{
		public static IntPoint MaxValue = new IntPoint
		{
			X = 2147483647,
			Y = 2147483647
		};

		public int X;

		public int Y;

		public IntPoint(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public bool IsEqual(IntPoint other)
		{
			return other.X == this.X && other.Y == this.Y;
		}

		public override string ToString()
		{
			return string.Format(string.Concat(new object[]
			{
				"X: ",
				this.X,
				" - Y: ",
				this.Y
			}), new object[0]);
		}

		public bool Equals(IntPoint other)
		{
			return other.X == this.X && other.Y == this.Y;
		}

		public override int GetHashCode()
		{
			int num = 0;
			num = (num * 397 ^ this.X);
			return num * 397 ^ this.Y;
		}
	}
}
