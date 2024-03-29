using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    public AudioSource audioSource;

 
    [SerializeField] float weaponLength;
    public int weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            RaycastHit hit;
 
            int layerMask = 1 << 8;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (hit.transform)
                    {
                        Character enemy = hit.transform.GetComponent<Character>();
                        Debug.Log("Sword deals " + weaponDamage + " damage to the target.");
                        enemy.TakeDamage(weaponDamage);
                        audioSource.Play();
                        hasDealtDamage = true;
                    }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}