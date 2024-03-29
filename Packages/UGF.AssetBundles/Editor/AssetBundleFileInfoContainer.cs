﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    internal class AssetBundleFileInfoContainer : ScriptableObject
    {
        [SerializeField] private string m_name;
        [SerializeField] private uint m_crc;
        [SerializeField] private bool m_isStreamedSceneAssetBundle;
        [SerializeField] private List<AssetInfo> m_assets = new List<AssetInfo>();
        [SerializeField] private List<string> m_scenePaths = new List<string>();
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Name { get { return m_name; } set { m_name = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public bool IsStreamedSceneAssetBundle { get { return m_isStreamedSceneAssetBundle; } set { m_isStreamedSceneAssetBundle = value; } }
        public List<AssetInfo> Assets { get { return m_assets; } }
        public List<string> ScenePaths { get { return m_scenePaths; } }
        public List<string> Dependencies { get { return m_dependencies; } }

        [Serializable]
        public class AssetInfo
        {
            [SerializeField] private string m_name;
            [SerializeField] private string m_type;
            [SerializeField] private string m_address;

            public string Name { get { return m_name; } set { m_name = value; } }
            public string Type { get { return m_type; } set { m_type = value; } }
            public string Address { get { return m_address; } set { m_address = value; } }
        }
    }
}
