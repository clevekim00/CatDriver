using UnityEngine;
using System.Collections;

public class ScoreShow : MonoBehaviour
{
	[SerializeField]
	private UISprite First;
	[SerializeField]
	private UISprite Second;

	void Start()
	{
		NGUITools.SetActive(First.gameObject, false);
		NGUITools.SetActive(Second.gameObject, false);
	}

	public void Show()
	{
		Debug.Log("ScoreShow");

		NGUITools.SetActive(First.gameObject, true);
		NGUITools.SetActive(Second.gameObject, true);

		int coin = PlayerPrefs.GetInt("Score", 0);

		if (coin < 10) {
			NGUITools.SetActive(Second.gameObject, false);
			First.spriteName = string.Format("number_0{0}", coin);
		} else {
			string str = System.Convert.ToString(coin);
			First.spriteName = string.Format("number_0{0}", str.Substring(0, 1));
			Second.spriteName = string.Format("number_0{0}", str.Substring(1));
		}
	}
}
