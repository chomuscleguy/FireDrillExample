using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
        stateMachine.player.playerMovement.CameraRotate(stateMachine.player.transform, stateMachine.MainCameraTransform);
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.player.playerMovement.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.player.PlayerAnimator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.player.PlayerAnimator.SetBool(animationHash, false);
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = GetInput();
        if (input == null)
            return;

        input.PlayerActions.Movement.canceled += OnMovementCanceled;

        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCanceled;

        input.PlayerActions.Jump.started += OnJumpStarted;
        input.PlayerActions.Interact.started += OnInteracted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = GetInput();
        if (input == null)
            return;

        input.PlayerActions.Movement.canceled -= OnMovementCanceled;

        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Run.canceled -= OnRunCanceled;

        input.PlayerActions.Jump.started -= OnJumpStarted;
        input.PlayerActions.Interact.started -= OnInteracted;
    }

    private PlayerInput GetInput()
    {
        return stateMachine?.player?.playerMovement?.Input;
    }


    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        stateMachine.MovementSpeedModifier = 3.0f;
    }
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.MovementSpeedModifier = 1.0f;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        Move();
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnInteracted(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        float interactDistance = 3f;
        Transform playerTransform = stateMachine.player.transform;
        Vector3 origin = playerTransform.position + Vector3.up * 1.5f;
        Vector3 direction = playerTransform.forward;

        if (Physics.Raycast(origin, direction, out hit, interactDistance))
        {
            IInteract interactable = hit.collider.GetComponent<IInteract>();
            if (interactable != null)
            {
                interactable.interact();
            }
        }
    }


    private void Move()
    {
        Vector3 move = stateMachine.player.playerMovement.GetMoveVector();
        Vector3 horizontalMove = new Vector3(move.x, 0, move.z);
        stateMachine.player.PlayerAnimator.SetFloat("Speed", horizontalMove.magnitude * stateMachine.MovementSpeedModifier);
        stateMachine.player.controller.Move(move * Time.deltaTime * stateMachine.MovementSpeedModifier);
    }
}
