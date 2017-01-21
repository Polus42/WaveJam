using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    public enum WaveColor { red, green, blue };

    [Header("Common")]
    public float acceleration;
    public float jumpForce;
    public float speedMax;
    public float wallJumpForce;
    public WaveColor waveColor;
    public float latencyWallJump;

    [Header("On Blue Wave")]
    public float blueAcceleration;
    public float blueSpeedMax;

    [Header("On Red Wave")]
    public float climbingSpeed;

    private Renderer rend;
    private Rigidbody rb;
    private Vector3 vectorJump;

    private bool onGround = true;

    private bool wallLeft = false;
    private bool wallRight = false;
    private bool wallTop = false;

    private bool isWallJumping = false;
    private float timerWallJump = 0f;

    private bool isOnGreenPlatform = false;
    private bool isOnWhitePlatform = false;
    
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        vectorJump = new Vector3(0, jumpForce, 0);
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // detect here if the player is falling
        if(rb.velocity.y < .5 && rb.velocity.y > -.5 && !(wallLeft || wallRight))
        {
            onGround = true;
        } else
        {
            onGround = false;
        }

        //Movement
        if (!isWallJumping)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (waveColor == WaveColor.blue && !isOnWhitePlatform)
                {
                    if (rb.velocity.x < blueSpeedMax && rb.velocity.x > -blueSpeedMax)
                    {
                        rb.velocity += Vector3.right * Input.GetAxisRaw("Horizontal") * blueAcceleration * Time.deltaTime;
                    }
                    else
                    {
                        rb.velocity = new Vector3(blueSpeedMax * Mathf.Sign(Input.GetAxisRaw("Horizontal")), rb.velocity.y, rb.velocity.z);
                    }
                }
                else
                {
                    if (rb.velocity.x < speedMax && rb.velocity.x > -speedMax)
                    {
                        rb.velocity += Vector3.right * Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime;
                    }
                    else
                    {
                        rb.velocity = new Vector3(speedMax * Mathf.Sign(Input.GetAxisRaw("Horizontal")), rb.velocity.y, rb.velocity.z);
                    }
                }
            }
            else
            {
                if (!isOnGreenPlatform)
                {
                    if (onGround)
                    {
                        rb.velocity = Vector3.zero;
                    }
                    else
                    {
                        rb.velocity = new Vector3(0, rb.velocity.y, 0);
                    }
                }
            }
        }

        //Jump
        if (Input.GetButtonDown("Jump") && !isOnGreenPlatform)
        {
            if (onGround && !wallTop)
            {
                rb.AddForce(vectorJump, ForceMode.VelocityChange);
                                
            }
            else if (wallLeft)
            {
                rb.AddForce(new Vector3(wallJumpForce, wallJumpForce, 0), ForceMode.VelocityChange);
                isWallJumping = true;
                timerWallJump = 0;
            }
            else if (wallRight)
            {
                rb.AddForce(new Vector3(-wallJumpForce, wallJumpForce, 0), ForceMode.VelocityChange);
                isWallJumping = true;
                timerWallJump = 0;
            } else if (wallTop)
            {
                rb.AddForce(new Vector3(0, -wallJumpForce, 0), ForceMode.VelocityChange);
            }
        }

        //Climb
        if(waveColor == WaveColor.red && !isOnWhitePlatform)
        {
            if((wallLeft || wallRight) && !isWallJumping)
            {
                rb.useGravity = false;

                if (wallLeft)
                {
                    rb.AddForce(Vector3.left * 50 * rb.mass);
                    
                }
                else
                {
                    rb.AddForce(Vector3.right * 50 * rb.mass);
                }

                if(Input.GetAxisRaw("Vertical") != 0)
                {
                        rb.velocity += Vector3.up * Input.GetAxisRaw("Vertical") * climbingSpeed * Time.deltaTime;
                } else
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else if(wallTop)
            {
                rb.useGravity = false;
                rb.AddForce(Vector3.up * 50 * rb.mass);
                
            }
            else
            {
                rb.useGravity = true;
            }
        } else
        {
            rb.useGravity = true;
        }

        //Change Wave now in global behavior
        /*if (Input.GetButtonDown("LeftTrigger"))
        {
            switchWaveLeft();
        }

        if (Input.GetButtonDown("RightTrigger"))
        {
            switchWaveRight();
        }*/

        //check timer wall jump
        timerWallJump += Time.deltaTime;
        if(timerWallJump > latencyWallJump)
        {
            isWallJumping = false;
        }
        
    }

    public void setWallBounce(string side, bool isCollided)
    {
        if (side == "left")
        {
            wallLeft = isCollided;
        }
        else if (side == "right")
        {
            wallRight = isCollided;
        }
        else if (side == "top")
        {
            wallTop = isCollided;
        }
    }

    public void switchWaveLeft()
    {
        switch (waveColor)
        {
            case WaveColor.blue:
                waveColor = WaveColor.red;
                break;
            case WaveColor.green:
                waveColor = WaveColor.blue;
                break;
            case WaveColor.red:
                waveColor = WaveColor.green;
                break;
        }
    }

    public void switchWaveRight()
    {
        switch (waveColor)
        {
            case WaveColor.blue:
                waveColor = WaveColor.green;
                break;
            case WaveColor.green:
                waveColor = WaveColor.red;
                break;
            case WaveColor.red:
                waveColor = WaveColor.blue;
                break;
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Green")
        {
            isOnGreenPlatform = true;
        } else if (coll.gameObject.tag == "White")
        {
            isOnWhitePlatform = true;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "Green")
        {
            isOnGreenPlatform = false;
        } else if (coll.gameObject.tag == "White")
        {
            isOnWhitePlatform = false;
        }
    }

    
}
