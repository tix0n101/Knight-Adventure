using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    public event EventHandler OnSwordSwing;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackCollideTurnOff();
    }
    public void AttackCollideTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void AttackCollideTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    public void Attack()
    {
        AttackCollideTurnOffOn();

        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
    }

    private void AttackCollideTurnOffOn() {
        AttackCollideTurnOff();
        AttackCollideTurnOn();
    }
}
