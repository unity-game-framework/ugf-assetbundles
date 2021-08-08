﻿using UGF.EditorTools.Editor.IMGUI.Pages;
using UnityEditor;

namespace UGF.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleFileInfoContainer), true)]
    internal class AssetBundleFileInfoContainerEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyName;
        private SerializedProperty m_propertyCrc;
        private SerializedProperty m_propertyIsStreamedSceneAssetBundle;
        private PagesCollectionDrawer m_listAssetNames;
        private PagesCollectionDrawer m_listScenePaths;
        private PagesCollectionDrawer m_listAssets;
        private PagesCollectionDrawer m_listDependencies;

        private void OnEnable()
        {
            m_propertyName = serializedObject.FindProperty("m_name");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyIsStreamedSceneAssetBundle = serializedObject.FindProperty("m_isStreamedSceneAssetBundle");
            m_listAssetNames = new PagesCollectionDrawer(serializedObject.FindProperty("m_assetNames"));
            m_listScenePaths = new PagesCollectionDrawer(serializedObject.FindProperty("m_scenePaths"));
            m_listAssets = new PagesCollectionDrawer(serializedObject.FindProperty("m_assets"));
            m_listDependencies = new PagesCollectionDrawer(serializedObject.FindProperty("m_dependencies"));

            m_listAssetNames.Enable();
            m_listScenePaths.Enable();
            m_listAssets.Enable();
            m_listDependencies.Enable();
        }

        private void OnDisable()
        {
            m_listAssetNames.Disable();
            m_listScenePaths.Disable();
            m_listAssets.Disable();
            m_listDependencies.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_propertyName);
            EditorGUILayout.PropertyField(m_propertyCrc);
            EditorGUILayout.PropertyField(m_propertyIsStreamedSceneAssetBundle);

            m_listAssetNames.DrawGUILayout();
            m_listScenePaths.DrawGUILayout();
            m_listAssets.DrawGUILayout();
            m_listDependencies.DrawGUILayout();
        }
    }
}
