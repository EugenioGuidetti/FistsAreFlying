using UnityEngine;
using System.Collections;

public class MenuTutorialGroup : MonoBehaviour {

	public GameObject next;
	public GameObject back;

	public GameObject tutorial1;
	public GameObject tutorial2;
	public GameObject tutorial3;
	public GameObject tutorial4;
	public GameObject tutorial5;
	public GameObject tutorial6;
	public GameObject tutorial7;
	public GameObject tutorial8;

	private int index;
	private GameObject[] tutorials = new GameObject[8];

	// Use this for initialization
	void Start () {
		index = 0;
		tutorials[0] = tutorial1;
		tutorials[1] = tutorial2;
		tutorials[2] = tutorial3;
		tutorials[3] = tutorial4;
		tutorials[4] = tutorial5;
		tutorials[5] = tutorial6;
		tutorials[6] = tutorial7;
		tutorials[7] = tutorial8;
	}
	
	// Update is called once per frame
	void Update () {
		if (index < tutorials.Length - 1) {
			ManageNext();
		}
		if (index > 0) {
			ManageBack();
		}
	}

	private void ManageNext () {
		if (next.GetComponent<MenuButton>().GetAmISelected()) {
			tutorials[index].SetActive(false);
			if (index == 0) {
				back.SetActive(true);
			}
			index = index + 1;
			tutorials[index].SetActive(true);
			if (index == tutorials.Length -1) {
				next.SetActive(false);
			}
		}
	}

	private void ManageBack () {
		if (back.GetComponent<MenuButton>().GetAmISelected()) {
			tutorials[index].SetActive(false);
			if (index == tutorials.Length - 1) {
				next.SetActive(true);
			}
			index = index - 1;
			tutorials[index].SetActive(true);
			if (index == 0) {
				back.SetActive(false);
			}
		}
	}

	public void Reset () {
		tutorials[index].SetActive(false);
		index = 0;
		tutorials[index].SetActive(true);
		back.SetActive(false);
		next.SetActive(true);
	}
	
}
