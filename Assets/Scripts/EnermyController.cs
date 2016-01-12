using UnityEngine;
using System.Collections;

public enum EnermyState
{
	Ready,
	Show,
}

public class EnermyController : MonoBehaviour
{
	private EnermyState _State = EnermyState.Ready;
	public EnermyState State {
		get {
			return _State;
		}
		set {
			_State = value;
		}
	}

	Animator ani;
	void Start ()
	{
		ani = GetComponent<Animator>();
	}

	void Update ()
	{
	
	}

	public void Show()
	{
		int i = UnityEngine.Random.Range(0, 3);
		CarDirection dir = CarDirection.Center;
		if (i == 0) {
			dir = CarDirection.Center;
		} else if (i == 1) {
			dir = CarDirection.Left;
		} else if (i == 2) {
			dir = CarDirection.Right;
		}

		Move(dir);

		State = EnermyState.Show;
		gameObject.SetActive(true);
	}

	public void Die()
	{
		int score = PlayerPrefs.GetInt("Score");
		score += 10;
		PlayerPrefs.SetInt("Score", score);
//		GameObject.Destroy(gameObject);
		State = EnermyState.Ready;
		gameObject.SetActive(false);
	}

	public CarDirection Direction {get; set;}
	public void Move(CarDirection dir)
	{
		Direction = dir;
		Vector3 pos = Vector3.zero;
		if (dir == CarDirection.Left) {
			pos = new Vector3(-230, 80, 0);
		} else if (dir == CarDirection.Center) {
			pos = new Vector3(0, 100, 0);
		} else {
			pos = new Vector3(230, 80, 0);
		}

		iTween.MoveTo(gameObject, iTween.Hash("position", pos, "islocal", true, "time", 0.8f));
	}

	public void Attack()
	{
		if (State == EnermyState.Show)
			ani.SetTrigger("attack");
	}

	public void PreferDie()
	{
		Debug.LogError("PreferDie");
		
		if (State == EnermyState.Show)
			ani.SetTrigger("die");
	}
}
