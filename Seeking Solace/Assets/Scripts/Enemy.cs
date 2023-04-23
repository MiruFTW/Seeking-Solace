using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 15;

    GameObject player;
    [HideInInspector]
    public Animator animator;

    Room room;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        room = transform.parent.GetComponent<Room>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;


        if (health <= 0)
        {
            Debug.Log("Enemy died");
            Die();
        }
    }


    void Die()
    {
        room.enemiesLeft--;
        Destroy(this.gameObject);
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
}
