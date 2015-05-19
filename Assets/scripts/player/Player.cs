using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public Color PlayerColor;
	public int Number = 1;

	private PlayerController _controller;

	private bool _shouldUpdate = true;

	private string _inputPrefix;
	public string InputPrefix { get { return _inputPrefix; } }

	private GameManager _gameManager; // Cambiar por un sistema de eventos

	void Awake()
	{
		_controller = GetComponent<PlayerController>();
		_inputPrefix = "P" + Number + "_";
	}

	void Start()
	{
		Messenger.AddListener<MatchResult>(GameManager.EVENT_GAME_OVER, OnGameFinished);
	}

	// Update is called once per frame
	void Update()
	{
		if (!_shouldUpdate)
		    return;

		HandleInput();

		transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sortingOrder = (int)(Camera.main.farClipPlane - (100 * transform.position.y));
	}

	void HandleInput()
	{
		Vector2 speed = new Vector2(Input.GetAxis(_inputPrefix + "Horizontal") * _controller.Speed, Input.GetAxis(_inputPrefix + "Vertical") * _controller.Speed);
		_controller.SetSpeed(speed);

		if (!_controller.State.IsJumping && Input.GetButton(_inputPrefix + "Jump"))
			_controller.StartJump();
	}

	void OnGameFinished(MatchResult result)
	{
		_shouldUpdate = false;
		_controller.SetSpeed(new Vector2(0, 0));
	}
}
