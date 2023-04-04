using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInteractable : MonoBehaviour
{

    public float radius = 3f;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocus == true)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Debug.Log("Interact");
            }
        }
    }
}
