using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Transform capsule;
    protected Interactable currentSelection;
    protected bool selected;

    private void Start()
    {
        capsule = transform.GetChild(0);
    }

    public virtual void Interact()  // Ment to be overwritten 
    {
        //Debug.Log("Interacting with " + transform.name);
    }

    public virtual void SetHighlighted(Interactable selection)  // Ment to be overwritten 
    {
        //Debug.Log("Highlighting " + transform.name);
        currentSelection = selection;
    }
}
