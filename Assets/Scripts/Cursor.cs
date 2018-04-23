using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour {

	[SerializeField] Vector2 pos;
	[SerializeField] Vector2 menuPos;
	[SerializeField] Vector2 menuInitPos;
	[SerializeField] GameObject moveRange;
	[SerializeField] GameObject manager;
	[SerializeField] GameObject menuSelector;
	[SerializeField] GameObject[] moveRangeSpace;
	[SerializeField] GameObject title;
	[SerializeField] GameObject[] popUps;
	[SerializeField] GameObject win;
	[SerializeField] GameObject lose;
	[SerializeField] AudioSource swoosh;
	[SerializeField] AudioSource whistle;
	[SerializeField] int menuSelection;
	[SerializeField] Tilemap tile;
	[SerializeField] Grid grid;

	enum State {inGame, OMenu, OBallMenu, DMenu, Main, moving, passing, shooting, oppTurn, title, endGame};
	State gameState;

	private int selectedPass;
	public bool ballYou;
	private bool turnOver;
	private bool moveRangeDrawn;
	private Vector2 savedCursor;

	void Start () {
		ballYou = true;
		turnOver = false;
		moveRangeSpace = new GameObject[12];
		moveRangeDrawn = false;
		menuPos = menuSelector.transform.position;
		menuInitPos = menuPos;
		pos = transform.position;
		gameState = State.title;
		menuSelection = 0;
		title.GetComponent<Renderer>().enabled = true;
		win.GetComponent<Renderer>().enabled = false;
		lose.GetComponent<Renderer>().enabled = false;
	}
	
	public IEnumerator LerpTest() {
		float StartTime = Time.time;
		float EndTime = StartTime + 0.1f;

		while (Time.time < EndTime) {
			float timeProgressed = (Time.time - StartTime) / 0.1f;
			manager.GetComponent<Overseer>().player[0].transform.position = Vector2.Lerp(manager.GetComponent<Overseer>().player[0].transform.position, transform.position, timeProgressed);
			yield return new WaitForFixedUpdate();
		}
		
	}

	public IEnumerator StartGame() {
		for (int i = 0; i < 20; i++) {
			whistle.Play();
			yield return new WaitForSeconds(0.01f);
		}
		gameState = State.inGame;
	}

	public IEnumerator YouShotMake() {
		for (int i = 0; i < 15; i++) {
			popUps[7].transform.position = new Vector2(popUps[7].transform.position.x, popUps[7].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[7].transform.position = new Vector2(popUps[7].transform.position.x, popUps[7].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator YouPass() {
		for (int i = 0; i < 15; i++) {
			popUps[5].transform.position = new Vector2(popUps[5].transform.position.x, popUps[5].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[5].transform.position = new Vector2(popUps[5].transform.position.x, popUps[5].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator YouRebound() {
		for (int i = 0; i < 15; i++) {
			popUps[6].transform.position = new Vector2(popUps[6].transform.position.x, popUps[6].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[6].transform.position = new Vector2(popUps[6].transform.position.x, popUps[6].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator YouShotMiss() {
		for (int i = 0; i < 15; i++) {
			popUps[4].transform.position = new Vector2(popUps[4].transform.position.x, popUps[4].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[4].transform.position = new Vector2(popUps[4].transform.position.x, popUps[4].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator TheyShotMake() {
		for (int i = 0; i < 15; i++) {
			popUps[3].transform.position = new Vector2(popUps[3].transform.position.x, popUps[3].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[3].transform.position = new Vector2(popUps[3].transform.position.x, popUps[3].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator TheyPass() {
		for (int i = 0; i < 15; i++) {
			popUps[1].transform.position = new Vector2(popUps[1].transform.position.x, popUps[1].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[1].transform.position = new Vector2(popUps[1].transform.position.x, popUps[1].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator TheyRebound() {
		for (int i = 0; i < 15; i++) {
			popUps[2].transform.position = new Vector2(popUps[2].transform.position.x, popUps[2].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[2].transform.position = new Vector2(popUps[2].transform.position.x, popUps[2].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator TheyShotMiss() {
		for (int i = 0; i < 15; i++) {
			popUps[0].transform.position = new Vector2(popUps[0].transform.position.x, popUps[0].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[0].transform.position = new Vector2(popUps[0].transform.position.x, popUps[0].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator YouSteal() {
		for (int i = 0; i < 15; i++) {
			popUps[8].transform.position = new Vector2(popUps[8].transform.position.x, popUps[8].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[8].transform.position = new Vector2(popUps[8].transform.position.x, popUps[8].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator YouStealFail() {
		for (int i = 0; i < 15; i++) {
			popUps[9].transform.position = new Vector2(popUps[9].transform.position.x, popUps[9].transform.position.y + 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < 15; i++) {
			popUps[9].transform.position = new Vector2(popUps[9].transform.position.x, popUps[9].transform.position.y - 0.15f);
			yield return new WaitForSeconds(0.02f);
		}
	}


	void Update () {

		//title
		if (gameState == State.title) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				title.GetComponent<Renderer>().enabled = false;
				StartCoroutine("StartGame");
			}
		}

		//endGame
		if (gameState == State.endGame) {
			if (manager.GetComponent<Overseer>().scoreA >= 12) {
				win.GetComponent<Renderer>().enabled = true;
			} else {
				lose.GetComponent<Renderer>().enabled = true;
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				SceneManager.LoadScene( SceneManager.GetActiveScene().name );
			}
		}

		//ingame
		if (gameState == State.inGame) {
			if (Input.GetKeyDown(KeyCode.A)) {
				pos += Vector2.left;
				transform.position = pos;
			}
			if (Input.GetKeyDown(KeyCode.D)) {
				pos += Vector2.right;
				transform.position = pos;
			}
			if (Input.GetKeyDown(KeyCode.W)) {
				pos += Vector2.up;
				transform.position = pos;
			}
			if (Input.GetKeyDown(KeyCode.S)) {
				pos += Vector2.down;
				transform.position = pos;
			}

			if (Input.GetKeyDown(KeyCode.E)) {
				endAllTurns();
			}

			/*
			if (Input.GetKeyDown(KeyCode.O)) {
				//test lerping
				StartCoroutine("LerpTest");
			}*/

			if (Input.GetKeyDown(KeyCode.Return)) {
				for (int i = 0; i < 5; i++) {
					if (manager.GetComponent<Overseer>().positionArray[i] == transform.position && (manager.GetComponent<Overseer>().moved[i] == false || manager.GetComponent<Overseer>().action[i] == false)) {
						if (manager.GetComponent<Overseer>().ballHolder == manager.GetComponent<Overseer>().positionArray[i]) {
							//its the ball handler
							manager.GetComponent<Overseer>().showOMenu();
							manager.GetComponent<Overseer>().selectedPlayer = i;
							gameState = State.OBallMenu;
							break;
						}
						manager.GetComponent<Overseer>().showONoBallMenu();
						manager.GetComponent<Overseer>().selectedPlayer = i;
						gameState = State.OMenu;
						}
					}
				}
			} 

		//OMenu - with ball
		else if (gameState == State.OBallMenu) {
			if (Input.GetKeyDown(KeyCode.W) && menuSelection > 0) {
				menuPos += Vector2.up;
				menuSelector.transform.position = menuPos;
				menuSelection--;
			}

			if (Input.GetKeyDown(KeyCode.S) && menuSelection < 3) {
				menuPos += Vector2.down;
				menuSelector.transform.position = menuPos;
				menuSelection++;
			}

			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 0 && manager.GetComponent<Overseer>().moved[manager.GetComponent<Overseer>().selectedPlayer] == false) {
				manager.GetComponent<Overseer>().hideOMenu();
				gameState = State.moving;
				menuInit();
			}

			//passing
			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 1 && manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] == false) {
				manager.GetComponent<Overseer>().hideOMenu();
				gameState = State.passing;
				menuInit();
			}

			//shooting
			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 2 && manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] == false) {
				manager.GetComponent<Overseer>().hideOMenu();
				gameState = State.shooting;
				menuInit();
			}


			//end turn, set move true
			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 3) {
				manager.GetComponent<Overseer>().hideOMenu();
				manager.GetComponent<Overseer>().moved[manager.GetComponent<Overseer>().selectedPlayer] = true;
				manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
				gameState = State.inGame;
				menuInit();
			}
		}

		//ONoBallMenu - no ball
		else if (gameState == State.OMenu) {
			if (Input.GetKeyDown(KeyCode.W) && menuSelection > 0) {
				menuPos += Vector2.up;
				menuSelector.transform.position = menuPos;
				menuSelection--;
			}

			if (Input.GetKeyDown(KeyCode.S) && menuSelection < 2) {
				menuPos += Vector2.down;
				menuSelector.transform.position = menuPos;
				menuSelection++;
			}

			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 0 && manager.GetComponent<Overseer>().moved[manager.GetComponent<Overseer>().selectedPlayer] == false) {
				manager.GetComponent<Overseer>().hideONoBallMenu();
				gameState = State.moving;
				menuInit();
			}

			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 1 && manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] == false) {
				bool stealUp = false;
				bool stealDown = false;
				bool stealLeft = false;
				bool stealRight = false;
				//check u, d, l, r for player with ball
				if (transform.position + Vector3.up == manager.GetComponent<Overseer>().ballPosition) {
					stealUp = true;
				} else if (transform.position + Vector3.down == manager.GetComponent<Overseer>().ballPosition) {
					stealDown = true;
				} else if (transform.position + Vector3.left == manager.GetComponent<Overseer>().ballPosition) {
					stealLeft = true;
				} else if (transform.position + Vector3.right == manager.GetComponent<Overseer>().ballPosition) {
					stealRight = true;
				}

				if (stealUp == true || stealDown == true || stealLeft == true || stealRight == true) {
					if (Random.value <= 0.1) {
					//stole ball
						manager.GetComponent<Overseer>().ballHolder = transform.position;
						manager.GetComponent<Overseer>().ball.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
						ballYou = true;
						StartCoroutine("YouSteal");
					} else {
						StartCoroutine("YouStealFail");
					}
					manager.GetComponent<Overseer>().hideONoBallMenu();
					manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
					gameState = State.inGame;
					menuInit();
				}
				
			}

			//end turn, set move true
			if (Input.GetKeyDown(KeyCode.Return) && menuSelection == 2) {
				manager.GetComponent<Overseer>().hideONoBallMenu();
				manager.GetComponent<Overseer>().moved[manager.GetComponent<Overseer>().selectedPlayer] = true;
				manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
				gameState = State.inGame;
				menuInit();
			}
		}

		//moving
		else if (gameState == State.moving) {
			//start by drawing grid around moving player
			if (moveRangeDrawn == false) {
				moveRangeSpace[0] = Instantiate(moveRange, transform.position + Vector3.up * 2, transform.rotation);
				moveRangeSpace[1] = Instantiate(moveRange, transform.position + Vector3.up * 1 + Vector3.left, transform.rotation);
				moveRangeSpace[2] = Instantiate(moveRange, transform.position + Vector3.up * 1, transform.rotation);
				moveRangeSpace[3] = Instantiate(moveRange, transform.position + Vector3.up * 1 + Vector3.right, transform.rotation);
				moveRangeSpace[4] = Instantiate(moveRange, transform.position + Vector3.left * 2, transform.rotation);
				moveRangeSpace[5] = Instantiate(moveRange, transform.position + Vector3.left * 1, transform.rotation);
				moveRangeSpace[6] = Instantiate(moveRange, transform.position + Vector3.right * 1, transform.rotation);
				moveRangeSpace[7] = Instantiate(moveRange, transform.position + Vector3.right * 2, transform.rotation);
				moveRangeSpace[8] = Instantiate(moveRange, transform.position + Vector3.down * 1 + Vector3.left * 1, transform.rotation);
				moveRangeSpace[9] = Instantiate(moveRange, transform.position + Vector3.down * 1, transform.rotation);
				moveRangeSpace[10] = Instantiate(moveRange, transform.position + Vector3.down * 1 + Vector3.right * 1, transform.rotation);
				moveRangeSpace[11] = Instantiate(moveRange, transform.position + Vector3.down * 2, transform.rotation);
				moveRangeDrawn = true;
				//also save cursors initial position
				savedCursor = pos = transform.position;
			}
			//move around that grid
			if (Input.GetKeyDown(KeyCode.W)) {
				for (int i = 0; i < moveRangeSpace.Length; i++) {
					if (transform.position + Vector3.up == moveRangeSpace[i].transform.position && transform.position.y < 2.5) {
						pos = transform.position + Vector3.up;
						transform.position = pos;
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.A)) {
				for (int i = 0; i < moveRangeSpace.Length; i++) {
					if (transform.position + Vector3.left == moveRangeSpace[i].transform.position && transform.position.x > -7.5) {
						pos = transform.position + Vector3.left;
						transform.position = pos;
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				for (int i = 0; i < moveRangeSpace.Length; i++) {
					if (transform.position + Vector3.down == moveRangeSpace[i].transform.position && transform.position.y > -4.5) {
						pos = transform.position + Vector3.down;
						transform.position = pos;
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.D)) {
				for (int i = 0; i < moveRangeSpace.Length; i++) {
					if (transform.position + Vector3.right == moveRangeSpace[i].transform.position && transform.position.x < 7.5) {
						pos = transform.position + Vector3.right;
						transform.position = pos;
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				for (int i = 0; i < moveRangeSpace.Length; i++) {
					if (transform.position == moveRangeSpace[i].transform.position) {
						//check if another player is in the position first
						//if so cant move
						for (int k = 0; k < manager.GetComponent<Overseer>().player.Length; k++) {
							if (manager.GetComponent<Overseer>().positionArray[k] == transform.position) {
								manager.GetComponent<Overseer>().spaceTaken = true;
								break;
							} else {
								manager.GetComponent<Overseer>().spaceTaken = false;
							}
						}
						//move selected player
						if (manager.GetComponent<Overseer>().spaceTaken == false) {
							//if player has ball that has to be moved also
							if (manager.GetComponent<Overseer>().player[manager.GetComponent<Overseer>().selectedPlayer].transform.position == manager.GetComponent<Overseer>().ballHolder) {

								manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = transform.position;
								manager.GetComponent<Overseer>().ball.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
							}
							manager.GetComponent<Overseer>().player[manager.GetComponent<Overseer>().selectedPlayer].transform.position = transform.position;
							manager.GetComponent<Overseer>().positionArray[manager.GetComponent<Overseer>().selectedPlayer] = transform.position;

							//if player next to ball on ground pick it up
							if (manager.GetComponent<Overseer>().player[manager.GetComponent<Overseer>().selectedPlayer].transform.position == manager.GetComponent<Overseer>().ballPosition && manager.GetComponent<Overseer>().ballHolder.x == -10) {
								manager.GetComponent<Overseer>().ballHolder = transform.position;
								manager.GetComponent<Overseer>().ball.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
								ballYou = true;
								StartCoroutine("YouRebound");
							}

							if (manager.GetComponent<Overseer>().player[manager.GetComponent<Overseer>().selectedPlayer].transform.position == manager.GetComponent<Overseer>().ballHolder) {
								gameState = State.OBallMenu;
								manager.GetComponent<Overseer>().showOMenu();
							} else {
								//no ball holder
								//manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
								gameState = State.OMenu;
								manager.GetComponent<Overseer>().showONoBallMenu();
							}

							moveRangeDrawn = false;

							//destroy the grid of movement
							for (int j = 0; j < moveRangeSpace.Length; j++) {
								Destroy(moveRangeSpace[j]);
							}

							//set that the player has moved
							manager.GetComponent<Overseer>().moved[manager.GetComponent<Overseer>().selectedPlayer] = true;
							break;
						}
					}
				}
			}
			//check if leaving grid
			if (Input.GetKeyDown(KeyCode.Escape)) {
				transform.position = pos = savedCursor;
				manager.GetComponent<Overseer>().showONoBallMenu();
				gameState = State.OMenu;
				moveRangeDrawn = false;
				//destroy the grid of movement
				for (int i = 0; i < 12; i++) {
					Destroy(moveRangeSpace[i]);
				}
			}
		}

		//passing the ball
		else if (gameState == State.passing) {

			if (Input.GetKeyDown(KeyCode.A)) {
				selectedPass++;
				if (selectedPass > 4) {
					selectedPass = 0;
				}
				//dont select self
				if (manager.GetComponent<Overseer>().positionArray[selectedPass] == manager.GetComponent<Overseer>().positionArray[manager.GetComponent<Overseer>().selectedPlayer]) {
					//increment selectedPass
					selectedPass++;
				}
				if (selectedPass >= 5) {
					selectedPass = 0;
				}
				transform.position = manager.GetComponent<Overseer>().positionArray[selectedPass];
			}

			if (Input.GetKeyDown(KeyCode.D)) {
				selectedPass--;
				if (selectedPass < 0) {
					selectedPass = 4;
				}
				//dont select self
				if (manager.GetComponent<Overseer>().positionArray[selectedPass] == manager.GetComponent<Overseer>().positionArray[manager.GetComponent<Overseer>().selectedPlayer]) {
					//increment selectedPass
					selectedPass--;
				}
				if (selectedPass < 0) {
					selectedPass = 4;
				}
				transform.position = manager.GetComponent<Overseer>().positionArray[selectedPass];
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				//make the pass
				manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = transform.position;
				manager.GetComponent<Overseer>().ball.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
				manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
				StartCoroutine("YouPass");
				gameState = State.inGame;
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				//go back to menu
				manager.GetComponent<Overseer>().showOMenu();
				gameState = State.OBallMenu;
			}
		}

		else if (gameState == State.shooting) {
			//determine chance of shot making it
			int distance = (int) (7.5 + transform.position.x);
			int chance = 0;
			switch (distance) 
			{
				case 0:
					chance = 80;
					break;
				case 1:
					chance = 75;
					break;
				case 2:
					chance = 70;
					break;
				case 3:
					chance = 65;
					break;
				case 4:
					chance = 60;
					break;
				case 5:
					chance = 55;
					break;
				case 6:
					chance = 50;
					break;
				case 7:
					chance = 45;
					break;
				case 8:
					chance = 40;
					break;
				case 9:
					chance = 35;
					break;
				case 10:
					chance = 30;
					break;
				case 11:
					chance = 25;
					break;
				case 12:
					chance = 20;
					break;
				case 13:
					chance = 15;
					break;
				case 14:
					chance = 10;
					break;
				case 15:
					chance = 5;
					break;
				default:
					chance = 0;
					break;
			}
			if (Random.Range(0,100) <= chance) {
				//shot was made, give ball to other team and reset positions
				swoosh.Play();
				endAllTurns();
				resetOppBall();
				manager.GetComponent<Overseer>().scoreA += 2;
				gameState = State.inGame;
				ballYou = false;
				if (manager.GetComponent<Overseer>().scoreA >= 12) {
					gameState = State.endGame;
					return;
				}
				StartCoroutine("YouShotMake");
				//OtherTurnOff();
			} else {
				//missed shot, put ball by hoop somewhere
				float minShotDropX = -7.5f;
				float maxShotDropX = -4.5f;
				float minShotDropY = -2.5f;
				float maxShotDropY =  0.5f;
				float incShotDrop = 1.0f;

				float randomX = Random.Range(minShotDropX, maxShotDropX);
				float numSteps = Mathf.Floor (randomX / incShotDrop);
				randomX = numSteps * incShotDrop + 0.5f;
				
				float randomY = Random.Range(minShotDropY, maxShotDropY);
				numSteps = Mathf.Floor (randomY / incShotDrop);
				randomY = numSteps * incShotDrop + 0.5f;

				Vector2 dropLocation = new Vector2 (randomX, randomY);

				manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ball.transform.position = dropLocation;
				manager.GetComponent<Overseer>().ballHolder = new Vector2(-10, -10);
				manager.GetComponent<Overseer>().action[manager.GetComponent<Overseer>().selectedPlayer] = true;
				Debug.Log("Missed shot");
				StartCoroutine("YouShotMiss");

				//check if someone picked up the rebound
				for (int i = 0; i < 10; i++) {
					if (manager.GetComponent<Overseer>().ballPosition == manager.GetComponent<Overseer>().player[i].transform.position && manager.GetComponent<Overseer>().ballHolder.x == -10) {
						//a player has the ball so give it to him
						manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[i].transform.position;
						manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[i].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[i].transform.position.y - 0.2f);
						if (i < 5) {
							ballYou = true;
							StartCoroutine("YouRebound");
						} else {
							ballYou = false;
							StartCoroutine("TheyRebound");
						}
						break;
					}
				}
			}
			gameState = State.inGame;
		}

		//check if turn is over
		turnOver = true;
		for (int i = 0; i < 5; i++) {
			if (manager.GetComponent<Overseer>().moved[i] == false || manager.GetComponent<Overseer>().action[i] == false) {
				turnOver = false;
				break;
			} 
		}
			//no breaks, turn is over. reset everything.
		if (turnOver == true) {
			for (int j = 0; j < 5; j++) {
				manager.GetComponent<Overseer>().moved[j] = false;
				manager.GetComponent<Overseer>().action[j] = false;
			}
			if (ballYou == true) {
				OtherTurnDef();
			} else {
				OtherTurnOff();
			}
			turnOver = false;
		}
	}

	void OtherTurnOff() {
		for (int j = 0; j < 2; j++) {
			for (int k = 5; k < 10; k++) {
				//going to start by trying to go right toward the basket
				bool shotTaken = false;
				bool spotLeft = false;
				bool spotRight = false;
				bool spotUp = false;
				bool spotDown = false;
				for (int i = 0; i < 10; i++) {
					if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.right) {
						spotRight = true;
						break;
					}
				}
				for (int i = 0; i < 10; i++) {
					if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.left) {
						spotLeft = true;
						break;
					}
				}
				for (int i = 0; i < 10; i++) {
					if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.up) {
						spotUp = true;
						break;
					}
				}
				for (int i = 0; i < 10; i++) {
					if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.down) {
						spotDown = true;
						break;
					}
				}
				if (spotRight == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 1.5f) {
					// if the player has the ball take it with him
					if (manager.GetComponent<Overseer>().player[k].transform.position == manager.GetComponent<Overseer>().ballHolder) {
						manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
							 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
					} else {
						//just a regular player, move him
						manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
					}
				} else if (spotRight == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 3.5f) {
					if (Random.value < 0.3) {
						// if the player has the ball take it with him
						if (manager.GetComponent<Overseer>().player[k].transform.position == manager.GetComponent<Overseer>().ballHolder) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
								 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
							manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
							//take shot
							if (Random.value <= 0.1) {
								shotTaken = true;
								Debug.Log("taking shot");
								if (Random.value <= 0.2) {
									Debug.Log("made shot");
									//shot was made, give ball to other team and reset positions
									swoosh.Play();
									endAllTurns();
									resetPlayerBall();
									manager.GetComponent<Overseer>().scoreB += 2;
									gameState = State.inGame;
									ballYou = true;
									StartCoroutine("TheyShotMake");
									if (manager.GetComponent<Overseer>().scoreB >= 12) {
										gameState = State.endGame;
										return;
									}
									return;
								} else {
									//missed shot, put ball by hoop somewhere
									float minShotDropX = 4.5f;
									float maxShotDropX = 7.5f;
									float minShotDropY = -2.5f;
									float maxShotDropY =  0.5f;
									float incShotDrop = 1.0f;

									float randomX = Random.Range(minShotDropX, maxShotDropX);
									float numSteps = Mathf.Floor (randomX / incShotDrop);
									randomX = numSteps * incShotDrop + 0.5f;
									
									float randomY = Random.Range(minShotDropY, maxShotDropY);
									numSteps = Mathf.Floor (randomY / incShotDrop);
									randomY = numSteps * incShotDrop + 0.5f;

									Vector2 dropLocation = new Vector2 (randomX, randomY);

									Debug.Log(randomX);
									Debug.Log(randomY);

									manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ball.transform.position = dropLocation;
									manager.GetComponent<Overseer>().ballHolder = new Vector2(-10, -10);
									Debug.Log("Missed shot");
									ballYou = true;
									StartCoroutine("TheyShotMiss");
									//check if someone picked up the rebound
									for (int i = 0; i < 10; i++) {
										if (manager.GetComponent<Overseer>().ballPosition == manager.GetComponent<Overseer>().player[i].transform.position && manager.GetComponent<Overseer>().ballHolder.x == -10) {
											Debug.Log("picked up");
											//a player has the ball so give it to him
											manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[i].transform.position;
											manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[i].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[i].transform.position.y - 0.2f);
											if (i < 5) {
												ballYou = true;
												StartCoroutine("YouRebound");
											} else { 
												ballYou = false;
												StartCoroutine("TheyRebound");
											}
											break;
										}
									}
								}
							}
							//make pass
							if (Random.value < 0.5 && shotTaken == false) {
								Debug.Log("passingA");
								int passTo = Random.Range(5,9);
								if (passTo == k) {
									passTo++;
								}
								if (passTo == 10) {
									passTo = 5;
								}
								Debug.Log(passTo);
								StartCoroutine("TheyPass");
								manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[passTo].transform.position;
								manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().ballPosition.x + 0.2f, manager.GetComponent<Overseer>().ballPosition.y - 0.2f);
							}
						} else {
							//just a regular player, move him
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						}
					}
				} else if (manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 8.5f) {
						// if the player has the ball take it with him
						if (manager.GetComponent<Overseer>().player[k].transform.position == manager.GetComponent<Overseer>().ballPosition) {
							int choice = Random.Range(0, 100);
							if (choice <= 25) {
								//up
								if (spotUp == false && manager.GetComponent<Overseer>().positionArray[k].y + 1.0f <= 2.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
									 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
									manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
								}
							} else if (choice <= 50 && choice > 25) {
								//down
								if (spotDown == false && manager.GetComponent<Overseer>().positionArray[k].y -1.0f >= -4.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
									 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
									manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
								}
							} else if (choice <= 75 && choice > 50) {
								//left
								if (spotLeft == false && manager.GetComponent<Overseer>().positionArray[k].x - 1.0f >= -7.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
									 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
									manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
								}
							} else {
								//right
								if (spotRight == false && manager.GetComponent<Overseer>().positionArray[k].x + 1.0f <= 7.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder =
									 new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
									manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[k].transform.position.y - 0.2f);
								}
							}
							//take shot
							if (Random.value < 0.3) {
								Debug.Log("taking shot");
								shotTaken = true;
								ballYou = true;
								if (Random.value <= 0.4) {
									Debug.Log("made shot");
									//shot was made, give ball to other team and reset positions
									swoosh.Play();
									endAllTurns();
									resetPlayerBall();
									manager.GetComponent<Overseer>().scoreB += 2;
									gameState = State.inGame;
									ballYou = true;
									StartCoroutine("TheyShotMake");
									if (manager.GetComponent<Overseer>().scoreB >= 12) {
										gameState = State.endGame;
										return;
									}
									return;
									//OtherTurnOff();
								} else {

									//missed shot, put ball by hoop somewhere
									float minShotDropX = 4.5f;
									float maxShotDropX = 7.5f;
									float minShotDropY = -2.5f;
									float maxShotDropY =  0.5f;
									float incShotDrop = 1.0f;

									float randomX = Random.Range(minShotDropX, maxShotDropX);
									float numSteps = Mathf.Floor (randomX / incShotDrop);
									randomX = numSteps * incShotDrop + 0.5f;
									
									float randomY = Random.Range(minShotDropY, maxShotDropY);
									numSteps = Mathf.Floor (randomY / incShotDrop);
									randomY = numSteps * incShotDrop + 0.5f;

									Vector2 dropLocation = new Vector2 (randomX, randomY);

									Debug.Log(randomX);
									Debug.Log(randomY);

									manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ball.transform.position = dropLocation;
									manager.GetComponent<Overseer>().ballHolder = new Vector2(-10, -10);
									Debug.Log("Missed shot");
									StartCoroutine("TheyShotMiss");
									ballYou = true;

									//check if someone picked up the rebound
									for (int i = 0; i < 10; i++) {
										if (manager.GetComponent<Overseer>().ballPosition == manager.GetComponent<Overseer>().player[i].transform.position && manager.GetComponent<Overseer>().ballHolder.x == -10) {
											Debug.Log("picked up");
											//a player has the ball so give it to him
											manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[i].transform.position;
											manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[i].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[i].transform.position.y - 0.2f);
											if (i < 5) {
												ballYou = true;
												StartCoroutine("YouRebound");
											} else {
												ballYou = false;
												StartCoroutine("TheyRebound");
											}
											break;
										}
									}
								}
							} 
							if (Random.value < 0.5 && shotTaken == false) {
								Debug.Log("passingB");
								int passTo = Random.Range(5,9);
								if (passTo == k) {
									passTo ++;
								}
								if (passTo == 10) {
									passTo = 5;
								}
								Debug.Log(passTo);
								StartCoroutine("TheyPass");
								manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[passTo].transform.position;
								manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().ballPosition.x + 0.2f, manager.GetComponent<Overseer>().ballPosition.y - 0.2f);
							}
						} else {
							//just a regular player, move him with the random stuff above
							int choice = Random.Range(0, 100);
							if (choice <= 25) {
								//up
								if (spotUp == false && manager.GetComponent<Overseer>().positionArray[k].y + 1.0f <= 2.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
								}
							} else if (choice <= 50 && choice > 25) {
								//down
								if (spotDown == false && manager.GetComponent<Overseer>().positionArray[k].y -1.0f >= -4.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
								}
							} else if (choice <= 75 && choice > 50) {
								//left
								if (spotLeft == false && manager.GetComponent<Overseer>().positionArray[k].x - 1.0f >= -7.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
								}
							} else {
								//right
								if (spotRight == false && manager.GetComponent<Overseer>().positionArray[k].x + 1.0f <= 7.5f) {
									manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
								}
							}
						}		
					}
				}
			}
		}
	

	void OtherTurnDef() {
		bool up, left, right, down;
		float upI, leftI, rightI, downI;
		for (int j = 0; j < 2; j++) {
			for (int k = 5; k < 10; k++) {
				//first thing to do is if the ball is loose go after it
				Vector2 distance;
				if (manager.GetComponent<Overseer>().ballHolder.x == -10) {
					distance = manager.GetComponent<Overseer>().positionArray[k] - manager.GetComponent<Overseer>().ballPosition;
				} else {
					distance = manager.GetComponent<Overseer>().positionArray[k] - manager.GetComponent<Overseer>().positionArray[k-5];
				}
				if (distance.x <= 0) {
					right = true;
					rightI = -distance.x;
					leftI = 0;
					left = false;
				} else {
					right = false;
					left = true;
					leftI = distance.x;
					rightI = 0;
				}
				if (distance.y <= 0) {
					up = true;
					upI = -distance.y;
					downI = 0;
					down = false;
				} else {
					up = false;
					down = true;
					downI = distance.y;
					upI = 0;
				}
				if (up && right) {
					bool spotTaken = false;
					if (upI > rightI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.up) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f <= 2.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
						}
					} else if (rightI > upI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.right) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 7.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						}
					} else if (rightI == upI) {
						if (Random.value < 0.5) {
							//up
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.up) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f <= 2.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
							}	
						} else {
							//right
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.right) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 7.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
							}
						}
					}
				} else if (up && left) {
					bool spotTaken = false;
					if (upI > leftI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.up) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f <= 2.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
						}
					} else if (leftI > upI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.left) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f >= -7.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						}
					} else if (leftI == upI) {
						if (Random.value < 0.5) {
							//up
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.up) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f <= 2.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y + 1.0f);
							}
						} else {
							//left
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.left) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f >= -7.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
							}
						}
					}
				} else if (down && right) {
					bool spotTaken = false;
					if (downI > rightI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.down) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f >= -4.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
						}
					} else if (rightI > downI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.right) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 7.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						}
					} else if (rightI == downI) {
						if (Random.value < 0.5) {
							//down
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.down) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f >= -4.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
							}
						} else {
							//right
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.right) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f <= 7.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x + 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
							}
						}

					}
				} else if (down && left) {
					bool spotTaken = false;
					if (downI > leftI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.down) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f >= -4.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
						}
					} else if (leftI > downI) {
						for (int i = 0; i < 10; i++) {
							if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.left) {
								spotTaken = true;
								break;
							}
						}
						if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f >= -7.5f) {
							manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
						}
					} else if (leftI == downI) {
						if (Random.value < 0.5) {
							//down
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.down) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f >= -4.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x, manager.GetComponent<Overseer>().player[k].transform.position.y - 1.0f);
							}
						} else {
							//left
							for (int i = 0; i < 10; i++) {
								if (manager.GetComponent<Overseer>().positionArray[i] == manager.GetComponent<Overseer>().positionArray[k] + Vector3.left) {
									spotTaken = true;
									break;
								}
							}
							if (spotTaken == false && manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f >= -7.5f) {
								manager.GetComponent<Overseer>().positionArray[k] = manager.GetComponent<Overseer>().player[k].transform.position = new Vector2(manager.GetComponent<Overseer>().player[k].transform.position.x - 1.0f, manager.GetComponent<Overseer>().player[k].transform.position.y);
							}
						}
					}
				}
				//if ball loose and def on it they pick it up
				if (manager.GetComponent<Overseer>().positionArray[k] == manager.GetComponent<Overseer>().ballPosition && manager.GetComponent<Overseer>().ballHolder.x == -10.0f) {
					manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().positionArray[k];
					manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().positionArray[k].x + 0.2f, manager.GetComponent<Overseer>().positionArray[k].y - 0.2f);
					//turn changes
					StartCoroutine("TheyRebound");
					ballYou = false;
					return;
				}
			}
		}
	}

	void endAllTurns() {
		for (int i = 0; i < 5; i++) {
			manager.GetComponent<Overseer>().moved[i] = true;
			manager.GetComponent<Overseer>().action[i] = true;
		}
	}

	void resetOppBall() {
		manager.GetComponent<Overseer>().positionArray[0] = manager.GetComponent<Overseer>().player[0].transform.position = new Vector2(-3.5f, 0.5f);
		manager.GetComponent<Overseer>().positionArray[1] = manager.GetComponent<Overseer>().player[1].transform.position = new Vector2(-6.5f, 1.5f);
		manager.GetComponent<Overseer>().positionArray[2] = manager.GetComponent<Overseer>().player[2].transform.position = new Vector2(-6.5f, 0.5f);
		manager.GetComponent<Overseer>().positionArray[3] = manager.GetComponent<Overseer>().player[3].transform.position = new Vector2(-3.5f,-1.5f);
		manager.GetComponent<Overseer>().positionArray[4] = manager.GetComponent<Overseer>().player[4].transform.position = new Vector2(-4.5f,-2.5f);

		manager.GetComponent<Overseer>().positionArray[5] = manager.GetComponent<Overseer>().player[5].transform.position = new Vector2(-4.5f, 1.5f);
		manager.GetComponent<Overseer>().positionArray[6] = manager.GetComponent<Overseer>().player[6].transform.position = new Vector2(-7.5f, 2.5f);
		manager.GetComponent<Overseer>().positionArray[7] = manager.GetComponent<Overseer>().player[7].transform.position = new Vector2(-7.5f,-0.5f);
		manager.GetComponent<Overseer>().positionArray[8] = manager.GetComponent<Overseer>().player[8].transform.position = new Vector2(-2.5f,-1.5f);
		manager.GetComponent<Overseer>().positionArray[9] = manager.GetComponent<Overseer>().player[9].transform.position = new Vector2(-3.5f,-3.5f);

		//put ball on BB
		manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = manager.GetComponent<Overseer>().player[6].transform.position;
		manager.GetComponent<Overseer>().ball.transform.position = new Vector2(manager.GetComponent<Overseer>().player[6].transform.position.x + 0.2f, manager.GetComponent<Overseer>().player[6].transform.position.y - 0.2f);
	}

	void resetPlayerBall() {
		manager.GetComponent<Overseer>().positionArray[0] = manager.GetComponent<Overseer>().player[0].transform.position = new Vector2( 4.5f, 1.5f);
		manager.GetComponent<Overseer>().positionArray[1] = manager.GetComponent<Overseer>().player[1].transform.position = new Vector2( 7.5f, 2.5f);
		manager.GetComponent<Overseer>().positionArray[2] = manager.GetComponent<Overseer>().player[2].transform.position = new Vector2( 7.5f,-0.5f);
		manager.GetComponent<Overseer>().positionArray[3] = manager.GetComponent<Overseer>().player[3].transform.position = new Vector2( 2.5f,-1.5f);
		manager.GetComponent<Overseer>().positionArray[4] = manager.GetComponent<Overseer>().player[4].transform.position = new Vector2( 3.5f,-3.5f);

		manager.GetComponent<Overseer>().positionArray[5] = manager.GetComponent<Overseer>().player[5].transform.position = new Vector2( 3.5f, 0.5f);
		manager.GetComponent<Overseer>().positionArray[6] = manager.GetComponent<Overseer>().player[6].transform.position = new Vector2( 5.5f, 1.5f);
		manager.GetComponent<Overseer>().positionArray[7] = manager.GetComponent<Overseer>().player[7].transform.position = new Vector2( 6.5f, 0.5f);
		manager.GetComponent<Overseer>().positionArray[8] = manager.GetComponent<Overseer>().player[8].transform.position = new Vector2( 3.5f,-1.5f);
		manager.GetComponent<Overseer>().positionArray[9] = manager.GetComponent<Overseer>().player[9].transform.position = new Vector2( 4.5f,-2.5f);

		//put ball on AB
		transform.position = pos = manager.GetComponent<Overseer>().player[1].transform.position;
		manager.GetComponent<Overseer>().ballPosition = manager.GetComponent<Overseer>().ballHolder = transform.position;
		manager.GetComponent<Overseer>().ball.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
	}


	void menuInit() {
		menuSelector.transform.position = menuPos = menuInitPos;
		menuSelection = 0;
	}
}

