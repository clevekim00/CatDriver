using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
	[SerializeField]
	private GameObject Car1;
	[SerializeField]
	private GameObject Car2;
	[SerializeField]
	private GameObject Tit;

	void Start ()
	{
//		NGUITools.SetActive(Car2, true);
		NGUITools.SetActive(Tit, false);

		Invoke("TitleOn", 1.0f);
//		Invoke("MenuOn", 3.0f);
	}

	void TitleOn()
	{
		NGUITools.SetActive(Tit, true);
		Tit.GetComponent<Animator>().Play("Title");
	}

	public void OnTouchStart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
	}
}
