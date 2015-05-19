using UnityEngine;
using System.Collections;

public class PlayerState
{

	public bool IsJumping {	get; set; }
	public bool IsMovingUp { get; set; }
	public bool IsMovingDown { get; set; }
	public bool IsMovingLeft { get; set; }
	public bool IsMovingRight { get; set; }

	public PlayerState()
	{
		IsJumping = false;
	}

	public void Reset()
	{
		IsJumping = IsMovingUp = IsMovingDown = IsMovingLeft = IsMovingRight = false;
	}

	public override string ToString()
	{
		return string.Format("u: {0}, d: {1}, l: {2}, r: {3} | j: {4}",
			IsMovingUp,
			IsMovingDown,
			IsMovingLeft,
			IsMovingRight,
			IsJumping
		);
	}
}
