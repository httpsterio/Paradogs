using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    private float timeToLive = 4.0f;
    private bool enable = true;
    private float timer;

    // Use this for initialization
    void Start()
    {
        //Destroy(gameObject, 1);
        timer = timeToLive;
    }

    void Update()
    {
        if (enable)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                enable = false;
                gameObject.Recycle();
            }
        }
    }

    public void AddForces(Vector2 dir)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.rigidbody2D.AddForce(dir * 3, ForceMode2D.Impulse);
        }
    }

    void onEnable()
    {
        enable = true;
        timer = timeToLive;
    }

}
