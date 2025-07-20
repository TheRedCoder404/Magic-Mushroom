using UnityEngine;

public class StyleApplierStem : StyleApplier
{
    public override void Apply(EnemyStyle style)
    {
        material.SetColor("_PrimaryColor", style.stemPrimaryColor);
        material.SetColor("_SecondaryColor", style.stemSecondaryColor);
        material.SetColor("_UndersideColor", style.undersideColor);
        material.SetFloat("_Offset", randomOffset);

    }
}
