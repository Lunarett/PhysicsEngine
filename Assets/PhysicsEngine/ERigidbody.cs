using UnityEngine;

public class ERigidbody : MonoBehaviour
{
	[SerializeField] private bool _enableGravity = true;
	[SerializeField] private float _mass = 1.0f;
	[SerializeField] private Vector3 _velocity;
	[SerializeField] private Vector3 _angularVelocity;

	public float Mass { get => _mass; }
	public bool EnableGravity { get => _enableGravity; }
	public Vector3 Velocity { get => _velocity; set => _velocity = value; }
	public Vector3 AngularVelocity { get => _angularVelocity; set => _angularVelocity = value; }

	private void FixedUpdate()
	{
		if (_enableGravity)
		{
			Velocity += Physics.gravity * Time.fixedDeltaTime;
		}
		
		transform.position += Velocity * Time.fixedDeltaTime;
		transform.Rotate(_angularVelocity * Time.fixedDeltaTime);
	}

	public void AddForce(Vector3 direction, float force)
	{
		_velocity += direction * force / _mass;
	}
}
