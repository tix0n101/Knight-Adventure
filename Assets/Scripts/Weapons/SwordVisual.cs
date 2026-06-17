using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash(Attack);

    private const string Attack = "Attack";

    [SerializeField] private Sword sword;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(AttackHash);
    }

    public void TriggerEndAttackAnimation()
    {
        sword.AttackCollideTurnOff();
    }

    private void OnDestroy()
    {
        sword.OnSwordSwing -= Sword_OnSwordSwing;
    }
}
