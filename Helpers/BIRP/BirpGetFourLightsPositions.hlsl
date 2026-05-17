void GetFourLightsPositions_float(out float4 fourLightsXPosition, out float4 fourLightsYPosition, out float4 fourLightsZPosition) {
    #if SHADERGRAPH_PREVIEW
    fourLightsXPosition = float4(1.0, 0.0, 0.0, 0.0);
    fourLightsYPosition = float4(0.0, 1.0, 0.0, -1.0);
    fourLightsZPosition = float4(0.0, 0.0, 1.0, 0.0);
    #elif defined(BUILTIN_TARGET_API)
    fourLightsXPosition = unity_4LightPosX0;
    fourLightsYPosition = unity_4LightPosY0;
    fourLightsZPosition = unity_4LightPosZ0;
    #else
    fourLightsXPosition = float4(0.0, 0.0, 0.0, 0.0);
    fourLightsYPosition = float4(0.0, 0.0, 0.0, 0.0);
    fourLightsZPosition = float4(0.0, 0.0, 0.0, 0.0);
    #endif
}