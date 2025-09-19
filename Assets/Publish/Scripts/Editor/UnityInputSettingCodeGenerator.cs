using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using FDSoft.UnityModules.Editor;
using FDSoft.UnityModules.Editor.Utility;

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
            string inputSettingFilePath = KpmEditorInfo.PathInfo.InputManagerSettingFilePath;
            string inputIDTemplateFilePath = KpmEditorInfo.PathInfo.InputIDGenerateTemplateFilePath;
            string inputIDSaveFilePath = KpmEditorInfo.PathInfo.InputIDGenerateFileSavePath;
            string inputIDSaveDirPath = string.Join("/", inputIDSaveFilePath.Split("/").SkipLast(1));
            
            if (!File.Exists(inputSettingFilePath)
                || !File.Exists(inputIDTemplateFilePath))
            {
                return;
            }
            
            var variableNameIssueTable = new Dictionary<string, uint>(); // 変数で使う用の名前発行管理テーブル【[キー：変換前の名前][値：発行回数]】
            
            EditorFileSystem.CreateDirectory(inputIDSaveDirPath);
            
            // Unity定義分のキーコード出力
            string inputIDTemplateFileContent = File.ReadAllText(inputIDTemplateFilePath);
            string inputIDStaticVariableContent = ""; // 静的な変数フィールド生成文字列
            uint inputIDValueCount = 1; // 0は未定義値で使用するので1から開始。
            
            foreach (var keyCode in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Distinct())
            {
                string variableKeyCodeName = keyCode.ToString().ToVariableName(variableNameIssueTable); // 変数で使う用のキーコード名
                
                if (!string.IsNullOrEmpty(inputIDStaticVariableContent))
                {
                    inputIDStaticVariableContent += "\n";
                }
                
                inputIDStaticVariableContent +=         "        /// <summary>";
                inputIDStaticVariableContent += "\n" + $"        /// {keyCode}ボタン";
                inputIDStaticVariableContent += "\n" +  "        /// </summary>";
                inputIDStaticVariableContent += "\n" + $"        public static readonly InputId {variableKeyCodeName} = new ({inputIDValueCount}, KeyCode.{keyCode});";
                inputIDStaticVariableContent += "\n" +  "        ";
                
                inputIDValueCount++;
            }
            
            // ユーザー設定分を出力
            var inputSettingAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(inputSettingFilePath);
            var inputSetting = new SerializedObject(inputSettingAsset);
            var axesSettings = inputSetting.FindProperty("m_Axes");
            
            for (int i = 0; i < axesSettings.arraySize; i++)
            {
                var axesSetting = axesSettings.GetArrayElementAtIndex(i);
                var nameProperty = axesSetting.FindPropertyRelative("m_Name");
                
                if (string.IsNullOrEmpty(nameProperty.stringValue)) continue;
                
                string axesName = nameProperty.stringValue;
                string variableAxesName = axesName.ToVariableName(variableNameIssueTable, EditorStringUtility.NamingType.PASCAL_CASE); // 変数で使う用の軸名
                
                inputIDStaticVariableContent += $"\n        /// <summary>";
                inputIDStaticVariableContent += $"\n        /// {axesName}ボタン";
                inputIDStaticVariableContent += $"\n        /// </summary>";
                inputIDStaticVariableContent += $"\n        public static readonly InputId {variableAxesName} = new ({inputIDValueCount}, KeyCode.None);";
                inputIDStaticVariableContent += $"\n        ";
                
                inputIDValueCount++;
            }
            
            // ファイル出力
            inputIDTemplateFileContent = inputIDTemplateFileContent
                .Replace("{StaticVariableContent}", inputIDStaticVariableContent);
            File.WriteAllText(inputIDSaveFilePath, inputIDTemplateFileContent);
            
            AssetDatabase.Refresh();
        }
    }
}