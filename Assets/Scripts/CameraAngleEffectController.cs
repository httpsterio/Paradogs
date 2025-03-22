using UnityEngine;
using System.Collections;

public class CameraAngleEffectController : MonoBehaviour {

    public Camera mainCamera;

    public Quaternion target;
    public float fov = 80;
    public float rotationTimeScale = 4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, target, Time.deltaTime * rotationTimeScale);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, fov, Time.deltaTime * 4f);
	}
}
