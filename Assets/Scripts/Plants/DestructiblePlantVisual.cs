using UnityEngine;

public class DestructiblePlantVisual : MonoBehaviour
{
    [SerializeField] private DestructiblePLant destructiblePLant;
    [SerializeField] private GameObject bushDeathVFXprefab;

    private void Start()
    {
        destructiblePLant.OnDestructibleTakeDamage += DestructiblePlant_OnDestructibleTakeDamage;
    }

    private void DestructiblePlant_OnDestructibleTakeDamage(object sender, System.EventArgs e)
    {
        ShowDeathVFX();
    }

    private void ShowDeathVFX()
    {
        Instantiate(bushDeathVFXprefab, destructiblePLant.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        destructiblePLant.OnDestructibleTakeDamage -= DestructiblePlant_OnDestructibleTakeDamage;
    }
}
