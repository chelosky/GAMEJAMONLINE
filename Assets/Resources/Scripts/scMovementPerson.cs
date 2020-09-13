using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scMovementPerson : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private bool bWalking;
    private Animator animator;
    private float walkTime;
    private float walkCounter;
    private float waitTime;
    private float waitCounter;
    private float offSet;
    private float walkSpeed;
    public bool isAlive;

    private int[] directionMov = new int[] { -1, 0 , 1 };
    private Vector3 vecDirection;
    private int faceDirection;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        walkSpeed = 2f;
        walkTime = 3f;
        waitTime = 2f;
        offSet = 2f;
        walkCounter = walkTime;
        waitCounter = waitTime;
        SortLayerPerson(rb2D.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (bWalking)
            {
                walkCounter -= Time.deltaTime;

                if (walkCounter < 0)
                {
                    bWalking = false;
                    waitCounter = waitTime;
                }

                //rb2D.velocity = vecDirection;
                if (Vector2.Distance(rb2D.position, vecDirection) > offSet)
                {
                    rb2D.position = Vector2.MoveTowards(rb2D.position, vecDirection, walkSpeed * Time.deltaTime);
                }
                else
                {
                    bWalking = false;
                    waitCounter = waitTime;
                }
            }
            else
            {
                waitCounter -= Time.deltaTime;
                //rb2D.velocity = Vector2.zero;
                if (waitCounter < 0)
                {
                    ChooseDirection();
                }
            }
            animator.SetBool("bWalking", bWalking);
        }
        SortLayerPerson(rb2D.position.y);
    }

    public void DeathPerson()
    {
        isAlive = false;
        animator.SetBool("bDeath", true);
    }

    void ChooseDirection()
    {
        vecDirection = scGameManager.instance.ReturnRandomPositionMap();
        bWalking = true;
        walkCounter = walkTime;
    }

    void SortLayerPerson(float posY)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().sortingOrder = (int)(posY * -100);
        }
    }

    void ChangeDirection(int face)
    {
        foreach (Transform child in transform)
        {
            Vector3 newScale = child.localScale;
            newScale.x *= face;
            child.localScale = newScale;
        }
    }
}
