using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerMovement : MonoBehaviour
{
    //public LayerMask def, ignore;
    //public int layerDefalut = 0;
    public LayerMask sittingLayer, defaultLayer, hitableLayer;
    [SerializeField] int sittingLayerValue, defaultLayerValue, hitableLayerValue;
    [SerializeField] float speed = 3;
    [SerializeField] GameObject vfxPrefab;
    [SerializeField] float particleLifeTime;
    LineRenderer lineRenderer;
    Rigidbody2D rigid2D;
    Animator animator;
    Vector3 mousePos, target, previousTargetPos;
    bool isMoving = false, isColliding = false;
    GameObject targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {
            PickPosition();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving == true)
        {

            lineRenderer.enabled = false;
            rigid2D.position = Vector2.MoveTowards(rigid2D.position, targetPos.transform.position, speed * Time.deltaTime);
            if (VectorAppox(rigid2D.position, targetPos.transform.position) && isColliding == true)
            {
                isMoving = false;
            }
        }
        else
        {
            lineRenderer.enabled = true;
            if (targetPos != null)
                rigid2D.position = Vector2.Lerp(rigid2D.position, targetPos.transform.position, speed * Time.deltaTime);
            //transform.position = fixedPos.position;
        }
        animator.SetBool("isMoving", isMoving);
    }

    void PickPosition()
    {
        var sit = Physics2D.Linecast(transform.position, mousePos, sittingLayer);
        //var sit = Physics2D.Raycast()
        if (sit.collider != null)
        {
            Debug.Log(sit.point);
           // previousTargetPos = transform.position;
            targetPos = new GameObject("FixedPosition");
            targetPos.transform.SetParent(sit.collider.transform);
            targetPos.transform.position = sit.point;
            //target = sit.point;
            animator.SetTrigger("Jump");
            isMoving = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //isColliding = true;
            //isMoving = false;
            //other.gameObject.layer = defaultLayerValue;
        }
        else if (other.gameObject.CompareTag("Death"))
        {
           /* Destroy(gameObject);
            GameManager.Instance.ResetScene();*/
            StartCoroutine(PlayerDeath());
        }
        else if (other.gameObject.layer == hitableLayerValue)
        {
            //Debug.Log("bich");

            if (other.gameObject.tag == "Bone")
            {
                // Destroy(SearchAncestorWithSpriteSkin(other.transform).gameObject);
                SearchAncestorWithIKRoot(other.transform).GetComponent<IKRootBehaviour>().DestructRoot();
                //Debug.Log("Bone hit!");
            }
            else
            {
                Destroy(other.gameObject);
                GameManager.Instance.CutRoot();
            }

        }
    }

    IEnumerator PlayerDeath()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        rigid2D.simulated = false;
        GameObject particle = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(particleLifeTime);
        Destroy(particle);
        GameManager.Instance.ResetScene();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
            //isMoving = true;

            //other.gameObject.layer = sittingLayerValue;
        }
    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
            //isMoving = true;

            //other.gameObject.layer = sittingLayerValue;
        }
    }

    bool VectorAppox(Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }

    Transform SearchAncestorWithIKRoot(Transform t)
    {
        Transform currentTransform = t;

        for (int i = 0; i < 100; i++)
        {
            Transform parentTransform = currentTransform.parent;

            if (parentTransform.TryGetComponent<IKRootBehaviour>(out IKRootBehaviour root))
            {
                //Do something with your parent transform
                return parentTransform;
                //break;
            }
            else
            {
                currentTransform = parentTransform;
            }
        }
        return null;
    }
}
