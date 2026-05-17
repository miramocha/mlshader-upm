using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MLGlobalShaderController))]
public class MLGlobalShaderControllerEditor : Editor
{
    private bool showColorModifierGroup1 = true;
    private bool showColorModifierGroup2 = true;
    private bool showColorModifierGroup3 = true;
    private bool showColorModifierGroup4 = true;

    private bool showLightingModifierGroup1 = true;
    private bool showLightingModifierGroup2 = true;
    private bool showLightingModifierGroup3 = true;
    private bool showLightingModifierGroup4 = true;

    public override void OnInspectorGUI()
    {
        // Pull current values into the serialized object
        serializedObject.Update();

        MLGlobalShaderController controller = (MLGlobalShaderController)target;

        // EditorGUILayout.LabelField(label: "ML Global Shader Control", style: EditorStyles.boldLabel);
        // EditorGUILayout.HelpBox(
        //     message: "Configures global shader variables for HSL shifts and color overrides across four groups.",
        //     type: MessageType.Info
        // );

        // Draw individual groups with foldouts
        DrawColorModifierGroup(
            label: "Color Modifier Group 1",
            property: serializedObject.FindProperty(propertyPath: "ColorModifierGroup1Settings"),
            foldout: ref showColorModifierGroup1
        );
        DrawColorModifierGroup(
            label: "Color Modifier Group 2",
            property: serializedObject.FindProperty(propertyPath: "ColorModifierGroup2Settings"),
            foldout: ref showColorModifierGroup2
        );
        DrawColorModifierGroup(
            label: "Color Modifier Group 3",
            property: serializedObject.FindProperty(propertyPath: "ColorModifierGroup3Settings"),
            foldout: ref showColorModifierGroup3
        );
        DrawColorModifierGroup(
            label: "Color Modifier Group 4",
            property: serializedObject.FindProperty(propertyPath: "ColorModifierGroup4Settings"),
            foldout: ref showColorModifierGroup4
        );
        DrawLightingModifierGroup(
            label: "Lighting Modifier Group 1",
            property: serializedObject.FindProperty(propertyPath: "LightingModifierGroup1Settings"),
            foldout: ref showLightingModifierGroup1
        );
        DrawLightingModifierGroup(
            label: "Lighting Modifier Group 2",
            property: serializedObject.FindProperty(propertyPath: "LightingModifierGroup2Settings"),
            foldout: ref showLightingModifierGroup2
        );
        DrawLightingModifierGroup(
            label: "Lighting Modifier Group 3",
            property: serializedObject.FindProperty(propertyPath: "LightingModifierGroup3Settings"),
            foldout: ref showLightingModifierGroup3
        );
        DrawLightingModifierGroup(
            label: "Lighting Modifier Group 4",
            property: serializedObject.FindProperty(propertyPath: "LightingModifierGroup4Settings"),
            foldout: ref showLightingModifierGroup4
        );

        if (GUILayout.Button(text: "Force Sync Shader Globals", options: GUILayout.Height(30)))
        {
            controller.UpdateShaderColorModifierGlobalVariables();
            controller.UpdateShaderLightingModifierGlobalVariables();
        }

        // Apply changes and trigger update if something changed
        if (serializedObject.ApplyModifiedProperties())
        {
            controller.UpdateShaderColorModifierGlobalVariables();
            controller.UpdateShaderLightingModifierGlobalVariables();
        }
    }

    private void DrawColorModifierGroup(string label, SerializedProperty property, ref bool foldout)
    {
        EditorGUILayout.BeginVertical();
        foldout = EditorGUILayout.Foldout(
            foldout: foldout,
            content: label,
            toggleOnLabelClick: true
        );
        if (foldout)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                property: property.FindPropertyRelative(
                    relativePropertyPath: "ColorOverrideSettings"
                ),
                includeChildren: true
            );
            EditorGUILayout.PropertyField(
                property: property.FindPropertyRelative(
                    relativePropertyPath: "PostProcessingSettings"
                ),
                includeChildren: true
            );

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(width: 2f);
    }

    private void DrawLightingModifierGroup(
        string label,
        SerializedProperty property,
        ref bool foldout
    )
    {
        EditorGUILayout.BeginVertical();
        foldout = EditorGUILayout.Foldout(
            foldout: foldout,
            content: label,
            toggleOnLabelClick: true
        );
        if (foldout)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(
                property: property.FindPropertyRelative(
                    relativePropertyPath: "MatcapLightingModifierSettings"
                ),
                includeChildren: true
            );
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(width: 2f);
    }
}
