using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    float radius;
    [SerializeField]
    Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    private void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (!hasInteracted && distance <= radius)
            {
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransfrom)
    {
        isFocus = true;
        hasInteracted = false;
        player = playerTransfrom;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
