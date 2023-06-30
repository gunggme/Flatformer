using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform Interact;

    [SerializeField] private float distance;
    [SerializeField] private LayerMask mask;

    void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(Interact.position, Vector2.right * gameObject.transform.localScale.x, distance, mask);
        Debug.DrawRay(Interact.position, Vector2.right * gameObject.transform.localScale.x);
        if (rayHit.collider == null)
        {
            return;
        }

        if (rayHit.collider.GetComponent<Interactable>() != null)
        {
            Interactable inter = rayHit.collider.GetComponent<Interactable>();
            if (Input.GetKeyDown(KeyCode.R))
            {
                inter.BaseInteract();
            }
        }
    }
}
