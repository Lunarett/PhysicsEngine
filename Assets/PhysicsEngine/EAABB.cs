using UnityEngine;

public class EAABB : MonoBehaviour
{
	[SerializeField] private float _scaleX;
	[SerializeField] private float _scaleY;
	[SerializeField] private float _scaleZ;
	[SerializeField] private Vector3 _offset;

	public float ScaleX { get => _scaleX; set => _scaleX = value; }
	public float ScaleY { get => _scaleY; set => _scaleY = value; }
	public float ScaleZ { get => _scaleZ; set => _scaleZ = value; }

	private Vector3 GetCenter()
	{
		return transform.position + transform.rotation * _offset;
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(GetCenter(), new Vector3(_scaleX, _scaleY, _scaleZ));
	}
}
