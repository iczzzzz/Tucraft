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
	
	void Start() {
        heartAnim = gameObject.GetComponent<Animator>();
	}
	
	public void SetFull() {
		image.sprite = fullHeart;
        heartAnim.SetTrigger("move");
	}
	
	public void SetHalf() {
		image.sprite = halfHeart;
        heartAnim.SetTrigger("move");
	}
	
	public void SetVoid() {
		image.sprite = voidHeart;
        heartAnim.SetTrigger("move");
	}
}
