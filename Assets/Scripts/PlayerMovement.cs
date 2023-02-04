using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public LayerMask def, ignore;
    //public int layerDefalut = 0;
    public LayerMask sittingLayer, defaultLayer, hitableLayer;
    public int sittingLayerValue, defaultLayerValue, hitableLayerValue;
    public float speed = 3;
    LineRenderer lineRenderer;
    Rigidbody2D rigid2D;
    Vector3 mousePos, target;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos);
        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {
            PickPosition();
        }
        if (isMoving == true)
        {
            rigid2D.position = Vector2.MoveTowards(rigid2D.position, target, speed * Time.deltaTime);
            /*if (transform.position == target)
            {
                isMoving = false;
            }*/
        }
        else
        {
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
        }

    }

    void PickPosition()
    {
        var sit = Physics2D.Linecast(transform.position, mousePos, sittingLayer);
        //var sit = Physics2D.Raycast()
        if (sit.collider != null)
        {
            Debug.Log(sit.point);
            //transform.position = Vector2.MoveTowards(transform.position, sit.point, speed * Time.deltaTime);
            //transform.position = sit.point;
            target = sit.point;
            isMoving = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isMoving = false;
            other.gameObject.layer = defaultLayerValue;
        }
        else if (other.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
            GameManager.Instance.ResetScene();
        }
        else if (other.gameObject.layer == hitableLayerValue)
        {
            Debug.Log("bich");
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            other.gameObject.layer = sittingLayerValue;
        }
    }
}
