void CalculateShade4PointLights_float (float3 worldPosition, float3 worldNormal, out float3 color)
{
    #if SHADERGRAPH_PREVIEW
    color = float3(0.0, 0.0, 0.0);
    #elif defined(unity_4LightPosX0)
    color = Shade4PointLights(
    unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
    unity_LightColor[0], unity_LightColor[1], unity_LightColor[2], unity_LightColor[3],
    unity_4LightAtten0,
    worldPosition, worldNormal
    );
    #else
    color = float3(0.0, 0.0, 0.0);
    #endif
}