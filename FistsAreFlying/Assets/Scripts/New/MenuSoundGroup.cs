using UnityEngine;
using System.Collections;

public class MenuSoundGroup : MonoBehaviour {

	private GameObject global;
	public GameObject soundOn;
	public GameObject soundOff;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
	}
	
	// Update is called once per frame
	void Update () {
		if (soundOn.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetSound(true);
		}
		if (soundOff.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetSound(false);
		}
	}
}
