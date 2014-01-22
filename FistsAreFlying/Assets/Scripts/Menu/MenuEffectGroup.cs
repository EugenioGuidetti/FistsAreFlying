using UnityEngine;
using System.Collections;

public class MenuEffectGroup : MonoBehaviour {

	private GameObject global;
	public GameObject effectOn;
	public GameObject effectOff;
	public GameObject checkOn;
	public GameObject checkOff;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		if (global.GetComponent<Global>().GetEffects()) {
			checkOn.SetActive(true);
		}
		else {
			checkOff.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (effectOn.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetEffects(true);
			checkOn.SetActive(true);
			checkOff.SetActive(false);
		}
		if (effectOff.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetEffects(false);
			checkOn.SetActive(false);
			checkOff.SetActive(true);
		}
	}
}
