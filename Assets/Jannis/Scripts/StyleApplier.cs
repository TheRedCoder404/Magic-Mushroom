using UnityEngine;

public abstract class StyleApplier : MonoBehaviour
{
    protected Material material;

    protected float randomOffset
    {
        get
        {
            return Random.Range(0.0f, 1000f);
        }
    }

    private void Awake()
    {
        SetupMaterial();
    }

    private void SetupMaterial()
    {
        var renderer = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedRenderer;
        bool hasNormalRenderer = renderer != null;
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();

        material = hasNormalRenderer ? renderer.material : skinnedRenderer.material;

        if(hasNormalRenderer)
            renderer.material = material;
        else
            skinnedRenderer.material = material;
    }

    public virtual void Apply(EnemyStyle style){}
}
