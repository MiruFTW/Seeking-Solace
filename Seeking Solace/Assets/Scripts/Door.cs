using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool roomCompleted = false;

    public Door connectedDoor = null;

    public bool connectedDoors = false;

    public Rigidbody doorRigidbody;
    public BoxCollider doorBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (connectedDoors == false)
        {
            doorBoxCollider.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            Debug.Log("Door is connected");
            connectedDoor = other.GetComponent<Door>(); // Set the connected door reference
            connectedDoors = true;
        }

        if (other.CompareTag("Player") && roomCompleted == false && connectedDoors == true)
        {
            Debug.Log("Player went through door");

            // Destroy the door and the connected door
            Destroy(this.gameObject);
            Destroy(connectedDoor.gameObject);
            connectedDoors = false;
            doorRigidbody.isKinematic = false;
        }
        else if (other.CompareTag("Player") && roomCompleted == true && connectedDoors == true)
        {
            Debug.Log("Enemies spawned");

            var enemyScript = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;

            enemyScript.spawnEnemies();
        }
    }
}
