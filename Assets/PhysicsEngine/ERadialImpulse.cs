using UnityEngine;

public class ERadialImpulse : MonoBehaviour
{
	[SerializeField] private float _force;
	private ECollider _triggerObjRef;

	private void Awake()
	{
		_triggerObjRef = GetComponent<ECollider>();

		_triggerObjRef.EOnTriggerEnter += OnTrigger;
	}

	private void OnTrigger(ESphereCollider other)
	{
		if (other != null)
		{
			Vector3 direction = EVector3.Normalize((other.Center - transform.position));

			other.MyRigidbody.AddForce(direction, _force);
		}
		else
			Debug.LogError("Trigger \"other\": NULL");
	}
}
