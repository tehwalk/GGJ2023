using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveBehaviour : MonoBehaviour
{
    public Transform[] places;
    private Transform nextTarget;
    public float speed, waitTime;
    int i = 0;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(LinearMove());
        if (isMoving == false)
        {
            nextTarget = places[i];
            isMoving = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, nextTarget.position, speed * Time.deltaTime);
            if (VectorAppox(transform.position, nextTarget.position))
            {
                if (i >= places.Length - 1) i = 0;
                else i++;
                isMoving = false;
            }
        }
    }

    IEnumerator LinearMove()
    {
        // int ii = i % places.Length;
        nextTarget = places[i];
        //transform.position = Vector2.MoveTowards(transform.position, nextTarget.position, speed * Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, nextTarget.position, speed * Time.deltaTime);
        //yield return new WaitUntil(() => VectorAppox(transform.position, nextTarget.position));
        yield return new WaitUntil(() => VectorAppox(transform.position, nextTarget.position));
        Debug.Log("i am here");
        if (i >= places.Length - 1) i = 0;
        else i++;
        yield return new WaitForSeconds(waitTime);
    }



    bool VectorAppox(Vector2 a, Vector2 b)
    {
        //return Mathf.Approximately(Mathf.Round(a.x), Mathf.Round(b.x)) && Mathf.Approximately(Mathf.Round(a.y), Mathf.Round(b.y));
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
