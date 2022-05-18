# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0](https://github.com/unity-game-framework/ugf-assetbundles/releases/tag/1.0.0) - 2022-05-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-assetbundles/milestone/4?closed=1)  
    

### Added

- Add option to delete manifest files after build ([#8](https://github.com/unity-game-framework/ugf-assetbundles/issues/8))  
    - Update dependencies: `com.ugf.editortools` to `2.5.0` and `com.ugf.runtimetools` to `2.7.0` version.
    - Update package _Unity_ version to `2021.3`.
    - Update package _API Compatibility_ level to `.NET Standard 2.1`.
    - Add `AssetBundleBuildUtility.DeleteManifestFiles()` method to delete manifest files.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-assetbundles/releases/tag/1.0.0-preview.2) - 2021-08-08  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-assetbundles/milestone/3?closed=1)  
    

### Added

- Add display of known asset names in assetbundle information ([#6](https://github.com/unity-game-framework/ugf-assetbundles/pull/6))  
    - Add `AssetBundleFileInfo.AssetNames` and `ScenePaths` properties with collection of all known asset names and scene paths from assetbundle.

### Changed

- Change to draw assets and dependencies as paged collection in assetbundle information ([#5](https://github.com/unity-game-framework/ugf-assetbundles/pull/5))  
    - Add `AssetBundleFileDrawer.DrawerNormal` and `DrawerDebug` properties to access drawers for each mode.
    - Add `AssetBundleFileInfoDrawer.DisplayAsReadOnly` property to determine whether to display information in readonly GUI mode.
    - Change _AssetBundle Information_ to display _Assets_ and _Dependencies_ collections as collection with pages.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-assetbundles/releases/tag/1.0.0-preview.1) - 2021-08-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-assetbundles/milestone/2?closed=1)  
    

### Fixed

- Fix missing reference errors when loading assetbundle information ([#2](https://github.com/unity-game-framework/ugf-assetbundles/pull/2))  
    - Fix missing reference errors by loading assetbundle all known assets when getting information.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-assetbundles/releases/tag/1.0.0-preview) - 2021-08-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-assetbundles/milestone/1?closed=1)


