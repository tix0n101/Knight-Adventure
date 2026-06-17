using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private GameObject playerDeathVFXprefab;

    private static readonly int DieHash = Animator.StringToHash(IsDie);
    private static readonly int RunningHash = Animator.StringToHash(IsRunning);

    private const string IsRunning = "isRunning";
    private const string IsDie = "isDie";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private FlashBlink _flashBlink;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += PLayer_OnPLayerDeath;
    }

    private void Update()
    {
        _animator.SetBool(RunningHash, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive())
            AdjustPlayerFacingDirection();
    }

    private void PLayer_OnPLayerDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(DieHash, true);
        _flashBlink.StopBlinking();;
    }

    //private void ShowDeathVFX()
    //{
    //    Instantiate(playerDeathVFXprefab, transform.position, Quaternion.identity);
    //}

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerScreenPosition();

        _spriteRenderer.flipX = mousePos.x < playerPos.x;
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= PLayer_OnPLayerDeath;
    }
}
