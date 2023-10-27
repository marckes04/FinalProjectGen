using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMorlen : MonoBehaviour
{
    public float speed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput ) * speed * Time.deltaTime;
        transform.Translate(movement);

        float movementMagnitude = movement.magnitude;

        bool isMovingHorizontally = Mathf.Abs(horizontalInput) > 0.1f;
        animator.SetBool("isWalk", isMovingHorizontally);
    }
}
