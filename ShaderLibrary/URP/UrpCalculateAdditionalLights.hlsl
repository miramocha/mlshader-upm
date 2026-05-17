void CalculateAdditionalLights_float(float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, out float Diffuse, out float3 Specular, out float3 Color)
{
    Diffuse = MainDiffuse;
    Specular = MainSpecular;
    Color = MainColor * (MainDiffuse + MainSpecular);

    #ifndef SHADERGRAPH_PREVIEW
        uint pixelLightCount = GetAdditionalLightsCount();

    #if USE_FORWARD_PLUS
        // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        InputData inputData = (InputData)0;
        float4 screenPos = ComputeScreenPos(TransformWorldToHClip(WorldPosition));
        inputData.normalizedScreenSpaceUV = screenPos.xy / screenPos.w;
        inputData.positionWS = WorldPosition;
    #endif

        LIGHT_LOOP_BEGIN(pixelLightCount)
            // Convert the pixel light index to the light data index
            #if !USE_FORWARD_PLUS
                lightIndex = GetPerObjectLightIndex(lightIndex);
            #endif
            // Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
            Light light = GetAdditionalPerObjectLight(lightIndex, WorldPosition);
            // Manually set the shadow attenuation by calculating realtime shadows
            light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
            float NdotL = saturate(dot(WorldNormal, light.direction));
            float atten = light.distanceAttenuation * light.shadowAttenuation;
            float thisDiffuse = atten * NdotL;
            float3 thisSpecular = LightingSpecular(thisDiffuse, light.direction, WorldNormal, WorldView, 1, Smoothness);
            Diffuse += thisDiffuse;
            Specular += thisSpecular;
            #if defined(_LIGHT_COOKIES)
                float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
                light.color *= cookieColor;
            #endif
            Color += light.color * (thisDiffuse + thisSpecular);
        LIGHT_LOOP_END
        float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        Color = total <= 0 ? MainColor : Color / total;
    #endif
}