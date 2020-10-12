using UnityEngine;

public class ECollider : MonoBehaviour
{
	[SerializeField] public Vector3 _offset;

	public Vector3 Center { get => GetCenter(); set => SetCenter(value); }
	public ERigidbody MyRigidbody { get; private set; }


	private Vector3 GetCenter()
	{
		return transform.position + transform.rotation * _offset;
	}

	private Vector3 SetCenter(Vector3 value)
	{
		return transform.position = value - transform.rotation * _offset;
	}
}
