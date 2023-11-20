using Assets.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActtackController : MonoBehaviour
{
    void Start()
    {
        Shot();
    }
    private void Shot()
    {
        Rigidbody rigidbosy = GetComponent<Rigidbody>();
        rigidbosy.AddForce(Vector3.forward * 100f, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tags.Floor))
            Destroy(gameObject);
    }
}
