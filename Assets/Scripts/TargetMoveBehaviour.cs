using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveBehaviour : MonoBehaviour
{
    public Transform[] places;
    Transform nextTarget;
    public float speed, waitTime;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        StartCoroutine(LinearMove());
    }

    IEnumerator LinearMove()
    {
        int ii = i % places.Length;
        nextTarget = places[ii];
        //transform.position = Vector2.MoveTowards(transform.position, nextTarget.position, speed * Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, nextTarget.position, speed * Time.deltaTime);
        //yield return new WaitUntil(() => VectorAppox(transform.position, nextTarget.position));
        yield return new WaitUntil(() => transform.position == nextTarget.position);
        i++;
        yield return new WaitForSeconds(waitTime);
    }

    bool VectorAppox(Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
