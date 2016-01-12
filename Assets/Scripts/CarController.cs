using UnityEngine;
using System.Collections;

public enum CarDirection
{
	Left,
	Center,
	Right
}

public enum CarState
{
	Normal,
	FirstAttack,
	Die,
	Died,
}

public class CarController : MonoBehaviour
{
	private Animator ani;

	private CarState _State = CarState.Normal;
	public CarState State {
		get {
			return _State;
		}
		set {
			_State = value;
		}
	}

	void Start()
	{
		ani = GetComponent<Animator>();
	}

	private CarDirection _Direction = CarDirection.Center;
	public CarDirection Direction {
		get {
			return _Direction;
		}
		set {
			_Direction = value;
		}
	}

	public void Left()
	{
		CarDirection dir = CarDirection.Left;
		if (Direction == CarDirection.Right) {
			ani.SetTrigger("left");
			dir = CarDirection.Center;
		} else if (Direction == CarDirection.Center) {
			ani.SetTrigger("left");
			dir = CarDirection.Left;
		}

		_Move(dir);
	}

	public void Center()
	{
		CarDirection dir = CarDirection.Center;
		if (Direction == CarDirection.Left) {
			ani.SetTrigger("right");
			dir = CarDirection.Center;
		} else if (Direction == CarDirection.Right) {
			ani.SetTrigger("left");
			dir = CarDirection.Center;
		}

		_Move(dir);
	}

	public void Right()
	{
		CarDirection dir = CarDirection.Right;
		if (Direction == CarDirection.Left) {
			ani.SetTrigger("right");
			dir = CarDirection.Center;
		} else if (Direction == CarDirection.Center) {
			ani.SetTrigger("right");
			dir = CarDirection.Right;
		}

		_Move(dir);
	}

	public void Shoot()
	{
		
	}

	public void Attack()
	{
		if (Direction == CarDirection.Left)
			ani.SetTrigger("att_left");
		else if (Direction == CarDirection.Center)
			ani.SetTrigger("att_center");
		else if (Direction == CarDirection.Right)
			ani.SetTrigger("att_right");
	}

	public void Demage()
	{
		if (State == CarState.Normal) {
			State = CarState.FirstAttack;
		} else {
			State = CarState.Die;
		}

		Debug.LogError("Demage:" + State);
		//animation change

		PreferDie();
	}

	void PreferDie()
	{
		State = CarState.Died;
	}

	public void Die()
	{
		
	}

	void _Move(CarDirection dir)
	{
		Vector3 pos = Vector3.zero;
		if (dir == CarDirection.Center) {
			pos = new Vector3(0, -100, 0);
		} else if (dir == CarDirection.Left) {
			pos = new Vector3(-300, -100, 0);
		} else if (dir == CarDirection.Right) {
			pos = new Vector3(300, -100, 0);
		}

		Direction = dir;
		iTween.MoveTo(gameObject, iTween.Hash("position", pos, "islocal", true, "time", 0.5f));
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Coin") {
			int coin = PlayerPrefs.GetInt("Coin", 0);
			coin++;

			PlayerPrefs.SetInt("Coin", coin);
		}
	}
}
