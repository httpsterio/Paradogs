using UnityEngine;
using System.Collections;

public class VariableRotationPlatform : MonoBehaviour {
    public float rotationSpeed = 100.0f;
    public bool clockwise = true;
    public bool canKill = true;


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (canKill && coll.gameObject.tag == "Player")
            GameManager.instance.playerKilled();
    }
	
	void Update () {
        if (clockwise)
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);         
        else
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}
}
