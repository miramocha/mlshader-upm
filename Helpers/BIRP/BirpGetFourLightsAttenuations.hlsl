void GetFourLightsAttenuations_float(out float4 fourLightsAttenuations) {
    #if SHADERGRAPH_PREVIEW
    fourLightsAttenuations = float4(1.0, 1.0, 1.0, 1.0);
    // #elif defined(unity_4LightAtten0)
    #elif defined(BUILTIN_TARGET_API)
    fourLightsAttenuations = unity_4LightAtten0;
    #else
    fourLightsAttenuations = float4(1.0, 1.0, 1.0, 1.0);
    #endif
}