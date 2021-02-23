using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
	private void Start()
	{
		Pause();
	}

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void Resume()
	{
		Time.timeScale = 1;
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
