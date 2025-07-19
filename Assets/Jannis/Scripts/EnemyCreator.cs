using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] HeadStyle[] headStyles;
    [SerializeField] GameObject[] gemPrefabs;
    [SerializeField] GameObject eyePrefab;
    [SerializeField] GameObject armaturePrefab;




    public GameObject Create(Transform parent)
    {
        var armatureInstance = InstantiateArmature(parent, out EnemyArmature armature);
        var headStyle = GetRandomHeadStyle();

        var style = headStyle.GetRandomStyle();
        var headInstance = CreateStylisedPartInstance(headStyle.headPrefab, style);
        var eyeInstance = CreateStylisedPartInstance(eyePrefab, style);
        ScatterGems(headInstance.transform, headStyle, out Material gemMaterial);


        armature.Setup(style, headInstance, eyeInstance, gemMaterial);

        return armatureInstance;
    }

    private GameObject InstantiateArmature(Transform parent, out EnemyArmature armature)
    {
        var instance = Instantiate(armaturePrefab, parent);
        armature = instance.GetComponent<EnemyArmature>();
        return instance;

    }
    private HeadStyle GetRandomHeadStyle()
    {
        var randomHead = Random.Range(0, headStyles.Length);
        return headStyles[randomHead];
    }
    private GameObject CreateStylisedPartInstance(GameObject prefab, EnemyStyle style)
    {
        var instance = Instantiate(prefab);
        instance.GetComponent<StyleApplier>().Apply(style);
        return instance;
    }

    private void ScatterGems(Transform scatterFrom, HeadStyle headStyle, out Material gemsMaterial)
    {
        var gemCount = headStyle.randomGemCount;

        var emptyHolder = scatterFrom.GetChild(0);

        List<Transform> empties = new List<Transform>();
        for (int i = 0; i < emptyHolder.childCount; i++)
        {
            empties.Add(emptyHolder.GetChild(i));
        }

        while (empties.Count > gemCount)
        {
            var randomInt = Random.Range(0, empties.Count - 1);
            empties.RemoveAt(randomInt);
        }

        var instances = new List<GameObject>();

        foreach (Transform empty in empties)
        {
            var instance = Instantiate(GetRandomGemPrefab(), empty.position, empty.rotation, scatterFrom);
            Vector3 scale =
            instance.transform.localScale = headStyle.randomSize;
            instance.transform.localPosition = instance.transform.localPosition * 0.97f;
            instances.Add(instance);
        }

        if (instances.Count == 0)
        {
            gemsMaterial = null;
            return;
        }


        gemsMaterial = Instantiate(instances[0].GetComponent<MeshRenderer>().material);
        float randomPrimaryColor = Random.Range(0f, 1f);
        gemsMaterial.SetFloat("_PrimaryColor", randomPrimaryColor);

        foreach (var instance in instances)
        {
            instance.GetComponent<MeshRenderer>().material = gemsMaterial;
        }
    }

    private GameObject GetRandomGemPrefab()
    {
        var randomInt = Random.Range(0, gemPrefabs.Length);
        return gemPrefabs[randomInt];
    }
    
}
