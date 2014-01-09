using UnityEngine;
using System.Collections;

public class MenuEffectGroup : MonoBehaviour {

	private GameObject global;
	public GameObject effectOn;
	public GameObject effectOff;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
	}
	
	// Update is called once per frame
	void Update () {
		if (effectOn.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetEffects(true);
		}
		if (effectOff.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetEffects(false);
		}
	}
}
