using System;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Preferences;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileInfoDrawer : DrawerBase
    {
        private readonly PreferenceEditorValue<bool> m_assetsFoldout = new PreferenceEditorValue<bool>(nameof(AssetBundleFileInfo.Assets));
        private readonly PreferenceEditorValue<bool> m_dependenciesFoldout = new PreferenceEditorValue<bool>(nameof(AssetBundleFileInfo.Dependencies));

        protected override void OnEnable()
        {
            base.OnEnable();

            m_assetsFoldout.Enable();
            m_dependenciesFoldout.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_assetsFoldout.Disable();
            m_dependenciesFoldout.Disable();
        }

        public void DrawGUILayout(AssetBundleFileInfo info)
        {
            float height = GetHeight(info);
            Rect position = EditorGUILayout.GetControlRect(false, height);

            GUI.Box(position, GUIContent.none);

            DrawGUI(position, info);
        }

        public void DrawGUI(Rect position, AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            OnDrawGUI(position, info);
        }

        public float GetHeight(AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return OnGetHeight(info);
        }

        protected virtual void OnDrawGUI(Rect position, AssetBundleFileInfo info)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            float heightAssets = m_assetsFoldout.Value && info.Assets.Count > 0
                ? height * info.Assets.Count + space * info.Assets.Count - 1
                : 0F;

            var rectName = new Rect(position.x, position.y, position.width, height);
            var rectCrc = new Rect(position.x, rectName.yMax + space, position.width, height);
            var rectIsStreamedSceneAssetBundle = new Rect(position.x, rectCrc.yMax + space, position.width, height);
            var rectAssetsFoldout = new Rect(position.x, rectIsStreamedSceneAssetBundle.yMax + space, position.width, height);
            var rectAssetsElement = new Rect(position.x, rectAssetsFoldout.yMax + space, position.width, height);
            var rectDependenciesFoldout = new Rect(position.x, rectAssetsFoldout.yMax + heightAssets + space, position.width, height);
            var rectDependenciesElement = new Rect(position.x, rectDependenciesFoldout.yMax + space, position.width, height);

            EditorGUI.TextField(rectName, nameof(info.Name), info.Name);
            EditorGUI.TextField(rectCrc, nameof(info.Crc), info.Crc.ToString());
            EditorGUI.Toggle(rectIsStreamedSceneAssetBundle, nameof(info.IsStreamedSceneAssetBundle), info.IsStreamedSceneAssetBundle);

            m_assetsFoldout.Value = EditorGUI.Foldout(rectAssetsFoldout, m_assetsFoldout.Value, nameof(info.Assets), true);

            if (m_assetsFoldout.Value)
            {
                using (new IndentIncrementScope(1))
                {
                    for (int i = 0; i < info.Assets.Count; i++)
                    {
                        AssetBundleFileInfo.AssetInfo assetInfo = info.Assets[i];
                    }
                }
            }

            m_dependenciesFoldout.Value = EditorGUI.Foldout(rectDependenciesFoldout, m_dependenciesFoldout.Value, nameof(info.Dependencies), true);

            if (m_dependenciesFoldout.Value)
            {
                using (new IndentIncrementScope(1))
                {
                    for (int i = 0; i < info.Dependencies.Count; i++)
                    {
                        string dependency = info.Dependencies[i];

                        EditorGUI.TextField(rectDependenciesElement, dependency);

                        rectDependenciesElement.y += height + space;
                    }
                }
            }
        }

        protected virtual float OnGetHeight(AssetBundleFileInfo info)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float heightProperties = height * 3 + space * 2;

            float heightAssets = m_assetsFoldout.Value && info.Assets.Count > 0
                ? height * info.Assets.Count + space * info.Assets.Count - 1
                : height + space;

            float heightDependencies = m_dependenciesFoldout.Value && info.Dependencies.Count > 0
                ? height * info.Dependencies.Count + space * info.Dependencies.Count - 1
                : height + space;

            return heightProperties + heightAssets + heightDependencies;
        }
    }
}
