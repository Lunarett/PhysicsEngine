using UnityEngine;

public struct EVector3
{
	public float Z { get; set; }
	public float X { get; set; }
	public float Y { get; set; }

	public EVector3(Vector3 convrt)
	{
		X = convrt.x;
		Y = convrt.y;
		Z = convrt.z;
	}

	public EVector3(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public Vector3 Normalized { get => Normalize(this); }

	public static Vector3 operator +(EVector3 v1, EVector3 v2)
	{
		return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
	}

	public static EVector3 operator *(EVector3 v, float s)
	{
		return new EVector3(v.X * s, v.Y * s, v.Z * s);
	}

	public static EVector3 operator *(float s, EVector3 v)
	{
		return v * s;
	}

	public static float Magnitude(EVector3 v)
	{
		return Mathf.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
	}

	public static float Magnitude(Vector3 v)
	{
		return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
	}

	public static Vector3 Normalize(EVector3 v)
	{
		float m = 1.0f / Magnitude(v);

		return new Vector3(v.X * m, v.Y * m, v.Z * m);
	}

	public static Vector3 Normalize(Vector3 v)
	{
		EVector3 myVector = new EVector3(v.x, v.y, v.z);
		float m = 1 / Magnitude(myVector);
		Vector3 newVector = new Vector3(myVector.X, myVector.Y, myVector.Z);

		return new Vector3(newVector.x * m, newVector.y * m, newVector.z * m);
	}

	public static float Dot(Vector3 v1, Vector3 v2)
	{
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
	}

	public static Vector3 Cross(Vector3 v1, Vector3 v2)
	{
		float x = v1.y * v2.z - v2.y * v1.z;
		float y = v1.z * v2.x - v2.z * v1.x;
		float z = v1.x * v2.y - v2.x * v1.y;

		return new Vector3(x, y, z);
	}
}
