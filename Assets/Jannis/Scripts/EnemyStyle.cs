using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Styles/EnemyStyle")]
public class EnemyStyle : ScriptableObject
{
    [SerializeField] public Color headPrimaryColor = Color.black;
    [SerializeField] public Color headSecondaryColor = Color.black;
    [SerializeField] public Color undersideColor = Color.black;
    [SerializeField] public Color stemPrimaryColor = Color.black;
    [SerializeField] public Color stemSecondaryColor = Color.black;

    [SerializeField] public Vector2 gemRandomColorRange = new Vector2(0, 1);
}
