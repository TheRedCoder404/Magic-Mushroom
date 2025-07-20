using UnityEngine;

public class StyleApplierHead : StyleApplier
{
    public override void Apply(EnemyStyle style)
    {
        material.SetColor("_PrimaryColor", style.headPrimaryColor);
        material.SetColor("_SecondaryColor", style.headSecondaryColor);
        material.SetColor("_UndersideColor", style.undersideColor);
        material.SetFloat("_Offset", randomOffset);
    }
}
