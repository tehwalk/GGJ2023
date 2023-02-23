using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerMovement : MonoBehaviour
{
    //public LayerMask def, ignore;
    //public int layerDefalut = 0;
    MainGameControl gameControl;
    PauseMenuUI pauseMenuUI;

    [Header("Basic Properties")]
    public LayerMask sittingLayer, defaultLayer, hitableLayer;
    [SerializeField] int sittingLayerValue, defaultLayerValue, hitableLayerValue;
    [SerializeField] float speed = 3;

    [Header("Death Particle Properties")]
    [SerializeField] GameObject vfxPrefab;
    [SerializeField] float particleLifeTime;

    [Header("Target Properties")]
    [SerializeField] GameObject targetPrefab;

    LineRenderer lineRenderer;
    Rigidbody2D rigid2D;
    Animator animator;
    Vector3 mousePos, target, previousTargetPos;
    bool isMoving = false, isColliding = false;
    GameObject targetPos, targetVisual;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = MainGameControl.Instance;
        pauseMenuUI = gameControl.GetComponentInChildren<PauseMenuUI>();
        rigid2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponentInChildren<Animator>();
        targetVisual = Instantiate(targetPrefab);
        targetVisual.transform.SetParent(transform);
        targetVisual.SetActive(false);
    }

    private void Update()
    {
        if(pauseMenuUI.isOpen == true) return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
        //lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
        var sit = Physics2D.Linecast(transform.position, mousePos, sittingLayer);
        //var sit = Physics2D.Raycast()
        if (sit.collider != null)
        {
            //lineRenderer.SetPosition(1, new Vector3(sit.point.x, sit.point.y, 0));
            targetVisual.SetActive(true);
            targetVisual.transform.position = sit.point;
            //show a sign that it can land here
            if (Input.GetMouseButtonDown(0) && isMoving == false)
            {
                PickPosition(sit);
            }
        }
        else
        {
            targetVisual.SetActive(false);
            //lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pauseMenuUI.isOpen == true) return;
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

    void PickPosition(RaycastHit2D sit)
    {
        Debug.Log(sit.point);
        // previousTargetPos = transform.position;
        targetPos = new GameObject("FixedPosition");
        targetPos.transform.SetParent(sit.collider.transform);
        targetPos.transform.position = sit.point;
        gameControl.AddClick();
        //target = sit.point;
        animator.SetTrigger("Jump");
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
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
        }
    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
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
