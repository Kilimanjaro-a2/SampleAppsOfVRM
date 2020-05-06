using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Animator _animator;
    protected readonly float _moveSpeed = 10f;
    protected bool _lookLeft = false;
    void Update()
    {
        var moveFlag = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            moveFlag = true;
            _lookLeft = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            moveFlag = true;
            _lookLeft = false;
        }

        if (moveFlag)
        {
            var moveValue = Vector3.forward * _moveSpeed;
            transform.Translate(moveValue * Time.deltaTime);
            _animator.SetFloat("Move", Mathf.Abs(moveValue.magnitude));
        }
        else
        {
            _animator.SetFloat("Move", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Punch");
            var particle = Instantiate(Resources.Load("AttackParticle")) as GameObject;
            particle.transform.position += transform.position + new Vector3(_lookLeft ? -1f : 1f, 1f, 0);
            Destroy(particle, 2f);
        }

        var pos = transform.position;
        if (pos.x > 5)
        {
            pos.x = 5;
            transform.position = pos;
        }
        if (pos.x < -5)
        {
            pos.x = -5;
            transform.position = pos;
        }
    }

    public void SetAnimatorController(RuntimeAnimatorController rac)
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = rac;
    }

    protected IEnumerator OnDamaged()
    {
        _animator.SetTrigger("Damaged");
        yield return new WaitForSeconds(1f);
    }
}
