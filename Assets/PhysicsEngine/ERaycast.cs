using UnityEngine;

public class ERaycast : MonoBehaviour
{
	[SerializeField] private Material _material;
 


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1000);
	}

	private void Update()
	{
		ECollider result = ECollisionHandler.Instance.ERayCast(transform.position, transform.forward);

		if(result != null)
		{
			result.GetComponent<MeshRenderer>().material = _material;
		}
	}
}
