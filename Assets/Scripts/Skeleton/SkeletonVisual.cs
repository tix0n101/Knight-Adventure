using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class SkeletonVisual : MonoBehaviour
{
    private static readonly int DieHash = Animator.StringToHash(IsDie);
    private static readonly int RunningHash = Animator.StringToHash(IsRunning);
    private static readonly int SpeedMultiplierHash = Animator.StringToHash(ChasingSpeedMultiplier);
    private static readonly int AttackHash = Animator.StringToHash(Attack);
    private static readonly int TakeHitHash = Animator.StringToHash(TakeHit);

    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject enemyShadow;

    private Animator _animator;

    private const string IsRunning = "isRunning";
    private const string ChasingSpeedMultiplier = "ChasingSpeedMultiplier";
    private const string Attack = "Attack";
    private const string TakeHit = "TakeHit";
    private const string IsDie = "isDie";

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }
    private void Update()
    {
        _animator.SetBool(RunningHash, enemyAI.IsRunning);
        _animator.SetFloat(SpeedMultiplierHash, enemyAI.GetRoamingSpeedAcceleration());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.AttackCollideTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.AttackCollideTurnOn();
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(DieHash, true);
        _spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TakeHitHash);
    }

    private void EnemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(AttackHash);
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit -= EnemyEntity_OnTakeHit;
        enemyEntity.OnDeath -= EnemyEntity_OnDeath;
    }
}
