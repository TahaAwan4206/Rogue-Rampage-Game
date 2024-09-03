using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    public float jumpForce;

    public Rigidbody2D rigidBody;
    public Transform groundPoint;
    public bool isOnGround = true;
    public LayerMask whatisGround;

    public Animator animator;

    public BulletController shotToFire;
    public Transform shotPoint;

    private bool canDoubleJump;


    public float dashSpeed = 15.0f;
    public float dashLimit = 0.2f;
    private float dashCounter;

    public SpriteRenderer playerSR, afterImage;
    public float afterImageLimit, timeBetweenafterImage;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterFDash;
    private float dashRechargecounter;


    public GameObject standing, ball; //standing and ball sprites

    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;

    private AbilityTracker abilities;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<AbilityTracker>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        { 
            if (dashRechargecounter > 0)
            {

                dashRechargecounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.Dash)
                {
                    dashCounter = dashLimit;
                    ShowAfterImage();
                    AudioManager.instance.PlaySFXAdjusted(7);
                }
            }
            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;
                rigidBody.velocity = new Vector2(dashSpeed * transform.localScale.x, rigidBody.velocity.y);
                afterImageCounter = afterImageCounter - Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }
                dashRechargecounter = waitAfterFDash;
            }
            else
            {


                //moving on x axis
                rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidBody.velocity.y);

                
                if (rigidBody.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (rigidBody.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;

                }

            }

            isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatisGround);
            //jumping
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.DoubleJump)))
            {
                if (isOnGround)
                {

                    canDoubleJump = true;
                    AudioManager.instance.PlaySFXAdjusted(12);

                }
                else
                {
                    canDoubleJump = false;
                    animator.SetTrigger("doubleJump");
                    AudioManager.instance.PlaySFXAdjusted(9);
                }
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            }



            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0f);

                    animator.SetTrigger("ShotFired");
                    AudioManager.instance.PlaySFXAdjusted(13);
                }
                else if (ball.activeSelf && abilities.DropBomb)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                    AudioManager.instance.PlaySFXAdjusted(14);
                }

            }


            //player becomes a ball and rolls forward - ability 

            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -0.9f && abilities.Ball)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                        AudioManager.instance.PlaySFX(6);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0.9f)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                        AudioManager.instance.PlaySFX(10);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }





        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }



        if (standing.activeSelf) {
            animator.SetBool("isOnGround", isOnGround);
            animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        }
        if (ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));

        }
    }


    public void ShowAfterImage()
    {
       SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = playerSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;
        Destroy(image.gameObject, afterImageLimit);
        afterImageCounter = timeBetweenafterImage;
    }
}
