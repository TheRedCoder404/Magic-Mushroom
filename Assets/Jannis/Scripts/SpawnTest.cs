using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField] private EnemyCreator creator;

    private void Start()
    {
        creator.Create(transform);
    }
}
