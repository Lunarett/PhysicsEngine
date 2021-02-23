using UnityEngine;

public class ECollider : MonoBehaviour
{
	[SerializeField] private Vector3 _offset;
	[SerializeField] private bool _isTrigger;

	public static ECollider Instance;

	public Vector3 Center { get => GetCenter(); set => SetCenter(value); }
	public ERigidbody MyRigidbody { get; private set; }
	public bool IsTrigger { get => _isTrigger; }

	public event System.Action<ESphereCollider> EOnTriggerEnter;

	private void Awake()
	{
		MyRigidbody = GetComponent<ERigidbody>();
	}

	private Vector3 GetCenter()
	{
		return transform.position + transform.rotation * _offset;
	}

	private Vector3 SetCenter(Vector3 value)
	{
		return transform.position = value - transform.rotation * _offset;
	}

	public void ETriggerEnter(ESphereCollider other)
	{
		 EOnTriggerEnter.Invoke(other);
	}
}
