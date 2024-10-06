using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public bool IsGrounded;

    void OnTriggerEnter2D(Collider2D coll){
        IsGrounded = true;
    }

    void OnTriggerExit2D(Collider2D coll){
        IsGrounded = false;
    }
}
