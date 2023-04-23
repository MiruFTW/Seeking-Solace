using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    public float health = 6f;
    [Header("Controls")]
    public float playerSpeed = 10f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f; 
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;
 
    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;
 
    public StateMachine movementSM;
    public StandingState standing;
    public AttackState attacking;
 
    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;
 
 
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        //cameraTransform = Camera.main.transform;
 
        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        attacking = new AttackState(this, movementSM);
        
        
 
        movementSM.Initialize(standing);
 
        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }
 
    private void Update()
    {
        movementSM.currentState.HandleInput();
 
        movementSM.currentState.LogicUpdate();
    }
 
    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;


        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}