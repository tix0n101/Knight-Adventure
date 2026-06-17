using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private int _currentHealth;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        enemySO.enemyDamageAmount = 2;
        _currentHealth = enemySO.health;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, enemySO.enemyDamageAmount);
        }
    }

    // œŒœ€“¿“‹—ﬂ —ƒ≈À¿“‹ “¿ , ◊“Œ¡€ NPC Õ≈ Õ¿ÕŒ—»À ”–ŒÕ —¬Œ»Ã BOX COLLIDER
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Player>())
    //    {
    //        Player.Instance.TakeDamage(transform, enemySO.enemyDamageAmount);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
        
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _polygonCollider2D.enabled = false;
            _boxCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    public void AttackCollideTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void AttackCollideTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }
}
