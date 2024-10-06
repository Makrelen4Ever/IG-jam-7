using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Following")]
    public float Speed;
    public GameObject Target;

    private Vector3 TargetPosition;

    void FixedUpdate(){
        if(Target.transform.position.y > Target.GetComponent<PlayerMovement>().LastYPosition){
            TargetPosition = Target.transform.position;
        }else{
            TargetPosition = new Vector3(Target.transform.position.x, Target.GetComponent<PlayerMovement>().LastYPosition, 0);
        }
        transform.position = Vector3.Lerp(transform.position, Target.transform.position + new Vector3(0, 0,  -10), Speed);
    }
}
