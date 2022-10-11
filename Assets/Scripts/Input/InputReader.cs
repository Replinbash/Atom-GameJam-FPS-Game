using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : DescriptionBaseSO
{
	// Assign delegate{} to events to initialise them with an empty delegate
	// so we can skip the null check when we use them

	// Gameplay
	public event UnityAction JumpEvent = delegate { };
	public event UnityAction JumpCanceledEvent = delegate { };
	public event UnityAction AttackEvent = delegate { };
	public event UnityAction AttackCanceledEvent = delegate { };
	public event UnityAction InteractEvent = delegate { }; // Used to talk, pickup objects, interact with tools like the cooking cauldron
	public event UnityAction InventoryActionButtonEvent = delegate { };
	public event UnityAction SaveActionButtonEvent = delegate { };
	public event UnityAction ResetActionButtonEvent = delegate { };
	public event UnityAction<Vector2> MoveEvent = delegate { };
	public event UnityAction<Vector2, bool> CameraMoveEvent = delegate { };
	public event UnityAction EnableMouseControlCameraEvent = delegate { };
	public event UnityAction DisableMouseControlCameraEvent = delegate { };
	public event UnityAction StartedRunning = delegate { };
	public event UnityAction StoppedRunning = delegate { };

	public void OnAttack(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				AttackEvent.Invoke();
				break;
			case InputActionPhase.Canceled:
				AttackCanceledEvent.Invoke();
				break;
		}
	}

	public void OnInventoryActionButton(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			InventoryActionButtonEvent.Invoke();
	}

	public void OnSaveActionButton(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			SaveActionButtonEvent.Invoke();
	}

	public void OnResetActionButton(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			ResetActionButtonEvent.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		//if ((context.phase == InputActionPhase.Performed)
		//	&& (_gameStateManager.CurrentGameState == GameState.Gameplay)) // Interaction is only possible when in gameplay GameState
		//	InteractEvent.Invoke();
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			JumpEvent.Invoke();

		if (context.phase == InputActionPhase.Canceled)
			JumpCanceledEvent.Invoke();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		MoveEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnRun(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				StartedRunning.Invoke();
				break;
			case InputActionPhase.Canceled:
				StoppedRunning.Invoke();
				break;
		}
	}

	public void OnRotateCamera(InputAction.CallbackContext context)
	{
		CameraMoveEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
	}

	public void OnMouseControlCamera(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			EnableMouseControlCameraEvent.Invoke();

		if (context.phase == InputActionPhase.Canceled)
			DisableMouseControlCameraEvent.Invoke();
	}

	private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

}
