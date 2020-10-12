using UnityEngine;

public class ERigidbody : MonoBehaviour
{
	[SerializeField] private bool _enableGravity = true;
	[SerializeField] private float _mass = 1;
	
	public Vector3 Velocity;

	private void FixedUpdate()
	{
		if(_enableGravity)
		{
			Velocity += Physics.gravity * Time.fixedDeltaTime;
		}

		transform.position += Velocity * Time.fixedDeltaTime;
	}
}
