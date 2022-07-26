using System;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    [CustomPropertyDrawer(typeof(AssetBundleFileInfoContainer.AssetInfo), true)]
    internal class AssetBundleFileInfoContainerAssetInfoPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty propertyName = serializedProperty.FindPropertyRelative("m_name");
            SerializedProperty propertyType = serializedProperty.FindPropertyRelative("m_type");
            SerializedProperty propertyAddress = serializedProperty.FindPropertyRelative("m_address");

            var rectFoldout = new Rect(position.x, position.y, position.width, height);
            var rectAddress = new Rect(position.x, rectFoldout.yMax + space, position.width, height);

            Type contentType = Type.GetType(propertyType.stringValue) ?? typeof(DefaultAsset);
            Texture2D contentIcon = AssetPreview.GetMiniTypeThumbnail(contentType);
            string contentName = $"{propertyName.stringValue} ({contentType.FullName})";
            var content = new GUIContent(contentName, contentIcon);

            serializedProperty.isExpanded = EditorGUI.Foldout(rectFoldout, serializedProperty.isExpanded, content, true);

            if (serializedProperty.isExpanded)
            {
                using (new IndentIncrementScope(1))
                {
                    EditorGUI.PropertyField(rectAddress, propertyAddress);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            return serializedProperty.isExpanded ? height * 2F + space : height;
        }
    }
}
