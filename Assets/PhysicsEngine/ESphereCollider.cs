using UnityEngine;

public class ESphereCollider : ECollider
{
	public float Radius;


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Radius);
	}

	// Calculating Inertia: 2/5 * mass * radius squared
	public float GetInertia()
	{
		return 0.4f * MyRigidbody.Mass * Radius * Radius;
	}
}
