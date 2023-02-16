using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKRootBehaviour : MonoBehaviour
{
    bool isHit = false;

    public void DestructRoot()
    {
        isHit = true;
    }

    private void Update()
    {
        if (isHit == false) return;
        GameManager.Instance.CutRoot();
        Destroy(gameObject);

    }


}
