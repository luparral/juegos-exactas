using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	#region parameters
	public float Speed = 5f;
	#endregion

	#region character control
	private Rigidbody2D _rigidbody;

	private PlayerState _state = new PlayerState();
	public PlayerState State { get { return _state; } }
	#endregion

	#region cabeceadas que hay que hacer bien
	private Animator _animator;		  // Cambiar por un script que se encargue de manejar las animaciones
	#endregion

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	   
		
	}

	void Update()
	{
		#region animator variables update
		_animator.SetBool("isJumping", State.IsJumping);
		#endregion
	}

	#region movimiento
	public void SetSpeed(Vector2 newSpeed)
	{
		#region update controller state
		_state.IsMovingUp = newSpeed.y > 0;
		_state.IsMovingDown = newSpeed.y < 0;
		_state.IsMovingLeft = newSpeed.x < 0;
		_state.IsMovingRight = newSpeed.x > 0;
		#endregion

		_rigidbody.velocity = newSpeed;
	}

	public void StartJump()
	{
		_state.IsJumping = true;
		gameObject.layer = LayerMask.NameToLayer("JumpingPlayers");
	}

	public void EndJump()
	{
		_state.IsJumping = false;
		gameObject.layer = LayerMask.NameToLayer("Players");
	}
	#endregion
	
}
