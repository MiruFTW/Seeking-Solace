using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float attackDamage = 10f;
    public float attackRange = 2f;
    public LayerMask attackLayer;

    public GameInteractable focus;

    public GameObject weaponInHand;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized; 
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = 0f; // Set the y value to 0 to prevent vertical movement
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        // If we press left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("M1 Clicked");
            
        }
    }

    void SetFocus(GameInteractable newFocus)
    {

    }

    void RemoveFocus(GameInteractable newFocus)
    {

    }

    public void StartDealDamage()
    {
        weaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        weaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            Debug.Log("Player");
        }
    }
}
