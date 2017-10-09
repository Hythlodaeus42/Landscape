using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion target = Quaternion.Euler(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);

        //transform.rotation = Camera.main.transform.rotation;
    }
}
