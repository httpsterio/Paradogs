using UnityEngine;
using System.Collections;

public class BadStuffController : MonoBehaviour {
  
    public GameObject emptyDetectorPrefab;

    private GameObject emptyDetector;
    private float timer = -1;

	// Use this for initialization
	void Start () {
        if (emptyDetectorPrefab != null)
        {
            // Create empty collider to follow badstufftrigger
            //emptyDetector = (GameObject)Instantiate(emptyDetectorPrefab);
            emptyDetectorPrefab.CreatePool(1);
            emptyDetector = emptyDetectorPrefab.Spawn(transform.position, transform.rotation);
        }
	}

    void Update()
    {
        if (emptyDetector != null)
        {
            emptyDetector.transform.position = transform.position;
            emptyDetector.transform.rotation = transform.rotation;
        }

        if(timer != -1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.Recycle();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.instance.playerKilled();
        }
    }

    void onEnable()
    {
        if (emptyDetectorPrefab != null)
            emptyDetector = emptyDetectorPrefab.Spawn(transform.position, transform.rotation);
    }

    void onDisable()
    {
        if (emptyDetectorPrefab != null)
            emptyDetectorPrefab.Recycle();
    }

    public void recycleTimer(float time)
    {
        timer = time;        
    }
}
