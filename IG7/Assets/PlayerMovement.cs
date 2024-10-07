using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float Acceleration;
    public float Decceleration;
    public float MaxVelocity;

    [Header("Launching")]
    public float LaunchVelocity;

    [Header("Jump")]
    public float JumpForce;

    [Header("Wall")]
    public Vector2 WallJumpVelocity;

    [Header("Input")]
    public Vector2 InputAxis;

    private Rigidbody2D rb;

    [Header("Dying")]
    public float LastYPosition;
    public GameObject DiePar;
    public Vector3 SpawnPoint;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        LastYPosition = transform.position.y - 25;
    }

    void Update()
    {
        InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(InputAxis.magnitude > 1){
            InputAxis = InputAxis.normalized;
        }

        if(IsGrounded() && InputAxis.y > 0){
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }else if(InputAxis.y > 0){
            if(IsRight() || IsLeft()){
                if(IsRight()){
                    rb.velocity = new Vector2(-WallJumpVelocity.x, WallJumpVelocity.y);
                }else{
                    rb.velocity = new Vector2(WallJumpVelocity.x, WallJumpVelocity.y);
                }
            }
        }

        if(transform.position.y <= -25 || Input.GetKeyDown("r")){
            Debug.Log("Die");
            StartCoroutine(Die());
        }
    }

    void FixedUpdate(){
        Vector2 force = new Vector2(InputAxis.x * Acceleration, 0);
        rb.AddForce(force);

        if(Mathf.Abs(rb.velocity.x) > MaxVelocity && Mathf.Abs(InputAxis.x) > 0){
            rb.velocity = new Vector2(MaxVelocity * Mathf.Clamp(rb.velocity.x, -1, 1), rb.velocity.y);
        }else{
            if(Mathf.Abs(InputAxis.x) < 1){
                rb.velocity = new Vector2(rb.velocity.x * Decceleration, rb.velocity.y);
            }
        }

        if(IsRight() || IsLeft()){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1, 100));
        }
    }

    bool IsGrounded(){
        return transform.GetChild(0).GetComponent<CollisionDetect>().IsGrounded;
    }

    bool IsRight(){
        return transform.GetChild(1).GetComponent<CollisionDetect>().IsGrounded;
    }

    bool IsLeft(){
        return transform.GetChild(2).GetComponent<CollisionDetect>().IsGrounded;
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Launcher")){
            rb.velocity = new Vector2(0, LaunchVelocity);
        }

        if(coll.CompareTag("Spikes")){
            StartCoroutine(Die());
        }

        if(coll.CompareTag("End")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator Die(){
        var par = Instantiate(DiePar, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(par, 5);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
        transform.position = SpawnPoint;
    }
}
