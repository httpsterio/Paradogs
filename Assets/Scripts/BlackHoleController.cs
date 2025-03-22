using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour {

    public float radius = 5;
    public float power = 8000f;

	// Use this for initialization
	void Start () {
        AudioManager.PlaySound("Sounds/blackhole", gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D col in colliders)
        {

            if (!col)
                continue;

            if (col.rigidbody2D)
            {
                Vector2 f = transform.position - col.rigidbody2D.transform.position;
                //float dist = Mathf.Sqrt((transform.position.x - col.rigidbody2D.transform.position.x) + (transform.position.y - col.rigidbody2D.transform.position.y));
                f.Normalize();
                float dist = Vector2.Distance(transform.position, col.rigidbody2D.transform.position);
                //Debug.Log(1/dist);

                dist = 1 / dist;

                //TODO calculate the force to be relative to the distance.
                //f.Set(f.x * power * dist*dist * Time.fixedDeltaTime, f.y * power * dist*dist * Time.fixedDeltaTime);
                //col.rigidbody2D.AddForce(f);

                if (dist > 1f)
                {
                    dist = 1f;
                }

                col.rigidbody2D.AddForce(power * (dist) * (dist) * Time.fixedDeltaTime * f);
            }

        }

	}
}
