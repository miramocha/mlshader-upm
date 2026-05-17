using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public class MLShaderRoot : MonoBehaviour
{
    private static readonly int RootWorldPositionId = Shader.PropertyToID(name: "_Root_World_Position");
    private static readonly int RootWorldRotationId = Shader.PropertyToID(name: "_Root_World_Rotation");
    private static readonly int DissolveOffsetId = Shader.PropertyToID(name: "_Dissolve_Offset");

    [SerializeField]
    public float DissolveOffset = 0f;

    [SerializeField]
    public List<Renderer> TargetRenderers = new List<Renderer>();

    private MaterialPropertyBlock _materialPropertyBlock;

    private void LateUpdate()
    {
        UpdateTargetRenderers();
    }

    private void OnValidate()
    {
        UpdateTargetRenderers();
    }

    private void UpdateTargetRenderers()
    {
        if (_materialPropertyBlock == null)
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        foreach (var renderer in TargetRenderers)
        {
            if (renderer == null)
            {
                continue;
            }

            // Get existing properties to avoid overwriting other block data if necessary
            renderer.GetPropertyBlock(properties: _materialPropertyBlock);

            SetDissolveOffset(materialPropertyBlock: _materialPropertyBlock);
            SetWorldTransform(materialPropertyBlock: _materialPropertyBlock);

            renderer.SetPropertyBlock(properties: _materialPropertyBlock);
        }
    }

    private void SetDissolveOffset(MaterialPropertyBlock materialPropertyBlock)
    {
        materialPropertyBlock.SetFloat(nameID: DissolveOffsetId, value: DissolveOffset);
    }

    private void SetWorldTransform(MaterialPropertyBlock materialPropertyBlock)
    {
        materialPropertyBlock.SetVector(nameID: RootWorldPositionId, value: transform.position);
        materialPropertyBlock.SetVector(nameID: RootWorldRotationId, value: transform.eulerAngles);
    }
}
