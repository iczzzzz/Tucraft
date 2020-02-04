using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
	public Sprite fullHeart;
	public Sprite halfHeart;
	public Sprite voidHeart;
	
	public Image image;
	
    Animator heartAnim;
	
	void Awake() { 
        heartAnim = gameObject.GetComponent<Animator>();
	}
	
	public void SetFull(bool animation) { 
        image.sprite = fullHeart;
        if (animation) heartAnim.SetTrigger("move");
	}
	
	public void SetHalf(bool animation) {
        image.sprite = halfHeart;
        if (animation) heartAnim.SetTrigger("move");
    }
	
	public void SetVoid(bool animation) {
        image.sprite = voidHeart;
        if (animation) heartAnim.SetTrigger("move");
    }
}
