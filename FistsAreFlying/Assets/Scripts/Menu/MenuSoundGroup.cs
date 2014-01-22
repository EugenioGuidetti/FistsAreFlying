using UnityEngine;
using System.Collections;

public class MenuSoundGroup : MonoBehaviour {

	private GameObject global;
	public GameObject menuLoop;
	public GameObject soundOn;
	public GameObject soundOff;
	public GameObject checkOn;
	public GameObject checkOff;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		if (global.GetComponent<Global>().GetSound()) {
			checkOn.SetActive(true);
		}
		else {
			checkOff.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (soundOn.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetSound(true);
			menuLoop.SetActive(true);
			checkOn.SetActive(true);
			checkOff.SetActive(false);
		}
		if (soundOff.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetSound(false);
			menuLoop.SetActive(false);
			checkOff.SetActive(true);
			checkOn.SetActive(false);
		}
	}
}
