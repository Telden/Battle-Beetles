using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseClassic : MonoBehaviour
{

    public Text healthText;
    public float axisX = 0;
    public float axisY = 0;
    public float cameraX = 0;
    public float cameraY = 0;

    [Header("Player 1 or Player 2")]
    public bool isPlayer1;

    [Header("Player 1 or Player 2")]
    public int playerHealth = 100;

    [Header("Movement Parameters")]
    public float movementSpeed;
    public float terminalVelocityXZ;
    public float terminalVelocityY;
    public bool canMove;
    public bool applyTerminalVelocity;

    [Header("Jump Parameters")]
    public bool grounded;
    public float jumpForce;
    public float gravityForce;
    public LayerMask floorLayerMask;
    public LayerMask playerLayerMask;
    public float aerialHindrance;
    public Collider groundCheck;

    [Header("Lag")]
    public bool endLagTotalStop = false;

    [Header("Camera Parameters")]
    public float cameraRotationSpeed;

    /*[Header("Ability Parameters")]
    public float hoverTime;
    public bool isHovering;
    public bool justHovered;

    public ParticleSystem parts;*/

    [Header("Knockback Angle")]
    public float horizontalKnockback;
    public float verticalKnockback;

    [Header("Groundpound Damage")]
    public int damageDealt;

    [Header("AudioClips")]
    public AudioClip audioKnockback;
    public AudioClip audioJump;

    [Header("Class Mode Hover")]
    public float lift;
    public ParticleSystem partsHover;

    //Raycast Sphere Variables
    //float characterHeight;
    float characterWidth;
    //float characterLength;
    RaycastHit hit;

    //When the player is getting knocked back, this makes it so terminal velocity will not be applied
    private bool applyingknockback = false;

    void Start()
    {
    	partsHover.Stop();
        canMove = true;
        playerHealth = 100;
    }

    void Update()
    {
        // check input and move accordingly
        Movement();
        // check input and jump accordingly
        Jumping();
        // maximum velocities
        TerminalVelocities();
        // apparently there's no Gravity Scale in 3D so add some extra force
        ExtraGravity();
        // check if the player is grounded
        CheckGround();
        // rotate the camera with the second joystick
        RotateCamera();
        // update health UI
        UpdateUI();
        // if health reaches 0... you lose!
        CheckHealth();
    }

    void Movement()
    {
        // normal movement on the ground and when hovering
        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {
				GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed * -1);
            }
			if (Input.GetMouseButton(1))
            {
				GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed);
            }

			if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
			{
				GetComponent<Rigidbody>().AddForce(transform.up * lift);
				if (!partsHover.isPlaying)
				{
					partsHover.Play();
				}
			}
        }
    }

    void Jumping()
    {
        // jump
        if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("Jump", isPlayer1)) && grounded && canMove)
        {
            //isJumping = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));

            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound(audioJump, 0.1f);
        }

        // press the jump button a second time to hover
        /*if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("Jump", isPlayer1)) && !grounded && !isHovering && !justHovered)
        {
            parts.Play();
            isHovering = true;
            justHovered = true;
            Invoke("StopHovering", hoverTime);
        }*/
    }

    void TerminalVelocities()
    {
        // don't control terminal velocities if you're dashing
		if (gameObject.GetComponent<Dashing>() != null && gameObject.GetComponent<Dashing>().isDashing)
        {
            //Wait to slow down
        }
        // Don't apply terminal velocity if you're dash attacking
		else if (gameObject.GetComponent<Lunge>() != null && gameObject.GetComponent<Lunge>().isDashing)
        {

        }
        // Don't apply terminal velocity if your're ground-pounding
		else if (gameObject.GetComponent<GroundPound>() != null && gameObject.GetComponent<GroundPound>().isAttacking)
        {

        }
        //Don't apply terminal velocity if the player is being knocked back
        else if (applyingknockback)
        {

        }
        else
        {
            if (applyTerminalVelocity)
            {
                // controls max movement speed (x)
                if (GetComponent<Rigidbody>().velocity.x > terminalVelocityXZ)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(terminalVelocityXZ, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }
                else if (GetComponent<Rigidbody>().velocity.x < terminalVelocityXZ * -1)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(terminalVelocityXZ * -1, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }

                // controls max movement speed (z)
                if (GetComponent<Rigidbody>().velocity.z > terminalVelocityXZ)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, terminalVelocityXZ);
                }

                else if (GetComponent<Rigidbody>().velocity.z < terminalVelocityXZ * -1)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, terminalVelocityXZ * -1);
                }
            }
        }

        // controls max falling speed (this still happens even if you're dashing)
        if (GetComponent<Rigidbody>().velocity.y < terminalVelocityY * -1)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, terminalVelocityY * -1, GetComponent<Rigidbody>().velocity.z);
        }
    }

    void ExtraGravity()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, gravityForce * -1, 0));
    }

    void CheckGround()
    {
        if (!applyingknockback)
        {
            // create an array of the all of the colliders detected this frame
            Collider[] temp = Physics.OverlapBox(groundCheck.bounds.center,
                                                 groundCheck.bounds.extents,
                                                 groundCheck.transform.rotation);
            bool ground = true;//false;

            /*foreach (Collider c in temp)
            {
                // if one or more hitboxes that are ground are found, break and say whether or not the player is grounded
                // also, if a player is detected, check to see if the isPlayer1 detected matches this object's isPlayer1 so you can't jump in midair
                if (c.gameObject.layer == 8 || (c.gameObject.layer == 9 && c.gameObject.GetComponent<BasePlayer>().isPlayer1 != isPlayer1))
                {
                    if (c.gameObject.layer == 9)
                        Debug.Log("Grounded by player");
                    //Debug.Log("Found ground");
                    ground = true;
                    break;
                }
            }*/

            // if a ground hitbox was detected, act accordingly
            if (ground)
            {
                grounded = true;
                if (!endLagTotalStop)
                {
                	canMove = true;
					partsHover.Stop();
                }

                applyTerminalVelocity = true;
                gameObject.GetComponent<GroundPound>().canGroundPound = true;
                gameObject.GetComponent<Dashing>().canDash = true;
            }
            else
            {
                grounded = false;
                //Debug.Log("Not Grounded");
            }
        }
    	
    }

    void RotateCamera()
    {
        transform.Rotate(0, cameraX * cameraRotationSpeed * Time.deltaTime, 0);
    }

    /*void OnCollisionEnter(Collision hitbox)
    {
        //http://answers.unity3d.com/questions/1113304/how-can-make-my-player-to-jump-like-parabola.html

        if (hitbox.gameObject.tag == "Player" && gameObject.GetComponent<GroundPound>().isAttacking)
        {
            gameObject.GetComponent<GroundPound>().isAttacking = false;
            applyKnocback();
            if (isPlayer1)
            {
                GameObject.Find("Player2").GetComponent<BasePlayer>().applyKnocback();
                GameObject.Find("Player2").GetComponent<BasePlayer>().playerHealth -= damageDealt;
            }
            else
            {
                GameObject.Find("Player1").GetComponent<BasePlayer>().applyKnocback();
                GameObject.Find("Player1").GetComponent<BasePlayer>().playerHealth -= damageDealt;
            }
            
        }
    }*/

    public void applyKnocback()
    {
        //if(isPlayer1)
        //{
        //    Debug.Log("Knocking back player 1");
        //}
        //else
        //{
        //    Debug.Log("Knocking back player 2");
        //}
        applyingknockback = true;
        canMove = false;
        applyTerminalVelocity = false;
        gameObject.GetComponent<GroundPound>().canGroundPound = false;
        gameObject.GetComponent<Dashing>().canDash = false;
        Physics.IgnoreCollision(GameObject.Find("Player2").GetComponent<BoxCollider>(), GameObject.Find("Player1").GetComponent<BoxCollider>(), true);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x * -1 * horizontalKnockback,
                                                        verticalKnockback,
                                                        transform.forward.z * -1 * horizontalKnockback);

        // temporary sound to play when getting hit
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound(audioKnockback, 0.1f);

        Invoke("resetKnockback", 1f);
        Invoke("resetCollisions", 1f);
    }

    void UpdateUI()
    {
        if (isPlayer1)
        {
            string tmp = playerHealth.ToString();
            healthText.text = "HP: " + tmp;
        }

        else
        {
            string tmp = playerHealth.ToString();
            healthText.text = "HP: " + tmp;
        }
    }

    void CheckHealth()
    {
        if (playerHealth <= 0)
        {
            if (isPlayer1)
            {
                SceneManager.LoadScene("Player 2 Wins");
            }
            else
            {
                SceneManager.LoadScene("Player 1 Wins");
            }
        }
    }

    void resetKnockback ()
    {
        applyingknockback = false;
    }

    void resetCollisions()
    {
        Physics.IgnoreCollision(GameObject.Find("Player2").GetComponent<BoxCollider>(), GameObject.Find("Player1").GetComponent<BoxCollider>(), false);
    }

    public void applyEndLag(float time)
    {
		canMove = false;
    	endLagTotalStop = true;
    	Invoke("stopEndLag", time);
    }

    void stopEndLag()
    {
    	canMove = true;
    	endLagTotalStop = false;
    }
}