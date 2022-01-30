using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    [Header("Posizioni")]
    public Vector2 posA, posB;

    [Header("Parameters")]
    public float speed;
    public bool isMoving;

    Vector2 nextPos;

    void Start()
    {
        nextPos = posB;
    }

    private void Update()
    {
        if(isMoving)
            OnMoveToPosition();
    }

    private void OnMoveToPosition()
    {
        if (transform.position.x == posA.x && transform.position.y == posA.y)
        {
            isMoving = false;
            nextPos = posB;
        }
        else if (transform.position.x == posB.x && transform.position.y == posB.y)
        {
            isMoving = false;
            nextPos = posA;
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posA, posB);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isMoving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isMoving = false;
            collision.collider.transform.SetParent(null);
        }
    }

}
