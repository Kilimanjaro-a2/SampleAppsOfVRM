using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    protected bool _isBroken = false;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Attack" && !_isBroken)
        {
            StartCoroutine(BrokenEffect());
        }
    }
    protected IEnumerator BrokenEffect()
    {
        _isBroken = true;
        var go = Instantiate(Resources.Load("DestroyParticle")) as GameObject;
        go.transform.position = transform.position;
        Destroy(go, 2f);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
