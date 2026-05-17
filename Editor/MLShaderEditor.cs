
#if UNITY_EDITOR 
using UnityEditor;
using UnityEngine;

namespace MiraGameDev.MLShader.Editor
{
public class MLShaderEditor : ShaderGUI
{
    private bool showBaseSettings = true;
    private bool showGradientSettings = true;
    private bool showLightingSettings = true;

    private bool useDefaultShaderGraphEditor = false;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        useDefaultShaderGraphEditor = EditorGUILayout.Toggle(
            label: "Use Default Editor",
            value: useDefaultShaderGraphEditor
        );

        if (useDefaultShaderGraphEditor)
        {
            DrawDefaultShaderGraphEditor(materialEditor: materialEditor, properties: properties);
            return;
        }

        showBaseSettings = EditorGUILayout.Foldout(
            foldout: showBaseSettings,
            content: "Base",
            toggleOnLabelClick: true
        );
        if (showBaseSettings)
        {
            DrawProperty(
                materialEditor: materialEditor,
                properties: properties,
                propertyName: "_Base_Lit_Color",
                label: "Base Lit Color"
            );

            DrawProperty(
                materialEditor: materialEditor,
                properties: properties,
                propertyName: "_Base_Shadow_Color",
                label: "Base Shadow Color"
            );

            DrawSlider(
                properties: properties,
                propertyName: "_Base_Opacity",
                label: "Base Opacity",
                min: 0f,
                max: 1f
            );
        }

        showGradientSettings = EditorGUILayout.Foldout(
            foldout: showGradientSettings,
            content: "Gradient",
            toggleOnLabelClick: true
        );
        if (showGradientSettings)
        {
            DrawProperty(
                materialEditor: materialEditor,
                properties: properties,
                propertyName: "_Gradient_Lit_Color",
                label: "Gradient Lit Color"
            );

            DrawProperty(
                materialEditor: materialEditor,
                properties: properties,
                propertyName: "_Gradient_Shadow_Color",
                label: "Gradient Shadow Color"
            );

            DrawSlider(
                properties: properties,
                propertyName: "_Gradient_Opacity",
                label: "Gradient Opacity",
                min: 0f,
                max: 1f
            );

            DrawUVDropdown(
                properties: properties,
                propertyName: "_Gradient_UV_Index",
                label: "Gradient UV"
            );
        }

        showLightingSettings = EditorGUILayout.Foldout(
            foldout: showLightingSettings,
            content: "Lighting",
            toggleOnLabelClick: true
        );

        if (showLightingSettings)
        {
            DrawVector2Pad(properties: properties, propertyName: "_Lighting_Circle_Offset");
            DrawSlider(
                properties: properties,
                propertyName: "_Lighting_Circle_Diameter",
                label: "Lighting Circle Diameter",
                min: 0f,
                max: 1f
            );

            DrawVector2Pad(
                properties: properties,
                propertyName: "_Star_Highlight_Center",
                invertX: false,
                invertY: true,
                center: new Vector2(0.5f, 0.5f),
                range: 1.0f
            );
        }

        drawImage = EditorGUILayout.Foldout(
            foldout: drawImage,
            content: "Draw Test Image",
            toggleOnLabelClick: true
        );
        if (drawImage)
        {
            Texture testimage = (Texture)
                AssetDatabase.LoadAssetAtPath("Assets/testimage.png", typeof(Texture));
            GUILayout.Box(testimage);
        }
    }

    private bool drawImage = false;

    private void DrawDefaultShaderGraphEditor(
        MaterialEditor materialEditor,
        MaterialProperty[] properties
    )
    {
        base.OnGUI(materialEditor: materialEditor, properties: properties);
    }

    private void DrawProperty(
        MaterialEditor materialEditor,
        MaterialProperty[] properties,
        string propertyName,
        string label = null
    )
    {
        MaterialProperty property = FindProperty(
            propertyName: propertyName,
            properties: properties,
            propertyIsMandatory: false
        );
        if (property != null)
        {
            materialEditor.ShaderProperty(prop: property, label: label ?? property.displayName);
        }
    }

