using UnityEngine;
using System.Collections;

public class Logos : MonoBehaviour {

	public GameObject oniricPlatypusLogo;
	public GameObject oniricPlatypusText;
	public GameObject polimiGameCollectiveLogo;
	public GameObject menteZeroLogo;

	// Use this for initialization
	void Start () {
		StartCoroutine("LogosManager");
	}
	
	// Update is called once per frame
	void Update () { }

	private IEnumerator LogosManager () {
		oniricPlatypusLogo.SetActive(true);
		oniricPlatypusText.SetActive (true);
		yield return new WaitForSeconds (2f);
		oniricPlatypusLogo.SetActive(false);
		oniricPlatypusText.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		polimiGameCollectiveLogo.SetActive(true);
		yield return new WaitForSeconds (2f);
		polimiGameCollectiveLogo.SetActive(false);
		yield return new WaitForSeconds (0.5f);
		menteZeroLogo.SetActive (true);
		yield return new WaitForSeconds (2f);
		Application.LoadLevel("MainMenu");
	}
}
