using UnityEngine;
using System.Collections;

public class MoveOneWay : MonoBehaviour
{
    // Is this script needed?
    public int moveAxis = 0;
    public float moveSpeed = 1.5f;
    public float moveDistance = 5.0f;
    public float sizeSmallest = 0.5f;
    public float sizeSpeed = 0.1f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector3 sizeStart;
    private Vector3 sizeEnd;
    private bool OneWay = true;
    private float _t, sizeTime;
    private bool OneSize = true;


    void Start()
    {
        startPosition = transform.position;
        sizeStart = transform.localScale;

        Vector2 axisVector = new Vector2((float)Mathf.Cos(moveAxis * Mathf.Deg2Rad), (float)Mathf.Sin(moveAxis * Mathf.Deg2Rad));
        endPosition = startPosition + (moveDistance * axisVector);

        sizeEnd = transform.localScale * sizeSmallest;

        sizeEnd.y = sizeStart.y;
        sizeEnd.z = sizeStart.z;

        transform.parent = transform;
    }

    #region collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            collision.transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    #endregion

    void Update()
    {
        _t = Time.deltaTime * moveSpeed;

        transform.position = new Vector3(transform.position.x + _t,transform.position.y,transform.position.z);//Vector2.Lerp(startPosition, endPosition, _t);

        _t = Mathf.Clamp(_t, 0.0f, 1.0f); //avoids platforms getting stuck

        if (OneSize)
            sizeTime += Time.deltaTime * sizeSpeed;
        else
            sizeTime -= Time.deltaTime * sizeSpeed;
        transform.localScale = Vector3.Lerp(sizeStart, sizeEnd, sizeTime);

        if ((Vector2)transform.position == endPosition || (Vector2)transform.position == startPosition)
            OneWay = !OneWay;

        if (transform.localScale == sizeEnd || transform.localScale == sizeStart)
            OneSize = !OneSize;
    }
}
