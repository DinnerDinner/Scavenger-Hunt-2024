// using UnityEngine;
// using UnityEngine.PostProcessing;

// namespace UnityEditor.PostProcessing
// {
//     [CustomPropertyDrawer(typeof(MinAttribute))]
//     sealed class MinDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             MinAttribute attribute = (MinAttribute)base.attribute;

//             if (property.propertyType == SerializedPropertyType.Integer)
//             {
//                 int v = EditorGUI.IntField(position, label, property.intValue);
//                 property.intValue = (int)Mathf.Max(v, attribute.min);
//             }
//             else if (property.propertyType == SerializedPropertyType.Float)
//             {
//                 float v = EditorGUI.FloatField(position, label, property.floatValue);
//                 property.floatValue = Mathf.Max(v, attribute.min);
//             }
//             else
//             {
//                 EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
//             }
//         }
//     }
// }

using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEditor;

[CustomPropertyDrawer(typeof(UnityEngine.PostProcessing.MinAttribute))]
public class MinDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UnityEngine.PostProcessing.MinAttribute min = (UnityEngine.PostProcessing.MinAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.Slider(position, property, min.min, float.MaxValue, label);
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.IntSlider(position, property, (int)min.min, int.MaxValue, label);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
        }
    }
}
