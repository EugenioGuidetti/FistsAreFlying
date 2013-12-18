using UnityEngine;
using System.Collections;

public class ConflictResult : MonoBehaviour {

	private string result = "";

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void SetResult (string result) {
		this.result = result;
	}

	public string GetResult () {
		return result;
	}
}
