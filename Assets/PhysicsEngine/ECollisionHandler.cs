using UnityEngine;

public class ECollisionHandler : MonoBehaviour
{
	[SerializeField] private ECollider[] _activeColliders;

	private void FixedUpdate()
	{
		for (int a = 0; a < _activeColliders.Length; a++)
		{
			for (int b = a + 1; b < _activeColliders.Length; b++)
			{
				if(CheckForIntersectionBetween(_activeColliders[a], _activeColliders[b]))
				{
					Debug.Log("is intersecting");

				}
			}
		}
	}

	private void ResolveCollisionFor(ESphereCollider c1, ESphereCollider c2)
	{
		if(c1 is ESphereCollider && c2 is ESphereCollider)
		{
			ResolveCollisionForSpheres((ESphereCollider)c1, (ESphereCollider)c2);
		}
	}

	private void ResolveCollisionForSpheres(ESphereCollider c1, ESphereCollider c2)
	{
		ERigidbody r1 = c1.MyRigidbody;
		ERigidbody r2 = c2.MyRigidbody;

		if(r1 != null && r2 != null)
		{
			Vector3 difference = c2.Center - c1.Center;
			Vector3 normal = difference.normalized;

			Vector3 relativeVelocity = r2.Velocity - r1.Velocity;
			Vector3 normalVelocity = normal * Vector3.Dot(normal, relativeVelocity);

			r1.Velocity += normalVelocity;
			r2.Velocity -= normalVelocity;

			c2.Center = c1.Center + normal * (c1.Radius + c2.Radius);
		}
	}

	private bool CheckForIntersectionBetween(ECollider c1, ECollider c2)
	{
		if(c1 is ESphereCollider && c2 is ESphereCollider)
		{
			CheckForIntersectionBetweenSpheres((ESphereCollider)c1, (ESphereCollider)c2);
		}

		return false;
	}

	private bool CheckForIntersectionBetweenSpheres(ESphereCollider s1, ESphereCollider s2)
	{
		float distance = (s2.Center - s1.Center).magnitude;
		float radiusSum = s1.Radius + s2.Radius;

		if(distance < radiusSum)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
}
