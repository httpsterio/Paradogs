using UnityEngine;
using System.Collections;

public class VariableMoveController : MonoBehaviour {

    public int moveAxis = 0;
    public float moveDistance = 5.0f;
    public float normalMoveSpeed = 0.5f;
    public float fastMoveSpeed = 1.0f;
    public float normalSpeedTime = 2.0f;
    public float FastSpeedTime = 1.0f;
    public bool moveChildren = false;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float _t, sizeTime;
    private bool OneWay = false;
    private Transform player;
    private Vector3 axisVector;
    private bool goFast = false;
    private float timer = 0.0f;

    void Start()
    {
        startPosition = transform.position;

        axisVector = new Vector3((float)Mathf.Cos(moveAxis * Mathf.Deg2Rad), (float)Mathf.Sin(moveAxis * Mathf.Deg2Rad), 0);
        endPosition = startPosition + (moveDistance * axisVector);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player") && collision.gameObject.transform.position.y > transform.position.y)
        {
            player = collision.transform;
            //player.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //player.parent = null;
            player = null;
        }
    }

    void Update()
    {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                goFast = !goFast;

                if(!goFast)
                    timer = normalSpeedTime;
                else
                    timer = FastSpeedTime;
            }
            

            // Position changing
            if (OneWay) 
            {
                if(!goFast)
                    _t += Time.deltaTime * normalMoveSpeed;
                else
                    _t += Time.deltaTime * fastMoveSpeed;
                
            }            
            else 
            {
                if(!goFast)
                    _t -= Time.deltaTime * normalMoveSpeed;
                else
                    _t -= Time.deltaTime * fastMoveSpeed;
            }

            _t = Mathf.Clamp(_t, 0.0f, 1.0f); //avoids platforms getting stuck

            Vector3 oldPos = transform.position;

            transform.position = Vector2.Lerp(startPosition, endPosition, _t);

            if (player != null)
            {
                player.position = new Vector3(
                                player.position.x + (transform.position.x - oldPos.x),
                                player.position.y + (transform.position.y - oldPos.y),
                                player.position.z);
            }

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

            if (GameManager.instance.PlayerDeath)
                player = null;

            if (transform.position == endPosition || transform.position == startPosition)
                OneWay = !OneWay;

            if (moveChildren)
            {
                foreach (Transform child in transform)
                {
                    child.position = new Vector3(
                                child.position.x + (transform.position.x - oldPos.x),
                                child.position.y,
                                child.position.z);
                }
            }
    }
}
