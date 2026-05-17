# Changelog

All notable changes to this package are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2026-05-16

### Added

- UPM package manifest with `com.unity.shadergraph` and `com.unity.render-pipelines.universal` dependencies.
- Optional **Samples** entry for example content under `Samples~/MLShaderExamples`.
- `Third Party Notices.md` for vendored Unity Shader Graph sample/template subgraphs.

### Changed

- Package layout aligned with common Unity UPM conventions: `Runtime/`, `ShaderLibrary/`, `Graph/` (subgraphs and fullscreen graphs), `ThirdParty/ShaderGraph/`, `Samples~/MLShaderExamples/`.
- Subgraph assets: removed `_MLS` prefix from filenames where applicable (GUIDs unchanged).
- C# scripts use `MiraGameDev.MLShader` namespace; assembly definitions use `rootNamespace`.
