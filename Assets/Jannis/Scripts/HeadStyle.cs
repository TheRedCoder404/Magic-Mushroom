using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Styles/HeadStyle")]
public class HeadStyle : ScriptableObject
{
    [SerializeField] public GameObject headPrefab;
    [SerializeField] public EnemyStyle[] enemyStyles;
    [SerializeField] Vector2Int gemCountRange = new Vector2Int(5, 8);
    [SerializeField] Vector2 gemSizeRange = new Vector2(0.6f, 1.3f);
    [SerializeField] float gemSizeOffset = 0.4f;


    public int randomGemCount
    {
        get
        {
            return Random.Range(gemCountRange.x, gemCountRange.y + 1);
        }
    }
    private float randomScale
    {
        get
        {
            return Random.Range(gemSizeRange.x, gemSizeRange.y);
        }
    }
    private float randomScaleOffset
    {
        get
        {
            float offset = Random.Range(1 - gemSizeOffset, 1);
            return offset;
        }
    }

    public Vector3 randomSize
    {
        get
        {
            float scale = randomScale;
            return new Vector3(scale * randomScaleOffset, scale * randomScaleOffset, scale * randomScaleOffset);
        }
    }


    public EnemyStyle GetRandomStyle()
    {
        var randomStyle = Random.Range(0, enemyStyles.Length);
        return enemyStyles[randomStyle];
    }
}
