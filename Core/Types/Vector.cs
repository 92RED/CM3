﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix
{
	using System;

	public struct Vector
	{
		public static readonly Vector Zero = new Vector(0, 0, 0);
		public static readonly Vector One = new Vector(1, 1, 1);

		public Vector(float x = 0, float y = 0, float z = 0)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public static Vector operator *(Vector left, Vector right)
		{
			return new Vector(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		public static Vector operator *(Vector left, float right)
		{
			return new Vector(left.X * right, left.Y * right, left.Z * right);
		}

		public static Vector FromString(string str)
		{
			string[] parts = str.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

			if (parts.Length != 3)
				throw new FormatException();

			Vector v = default;
			v.X = float.Parse(parts[0]);
			v.Y = float.Parse(parts[1]);
			v.Z = float.Parse(parts[2]);
			return v;
		}

		public void NormalizeAngles()
		{
			this.X = NormalizeAngle(this.X);
			this.Y = NormalizeAngle(this.Y);
			this.Z = NormalizeAngle(this.Z);
		}

		public override string ToString()
		{
			return this.X + ", " + this.Y + ", " + this.Z;
		}

		private static float NormalizeAngle(float angle)
		{
			while (angle > 360)
				angle -= 360;

			while (angle < 0)
				angle += 360;

			return angle;
		}
	}
}
