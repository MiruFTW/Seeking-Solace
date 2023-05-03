using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Character character;
    public DamageDealer damage;

    public Message gameMessage;


    bool hasUsed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasUsed == false)
            {
                
                GameObject playerObj = GameObject.Find("PlayerObj");
                character = playerObj.GetComponent<Character>();

                GameObject sword = GameObject.Find("sword_rare");
                damage = sword.GetComponentInChildren<DamageDealer>();

                gameMessage = GetComponent<Message>();

                int rand = Random.Range(0, 4);

                switch (rand)
                {
                case 0:
                character.playerSpeed = character.playerSpeed + 2f;
                gameMessage.ShowMessage("Speed Up!");
                Debug.Log("Case 0");
                Debug.Log("Speed Up\n Current Speed: " + character.playerSpeed);
                break;

                case 1:
                damage.weaponDamage = damage.weaponDamage + 3;
                gameMessage.ShowMessage("Damage Up!");;
                Debug.Log("Case 1");
                Debug.Log("Damge Up\n Current Damage: " + damage.weaponDamage);
                break;

                case 2:
                damage.weaponLength = damage.weaponLength + 0.4f;
                gameMessage.ShowMessage("Range Up!");;
                Debug.Log("Case 2");
                Debug.Log("Range Up\n Current Range: " + damage.weaponLength);
                break;

                case 3:
                damage.weaponDamage = damage.weaponDamage + 3;
                character.playerSpeed = character.playerSpeed + 2f;
                damage.weaponLength = damage.weaponLength + 0.4f;
                gameMessage.ShowMessage("All Stats Up!");
                Debug.Log("Case 3");
                Debug.Log("Speed Up\n Current Speed: " + character.playerSpeed);
                Debug.Log("Damge Up\n Current Damage: " + damage.weaponDamage);
                Debug.Log("Range Up\n Current Range: " + damage.weaponLength);
                break;
                }


                hasUsed = true;

                StartCoroutine(wait());
            }
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
        gameMessage.HideMessage();
    }
}
