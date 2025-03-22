using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

    public int moveAxis = 0;
    public float moveSpeed = 0.5f;
    public float moveDistance = 5.0f;
    public bool moveOneWay = false;
    public float sizeSmallest = 1f;
    public float sizeSpeed = 0.1f;
    public bool moveChildren = false;

    private Vector3 startPosition, endPosition, sizeStart, sizeEnd, axisVector;
    private bool OneWay = true, OneSize = true;
    private float _t, sizeTime;
    private GameObject player;


    void Start()
    {
        startPosition = transform.position;
        sizeStart = transform.localScale;

        axisVector = new Vector3((float)Mathf.Cos(moveAxis * Mathf.Deg2Rad), (float)Mathf.Sin(moveAxis * Mathf.Deg2Rad), 0);
        endPosition = startPosition + (moveDistance * axisVector);

        sizeEnd = transform.localScale * sizeSmallest;

        sizeEnd.y = sizeStart.y;
        sizeEnd.z = sizeStart.z;

        transform.parent = transform;
    }

    #region collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player") && collision.gameObject.transform.position.y > transform.position.y)
            player = collision.gameObject;   
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
            player = null;
    }
    #endregion

    void Update()
    {
        // Is this needed?
        if (moveOneWay)
        {
            _t = Time.deltaTime * moveSpeed;
            transform.position = new Vector3(transform.position.x + _t, transform.position.y, transform.position.z);      
        }
        else
        {
            #region platform position
            // Position changing
            if (OneWay)
                _t += Time.deltaTime * moveSpeed;
            else
                _t -= Time.deltaTime * moveSpeed;

            //avoids platforms getting stuck
            _t = Mathf.Clamp(_t, 0.0f, 1.0f); 

            // Save old postion for movement calculation
            Vector3 oldPos = transform.position;

            // Set new position
            transform.position = Vector2.Lerp(startPosition, endPosition, _t);

            // Change direction if platform is on turning point
            if (transform.position == endPosition || transform.position == startPosition)
                OneWay = !OneWay;
            #endregion

            #region player position
            // Remove player from object after death
            if (GameManager.instance.PlayerDeath)
                player = null; 

            // If player is on platform change player location
            if (player != null)
            {
                player.transform.position = new Vector3(
                                player.transform.position.x + (transform.position.x - oldPos.x),
                                player.transform.position.y + (transform.position.y - oldPos.y),
                                player.transform.position.z);
            }
            #endregion

            #region move child objects
            // is this still needed?
            if (moveChildren)
            {
                foreach (Transform child in transform)
                {
                    child.position = new Vector3(
                                child.position.x + (transform.position.x - oldPos.x),
                                child.position.y + (transform.position.y - oldPos.y),
                                child.position.z);
                }
            }
            #endregion
        }

        #region platform size
        // Size changing
        if (OneSize)
            sizeTime += Time.deltaTime * sizeSpeed;
        else
            sizeTime -= Time.deltaTime * sizeSpeed;

        transform.localScale = Vector3.Lerp(sizeStart, sizeEnd, sizeTime);        
        
        if (transform.localScale == sizeEnd || transform.localScale == sizeStart)
            OneSize = !OneSize;
        #endregion
    }
}
