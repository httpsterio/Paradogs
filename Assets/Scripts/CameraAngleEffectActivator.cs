using UnityEngine;
using System.Collections;

public class CameraAngleEffectActivator : MonoBehaviour {

    public float fov = 80;
    public float xAxis = 0;
    public float yAxis = 0;
    public float timeScale = 4f;

    private CameraAngleEffectController contr;

	// Use this for initialization
	void Start () {
        this.contr = GameObject.Find("CameraEffects").GetComponent<CameraAngleEffectController>();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            contr.fov = fov;
            contr.rotationTimeScale = timeScale;
            contr.target = Quaternion.Euler(xAxis, yAxis, 0);
        }
    }

}
