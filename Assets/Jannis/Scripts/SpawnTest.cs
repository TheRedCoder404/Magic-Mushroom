using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField] private EnemyCreator creator;

    private void Awake()
    {
        creator.Create(transform);
    }
}
