using UnityEngine;
using System.Collections;

public class BadStuffEmitterController : MonoBehaviour {

    public GameObject badStuffPrefab;
    public float rate = 0.3f;
    public Vector2 startForce = new Vector2(0, 0);
    public bool emitterActivated = true;
    public float timeToLive = 10;

    private float cooldown = 0f;

	// Use this for initialization
	void Start () {
        badStuffPrefab.CreatePool();
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f && emitterActivated)
        {
            GameObject badStuffBlock = badStuffPrefab.Spawn(transform.position, transform.rotation);
            BadStuffController contr = badStuffBlock.GetComponent<BadStuffController>();
            contr.recycleTimer(timeToLive);
            badStuffBlock.rigidbody2D.AddForce(startForce);
            cooldown = rate;
            /*
            GameObject badStuffBlock = (GameObject)Instantiate(badStuffPrefab);
            badStuffBlock.transform.position = transform.position;
            badStuffBlock.rigidbody2D.AddForce(startForce);
            Destroy(badStuffBlock, timeToLive);
            cooldown = rate;
            */
        }
    }
}
