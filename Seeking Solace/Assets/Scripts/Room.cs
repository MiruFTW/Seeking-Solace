using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int numEnemies;

    public int enemiesLeft;

    public bool roomCompleted = false;


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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
    }

}

