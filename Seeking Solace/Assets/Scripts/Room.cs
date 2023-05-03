using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int numEnemies;

    public int enemiesLeft;

    public Door connectedDoor = null;

    public bool connectedDoors = false;

    public bool roomCompleted = false;

    public Rigidbody doorRigidbody;
    public BoxCollider doorBoxCollider;

    public List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        findWithTag("Enemy");
        enemiesLeft = enemies.Count;
        Debug.Log("Number of Enemies: " + enemiesLeft);
    }

    private void Update()
    {

        if (roomCompleted == false)
        {
            if (enemiesLeft <= 0)
            {
                Debug.Log("All enemies in the room are dead");
                roomCompleted = true;
                //disableWithTag("Door");

            }
        }
    }


    public void findWithTag(string _tag)
    {
        enemies.Clear();
        Transform parent = transform;
        getNumEnemies(parent, _tag);
    }

    public void getNumEnemies(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                enemies.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                getNumEnemies(child, _tag);
            }
        }
    }

    public void disableWithTag(string _tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(_tag);
        List<GameObject> doorsToDisable = new List<GameObject>();

        // Find doors that are touching another door
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<Collider>().bounds.Intersects(transform.GetComponent<Collider>().bounds))
            {
                doorsToDisable.Add(obj);
            }
        }

        foreach (GameObject door in doorsToDisable)
        {
            door.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
    }

}

