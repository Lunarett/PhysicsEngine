using UnityEngine;

public class ERigidbody : MonoBehaviour
{
	[SerializeField] private bool _enableGravity = true;
	[SerializeField] private float _mass = 1;
	[SerializeField] private Vector3 _velocity;
	
	public float Mass { get => _mass; }
	public Vector3 Velocity { get => _velocity; set => _velocity = value; }

	private void FixedUpdate()
	{
		if(_enableGravity)
		{
			Velocity += Physics.gravity * Time.fixedDeltaTime;
		}

		transform.position += Velocity * Time.fixedDeltaTime;
	}
}
