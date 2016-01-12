using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private CoinShow Coin;
	[SerializeField]
	private ScoreShow Score;
	[SerializeField]
	private UIPanel GamePanel;
	[SerializeField]
	private UIPanel TitlePanel;
	[SerializeField]
	private GameObject ShootOn;
	[SerializeField]
	private GameObject ShootOff;
	[SerializeField]
	private CarController Car;
	[SerializeField]
	private EnermyController Enermy;

	void Awake ()
	{
		Time.timeScale = 0.0f;

		//Test
		PlayerPrefs.SetInt("Coin", 123);
		PlayerPrefs.SetInt("Score", 23);
	}

	bool _IsFirst = false;
	void Start ()
	{
		NGUITools.SetActive(GamePanel.gameObject, false);
		NGUITools.SetActive(ShootOn, false);

		Coin.Show();
		Score.Show();

		_IsFirst = true;
	}
	
	void Update ()
	{
		Coin.Show();
		Score.Show();

		if (Car.State == CarState.Die) {
			Debug.LogError("Die");

			Time.timeScale = 0.0f;
		}
	}

	public void OnStart()
	{
		NGUITools.SetActive(TitlePanel.gameObject, false);
		NGUITools.SetActive(GamePanel.gameObject, true);

		Time.timeScale = 1.0f;

		if (_IsFirst) {
			PlayerPrefs.SetInt("Coin", 0);
			PlayerPrefs.SetInt("Score", 0);

			_IsFirst = false;
		}

		Invoke("_CreateEnemy", 1.0f);
		Invoke("_Charging", 5.0f);
		InvokeRepeating("_Distance", 0.1f, 10.0f);
//		InvokeRepeating("_CreateCoin", 0.1f, 1.0f);
	}

	void _CreateEnemy()
	{
		if (Enermy.State == EnermyState.Show) {
			return;
		}

		Enermy.Show();

		InvokeRepeating("_Attack", 0.1f, 2.0f);
	}

	void _Attack()
	{
		if (Enermy.State == EnermyState.Ready)
			_CreateEnemy();

		if (Car.Direction == Enermy.Direction) {
			Car.Attack();
			Enermy.PreferDie();
		} else {

			int i = UnityEngine.Random.Range(0, 3);
			CarDirection dir = CarDirection.Left;
			if (i == 0) {
				dir = CarDirection.Center;
			} else if (i == 1) {
				dir = CarDirection.Left;
			} else if (i == 2) {
				dir = CarDirection.Right;
			}

			if (dir == Car.Direction) {
				Enermy.Attack();
				Car.Demage();
			}
			
			Enermy.Move(dir);
		}
	}

	void _CreateCoin()
	{
		Debug.Log("_CreateCoin");
		GameObject coin = Instantiate( Resources.Load<GameObject>("Prefab/Coin") );

		CoinController controller = coin.GetComponent<CoinController>();

		controller.CoinMove(CarDirection.Center);
	}

	void _Distance()
	{
		Debug.Log("_Distance");
		
		int distance = PlayerPrefs.GetInt("Score", 0) + 1;
		PlayerPrefs.SetInt("Score", distance);
	}

	void _Charging()
	{
		NGUITools.SetActive(ShootOn, true);
		NGUITools.SetActive(ShootOff, false);
	}

	public void OnPause()
	{
		Time.timeScale = 0.0f;

		NGUITools.SetActive(TitlePanel.gameObject, true);
		NGUITools.SetActive(GamePanel.gameObject, false);
	}

	public void Shoot()
	{
		Debug.Log("Shoot");

		NGUITools.SetActive(ShootOn, false);
		NGUITools.SetActive(ShootOff, true);

		Invoke("_Charging", 5.0f);
	}

	public void MoveLeft()
	{
		Car.Left();
	}

	public void MoveCenter()
	{
		Car.Center();
	}

	public void MoveRight()
	{
		Car.Right();
	}
}
