using UnityEngine;

/** Color **/
[System.Serializable]
public class ColorModiferSettingsGroup
{
    public GroupColorOverrideSettings ColorOverrideSettings = new GroupColorOverrideSettings();

    public GroupPostProcessingSettings PostProcessingSettings = new GroupPostProcessingSettings();
}

[System.Serializable]
public class GroupColorOverrideSettings
{
    [ColorUsage(true, true)]
    public Color BaseColorOverride = Color.clear;

    [ColorUsage(true, true)]
    public Color ShadeColorOverride = Color.clear;
}

[System.Serializable]
public class GroupPostProcessingSettings
{
    public Vector3 HslShift = Vector3.zero;
    public bool BaseShadeInverted = false;
}

/** Lighting **/
[System.Serializable]
public class LightingModifierSettingsGroup
{
    public MatcapLightingModifierSettings MatcapLightingModifierSettings =
        new MatcapLightingModifierSettings();
}

[System.Serializable]
public class MatcapLightingModifierSettings
{
    public Vector2 MatcapShadingUVOffset = Vector2.zero;
}

[ExecuteAlways]
public class MLGlobalShaderController : MonoBehaviour
{
    // Color
    private static readonly int HslShiftMatrixId = Shader.PropertyToID("_HSL_Shift_Group_Matrix");
    private static readonly int InvertBaseShadeVectorId = Shader.PropertyToID(
        "_Invert_Base_Shade_Group_Vector4"
    );
    private static readonly int BaseColorOverrideMatrixId = Shader.PropertyToID(
        "_Base_Color_Override_Group_Matrix"
    );
    private static readonly int ShadeColorOverrideMatrixId = Shader.PropertyToID(
        "_Shade_Color_Override_Group_Matrix"
    );

    // Lighting
    private static readonly int MatcapShadingUVOffsetMatrixId = Shader.PropertyToID(
        "_Matcap_Shading_UV_Offset_Group_Matrix"
    );

    public ColorModiferSettingsGroup ColorModifierGroup1Settings = new ColorModiferSettingsGroup();

    public ColorModiferSettingsGroup ColorModifierGroup2Settings = new ColorModiferSettingsGroup();

    public ColorModiferSettingsGroup ColorModifierGroup3Settings = new ColorModiferSettingsGroup();

    public ColorModiferSettingsGroup ColorModifierGroup4Settings = new ColorModiferSettingsGroup();

    public LightingModifierSettingsGroup LightingModifierGroup1Settings =
        new LightingModifierSettingsGroup();

    public LightingModifierSettingsGroup LightingModifierGroup2Settings =
        new LightingModifierSettingsGroup();

    public LightingModifierSettingsGroup LightingModifierGroup3Settings =
        new LightingModifierSettingsGroup();

    public LightingModifierSettingsGroup LightingModifierGroup4Settings =
        new LightingModifierSettingsGroup();

    private void Awake()
    {
        ResetGlobalColorModifierShaderVariables();
        UpdateShaderColorModifierGlobalVariables();
        ResetGlobalLightingModifierShaderVariables();
        UpdateShaderLightingModifierGlobalVariables();
    }

    private void LateUpdate()
    {
        UpdateShaderColorModifierGlobalVariables();
        UpdateShaderLightingModifierGlobalVariables();
    }

    private void OnValidate()
    {
        UpdateShaderColorModifierGlobalVariables();
        UpdateShaderLightingModifierGlobalVariables();
    }

    private void OnDestroy()
    {
        ResetGlobalColorModifierShaderVariables();
        ResetGlobalLightingModifierShaderVariables();
    }

    private static void ResetGlobalColorModifierShaderVariables()
    {
        Shader.SetGlobalMatrix(nameID: HslShiftMatrixId, value: Matrix4x4.zero);
        Shader.SetGlobalVector(nameID: InvertBaseShadeVectorId, value: Vector4.zero);
        Shader.SetGlobalMatrix(nameID: BaseColorOverrideMatrixId, value: Matrix4x4.zero);
        Shader.SetGlobalMatrix(nameID: ShadeColorOverrideMatrixId, value: Matrix4x4.zero);
    }

    private static void ResetGlobalLightingModifierShaderVariables()
    {
        Shader.SetGlobalMatrix(nameID: MatcapShadingUVOffsetMatrixId, value: Matrix4x4.zero);
    }

    public void UpdateShaderColorModifierGlobalVariables()
    {
        UpdateGlobalHslShift();
        UpdateGlobalBaseShadeInverted();
        UpdateGlobalBaseColorOverride();
        UpdateGlobalShadeColorOverride();
    }

    public void UpdateShaderLightingModifierGlobalVariables()
    {
        UpdateGlobalMatcapShadingUVOffset();
    }

