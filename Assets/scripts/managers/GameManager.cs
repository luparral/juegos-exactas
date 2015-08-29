using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public string EVENT_GAME_OVER = "GameOver";
	static public string EVENT_TICK = "Tick";

	public int Player1Blocks = 0;
	public int Player2Blocks = 0;

	// Tiempo en segundos
	public float MatchTime = 30f;

	public bool Ongoing = true;
	// Use this for initialization
	void Start()
	{
		Messenger.AddListener<FloorTile.Owner>(FloorTile.EVENT_PLAYER_GAINED_TILE, OnPlayerGainedTile);
		Messenger.AddListener<FloorTile.Owner>(FloorTile.EVENT_PLAYER_LOST_TILE, OnPlayerLostTile);

		StartCoroutine("ClockTick");
	}

	// Update is called once per frame
	void Update()
	{
		if (!Ongoing)
			return;
	}

	private IEnumerator ClockTick()
	{
		do
		{
			MatchTime--;
			Messenger.Broadcast<float>(EVENT_TICK, MatchTime);
			yield return new WaitForSeconds(1f);
		} while (MatchTime > 0);

		Messenger.Broadcast<MatchResult>(EVENT_GAME_OVER, GetResult());
	}

	MatchResult GetResult()
	{
		if (Player1Blocks > Player2Blocks)
			return MatchResult.PLAYER1_WINS;

		if (Player2Blocks > Player1Blocks)
			return MatchResult.PLAYER2_WINS;

		return MatchResult.TIE;
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

public enum MatchResult
{
	PLAYER1_WINS,
	PLAYER2_WINS,
	TIE
}