using UnityEngine;
 
public class StandingState: State
{  
    float gravityValue;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    float playerSpeed;

    bool Attack;

 
    Vector3 cVelocity;
 
    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        input = Vector2.zero;
        
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        Attack = false;
 
        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = true;
        gravityValue = character.gravityValue;    
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y).normalized;
 
        //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        //velocity = character.transform.position + velocity.z * character.transform.position;
        velocity.y = 0f;
        Attack = false;
        if (attackAction.triggered)
        {
            Attack = true;
        }
     
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Attack == true)
        {
            character.animator.SetTrigger("Attack");
        }

        character.animator.SetFloat("Speed", input.magnitude);

    
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
 
        gravityVelocity.y += 1 * Time.deltaTime;

        grounded = true;
 
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
       
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity,ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
        //cam.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
  
        if (velocity.sqrMagnitude>0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);
        }
        
    }
 
    public override void Exit()
    {
        base.Exit();
 
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);
 
        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
 
}