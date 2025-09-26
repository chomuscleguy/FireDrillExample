using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player;

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }
    public Transform MainCameraTransform { get; set; }

    public PlayerIdleState playerIdleState { get; }
    public PlayerMoveState playerMoveState { get; }
    public PlayerJumpState playerJumpState { get; }
    public PlayerFallState playerFallState { get; }



    public PlayerStateMachine(Player player)
    {
        this.player = player;

        playerIdleState = new PlayerIdleState(this);
        playerMoveState = new PlayerMoveState(this);
        playerJumpState = new PlayerJumpState(this);
        playerFallState = new PlayerFallState(this);

        MainCameraTransform = Camera.main.transform;
        MovementSpeed = 1;
    }

}
