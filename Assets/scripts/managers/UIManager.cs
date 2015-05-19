using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public string TEXT_PLAYER1_WINS = "Player 1 Wins!";
	static public string TEXT_PLAYER2_WINS = "Player 2 Wins!";
	static public string TEXT_TIE = "Tie!";

	private Text _timer;
	private Text _resultText;

	void Awake()
	{
		_timer = GameObject.Find("Timer").GetComponent<Text>();
		_resultText = GameObject.Find("ResultText").GetComponent<Text>();
	}

	// Use this for initialization
	void Start()
	{
		Messenger.AddListener<float>(GameManager.EVENT_TICK, OnTimeChanged);
		Messenger.AddListener<MatchResult>(GameManager.EVENT_GAME_OVER, OnGameOver);
	}

	void OnTimeChanged(float currentTime)
	{
		int minutes = (int)currentTime / 60;
		int seconds = (int)currentTime % 60;

		string minutesString = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
		string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

		_timer.text = minutesString + ":" + secondsString;
	}

	void OnGameOver(MatchResult result)
	{
		switch (result)
		{
			case MatchResult.PLAYER1_WINS:
				_resultText.text = TEXT_PLAYER1_WINS;
				break;
			case MatchResult.PLAYER2_WINS:
				_resultText.text = TEXT_PLAYER2_WINS;
				break;
			case MatchResult.TIE:
				_resultText.text = TEXT_TIE;
				break;
		}

		_resultText.enabled = true;
	}
}
