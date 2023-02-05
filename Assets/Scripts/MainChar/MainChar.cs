using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainChar : MonoBehaviour
{
    private Rigidbody2D rbody2D;
    private Animator animator;
    private SpriteRenderer spRender;

    private bool inGround = false;

    private LineRenderer lineRender;

    private Vector2 movement = Vector2.zero;

    private bool flipFace = false;

    public Transform linePosition;

    public Seeds seeds;

    public float InvencibleTimeAfterDamage = 2;

    public float blinkTime = 0.5f;

    public float takeDamageRecoiForce = 5;
    public Vector2 recoiDirection = new Vector2(0.8f, 0.2f);

    [Range(0, 10)]
    public float jumpForce;

    [Range(0, 10)]
    public float moveSpeed;

    [Range(0, 10)]
    public float lineDistance = 2;

    [Range(0, 20)]
    public int seedLimit = 0;

    private Vector3 mousePos = Vector3.zero;

    private bool inInvencible = false;

    private float countInvencibleTime = 0;
    private float countBlinkTime = 0;
    private bool upBlink = false;

    private bool playerDead = false;
    private Vector2 initPos = Vector2.zero;


    void Awake()
    {
        initPos = transform.position;
    }

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRender = GetComponent<SpriteRenderer>();
        lineRender = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            lineRender.enabled = true;
            lineRender.SetPosition(0, transform.InverseTransformPoint(linePosition.position));
            //lineRender.SetPosition(1, Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lineDistance));
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            lineRender.SetPosition(1, LerpByDistance(transform.InverseTransformPoint(linePosition.position), transform.InverseTransformPoint(mousePos), lineDistance));
        } else
        {
            lineRender.enabled = false;
        }
        


        if (inInvencible)
        {
            if (upBlink)
            {
                spRender.color = new Color(1f, 1f, 1f, Mathf.Lerp(0.5f, 1f, countBlinkTime / blinkTime ));
            } else
            {
                spRender.color = new Color(1f, 1f, 1f, Mathf.InverseLerp(1f, 0.5f, countBlinkTime / blinkTime));
            }


            countInvencibleTime += Time.deltaTime;
            countBlinkTime += Time.deltaTime;

            if(countBlinkTime > blinkTime)
            {
                countBlinkTime = 0;
                upBlink = !upBlink;
            }

            if (countInvencibleTime > InvencibleTimeAfterDamage)
            {
                inInvencible = false;
            }
        } else
        {
            spRender.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        inGround = rbody2D.velocity.y == 0;
        animator.SetBool("inGround", inGround);

        //rbody2D.AddForce(movement * (inGround ? 2.5f : 1), ForceMode2D.Force);

        spRender.flipX = flipFace;

        rbody2D.velocity = new Vector2(movement.x, rbody2D.velocity.y); ;

        animator.SetFloat("speed", Mathf.Abs(rbody2D.velocity.x));
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isDead)
        {
            return;
        }
        movement = new Vector2(context.ReadValue<Vector2>().x, 0) * moveSpeed;

        if(movement.x < 0)
        {
            flipFace = true;
        }

        if(movement.x > 0)
        {
            flipFace = false;
        }
    }

    public void Jump()
    {
        if (isDead)
        {
            return;
        }
        if (inGround)
        {
            rbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void ThrowSeeds(InputAction.CallbackContext context)
    {
        if (isDead)
        {
            restart();
        } else
        {
            if (Seeds.SeedsInStage >= seedLimit && seedLimit != 0)
            {
                return;
            }

            if (context.phase == InputActionPhase.Started)
            {
                Seeds seed = Instantiate(seeds, linePosition.position, linePosition.rotation);

                Vector3 direction = (mousePos - seed.transform.position).normalized;
                seed.throwSeeds(new Vector2(direction.x, direction.y));
            }
        }
    }

    public void TakeDamage(Vector3 originDamage, int power)
    {
        if (isDead)
        {
            return;
        }

        if (!inInvencible)
        {

            EnvironmentSystem.CurrentLife--;
            animator.SetTrigger("damage");
            if (EnvironmentSystem.CurrentLife == 0)
            {
                dead();

                //rbody2D.simulated = false;
            }
            else
            {
                //Debug.Log(originDamage + " -> " + transform.position + " = " +(originDamage - transform.position).normalized);
                //rbody2D.AddForce((originDamage - transform.position).normalized * power * -20, ForceMode2D.Impulse);
                Vector2 forceDirection = (originDamage - transform.position).x <= 0 ? new Vector2(-recoiDirection.x, recoiDirection.y) : recoiDirection;
                rbody2D.AddForce(forceDirection * takeDamageRecoiForce, ForceMode2D.Impulse);
                //rbody2D.AddForce(Vector2.up * power * 20, ForceMode2D.Impulse);
                setInvencible();
            }

        }
    }

    private Vector2 LerpByDistance(Vector2 firstVector, Vector2 secondVector, float distance)
    {
        return (distance * (secondVector - firstVector).normalized) + firstVector;
    }

    private void setInvencible()
    {
        inInvencible = true;
        countInvencibleTime = 0;
        countBlinkTime = 0;
    }

    public void restart()
    {
        animator.SetTrigger("restart");
        playerDead = false;
        rbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = initPos;
        setInvencible();
    }

    public void dead()
    {
        animator.SetBool("dead", true);
        playerDead = true;
        rbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public bool isDead
    {
        get{
            return playerDead;
        }
    }


    /*private Vector3 LerpByDistance(Vector3 firstVector, Vector3 secondVector, float distance)
    {
        Vector3 point = distance * Vector3.Normalize(secondVector - firstVector) + firstVector;

        return point;
    }*/

}
