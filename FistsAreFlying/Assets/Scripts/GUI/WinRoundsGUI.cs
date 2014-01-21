using UnityEngine;
using System.Collections;

public class WinRoundsGUI : MonoBehaviour {

	public GameObject player1Rounds;
	public GameObject player2Rounds;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }


	public void SetRoundPlayers (int roundsPlayer1, int roundsPlayer2) {
		player1Rounds.GetComponent<RoundsPlayerSprite>().EnableRounds (roundsPlayer1);
		player2Rounds.GetComponent<RoundsPlayerSprite>().EnableRounds (roundsPlayer2);
	}
}
