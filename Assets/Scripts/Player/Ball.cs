using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public bool right = true;
    Rigidbody rb;
    Collider col;

    public string[] colorNames;
    public int selectedColorId;
    public bool colliderCheck = false;
    public float collisionCheckRadius = 2f;
    public LayerMask collideWithObstuclesMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (colliderCheck)
            CollisionCheck();
    }


    void FixedUpdate()
    {
        if (right)
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Force);
        }
        else
            rb.AddForce(Vector3.left * speed, ForceMode.Force);   
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Border"))
        {
            right = !right;
        }

        /*
        if(colliderCheck)
        {
            if (collision.collider.CompareTag(colorNames[selectedColorId]))
            {
                collision.collider.isTrigger = true;
            }
            colliderCheck = false;
        }*/
    }

    void CollisionCheck()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, collisionCheckRadius, collideWithObstuclesMask);
        foreach (Collider nearbyObject in collider)
        {
            if (nearbyObject.CompareTag(colorNames[selectedColorId]))
            {
                nearbyObject.GetComponent<Collider>().isTrigger = true;
            }
        }
    }

    public void SetColorID(int _colorId)
    {
        colliderCheck = true;
        selectedColorId = _colorId;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, collisionCheckRadius);
    }

}
