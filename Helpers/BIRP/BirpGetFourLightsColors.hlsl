
void GetFourLightsColors_float(out float4 light1Color, out float4 light2Color, out float4 light3Color, out float4 light4Color) {
    #if SHADERGRAPH_PREVIEW
    light1Color = float4(0.5, 1.0, 0.5, 1.0);
    light2Color = float4(1.0, 0.5, 0.5, 1.0);
    light3Color = float4(0.5, 0.5, 1.0, 1.0);
    light4Color = float4(1.0, 1.0, 0.5, 1.0);
    #elif defined(BUILTIN_TARGET_API)
    light1Color = (float4) unity_LightColor[0];
    light2Color = (float4) unity_LightColor[1];
    light3Color = (float4) unity_LightColor[2];
    light4Color = (float4) unity_LightColor[3];
    #else
    light1Color = float4(0.0, 0.0, 0.0, 1.0);
    light2Color = float4(0.0, 0.0, 0.0, 1.0);
    light3Color = float4(0.0, 0.0, 0.0, 1.0);
    light4Color = float4(0.0, 0.0, 0.0, 1.0);
    #endif
}
