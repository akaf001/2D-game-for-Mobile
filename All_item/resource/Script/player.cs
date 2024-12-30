using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class player : MonoBehaviour
{
    private float Xinput;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private float _bottonJump = 10f;
    [SerializeField] private float _maxHealth = 100f;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadious = 1f;
    [SerializeField] private LayerMask _attackLayer;
    [SerializeField] private GameObject _gameOverUi;

    [SerializeField] private Text healthAmountText;


    public Animator _animator;
    private Rigidbody2D _rb;
    private Enemy _enemy;

    private bool isGround = true;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemy = GameObject.FindAnyObjectByType<Enemy>();
    }

    private void Update()
    {
        healthAmountText.text = _maxHealth.ToString();
        Jump();
        PlayerAttack();
        if (_maxHealth <= 0)
        {
            Die();
        }

    }
    private void FixedUpdate()
    {
        Movement();
  
        if (Xinput < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Xinput > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        AnimationPlayer();
    }

    private void Movement()
    {
        Xinput = Input.GetAxisRaw("Horizontal");
        Xinput = SimpleInput.GetAxis("Horizontal");
        transform.position += new Vector3(Xinput * _speed *Time.deltaTime, 0f,0f);
    }
    private void Jump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("jump");
            _animator.SetBool("Jump", true);
            _rb.AddForce(new Vector2(0f, _jumpForce));
            isGround = false;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGround = true;

            _animator.SetBool("Jump", false);
        }
    }
    public void BottonJump()
    {
        if (isGround)
        {
            _animator.SetBool("Jump", true);
            Vector2 velocity = _rb.linearVelocity;
            velocity.y = _bottonJump;
            _rb.linearVelocity = velocity;
            isGround = false;
            
        }
    }
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            AttackAnimation();
        }
    }

    public void AttackAnimation()
    {
        int randomAttack = Random.Range(0, 3);

        if (randomAttack == 0)
        {
            _animator.SetTrigger("Attack1");
        }
        else if (randomAttack == 1)
        {
            _animator.SetTrigger("Attack2");
        }
        else if (randomAttack == 2)
        {
            _animator.SetTrigger("Attack3");
        }

    }
    private void AnimationPlayer()
    {
        if (Mathf.Abs(Xinput) > 0.1f)
        {
            _animator.SetFloat("Run", 1);
        }else if (Xinput == 0f)
        {
            _animator.SetFloat("Run", 0);
        }

    }
    public void Attack()
    {
       Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadious, _attackLayer);
        if (hit == true)
        {
            _enemy.TakeDamage(20);
        }
       
    }
    public void TakeDamage(int damage)
    {
        if (_maxHealth <= 0)
        {
            return;
        }
        _maxHealth -= damage;
        FindAnyObjectByType<CameraShake>().shake(1f,4f);
    }
    private void Die()
    {
            FindAnyObjectByType<GameMAnager>().isGameActive = false;
            Destroy(this.gameObject);
        FindAnyObjectByType<CameraShake>().shake(.3f, 4);
        _gameOverUi.SetActive(true);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            Die();
        }
    }
}