    private void DrawSlider(
        MaterialProperty[] properties,
        string propertyName,
        string label = null,
        float min = 0f,
        float max = 1f
    )
    {
        MaterialProperty property = FindProperty(
            propertyName: propertyName,
            properties: properties,
            propertyIsMandatory: false
        );
        if (property != null)
        {
            EditorGUI.BeginChangeCheck();
            float newValue = EditorGUILayout.Slider(
                label: label ?? property.displayName,
                value: property.floatValue,
                leftValue: min,
                rightValue: max
            );
            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = newValue;
            }
        }
    }

    private void DrawUVDropdown(
        MaterialProperty[] properties,
        string propertyName,
        string label = null
    )
    {
        this.DrawDropdown(
            properties: properties,
            propertyName: propertyName,
            options: new[] { "UV0", "UV1", "UV2", "UV3" },
            label: label
        );
    }

    private void DrawDropdown(
        MaterialProperty[] properties,
        string propertyName,
        string[] options,
        string label = null,
        float[] values = null
    )
    {
        MaterialProperty property = FindProperty(
            propertyName: propertyName,
            properties: properties,
            propertyIsMandatory: false
        );

        if (property != null)
        {
            // Default to 0, 1, 2... if values are not provided or don't match options length
            if (values == null || values.Length != options.Length)
            {
                values = new float[options.Length];
                for (int i = 0; i < options.Length; i++)
                {
                    values[i] = i;
                }
            }

            int index = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (Mathf.Approximately(property.floatValue, values[i]))
                {
                    index = i;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();
            int newIndex = EditorGUILayout.Popup(
                label: label ?? property.displayName,
                selectedIndex: index,
                displayedOptions: options
            );
            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = values[newIndex];
            }
        }
    }

    private void DrawVector2Pad(
        MaterialProperty property,
        bool invertX = true,
        bool invertY = false,
        Vector2 center = default,
        float range = 2.0f
    )
    {
        EditorGUILayout.LabelField(label: property.displayName);
        property.vectorValue = EditorGUILayout.Vector2Field(label: "", value: property.vectorValue);

        Rect rect = GUILayoutUtility.GetRect(width: 100, height: 100);
        rect.width = 100; // Ensure it stays square

        GUI.Box(position: rect, content: GUIContent.none, style: EditorStyles.helpBox);

        float halfRange = range * 0.5f;
        float minX = center.x - halfRange;
        float maxX = center.x + halfRange;
        float minY = center.y - halfRange;
        float maxY = center.y + halfRange;

        float startX = invertX ? maxX : minX;
        float endX = invertX ? minX : maxX;
        float startY = invertY ? maxY : minY;
        float endY = invertY ? minY : maxY;

        Vector2 value = property.vectorValue;
        Event e = Event.current;

        if (
            (e.type == EventType.MouseDown || e.type == EventType.MouseDrag)
            && rect.Contains(e.mousePosition)
        )
        {
            float x = (e.mousePosition.x - rect.x) / rect.width;
            float y = (e.mousePosition.y - rect.y) / rect.height;

            value.x = Mathf.Lerp(startX, endX, x);
            value.y = Mathf.Lerp(startY, endY, y);

            property.vectorValue = value;
            e.Use();
        }

        float handleX = Mathf.InverseLerp(startX, endX, value.x);
        float handleY = Mathf.InverseLerp(startY, endY, value.y);

        Rect handleRect = new Rect(
            rect.x + Mathf.Clamp01(handleX) * rect.width - 4,
            rect.y + Mathf.Clamp01(handleY) * rect.height - 4,
            8,
            8
        );
        GUI.Box(position: handleRect, content: GUIContent.none, style: EditorStyles.radioButton);
    }

    private void DrawVector2Pad(
        MaterialProperty[] properties,
        string propertyName,
        bool invertX = true,
        bool invertY = false,
        Vector2 center = default,
        float range = 2.0f
    )
    {
        MaterialProperty property = FindProperty(
            propertyName: propertyName,
            properties: properties,
            propertyIsMandatory: false
        );
        if (property != null)
        {
            DrawVector2Pad(
                property: property,
                invertX: invertX,
                invertY: invertY,
                center: center,
                range: range
            );
        }
    }
}
}
#endif