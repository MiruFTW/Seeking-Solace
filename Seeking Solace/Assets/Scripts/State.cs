using UnityEngine;
using UnityEngine.InputSystem;
 
public class State
{
    public Camera cam;
    public Character character;
    
    public StateMachine stateMachine;
 
    protected Vector3 velocity;
    protected Vector3 gravityVelocity;
    protected Vector2 input;
 
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction attackAction;
 
    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
 
        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        attackAction = character.playerInput.actions["Attack"];
 
    }
 
    public virtual void Enter()
    {
        Debug.Log("enter state: "+this.ToString());
    }
 
    public virtual void HandleInput()
    {

    }
 
    public virtual void LogicUpdate()
    {

    }
 
    public virtual void PhysicsUpdate()
    {

    }
 
    public virtual void Exit()
    {
        
    }
}