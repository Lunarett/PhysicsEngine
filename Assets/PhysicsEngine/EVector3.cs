using UnityEngine;

public struct EVector3
{
	public float Z { get; set; }
	public float X { get; set; }
	public float Y { get; set; }

	public EVector3(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public EVector3 Normalized { get => Normalize(this); }

	public float Magnitude { get => Mathf.Sqrt(X * X + Y * Y + Z * Z); }

	public static EVector3 operator +(EVector3 v1, EVector3 v2)
	{
		return new EVector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
	}

	public static EVector3 operator *(EVector3 v, float s)
	{
		return new EVector3(v.X * s, v.Y * s, v.Z * s);
	}

	public static EVector3 operator *(float s, EVector3 v)
	{
		return v * s;
	}

	public static EVector3 Normalize(EVector3 v)
	{
		var m = 1 / v.Magnitude;
		return new EVector3(v.X * m, v.Y * m, v.Z * m);
	}

	public static float Dot(EVector3 v1, EVector3 v2)
	{
		return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
	}

	public static EVector3 Cross(EVector3 v1, EVector3 v2)
	{
		float x = v1.Y * v2.Z - v2.Y * v1.Z;
		float y = v1.Z * v2.X - v2.Z * v1.X;
		float z = v1.X * v2.Y - v2.X * v1.Y;

		return new EVector3(x, y, z);
	}
}
