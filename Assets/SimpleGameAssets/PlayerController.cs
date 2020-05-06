using System;
using System.Collections;
using UnityEngine;

namespace KiliWare.SampleVRMApp
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnDead;
        protected Animator _animator;
        protected readonly float _moveSpeed = 10f;
        protected readonly float _moveLimitX = 4;
        protected bool _lookLeft = false;
        protected bool _isDead = false;
        protected bool _isFalling = false;

        void Awake()
        {
            var colliderObject = Instantiate(Resources.Load("PlayerCollider")) as GameObject;
            colliderObject.transform.SetParent(transform);

            var colliderScript = colliderObject.GetComponent<PlayerColliderController>();
            colliderScript.OnEnemyCollided += OnDamaged;
        }

        void Update()
        {
            if (_isFalling)
            {
                transform.Translate(Vector3.up * 4f * Time.deltaTime);
                _animator.SetTrigger("Damaged");
            }

            if (_isDead)
            {
                return;
            }

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

                var offset = new Vector3(_lookLeft ? -1.5f : 1.5f, 1f, 0);
                particle.transform.position = transform.position + offset;

                Destroy(particle, 0.3f);
            }

            var pos = transform.position;
            if (pos.x > _moveLimitX)
            {
                pos.x = _moveLimitX;
                transform.position = pos;
            }
            if (pos.x < -_moveLimitX)
            {
                pos.x = -_moveLimitX;
                transform.position = pos;
            }
        }

        public void SetAnimatorController(RuntimeAnimatorController rac)
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = rac;
        }

        protected void OnDamaged()
        {
            if (!_isDead)
            {
                StartCoroutine(OnDamagedCoroutine());
            }
        }

        protected IEnumerator OnDamagedCoroutine()
        {
            OnDead?.Invoke();
            _isDead = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.back * 1f);
            _animator.SetTrigger("Damaged");
            yield return new WaitForSeconds(0.3f);
            _isFalling = true;
        }
    }
}