    private void UpdateGlobalMatcapShadingUVOffset()
    {
        Matrix4x4 matcapShadingUVOffsetMatrix = new Matrix4x4();
        matcapShadingUVOffsetMatrix.SetRow(
            0,
            (Vector4)
                LightingModifierGroup1Settings.MatcapLightingModifierSettings.MatcapShadingUVOffset
        );
        matcapShadingUVOffsetMatrix.SetRow(
            1,
            (Vector4)
                LightingModifierGroup2Settings.MatcapLightingModifierSettings.MatcapShadingUVOffset
        );
        matcapShadingUVOffsetMatrix.SetRow(
            2,
            (Vector4)
                LightingModifierGroup3Settings.MatcapLightingModifierSettings.MatcapShadingUVOffset
        );
        matcapShadingUVOffsetMatrix.SetRow(
            3,
            (Vector4)
                LightingModifierGroup4Settings.MatcapLightingModifierSettings.MatcapShadingUVOffset
        );
        Shader.SetGlobalMatrix(
            nameID: MatcapShadingUVOffsetMatrixId,
            value: matcapShadingUVOffsetMatrix
        );
    }

    private void UpdateGlobalBaseColorOverride()
    {
        Matrix4x4 baseColorOverrideGroupMatrix = new Matrix4x4();
        baseColorOverrideGroupMatrix.SetRow(
            0,
            (Vector4)ColorModifierGroup1Settings.ColorOverrideSettings.BaseColorOverride
        );
        baseColorOverrideGroupMatrix.SetRow(
            1,
            (Vector4)ColorModifierGroup2Settings.ColorOverrideSettings.BaseColorOverride
        );
        baseColorOverrideGroupMatrix.SetRow(
            2,
            (Vector4)ColorModifierGroup3Settings.ColorOverrideSettings.BaseColorOverride
        );
        baseColorOverrideGroupMatrix.SetRow(
            3,
            (Vector4)ColorModifierGroup4Settings.ColorOverrideSettings.BaseColorOverride
        );
        Shader.SetGlobalMatrix(
            nameID: BaseColorOverrideMatrixId,
            value: baseColorOverrideGroupMatrix
        );
    }

    private void UpdateGlobalShadeColorOverride()
    {
        Matrix4x4 shadeColorOverrideGroupMatrix = new Matrix4x4();
        shadeColorOverrideGroupMatrix.SetRow(
            0,
            (Vector4)ColorModifierGroup1Settings.ColorOverrideSettings.ShadeColorOverride
        );
        shadeColorOverrideGroupMatrix.SetRow(
            1,
            (Vector4)ColorModifierGroup2Settings.ColorOverrideSettings.ShadeColorOverride
        );
        shadeColorOverrideGroupMatrix.SetRow(
            2,
            (Vector4)ColorModifierGroup3Settings.ColorOverrideSettings.ShadeColorOverride
        );
        shadeColorOverrideGroupMatrix.SetRow(
            3,
            (Vector4)ColorModifierGroup4Settings.ColorOverrideSettings.ShadeColorOverride
        );
        Shader.SetGlobalMatrix(
            nameID: ShadeColorOverrideMatrixId,
            value: shadeColorOverrideGroupMatrix
        );
    }

    private void UpdateGlobalHslShift()
    {
        Matrix4x4 hslShiftMatrix = new Matrix4x4();
        hslShiftMatrix.SetRow(
            0,
            (Vector4)ColorModifierGroup1Settings.PostProcessingSettings.HslShift
        );
        hslShiftMatrix.SetRow(
            1,
            (Vector4)ColorModifierGroup2Settings.PostProcessingSettings.HslShift
        );
        hslShiftMatrix.SetRow(
            2,
            (Vector4)ColorModifierGroup3Settings.PostProcessingSettings.HslShift
        );
        hslShiftMatrix.SetRow(
            3,
            (Vector4)ColorModifierGroup4Settings.PostProcessingSettings.HslShift
        );
        Shader.SetGlobalMatrix(nameID: HslShiftMatrixId, value: hslShiftMatrix);
    }

    private void UpdateGlobalBaseShadeInverted()
    {
        Vector4 baseShadeInverted = new Vector4(
            ColorModifierGroup1Settings.PostProcessingSettings.BaseShadeInverted ? 1 : 0,
            ColorModifierGroup2Settings.PostProcessingSettings.BaseShadeInverted ? 1 : 0,
            ColorModifierGroup3Settings.PostProcessingSettings.BaseShadeInverted ? 1 : 0,
            ColorModifierGroup4Settings.PostProcessingSettings.BaseShadeInverted ? 1 : 0
        );
        Shader.SetGlobalVector(nameID: InvertBaseShadeVectorId, value: baseShadeInverted);
    }
}
