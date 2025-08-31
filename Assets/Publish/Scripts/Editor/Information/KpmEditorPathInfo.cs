using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FDSoft.KeyPadInput.Editor
{
    /// <summary>
    /// 本ライブラリのエディタパス情報<br />
    /// TODO:Rootディレクトリパス情報周りは後で共通処理としてパッケージ化した方がよさそう。
    /// </summary>
    public class KpmEditorPathInfo
    {
        #region 定数
        
        /// <summary>
        /// 開発用のRootディレクトリパス
        /// </summary>
        private const string DEVELOP_ROOT_DIR_PATH = "Assets/Publish";
        
        /// <summary>
        /// 本番用のRootディレクトリの全ライブラリ共通部分のパス
        /// </summary>
        private const string RELEASE_ROOT_DIR_PATH_BASE = "Library/PackageCache";
        
        /// <summary>
        /// 本番用のRootディレクトリ名
        /// ※実際にはこの名前の後に「@***」が付くが、その部分は含まない。
        /// </summary>
        private const string RELEASE_ROOT_DIR_NAME = "com.fdsoft.key-pad-manager";
        
        /// <summary>
        /// このライブラリを使用するユーザーのアセット保存先ディレクトリパス
        /// </summary>
        private const string USER_ASSET_ROOT_DIR_PATH = "Assets/FDSoft/KeyPadManager";
        
        /// <summary>
        /// プロジェクト設定-InputManagerタブの設定ファイルパス
        /// </summary>
        private const string INPUT_MANAGER_SETTING_FILE_PATH = "ProjectSettings/InputManager.asset";
        
        /// <summary>
        /// InputIdレコード生成用テンプレートファイルパス【Rootディレクトリからの相対パス】
        /// </summary>
        private const string INPUT_ID_GENERATE_TEMPLATE_RELATIVE_PATH = "GenerateTemplates/Scripts/InputId.Generate.cstemplate";
        
        /// <summary>
        /// 生成したInputIdレコードの保存先ファイルパス【ユーザーのアセットRootディレクトリからの相対パス】
        /// </summary>
        private const string INPUT_ID_GENERATE_FILE_SAVE_RELATIVE_PATH = "Generate/Scripts/InputId.Generate.cs";
        
        #endregion
        
        /// <summary>
        /// 本番用のRootディレクトリパス
        /// </summary>
        private string _releaseRootDirPath = string.Empty;
        
        
        /// <summary>
        /// Rootディレクトリパス
        /// </summary>
        public string RootDirPath => GetRootDirPath();
        
        /// <summary>
        /// プロジェクト設定-InputManagerタブの設定ファイルパス
        /// </summary>
        public string InputManagerSettingFilePath => INPUT_MANAGER_SETTING_FILE_PATH;
        
        /// <summary>
        /// InputIdレコード生成用テンプレートファイルパス
        /// </summary>
        public string InputIDGenerateTemplateFilePath => $"{RootDirPath}/{INPUT_ID_GENERATE_TEMPLATE_RELATIVE_PATH}";
        
        /// <summary>
        /// 生成したInputIdレコードの保存先ファイルパス
        /// </summary>
        public string InputIDGenerateFileSavePath => $"{USER_ASSET_ROOT_DIR_PATH}/{INPUT_ID_GENERATE_FILE_SAVE_RELATIVE_PATH}";
        
        
        /// <summary>
        /// Rootディレクトリパスを返す
        /// </summary>
        /// <returns>取得したRootディレクトリパス</returns>
        private string GetRootDirPath()
        {
            if (Directory.Exists(DEVELOP_ROOT_DIR_PATH))
            {
                return DEVELOP_ROOT_DIR_PATH;
            }
            
            if (!string.IsNullOrEmpty(_releaseRootDirPath)
                && Directory.Exists(_releaseRootDirPath))
            {
                return _releaseRootDirPath;
            }
            
            var dirPaths = Directory.GetDirectories(RELEASE_ROOT_DIR_PATH_BASE);
            
            _releaseRootDirPath = dirPaths
                .FirstOrDefault(dirPath => dirPath.Contains(RELEASE_ROOT_DIR_NAME))?
                .Replace("\\", "/") ?? string.Empty;
            
            return _releaseRootDirPath;
        }
    }
}