using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public LayerMask def, ignore;
    public int layerDefalut = 0, layerIgnore = 3;
    public LayerMask ignoreMe;
    public float speed = 3;
    LineRenderer lineRenderer;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos);
        
        if (Input.GetMouseButtonDown(0))
        {
            PickPosition();
        }

    }

    void PickPosition()
    {
        var sit = Physics2D.Linecast(transform.position, mousePos, ~ignoreMe);
        //var sit = Physics2D.Raycast()
        if (sit.collider != null)
        {
            Debug.Log(sit.point);
            //transform.position = Vector2.MoveTowards(transform.position, sit.point, speed * Time.deltaTime);
            transform.position = sit.point;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.layer = ignoreMe.value;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.layer = layerDefalut;
    }
}
