using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{

	TweenPosition tween;
	void Start()
	{
		tween = GetComponent<TweenPosition>();
		tween.enabled = false;
	}

	public void CoinMove(CarDirection dir)
	{
		float x = 0.0f;
		if (dir == CarDirection.Left) {
			x = -300.0f;
		} else if (dir == CarDirection.Left) {
			x = 300.0f;
		} else {
			x = 0.0f;
		}

		tween.enabled = true;
		tween.AddOnFinished(delegate {
			GameObject.Destroy(gameObject);
		});
		tween.from = new Vector3(x, -100.0f, 0.0f);
		tween.to = new Vector3(x,  100.0f, 0.0f);
		tween.method = UITweener.Method.EaseIn;
		tween.PlayForward();
	}
}
