# MLShader

**MLShader** is a Unity **Shader Graph** package for stylized **toon / cel** materials on the **Universal Render Pipeline (URP)**. It ships the main **MLShader** graph, reusable subgraphs, HLSL helpers, optional runtime scripts, and vendored Unity sample subgraphs so consumers do not need to import official Shader Graph samples.

The graph also includes **partial Built-in Render Pipeline (BIRP)** support: some subgraphs and `ShaderLibrary/BIRP` HLSL are used when the target is the legacy **Built-in** pipeline. **URP is the primary, fully intended target**; Built-in coverage is best-effort and may not match every URP feature path.

| | |
|---|---|
| **Unity package** | `com.miragamedev.mlshader` |
| **Minimum editor** | **Unity 6000.4** (`6000.4.0f1` or compatible) |
| **Render pipeline** | **URP** (primary). **Built-in (BIRP):** partial — see intro above |
| **Repository** | [github.com/miramocha/mlshader-upm](https://github.com/miramocha/mlshader-upm) |

Dependencies are declared in [`package.json`](package.json) (notably **Shader Graph** and **URP** 17.4.x).

---

## Requirements

- **Unity 6** (6000.4 recommended; see `package.json` fields `unity` / `unityRelease`).
- **URP** is the main supported pipeline (`package.json` lists `com.unity.render-pipelines.universal`). Use URP for the intended feature set and quality bar.
- **Built-in (BIRP):** you can use materials in a Built-in project where the shader’s BIRP branches apply, but support is **partial** and not all URP-only behavior is reproduced.
- **HDRP** is not a supported target for this graph.
- **Shader Graph** (installed with this package via UPM dependencies).

---

## Installation

### Option A — Add package from Git URL (recommended)

1. In Unity, open **Window → Package Manager**.
2. Click **+** → **Add package from git URL…**
3. Paste:

   ```text
   https://github.com/miramocha/mlshader-upm.git
   ```

4. Optional: pin a **branch** or **tag** by appending `#` after `.git`:

   ```text
   https://github.com/miramocha/mlshader-upm.git#main
   https://github.com/miramocha/mlshader-upm.git#develop
   https://github.com/miramocha/mlshader-upm.git#v1.0.0
   ```

   Prefer **`#v…` tags** for reproducible installs. **Release tags** use a **`v` prefix** (e.g. Git tag `v1.0.0`). The [`package.json`](package.json) `version` field stays plain SemVer (**`1.0.0`**, no `v`) — bump that on the tagged commit, then create and push the matching `v…` tag on GitHub.

5. Unity clones the repository. This repo’s **package root** is the repository root (where [`package.json`](package.json) lives).

### Option B — Edit `Packages/manifest.json`

Add (or merge) under `"dependencies"` in your Unity project:

```json
{
  "dependencies": {
    "com.miragamedev.mlshader": "https://github.com/miramocha/mlshader-upm.git#v1.0.0"
  }
}
```

Use `#branch` (e.g. **`main`**, **`develop`**), `#v1.0.0` (or another tag), or a full commit hash after `.git`. The dependency name must be **`com.miragamedev.mlshader`** (see `package.json` in this repo).

### Repository branches

| Branch | Role |
|--------|------|
| **`main`** | Default branch; release commits and **`v*`** tags are expected here. |
| **`develop`** | Day-to-day integration; merge into **`main`** when ready to release. |

There is no **`stable`** or **`release`** branch — **Git tags** (`v…`) are the supported way to pin versions.

### Option C — Local development (embedded / file path)

To test changes without pushing to Git, add a **local** dependency pointing at this folder (the directory that contains `package.json`):

```json
"com.miragamedev.mlshader": "file:../../path/to/MLShader/package/root"
```

Adjust the relative path from your project’s `Packages` folder so it resolves correctly.

---

## Optional samples

The package registers a UPM **sample** that copies example assets into your project:

1. **Window → Package Manager** → select **MLShader**.
2. Open the **Samples** list.
3. Import **MLShader Examples** (source path in the package: `Samples~/MLShaderExamples`).

Samples are optional; the core graphs and subgraphs work without importing them.

---

## Quick start

1. Create or select a **Material**.
2. Assign the shader **MLShader** (from Shader Graph `Graph/MLShader.shadergraph`, imported as a Shader Graph asset).
3. Tune properties on the material (see **Material property reference** below, or the **Custom** `ShaderGUI` if enabled in your project).

Runtime helpers (optional):

- `MiraGameDev.MLShader.MLShaderRoot` — per-renderer dissolve / root transform properties.
- `MiraGameDev.MLShader.MLGlobalShaderController` — global HSL / color groups for shader tuning.

---

## Package layout (overview)

| Path | Purpose |
|------|---------|
| `Graph/` | Main Shader Graph and fullscreen graphs; `Subgraphs/` for reusable blocks |
| `ShaderLibrary/` | HLSL: `URP/` for URP; `BIRP/` for **partial** Built-in pipeline paths used by the graph |
| `ThirdParty/ShaderGraph/` | Vendored Unity Shader Graph sample/template subgraphs ([`Third Party Notices.md`](Third%20Party%20Notices.md)) |
| `Runtime/` | Runtime C# (`MiraGameDev.MLShader` assembly) |
| `Editor/` | Editor C# (`MiraGameDev.MLShader.Editor`) |
| `Samples~/MLShaderExamples/` | Optional importable examples (UPM sample) |
| `Textures/` | Shared textures used by the package |

---

## License and third-party content

Vendored Shader Graph assets are described in **[`Third Party Notices.md`](Third%20Party%20Notices.md)**. Keep that file with the package if you redistribute it.

---

## Changelog

See **[`CHANGELOG.md`](CHANGELOG.md)**.

---

# Material property reference

This section documents user-facing properties on the **MLShader** material.

> **UV indices:** For properties using a UV index in the range **-1** to **3**, **-1** uses **view projection** (matcap-style) mapping. **0–3** map to mesh UV channels **UV0–UV3**.

> **Blend type indices:** For properties using a blend type index **0** to **3**:
> - **0**: Overwrite  
> - **1**: Multiply  
> - **2**: Screen  
> - **3**: Overlay  

## 1. URP system settings

Integration with the Universal Render Pipeline.

- **Enable URP Soft Shadows** (Toggle): Soft shadow filtering for URP lights.
- **Enable URP Screen Space Shadows** (Toggle): Support for screen-space shadows when configured on the URP asset.

## 2. Main material settings

- **Main Color** (Color): Tint for the main texture and lit areas.
- **Main Texture** (2D Texture): Base color map.
- **Main/Shade Texture UV Index** (Range -1 to 3): UV channel for main and shade textures.
- **Main/Shade Texture Blend Type Index** (Range 0 to 3): Blend mode for the shade texture layer.
- **Main/Shade Texture Opacity** (Range 0 to 1): Strength of the shade texture layer.

## 3. Toon shading settings

- **Shade Color** (Color): Color in shadowed or unlit regions.
- **Shade Texture** (2D Texture): Texture for shaded areas.
- **Shading Toony Factor** (Range 0 to 1): Sharpness of the lit/shadow boundary.
- **Shading Shift Factor** (Range -1 to 1): Moves the shadow threshold.
- **Shade Opacity** (Range 0 to 1): Intensity of the shade layer.
- **Shading Attenuation Invert Lerp Min/Max** (Vector2): Fine-tunes light attenuation vs. toon stepping.

## 4. Matcap settings

- **Enable Matcap Indirect Light** (Toggle): Matcap-style indirect contribution.
- **Matcap Shading Toony Factor** (Range 0 to 1): Edge sharpness for matcap shading.
- **Matcap Shading Shift Factor** (Range -1 to 1): Threshold shift for matcap highlights/shadows.
- **Matcap Shading UV Offset** (Vector2): Offsets spherical UV sampling.

## 5. Normal mapping

- **Normal Texture** (2D Normal Map): Normal map.
- **Normal Strength** (Float): Normal intensity.
- **Normal Texture UV Index** (Range -1 to 3): UV channel for normals.

## 6. Indirect and ambient lighting

- **Indirect Light Opacity** (Range 0 to 1): Environment light contribution.
- **Indirect Light Color Blend Type Index** (Range 0 to 3): Blend mode for indirect tint.
- **Ambient Color** (Color): Flat ambient tint.
- **Ambient Opacity** (Range 0 to 1): Ambient strength.

## 7. Direct lighting

- **Direct Light Toony Factor** (Range 0 to 1): Sharpness of direct-light shadow edges.
- **Direct Light Shift Factor** (Range -1 to 1): Coverage of direct-light shadows.
- **Direct Light Opacity** (Range 0 to 1): Direct light intensity.

## 8. Rim light settings

- **Rim Light Color** (HDR Color): Rim tint.
- **Rim Light Opacity** (Range 0 to 1): Rim strength.
- **Rim Light Toony Factor** (Range 0 to 1): Sharpness of the rim transition.
- **Rim Light Shift Factor** (Float): Expands or contracts the rim.
- **Rim Light Fresnel Power** (Float): View-angle falloff of the rim.

## 9. Color gradient settings

- **Gradient Main Color** (Color): Gradient start color.
- **Gradient Shade Color** (Color): Gradient end color.
- **Gradient Opacity** (Range 0 to 1): Overall gradient visibility.
- **Gradient UV Rotation** (Range 0 to 360): Rotates gradient direction.
- **Gradient Shift Factor** (Float): Shifts the gradient midpoint.
- **Gradient Toony Factor** (Range 0 to 1): Sharpness of the gradient.
- **Gradient UV Index** (Range -1 to 3): UV channel driving the gradient.

## 10. Alpha mask settings

- **Alpha Mask Texture** (2D Texture): Mask for transparency or cutouts.
- **Alpha Mask Texture UV Index** (Range -1 to 3): UV channel for the mask.
