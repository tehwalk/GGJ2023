using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveBehaviour : MonoBehaviour
{
    public Transform[] places;
    Transform nextTarget;
    public float speed;
    int i=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if(isMoving==false)
        {
            nextTarget = places[Random.Range(0, places.Length)];
            isMoving = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, nextTarget.position, speed*Time.deltaTime);
            if(Vector2.Equals(transform.position, nextTarget.position))
            {
                isMoving = false;
            }
        }*/
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        int ii = i%places.Length;
        nextTarget = places[ii];
        transform.position = Vector2.MoveTowards(transform.position, nextTarget.position, speed * Time.deltaTime);
        yield return new WaitUntil(() => transform.position == nextTarget.position);
        i++;
    }
}
