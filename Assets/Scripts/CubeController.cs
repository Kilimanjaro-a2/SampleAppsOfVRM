using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    protected bool _isBroken = false;
    protected bool _towardsLeft = true;
    protected float _speed = 2f;
    void Update()
    {
        var direction = _towardsLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Attack" && !_isBroken)
        {
            StartCoroutine(BrokenEffect());
        }
    }

    public void SetDirection(bool towardsLeft)
    {
        _towardsLeft = towardsLeft;
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
