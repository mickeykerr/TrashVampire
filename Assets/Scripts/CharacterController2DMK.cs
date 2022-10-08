using UnityEngine;
using UnityEngine.Events;

//Brackey's Base Code Combined with
//Health + Death code written by Michael Kerr
public class CharacterController2DMK : MonoBehaviour
{

	//Brackey's Declarations - Movement
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWall;
	[SerializeField] private Transform m_LGroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_RGroundCheck;
    [SerializeField] private Transform m_LWallCheck;							// A position marking where to check for the left wall.
    [SerializeField] private Transform m_RWallCheck;							// A position marking where to check for the right wall.
    [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
	const float k_WalledRadius = .2f;
	public bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Walled;				// Player on wall?
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	//END Brackey's Declarations

	//Mickey Declarations
	
	//health declarations

//	void iAmDed()
//	{ 
		//stop ability to move
//		movementStatus = false;

		//oh no, player fell over
//        gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
//		gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, .5f);
//		if (isDead == false) { gameObject.transform.Translate(0, -.25f, 0); }
		
		//he dead as hell
//		isDead = true;
//	}

	void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	void Start()
    {
		//set hp to max on level start
//		hp = maxhp;
    }

	void Update()
    {
		// hp loss / time
//		if (hp > -1 && !inHealthArea)
//		{
//			hp -= Time.deltaTime;
//		}

//		if(hp < 0 && isDead == false)
//        {
//			iAmDed();
//      }
		//healthbar should reflect HP
//		healthBar.health = hp;
	}

	void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] Lcolliders = Physics2D.OverlapCircleAll(m_LGroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        Collider2D[] Rcolliders = Physics2D.OverlapCircleAll(m_RGroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < Lcolliders.Length || i < Rcolliders.Length; i++)
		{
			if (Lcolliders[i].gameObject != gameObject || Rcolliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		//Check for walls for unlimited wall
	}


	public void Move(float move, bool crouch, bool jump, bool isDead)
	{
		//am i dead?
		if (isDead == false)
		{



			// If crouching, check to see if the character can stand up
			if (crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
				{
					crouch = true;
				}
			}

			//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_AirControl)
			{

				// If crouching
				if (crouch)
				{
					if (!m_wasCrouching)
					{
						m_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					move *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				}
				else
				{
					// Enable the collider when not crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = true;

					if (m_wasCrouching)
					{
						m_wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}
				}

				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
			}
			// If the player should jump...
			if (m_Grounded && jump)
			{
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddRelativeForce(new Vector2(0f, m_JumpForce));
			}
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
