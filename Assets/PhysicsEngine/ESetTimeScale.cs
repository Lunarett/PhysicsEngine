using UnityEngine;

public class ESetTimeScale : MonoBehaviour
{
	[SerializeField] private float _scale = 1;

	private void Start()
	{
		Time.timeScale = _scale;
	}
}
