using UnityEngine;

public class ETimeScale : MonoBehaviour
{
	[Range(0, 1)]
	[SerializeField] private float _scale;

	private void Update()
	{
		Time.timeScale = _scale;
	}
}
