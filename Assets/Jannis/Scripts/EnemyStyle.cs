using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Styles/EnemyStyle")]
public class EnemyStyle : ScriptableObject
{
    [SerializeField] private Color headPrimaryColor = Color.black;
    [SerializeField] private Color headSecondaryColor = Color.black;
    [SerializeField] private Color undersideColor = Color.black;
    [SerializeField] private Color stemPrimaryColor = Color.black;
    [SerializeField] private Color stemSecondaryColor = Color.black;


    [SerializeField] private Color gemSecondaryColor = Color.black;
}
