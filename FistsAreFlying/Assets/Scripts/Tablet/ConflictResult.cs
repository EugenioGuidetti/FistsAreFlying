using UnityEngine;
using System.Collections;

public class ConflictResult : MonoBehaviour {

	private string result = "";
	private bool freshness = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void SetResult (string result) {
		this.result = result;
		freshness = true;
	}

	public bool GetFreshness () {
		return freshness;
	}

	public string GetResult () {
		freshness = false;
		return result;
	}
}
