using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public PlayerMovement playerMovement;
    [SerializeField] public PlayerAnimationData AnimationData { get; private set; } = new PlayerAnimationData();

    public Animator PlayerAnimator { get; private set; }

    protected PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Init();

        controller = GetComponent<CharacterController>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.Initialize(controller);

        stateMachine = new PlayerStateMachine(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        stateMachine.ChangeState(stateMachine.playerIdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();

        
    }
}
