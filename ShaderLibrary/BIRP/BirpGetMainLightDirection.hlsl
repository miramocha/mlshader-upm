void GetMainLightDirection_float(float3 worldPosition, out float3 lightDirection) {
    #if SHADERGRAPH_PREVIEW
    lightDirection = float3(0.5, 0.5, 0.0);
    #elif defined(BUILTIN_TARGET_API)
    lightDirection = lerp(_WorldSpaceLightPos0.xyz, normalize(_WorldSpaceLightPos0.xyz - worldPosition.xyz), _WorldSpaceLightPos0.w);
    #else
    lightDirection = float3(0.5, 0.5, 0.0);
    #endif
}
