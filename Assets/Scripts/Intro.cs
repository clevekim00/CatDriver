using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour 
{
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	public void OnClickStartBtn()
	{
		Debug.Log("OnClickStartBtn");
		SceneManager.LoadScene("Game");
	}
}
