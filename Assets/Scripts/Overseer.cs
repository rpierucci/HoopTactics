using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Overseer : MonoBehaviour {

	[SerializeField] GameObject OMenu;
	[SerializeField] GameObject ONoBallMenu;
	[SerializeField] GameObject menuSelector;
	[SerializeField] Tilemap court;
	[SerializeField] Grid grid;
	[SerializeField] bool yourTurn;

	public GameObject ball;
	public GameObject[] player;
	public bool[] moved;
	public bool[] action;
	public Vector3 ballPosition;
	public Vector3[] positionArray;
	public Vector3 ballHolder;
	public int selectedPlayer;
	public int scoreA;
	public int scoreB;
	public bool spaceTaken;

	void Start () {
		spaceTaken = false;
		selectedPlayer = -1;
		scoreA = scoreB = 0;
		hideOMenu();
		hideONoBallMenu();
		//get all player positions
		for (int i = 0; i < player.Length; i++) {
			positionArray[i] = grid.GetCellCenterLocal(Vector3Int.FloorToInt(player[i].transform.position));
		}

		//Ball Position
		ballPosition = grid.GetCellCenterLocal(Vector3Int.FloorToInt(ball.transform.position));

		//set who is holding the ball
		for (int i = 0; i < positionArray.Length; i++) {
			if (ballPosition == grid.GetCellCenterLocal(Vector3Int.FloorToInt(positionArray[i]))) {
				ballHolder = positionArray[i];
			} 
		}

		//set all moves to false
		for (int i = 0; i < player.Length; i++) {
			moved[i] = false;
		}

		for (int i = 0; i < player.Length; i++) {
			action[i] = false;
		}


		//set turn
		yourTurn = true;


	}
	
	void Update () {
		updateScore();
	}

	void updateScore() {
		string p1 = scoreA.ToString();
		GameObject.Find("PlayerScore").GetComponent<Text>().text = "Player: " + p1;
		string p2 = scoreB.ToString();
		GameObject.Find("CompScore").GetComponent<Text>().text = "Computer: " + p2;
	}

	public void showOMenu() {
		OMenu.GetComponent<Renderer>().enabled = true;
		menuSelector.GetComponent<Renderer>().enabled = true;
	}

	public void hideOMenu() {
		OMenu.GetComponent<Renderer>().enabled = false;
		menuSelector.GetComponent<Renderer>().enabled = false;
	}

	public void showONoBallMenu() {
		ONoBallMenu.GetComponent<Renderer>().enabled = true;
		menuSelector.GetComponent<Renderer>().enabled = true;
	}

	public void hideONoBallMenu() {
		ONoBallMenu.GetComponent<Renderer>().enabled = false;
		menuSelector.GetComponent<Renderer>().enabled = false;
	}
}
