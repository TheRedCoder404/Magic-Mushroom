using System;
using UnityEngine;

public class EnemyArmature : MonoBehaviour
{
    [SerializeField] Transform[] m_Eyes;
    [SerializeField] Transform m_Head;
    [SerializeField] GameObject m_Stem;

    private Material gemMaterial; // all the gems of the Object

    public void Setup(EnemyStyle stemStyle, GameObject Head, GameObject Eye, Material gemMaterial)
    {
        Eye.transform.SetParent(m_Eyes[0], false);
        var Eye2 = Instantiate(Eye);
        Eye2.transform.SetParent(m_Eyes[1], false);

        Head.transform.SetParent(m_Head, false);
        m_Stem.GetComponent<StyleApplier>().Apply(stemStyle);
        this.gemMaterial = gemMaterial;
    }
}

