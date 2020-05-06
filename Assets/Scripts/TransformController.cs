using UnityEngine;

namespace KiliWare.SampleVRMApp
{
    public class TransformController : MonoBehaviour
    {
        protected float _rotationSpeed = 10;
        protected float _moveSpeed = 10;

        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Move(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            }
        }

        public void SetSpeed(float rotation, float move)
        {
            _rotationSpeed = rotation;
            _moveSpeed = move;
        }

        // From https://teratail.com/questions/147069#reply-221677
        protected void Rotate(Vector2 delta)
        {
            float deltaAngle = delta.magnitude * _rotationSpeed;

            if (Mathf.Approximately(deltaAngle, 0.0f))
            {
                return;
            }

            var cameraTransform = Camera.main.transform;
            var deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;
            var axisWorld = Vector3.Cross(deltaWorld, cameraTransform.forward).normalized;

            transform.Rotate(axisWorld, deltaAngle, Space.World);
        }

        protected void Move(Vector2 delta)
        {
            var cameraTransform = Camera.main.transform;
            var deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;

            transform.Translate(deltaWorld * _moveSpeed * Time.deltaTime, Space.World);
        }
    }
}