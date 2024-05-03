using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // var- Physics
    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;
    [SerializeField] LayerMask brickDetect;
    
    // var- get Ref
    private BallController ballController;

    private float fastForwardScale = 2f;

    private float lastVeloY;

    public bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        //cc2d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < LevelManager.levelManager.GetBallPosYScaled())
        {
            StopBall();
            transform.position = new Vector3(transform.position.x, LevelManager.levelManager.GetBallPosYScaled(), transform.position.z);
            //cc2d.enabled = false;
            this.ballController.ReNew(transform.position,this);
        }
    }
    private void FixedUpdate()
    {
        lastVeloY = rb2d.velocity.y;
    }
    private void LateUpdate()
    {
        
    }

    public void ControlledFire(float constantSpeed,Vector2 finalDirection)
    {
        rb2d.velocity = constantSpeed * finalDirection;
        cc2d.enabled = true;
    }

    public void ControlledOnReNew(bool isLast,Vector2 startPos)
    {
        StartCoroutine(MoveBack(isLast, startPos));
    }
    public void StopBall()
    {
        rb2d.velocity = Vector2.zero;
    }

    IEnumerator MoveBack(bool isLast, Vector2 startPos)
    {
        canHit = false;
        while ((Vector2)transform.position != startPos)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, startPos, 0.2f);
            yield return null;
        }
        if (isLast)
        {
            GameplayCanvas.gameplayCanvas.TurnOffButtonComeback();
            yield return new WaitForSeconds(0.5f);
            ballController.DisableDouble();
            ballController.ResetTurn();
            GameManager.gameManager.UpdateGameState(DefineGameState.BLOCK_DROPPING);
        }
    }
    public void SetRefToController(BallController ballController)
    {
        this.ballController = ballController;
    }
    public float GetLastVeloY()
    {
        return lastVeloY;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block" )&& canHit)
        {
            
            //collision.gameObject.GetComponent<BlockStatus>().OnBallEnter(this.GetComponent<BallStatus>());
            Vector2 towardCollision = collision.contacts[0].point - (Vector2)transform.position;
            Ray2D ray2D = new Ray2D((Vector2)transform.position, towardCollision);
            RaycastHit2D hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, 1f, brickDetect);
            if (hit2D.collider != null)
            {
                Debug.Log(hit2D.collider.gameObject.name);
                if (FindObjectOfType<AudioManager>())
                {
                    AudioManager.audioManager.audioSource.PlayOneShot(AudioManager.audioManager.audioClip[0]);
                }
                
                hit2D.collider.gameObject.transform.parent.GetComponentInParent<BlockStatus>().OnBallEnter(this.GetComponent<BallStatus>());
            }
        }
    }

    internal void FastForward()
    {
        rb2d.velocity = rb2d.velocity * fastForwardScale;
    }
}
