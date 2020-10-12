using UnityEngine;

public class ESphereCollider : ECollider
{
	public float Radius;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Radius);
	}
}
