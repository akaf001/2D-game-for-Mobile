using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxHealth = 60f;

    private bool FacingLeft = true;

    [SerializeField] private Transform _detectPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadious;
    private player _playerscript;
    

    public LayerMask _ground;
    public LayerMask _attackLayer;

    private float _rayDistance = 1f;

    private void Awake()
    {
        _playerscript = GameObject.FindAnyObjectByType<player>();
    }

    
    private void Update()
    {
        EnemyMove();
        Die();
    }

    private void EnemyMove()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(transform.position, _attackRange, _attackLayer);
        if (hitInfo == true)
        {
            _animator.SetBool("Attack",true);


            if (_player.position.x > transform.position.x && FacingLeft)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                FacingLeft = false;
            }
            else if (_player.position.x < transform.position.x && FacingLeft == false) 
            { 
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                FacingLeft = true;
            }
        }
        else {
            _animator.SetBool("Attack",false);
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
            RaycastHit2D CallInfo = Physics2D.Raycast(_detectPoint.position,Vector2.down,_rayDistance,_ground);
            
            if (CallInfo == false) 
            {
                
                if (FacingLeft == true)
                {
                    transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    FacingLeft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    FacingLeft = true;
                }
            }
            else if (CallInfo == true) 
            {
               
            }
        }
       

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.DrawRay(_detectPoint.position, Vector2.down * _rayDistance);
        
    }
    public void TakeDamage(int damage)
    {
        if (_maxHealth <= 0)
        {
            return;
        }
        _maxHealth -= damage;
        _animator.SetTrigger("Hurt");

    }

    private IEnumerator PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.1f);

        Destroy(this.gameObject);
    } 
     
    private void Die()
    {
        if (_maxHealth <= 0)
        {
            StartCoroutine("PlayDieAnimation");
        }

    }
    public void EnemyAttack()
    {
       Collider2D attackInfo = Physics2D.OverlapCircle(_attackPoint.position, _attackRadious, _attackLayer);
        if (attackInfo == true)
        {
            if (attackInfo.GetComponent<player>() != null)
            {
                _playerscript.TakeDamage(20);  
            }
        }
    }

}