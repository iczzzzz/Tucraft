using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
	#region Singleton
	
	public static HeartsManager instance;
	
	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of HeartsManager found!");
			return;
		}
		
		instance = this;
	}
	
	#endregion
		
	public Heart[] hearts;
	
    // Start is called before the first frame update
    void Start()
    {
        hearts = new Heart[10];
        hearts = gameObject.GetComponentsInChildren<Heart>();
    }
	
	public void SetNHearts(float n) {
		for (int i=0; i<hearts.Length; i++) {
			if (n == 0.5f) {
				hearts[i].SetHalf();
				n -= 0.5f;
			}
			else if (n > 0) {
				hearts[i].SetFull();
				n -= 1f;
			}
			else {
				hearts[i].SetVoid();
			}
		}
		
	}
	
	void SetAllFull() {
		for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetFull();
		}
	}
	
	void SetAllHalf() {
		for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetHalf();
		}
	}
	
	void SetAllVoid() {
		for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetVoid();
		}
	}
	
}
