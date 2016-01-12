using UnityEngine;
using System.Collections;

public class CoinShow : MonoBehaviour
{
	[SerializeField]
	private UISprite First;
	[SerializeField]
	private UISprite Second;
	[SerializeField]
	private UISprite Third;

	void Start()
	{
		NGUITools.SetActive(First.gameObject, false);
		NGUITools.SetActive(Second.gameObject, false);
		NGUITools.SetActive(Third.gameObject, false);
	}

	public void Show()
	{
		Debug.Log("CoinShow");
		
		NGUITools.SetActive(First.gameObject, true);
		NGUITools.SetActive(Second.gameObject, true);
		NGUITools.SetActive(Third.gameObject, true);

		int coin = PlayerPrefs.GetInt("Coin", 0);

		if (coin < 10) {
			NGUITools.SetActive(Second.gameObject, false);
			NGUITools.SetActive(Third.gameObject, false);
			First.spriteName = string.Format("coin_number_0{0}", coin);
		} else if (coin >= 10 && coin < 100) {
			NGUITools.SetActive(Third.gameObject, false);
			string str = System.Convert.ToString(coin);
			First.spriteName = string.Format("coin_number_0{0}", str.Substring(0, 1));
			Second.spriteName = string.Format("coin_number_0{0}", str.Substring(1));
		} else {
			string str = System.Convert.ToString(coin);
			First.spriteName = string.Format("coin_number_0{0}", str.Substring(0, 1));
			Second.spriteName = string.Format("coin_number_0{0}", str.Substring(1, 1));
			Third.spriteName = string.Format("coin_number_0{0}", str.Substring(2));
		}
	}
}
