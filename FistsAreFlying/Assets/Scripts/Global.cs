using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	private bool tablet = false;
	private float width = 0;
	private float heigth = 0;
	private float inches = 0;

	private bool onlineGame = false;
	private bool amIPlayer1 = false;
	private int onlineTime = 0;
	private bool timeGame = false;
	private int time = 10;
	private bool soundEnable = true;
	private bool effectsEnable = true;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this);
		if (Screen.dpi <= 0) {
		} else {
			width = Screen.width / Screen.dpi;
			heigth = Screen.height / Screen.dpi;
			inches = Mathf.Sqrt(Mathf.Pow(width,2f) + Mathf.Pow(heigth,2f));
			Debug.Log(inches.ToString());
			if (inches >= 6.5) {
				tablet = true;
			}
		}
		//da togliere
		//tablet = true;
		Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {}

	public bool GetTablet () {
		return tablet;
	}

	public bool GetOnlineGame () {
		return onlineGame;
	}

	public void SetOnlineGame (bool flag) {
		onlineGame = flag;
	}

	public bool GetAmIPlayer1 () {
		return amIPlayer1;
	}

	public void SetAmIPlayer1 (bool flag) {
		amIPlayer1 = flag;
	}

	public bool GetTimeGame () {
		return timeGame;
	}

	public void SetTimeGame (bool flag) {
		timeGame = flag;
	}

	public int GetTime () {
		return time;
	}

	public void SetTime (int time) {
		this.time = time;
	}
	
	public int GetOnlineTime () {
		return onlineTime;
	}
	
	public void SetOnlineTime (int time) {
		onlineTime = time;
	}

	public bool GetSound () {
		return soundEnable;
	}

	public void SetSound (bool flag) {
		soundEnable = flag;
	}

	public bool GetEffects () {
		return effectsEnable;
	}

	public void SetEffects (bool flag) {
		effectsEnable = flag;
	}

}
