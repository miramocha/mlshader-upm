void CalculateLightAttenuation_float(float4 rawScreenPosition, float3 worldPosition, out float output)
{
    #if SHADERGRAPH_PREVIEW
    output = 1.0;
    #elif defined(BUILTIN_TARGET_API)
    struct v2f {
        float4 pos;
        float4 _ShadowCoord;
    };
    v2f lightData;
    float4 clipPos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0));
    lightData.pos = clipPos;
    #if defined(SHADOWS_SCREEN)
        lightData._ShadowCoord = ComputeScreenPos(clipPos);
    #elif defined(SHADOWS_DEPTH) || defined(SHADOWS_SOFT)
        lightData._ShadowCoord = mul(unity_WorldToShadow[0], float4(worldPosition, 1.0));
    #elif defined(SHADOWS_CUBE)
        lightData._ShadowCoord = float4(worldPosition - _LightPositionRange.xyz, 1.0);
    #else
        lightData._ShadowCoord = float4(0, 0, 0, 0);
    #endif

    UNITY_LIGHT_ATTENUATION(
        attenuation,
        lightData,
        worldPosition
    );
    output = attenuation;
    #else
    output = 0.0;
    #endif
}