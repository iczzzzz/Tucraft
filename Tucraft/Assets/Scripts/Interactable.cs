using UnityEngine;

public class Interactable : MonoBehaviour
{

    public virtual void Interact()  // Ment to be overwritten 
    {
        Debug.Log("Interacting with " + transform.name);
    }
}
