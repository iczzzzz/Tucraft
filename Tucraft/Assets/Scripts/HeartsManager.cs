using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    public static HeartsManager instance;

    public Heart[] hearts;

	void Awake() {
        /* Singleton */
		if (instance != null) {
			Debug.LogWarning("More than one instance of HeartsManager found!");
			return;
		}
		instance = this;

        hearts = new Heart[10];
        hearts = gameObject.GetComponentsInChildren<Heart>();
    }
	
	public void SetNHearts(float n, bool animation)
    {
        for (int i=0; i<hearts.Length; i++) {
			if (n == 0.5f) {
				hearts[i].SetHalf(animation);
				n -= 0.5f;
			}
			else if (n > 0) {
				hearts[i].SetFull(animation);
				n -= 1f;
			}
			else {
				hearts[i].SetVoid(animation);
			}
		}
	}
	
	void SetAllFull(bool animation) {
		for (int i=0; i<hearts.Length; i++) {
            hearts[i].SetFull(animation);
		}
	}
	
	void SetAllHalf(bool animation) {
        for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetHalf(animation);
		}
	}
	
	void SetAllVoid(bool animation) {
        for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetVoid(animation);
		}
	}
	
}
