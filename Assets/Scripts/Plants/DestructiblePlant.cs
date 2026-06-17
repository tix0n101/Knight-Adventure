using UnityEngine;

public class DestructiblePLant : MonoBehaviour
{
    public event System.EventHandler OnDestructibleTakeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Sword>())
        {
            Destroy(gameObject);
            OnDestructibleTakeDamage?.Invoke(this, System.EventArgs.Empty);
            NavMeshSurfaceManagement.Instance.RebakeNavMeshSurface();
        }
    }

}
