using System;
using UnityEditor;
using UnityEngine;

namespace FDSoft.KeyPadInput.Editor
{
    /// <summary>
    /// Unityの入力設定から本ライブラリ用のコードや設定を生成するツール
    /// </summary>
    public sealed class UnityInputSettingCodeGenerator
        : EditorWindow
    {
        /// <summary>
        /// エディタウィンドウの表示
        /// </summary>
        [MenuItem("Window/FDSoft/KeyPadInput/UnityInputSettingCodeGenerator")]
        public static void ShowWindow()
        {
            var window = GetWindow<UnityInputSettingCodeGenerator>();
            window.titleContent = new GUIContent(nameof(UnityInputSettingCodeGenerator));
            window.Show();
        }
        
        /// <summary>
        /// GUI描画イベント
        /// </summary>
        private void OnGUI()
        {
            if (GUILayout.Button("InputId更新"))
            {
                UpdateInputId();
            }
        }
        
        
        /// <summary>
        /// 入力キーIDのレコード更新
        /// </summary>
        private void UpdateInputId()
        {
            var test = KpmEditorInfo.PathInfo.InputIDGenerateTemplateFilePath;
            Debug.Log($"@@@@@@@@@@@@@@@ [test:{test}]");
        }
    }
}