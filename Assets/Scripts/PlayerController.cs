using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private const int MAXJUMP= 1;
    public float speed = 5;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private Vector3 movement;
    private float yDiff;
    private int jumpCount = MAXJUMP;

    //private float movementX;
    //private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        yDiff = GetComponent<Collider>().bounds.extents.y;
    }

    /*void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }*/

    private void Update()
    {
        if (isGrounded())
        {
            jumpCount = MAXJUMP;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                jump();
            }
            else if(jumpCount > 0)
            {
                jump();
                jumpCount--;
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        /*Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);*/
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        
        rb.position += movement * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
        
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, yDiff + .02f);
    }

}
