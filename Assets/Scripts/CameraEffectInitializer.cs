using UnityEngine;
using System.Collections;

public class CameraEffectInitializer : MonoBehaviour {
	
	public string cameraName = "MainCamera";

	void Start () {
		GameObject cameraObject = GameObject.Find (cameraName);
		this.transform.parent = cameraObject.transform;
	}
}
