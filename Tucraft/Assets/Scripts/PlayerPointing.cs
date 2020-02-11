using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointing : MonoBehaviour
{
	
    const float INTERACT_RADIUS = 5f;
	
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Interactable interactable;
        if (RaycastWithDistance(out interactable) && interactable != null) {
            if (Input.GetButtonDown("Interact")) {
                interactable.Interact();
            }
            else {
                interactable.SetHighlighted(interactable);
            }

        }

        if (Input.GetButtonDown("Action")) {
            DoAction();
        }
    }
	
	
    bool RaycastWithDistance(out Interactable inter)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (Vector3.Distance(transform.position, hit.point) <= INTERACT_RADIUS)
            {
                inter = hit.collider.GetComponent<Interactable>();
                return true;
            }
        }

        inter = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, INTERACT_RADIUS);
    }

    void DoAction()
    {
        Inventory.instance.UseSelected();
    }
}
