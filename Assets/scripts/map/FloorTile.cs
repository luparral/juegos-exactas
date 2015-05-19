using UnityEngine;
using System.Collections;

public class FloorTile : MonoBehaviour
{
	static public string EVENT_PLAYER_LOST_TILE = "PlayerLostTile";
	static public string EVENT_PLAYER_GAINED_TILE = "PlayerGainedTile";

	public enum Owner
	{
		NOBODY,
		PLAYER1,
		PLAYER2
	}

	public Owner owner = Owner.NOBODY;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Player collidingPlayer = other.GetComponent<Player>();
			Debug.Log(collidingPlayer, gameObject);
			// Si me pisa mi dueño, me voy
			if (owner == Owner.PLAYER1 && collidingPlayer.Number == 1 || owner == Owner.PLAYER2 && collidingPlayer.Number == 2)
				return;

			Color playerColor = collidingPlayer.PlayerColor;

			GetComponent<SpriteRenderer>().color = playerColor;

			// Si el tile cambió de dueño
			if (owner == Owner.PLAYER1 && collidingPlayer.Number != 1 || owner == Owner.PLAYER2 && collidingPlayer.Number != 2)
				Messenger.Broadcast<Owner>(EVENT_PLAYER_LOST_TILE, owner);
			
			owner = collidingPlayer.Number == 1 ? Owner.PLAYER1 : Owner.PLAYER2;

			// Sumar tile al player que acaba de pisarlo
			Messenger.Broadcast<Owner>(EVENT_PLAYER_GAINED_TILE, owner);
		}
	}
}
