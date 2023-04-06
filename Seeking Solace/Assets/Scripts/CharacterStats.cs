using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public ObjStats armor;
    public int maxHealth = 6;
    public int currentHealth;

    //bool canDealDamage = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        this.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        this.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
        Destroy(this.gameObject);
    }


}