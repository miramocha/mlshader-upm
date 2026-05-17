void GetLightColor_float(out float4 lightColor) {
    #if SHADERGRAPH_PREVIEW
    lightColor = float4(0.5, 1.0, 0.5, 1.0);
    #elif defined(_LightColor0)
    lightColor = (float4) _LightColor0;
    #else
    lightColor = float4(0.0, 0.0, 0.0, 1.0);
    #endif
}