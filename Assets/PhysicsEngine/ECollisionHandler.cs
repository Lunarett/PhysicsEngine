using UnityEngine;

public class ECollisionHandler : MonoBehaviour
{
	private ECollider[] _activeColliders;
	private static ECollisionHandler _instance;
	public static ECollisionHandler Instance { get => _instance; }

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
			Debug.LogError("Second instance of collisionHandler found!");
	}

	private void Start()
	{
		_activeColliders = FindObjectsOfType<ECollider>();
	}

	private void FixedUpdate()
	{
		for (int a = 0; a < _activeColliders.Length; a++)
		{
			ECollider c1 = _activeColliders[a];
			for (int b = a + 1; b < _activeColliders.Length; b++)
			{
				ECollider c2 = _activeColliders[b];

				if (CheckForIntersectionBetween(c1, c2))
				{
					if(c1.IsTrigger || c2.IsTrigger)
					{
						ResolveTrigger((ESphereCollider)c1, (ESphereCollider)c2);
					}
					else
					{
						ResolveCollisionFor(c1, c2);
					}
				}
			}
		}
	}

	private void ResolveTrigger(ESphereCollider c1, ESphereCollider c2)
	{
		if(!c1.IsTrigger)
		{
			c2.ETriggerEnter(c1);
		}
		else
		{ 
			c1.ETriggerEnter(c2);
		}
	}

	private void ResolveCollisionFor(ECollider c1, ECollider c2)
	{
		if (c1 is ESphereCollider && c2 is ESphereCollider)
		{
			ResolveCollisionForSpheres((ESphereCollider)c1, (ESphereCollider)c2);
		}
		else
		{
			throw new System.NotImplementedException("Collision resolution between collider types unknown");
		}
	}

	private void ResolveCollisionForSpheres(ESphereCollider c1, ESphereCollider c2)
	{
		ERigidbody r1 = c1.MyRigidbody;
		ERigidbody r2 = c2.MyRigidbody;

		if (r1 != null && r2 != null)
		{
			// http://hitokageproduction.com/article/11#physics

			Vector3 va = c1.MyRigidbody.Velocity;
			Vector3 vb = c2.MyRigidbody.Velocity;

			Vector3 difference = c2.Center - c1.Center;
			Vector3 normal = EVector3.Normalize(difference);

			Vector3 collisionPoint = c1.Center + (c2.Center - c1.Center) * c1.Radius / (c1.Radius + c2.Radius);

			Vector3 ra = collisionPoint - c1.Center;
			Vector3 rb = collisionPoint - c2.Center;

			Vector3 totalVelocity = va + (EVector3.Cross(c1.MyRigidbody.AngularVelocity, ra) - vb - EVector3.Cross(c2.MyRigidbody.AngularVelocity, rb));
			float vab = EVector3.Dot(totalVelocity, normal);

			float elasticity = 1;
			float impulse = -(1 + elasticity) * vab / 1 / c1.MyRigidbody.Mass + 1 / c2.MyRigidbody.Mass + EVector3.Dot(1 / c1.GetInertia() * EVector3.Cross(EVector3.Cross(ra, normal), ra) + 1 / c2.GetInertia() * EVector3.Cross(EVector3.Cross(rb, normal), rb), normal);


			c1.MyRigidbody.Velocity = va + (impulse * normal) / c1.MyRigidbody.Mass;
			c2.MyRigidbody.Velocity = vb - (impulse * normal) / c2.MyRigidbody.Mass;


			c1.MyRigidbody.AngularVelocity = c1.MyRigidbody.AngularVelocity + EVector3.Cross(ra, impulse * normal) * 1 / c1.GetInertia();
			c2.MyRigidbody.AngularVelocity = c2.MyRigidbody.AngularVelocity - EVector3.Cross(rb, impulse * normal) * 1 / c2.GetInertia();
		}
		else
		{
			throw new System.NotImplementedException("Resolution without 2 Rigidbodies is not implemented");
		}
	}

	private bool CheckForIntersectionBetween(ECollider c1, ECollider c2)
	{
		if (c1 is ESphereCollider && c2 is ESphereCollider)
		{
			return CheckForIntersectionBetweenSpheres((ESphereCollider)c1, (ESphereCollider)c2);
		}

		throw new System.NotImplementedException("Intersection between collider types unknown");
	}

	private bool CheckForIntersectionBetweenSpheres(ESphereCollider s1, ESphereCollider s2)
	{
		float distance = EVector3.Magnitude((s2.Center - s1.Center));
		float radiusSum = s1.Radius + s2.Radius;

		if (distance < radiusSum)
		{
			return true;
		}
		else
		{
			return false;
		}

	}

	public ECollider ERayCast(Vector3 origin, Vector3 direction)
	{
		ECollider closestResult = null;
		float minValue = float.MaxValue;

		for (int i = 0; i < _activeColliders.Length; i++)
		{
			ECollider c1 = _activeColliders[i];

			if (c1 is ESphereCollider)
			{
				float distance = HitSphere(c1.Center, origin, direction, (ESphereCollider)c1);

				if (distance < minValue && distance > 0)
				{
					minValue = distance;
					closestResult = c1;
				}
			}
		}
		return closestResult;
	}

	private float HitSphere(Vector3 C, Vector3 A, Vector3 B, ESphereCollider sphere)
	{

		// http://viclw17.github.io/2018/07/16/raytracing-ray-sphere-intersection/#:~:text=When%20the%20ray%20and%20sphere,equations%20and%20solving%20for%20t
		// C is center point of the Sphere
		// A Origin point of Ray
		// B is the direction

		var CA = A - C;
		float a = EVector3.Dot(B, B);
		float b = 2 * EVector3.Dot(B, CA);
		float c = EVector3.Dot(CA,CA) - sphere.Radius * sphere.Radius;

		float discriminant = b * b - 4 * a * c;

		if (discriminant < 0)
		{
			return -1.0f;
		}
		else
		{
			float numerator = -b - Mathf.Sqrt(discriminant);

			if (numerator > 0)
			{
				return numerator / (2 * a);
			}

			numerator = -b + Mathf.Sqrt(discriminant);

			if (numerator > 0)
			{
				return numerator / (2 * a);
			}
			else
			{
				return -1;
			}
		}
	}
}
