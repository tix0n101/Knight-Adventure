using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Sword sword;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Player.Instance.IsAlive())
            FolowMousePosition();
    }

    public Sword GetActiveWeapon()
    {
        return sword;
    }

    private void FolowMousePosition()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerScreenPosition();

        transform.rotation = Quaternion.Euler(0, mousePos.x < playerPos.x ? 180 : 0, 0);
    }
}
