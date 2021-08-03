using System;
using UGF.EditorTools.Editor.IMGUI;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileInfoDrawer : DrawerBase
    {
        private readonly EditorDrawer m_drawer = new EditorDrawer();

        protected override void OnEnable()
        {
            base.OnEnable();

            m_drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_drawer.Disable();
        }

        public void Set(AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            AssetBundleFileInfoContainer container = AssetBundleFileInfoContainerUtility.CreateContainer(info);

            m_drawer.Set(container);
        }

        public void Clear()
        {
            m_drawer.Clear();
        }

        public void DrawGUILayout()
        {
            m_drawer.DrawGUILayout();
        }
    }
}
