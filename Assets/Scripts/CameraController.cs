using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    public float dampTime = 0.15f;
    public float yOffset = 0;

    private Vector3 velocity = Vector3.zero;
    private Transform target;

	void Start() 
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
        	target = player.transform;
		}
	}

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            destination.y += yOffset;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}