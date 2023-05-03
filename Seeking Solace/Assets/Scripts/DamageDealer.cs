using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;

    List<GameObject> hasDealtDamage;


    public float weaponLength;
    public int weaponDamage;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage == true)
        {
            RaycastHit hit;

            int layerMask = 1 << 9;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {

                    if (!hasDealtDamage.Contains(hit.transform.gameObject))
                    {
                        Enemy enemy = hit.transform.GetComponent<Enemy>();
                        Debug.Log("Sword deals " + weaponDamage + " damage to the target.");
                        enemy.TakeDamage(weaponDamage);
                        hasDealtDamage.Add(hit.transform.gameObject);
                        audioSource.Play();
                    }
                    
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
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
