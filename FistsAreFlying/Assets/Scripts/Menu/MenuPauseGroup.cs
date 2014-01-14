using UnityEngine;
using System.Collections;

public class MenuPauseGroup : MonoBehaviour {

	public GameObject resumePlay;
	public GameObject quit;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (resumePlay.GetComponent<MenuButton>().GetAmISelected()) {
			SendMessageUpwards("ResumeGame");
		}
		if (quit.GetComponent<MenuButton>().GetAmISelected()) {
			Application.LoadLevel("MainMenu");
		}
	}
}
