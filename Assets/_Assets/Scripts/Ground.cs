using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Vector2 box;
    public LayerMask groundLayer;
    public float CasDistance;
    bool isGround;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isGrounded()
    {

        if (Physics2D.BoxCast(transform.position, box, 0, -transform.up, CasDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * CasDistance, box);
    }
}
