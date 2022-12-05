using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    #region Plataforma
    [SerializeField]
    private Transform[] target;
    int curPos = 0;
    int nextPos = 1;
    public float speed = 6.0f;
    bool moveNext = true;
    public float timeToNext = 2.0f;
    #endregion

    private void FixedUpdate()
    {
        if (moveNext)
        {
            transform.position = Vector3.MoveTowards(transform.position, target[nextPos].position, speed * Time.deltaTime);
        }
        
        if(Vector3.Distance(transform.position, target[nextPos].position) <= 0)
        {
            StartCoroutine(TimeMove());
            curPos = nextPos;
            nextPos++;

            if(nextPos > target.Length - 1)
            {
                nextPos = 0;
            }
        }
    }

    IEnumerator TimeMove()
    {
        moveNext = false;
        yield return new WaitForSeconds(timeToNext);
        moveNext = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PadrePlayer"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PadrePlayer"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
