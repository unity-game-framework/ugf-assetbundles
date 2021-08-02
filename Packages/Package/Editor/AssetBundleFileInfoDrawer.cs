using System;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileInfoDrawer : DrawerBase
    {
        public void DrawGUILayout(AssetBundleFileInfo info)
        {
            float height = GetHeight(info);
            Rect position = EditorGUILayout.GetControlRect(false, height);

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

            var rectName = new Rect(position.x, position.y, position.width, height);
            var rectCrc = new Rect(position.x, rectName.yMax + space, position.width, height);
            var rectIsStreamedSceneAssetBundle = new Rect(position.x, rectCrc.yMax + space, position.width, height);

            EditorGUI.TextField(rectName, nameof(info.Name), info.Name);
            EditorGUI.TextField(rectCrc, nameof(info.Crc), info.Crc.ToString());
            EditorGUI.Toggle(rectIsStreamedSceneAssetBundle, nameof(info.IsStreamedSceneAssetBundle), info.IsStreamedSceneAssetBundle);
        }

        protected virtual float OnGetHeight(AssetBundleFileInfo info)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float heightProperties = height * 3 + space * 2;

            float heightAssets = info.Assets.Count > 0
                ? height * info.Assets.Count + space * info.Assets.Count - 1
                : height + space;

            float heightDependencies = info.Dependencies.Count > 0
                ? height * info.Dependencies.Count + space * info.Dependencies.Count - 1
                : height + space;

            return heightProperties + heightAssets + heightDependencies;
        }
    }
}
