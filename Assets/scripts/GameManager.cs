using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public string EVENT_GAME_OVER = "GameOver";

	static public string TEXT_PLAYER1_WINS = "Player 1 Wins!";
	static public string TEXT_PLAYER2_WINS = "Player 2 Wins!";
	static public string TEXT_TIE = "Tie!";

	public int Player1Blocks = 0;
	public int Player2Blocks = 0;

	public float MatchTime = 30f;

	public bool Ongoing = true;
	// Use this for initialization
	void Start()
	{
		Messenger.AddListener<FloorTile.Owner>(FloorTile.EVENT_PLAYER_GAINED_TILE, OnPlayerGainedTile);
		Messenger.AddListener<FloorTile.Owner>(FloorTile.EVENT_PLAYER_LOST_TILE, OnPlayerLostTile);
		Messenger.AddListener(EVENT_GAME_OVER, OnTimerFinished);
	}

	// Update is called once per frame
	void Update()
	{
		if (!Ongoing)
			return;

		MatchTime -= Time.deltaTime;

		if (MatchTime <= 0)
		{
			Messenger.Broadcast(EVENT_GAME_OVER);
		}
		else
		{
			// Update UI?
		}
	}

	int GetWinner()
	{
		if (Player1Blocks > Player2Blocks)
			return 1;

		if (Player2Blocks > Player1Blocks)
			return 2;

		return 0;
	}

	void OnTimerFinished()
	{
		Ongoing = false;

		int winner = GetWinner();

		if (winner == 0)
		{
			Messenger.Broadcast(EVENT_GAME_OVER, TEXT_TIE);
			return;
		}

		Messenger.Broadcast(EVENT_GAME_OVER, winner == 1 ? TEXT_PLAYER1_WINS : TEXT_PLAYER2_WINS);
	}

	void OnPlayerLostTile(FloorTile.Owner player)
	{
		switch (player)
		{
			case FloorTile.Owner.NOBODY:
				break;
			case FloorTile.Owner.PLAYER1:
				Player1Blocks--;
				break;
			case FloorTile.Owner.PLAYER2:
				Player2Blocks--;
				break;
			default:
				break;
		}
	}

	void OnPlayerGainedTile(FloorTile.Owner player)
	{
				switch (player)
		{
			case FloorTile.Owner.NOBODY:
				break;
			case FloorTile.Owner.PLAYER1:
				Player1Blocks++;
				break;
			case FloorTile.Owner.PLAYER2:
				Player2Blocks++;
				break;
			default:
				break;
		}
	}
}
