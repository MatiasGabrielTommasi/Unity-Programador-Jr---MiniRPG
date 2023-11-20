using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        void Start()
        {

        }

        public Transform target;
        public float smoothSpeed = 0f;
        Vector3 locationOffset = new Vector3(0f, 2f, -5f);
        public Vector3 rotationOffset;

        void FixedUpdate()
        {
            Vector3 desiredPosition = PlayerController.Instance.transform.position + PlayerController.Instance.transform.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = PlayerController.Instance.transform.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }
    }
}