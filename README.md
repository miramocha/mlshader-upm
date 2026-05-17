# MLShader Property Documentation

This document describes the user-facing properties for the **MLShader**.

> **Note on UV Indices:** For all properties using a UV Index range (-1 to 3), an index of **-1** uses the **View Projection** model (Matcap-style mapping). Indices 0 through 3 correspond to the standard mesh UV channels (UV0-UV3).

> **Note on Blend Type Indices:** For properties using a Blend Type Index (0 to 3), the mapping is:
> *   **0**: Overwrite
> *   **1**: Multiply
> *   **2**: Screen
> *   **3**: Overlay

## 1. URP System Settings
Settings regarding integration with the Universal Render Pipeline.

*   **Enable URP Soft Shadows** (Toggle): Enables or disables soft shadow filtering for URP lights.
*   **Enable URP Screen Space Shadows** (Toggle): Enables support for screen-space shadows if configured in the URP asset.

## 2. Main Material Settings
Primary colors and textures for the model surface.

*   **Main Color** (Color): The primary tint for the main texture and lit areas.
*   **Main Texture** (2D Texture): The base map for the material.
*   **Main/Shade Texture UV Index** (Range -1 to 3): Selects the UV channel for main and shade textures.
*   **Main/Shade Texture Blend Type Index** (Range 0 to 3): Controls the blend mode (Multiply, Add, etc.) for the shade texture.
*   **Main/Shade Texture Opacity** (Range 0 to 1): Visibility of the secondary shade texture layer.

## 3. Toon Shading Settings
Core parameters for the cel-shaded "toon" effect.

*   **Shade Color** (Color): The color used in shadowed or unlit areas.
*   **Shade Texture** (2D Texture): A texture specifically applied to the shaded areas.
*   **Shading Toony Factor** (Range 0 to 1): Sharpness of the transition between lit and shaded areas.
*   **Shading Shift Factor** (Range -1 to 1): Adjusts the shadow threshold to cover more or less of the model.
*   **Shade Opacity** (Range 0 to 1): Overall intensity of the shadow layer.
*   **Shading Attenuation Invert Lerp Min/Max** (Vector2): Fine-tunes how light attenuation (shadow maps and distance) affects toon stepping.

## 4. Matcap Settings
Material Capture (Matcap) settings for simulated reflections and lighting.

*   **Enable Matcap Indirect Light** (Toggle): When enabled, uses a Matcap calculation for indirect light behavior.
*   **Matcap Shading Toony Factor** (Range 0 to 1): Edge sharpness for the Matcap effect.
*   **Matcap Shading Shift Factor** (Range -1 to 1): Threshold shift for the Matcap highlights/shadows.
*   **Matcap Shading UV Offset** (Vector2): Offsets the spherical UV sampling for the Matcap.

## 5. Normal Mapping
Surface detail and bumpiness settings.

*   **Normal Texture** (2D Normal Map): The normal map texture.
*   **Normal Strength** (Float): Intensity of the surface detail.
*   **Normal Texture UV Index** (Range -1 to 3): The UV channel used for normal mapping.

## 6. Indirect & Ambient Lighting
Settings for environment and background light.

*   **Indirect Light Opacity** (Range 0 to 1): Contribution of environmental light to the final color.
*   **Indirect Light Color Blend Type Index** (Range 0 to 3): Blending mode for indirect light color.
*   **Ambient Color** (Color): A constant flat color added to the material.
*   **Ambient Opacity** (Range 0 to 1): Strength of the ambient color tint.

## 7. Direct Lighting
Behavior of real-time light sources.

*   **Direct Light Toony Factor** (Range 0 to 1): Sharpness of shadow edges from direct light sources.
*   **Direct Light Shift Factor** (Range -1 to 1): Adjusts the coverage area of shadows from direct lights.
*   **Direct Light Opacity** (Range 0 to 1): The intensity of light received from direct sources.

## 8. Rim Light Settings
Outer edge lighting used for silhouette definition.

*   **Rim Light Color** (HDR Color): The color of the rim highlight.
*   **Rim Light Opacity** (Range 0 to 1): Intensity of the rim light effect.
*   **Rim Light Toony Factor** (Range 0 to 1): Sharpness of the rim light transition.
*   **Rim Light Shift Factor** (Float): Expands or contracts the rim light area.
*   **Rim Light Fresnel Power** (Float): Controls the falloff of the rim light relative to the view angle.

## 9. Color Gradient Settings
Vertical or directional color gradients applied to the mesh.

*   **Gradient Main Color** (Color): The start color of the gradient.
*   **Gradient Shade Color** (Color): The end color of the gradient.
*   **Gradient Opacity** (Range 0 to 1): Overall visibility of the gradient overlay.
*   **Gradient UV Rotation** (Range 0 to 360): Rotates the direction of the gradient application.
*   **Gradient Shift Factor** (Float): Moves the midpoint of the color transition.
*   **Gradient Toony Factor** (Range 0 to 1): Sharpness of the gradient transition.
*   **Gradient UV Index** (Range -1 to 3): The UV channel used to calculate the gradient.

## 10. Alpha Mask Settings
Transparency and clipping settings.

*   **Alpha Mask Texture** (2D Texture): Grayscale texture used to define transparency or cutouts.
*   **Alpha Mask Texture UV Index** (Range -1 to 3): The UV channel used for the alpha mask.
