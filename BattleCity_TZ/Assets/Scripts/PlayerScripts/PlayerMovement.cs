using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    [SerializeField] Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;
    public GameObject playerGrahyx;
    private void Update()
    {
         moveHorizontal = Input.GetAxisRaw("Horizontal");
         moveVertical = Input.GetAxisRaw("Vertical");

         movement = new Vector2(moveHorizontal, moveVertical).normalized;
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg-90f;
            rb.rotation = angle;
        }
        

    
    }

    private void FixedUpdate()
    {
        
        rb.velocity = movement * speed;

    }
}
