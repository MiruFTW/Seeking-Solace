using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Character : MonoBehaviour
{
    public float health = 6f;
    public AudioSource audioSourcePlay;
    public AudioSource audioSourceStop;
    public GameObject gameOverScreen;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Controls")]
    public float playerSpeed = 10f;
    public float gravityMultiplier = 2;
 
    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;

 
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

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }
 
    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Character.cs");


        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        audioSourceStop.Stop();
        audioSourcePlay.Play();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}