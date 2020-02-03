using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	
	public float mouseSensitivity = 100f;
	
	public Transform playerBody;
	public Camera cameraView;
	
	float xRotation = 0f;
    Animator cameraChange;
	bool third;
	
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraChange = cameraView.GetComponent<Animator>();
		SetThird();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change Camera")){
			ChangeCamera();
		}
		
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		
		xRotation -= mouseY;
		if (third) {
			xRotation = Mathf.Clamp(xRotation, 0f, 90f);
		}
		else {
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		}
		
		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
		playerBody.Rotate(Vector3.up * mouseX); 
    }
	
	void ChangeCamera() {
		if (third) {
			SetFirst();
		}
		else {
			SetThird();
		}
	}
	
	void SetThird() {
		third = true;
		cameraChange.SetBool("third", true);
	}
	
	void SetFirst() {
		third = false;
		cameraChange.SetBool("third", false);
	}
}
