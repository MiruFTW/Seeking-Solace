using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : GameInteractable
{

    PlayerManager playerManager;
    CharacterStats myStats;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        Combat playerCombat = playerManager.player.GetComponent<Combat>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
