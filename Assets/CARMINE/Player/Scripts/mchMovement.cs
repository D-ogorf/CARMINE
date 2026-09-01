using System.Collections;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class mchMovement : MonoBehaviour
{
    const float gravity = 250;
    [Header("WALKING")]
    public float maxSpeed;
    [SerializeField] private float curSpeed;
    public sbyte forwardInt;
    public sbyte rightInt;

    [Header("JUMPING")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpBuffer;
    public sbyte jumpInt;

    [Header("STATES")]
    public bool _isWalk;
    public bool _isGround;
    public bool _canJump;
    public bool _wantJump;
    public bool _isJump;

    [Header("TIMERS")]
    public float timeGround;
    public float timeAir;

    private setKeybinds s;
    private Rigidbody r;
    private GameObject g;

    const float minVel = .35f;
    
    private void Start()
    {
        this.s = GameObject.Find("s").GetComponent<setKeybinds>();
        this.r = gameObject.GetComponent<Rigidbody>();
        this.g = GameObject.Find("mchGroundCheck");

        StartCoroutine(Timers());
    }

    private IEnumerator Timers()
    {
        while(true)
        {
            AddTime();
            yield return null;
        }        
    }

    private void AddTime()
    {
        if(_isGround)
        {
            timeGround += Time.deltaTime;
            timeAir = 0;
        }
        else
        {
            timeAir += Time.deltaTime;
            timeGround = 0;
        }
    }

    private void Update()
    {
        StateChecker();
        MovementForIntCTRL();
        MovementRgtIntCTRL();
        JumpCTRL();
    }

    private void StateChecker()
    {
        this._isWalk = Mathf.Abs(this.r.linearVelocity.x) > minVel || Mathf.Abs(this.r.linearVelocity.z) > minVel;
        this._isGround = this.g.GetComponent<uniGroundCheck>()._isGrounded;
        this._isJump = this.jumpInt != 0;
    }

    private void MovementForIntCTRL()
    {
        if(!Input.GetKey(this.s.forward) && !Input.GetKey(this.s.backward) || Input.GetKey(this.s.forward) && Input.GetKey(this.s.backward))
        {
            forwardInt = 0;
            return;
        }
        
        if(Input.GetKey(this.s.forward)) this.forwardInt = 1;
        if(Input.GetKey(this.s.backward)) this.forwardInt = -1;
    }

    private void MovementRgtIntCTRL()
    {
        if(!Input.GetKey(this.s.right) && !Input.GetKey(this.s.left) || Input.GetKey(this.s.right) && Input.GetKey(this.s.left))
        {
            this.rightInt = 0;
            return;
        }
        
        if(Input.GetKey(this.s.right)) this.rightInt = 1;
        if(Input.GetKey(this.s.left)) this.rightInt = -1;
    }

    private void JumpCTRL()
    {
        if(Input.GetKeyDown(this.s.jump)) StartCoroutine(JumpBuffer());
        if(this._wantJump && this._isGround) StartCoroutine(Jumping()); // change to _canJump later
    }

    private IEnumerator JumpBuffer()
    {
        float t = 0;

        while (t <= jumpBuffer)
        {
            t += Time.deltaTime;
            this._wantJump = true;
            yield return null;
        }
        this._wantJump = false;
    }

    private IEnumerator Jumping()
    {
        float t = 0;

        while(t <= this.maxJumpTime && Input.GetKey(this.s.jump))
        {
            t += Time.deltaTime;
            this.jumpInt = 1;
            if(t > .1f && this.r.linearVelocity.y == 0) break;
            yield return null;
        }
        this.jumpInt = 0;
    }

    private void FixedUpdate()
    {
        AddRunForce();
        AddGravity();
    }

    private void AddRunForce()
    {
        this.r.AddForce
        (
            this.forwardInt * this.maxSpeed * this.transform.forward + 
            this.rightInt * this.maxSpeed * this.transform.right +
            this.jumpForce * this.jumpInt * this.transform.up
        );
    }

    private void AddGravity()
    {
        if(!this._isJump) this.r.AddForce(gravity * -this.transform.up);
    }
}