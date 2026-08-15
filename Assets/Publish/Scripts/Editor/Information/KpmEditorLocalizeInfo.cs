using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using FDSoft.KeyPadInput.Editor.Profile;

namespace FDSoft.KeyPadInput.Editor
{
    /// <summary>
    /// 本ライブラリのエディタ用ローカライズ文言情報
    /// </summary>
    public sealed class KpmEditorLocalizeInfo
    {
        /// <summary>
        /// ローカライズ文言設定
        /// </summary>
        private KpmEditorLocalizeProfile _localizeProfile;
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KpmEditorLocalizeInfo()
        {
            UpdateProfile();
        }
        
        
        /// <summary>
        /// ローカライズ文言設定を更新する
        /// </summary>
        public void UpdateProfile()
        {
            string localizeProfilePath = KpmEditorInfo.PathInfo.LocalizeProfilePath;
            
            if (!File.Exists(localizeProfilePath))
            {
                Debug.LogError($"ローカライズ文言設定が見つかりません。設定は読み込まれません。({localizeProfilePath})");
                return;
            }
            
            _localizeProfile = AssetDatabase.LoadAssetAtPath<KpmEditorLocalizeProfile>(localizeProfilePath);
            
            if (_localizeProfile)
            {
                _localizeProfile.Initialize();
            }
        }
        
        
        /// <summary>
        /// 文言キーと言語種別からローカライズ済み文言を返す
        /// </summary>
        /// <param name="key">取得対象の文言キー</param>
        /// <param name="langType">取得対象の言語種別</param>
        /// <param name="formatParams">取得対象の文言に設定する文字列フォーマットパラメータ</param>
        /// <returns>ローカライズ済み文言</returns>
        public string GetText(
            in string key,
            in KpmEditorLocalizeProfile.LangType langType,
            params object[] formatParams)
            => _localizeProfile ? _localizeProfile.GetText(key, langType, formatParams) : string.Empty;
    }
}