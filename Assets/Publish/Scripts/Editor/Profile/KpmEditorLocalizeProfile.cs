using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FDSoft.KeyPadInput.Editor.Profile
{
    /// <summary>
    /// ローカライズ文言設定【Editor用】
    /// </summary>
    [CreateAssetMenu(fileName = "KpmEditorLocalizeProfile", menuName = "FDSoft/KeyPadInput/KpmEditorLocalizeProfile", order = 0)]
    public sealed class KpmEditorLocalizeProfile
        : ScriptableObject
    {
        [SerializeField,Tooltip("ローカライズ文言データテーブル")]
        private List<LocalizeData> _localizeDataTable = new ();
        
        
        /// <summary>
        /// 文言キーからローカライズ文言データ検索用辞書テーブル
        /// </summary>
        private readonly Dictionary<string, LocalizeData> _keyToLocalizeDataTable = new ();
        
        
        /// <summary>
        /// 言語種別
        /// </summary>
        public enum LangType : byte
        {
            JP, // 日本語版
            EN, // 英語版
            CN, // 中国語版
        }
        
        
        /// <summary>
        /// ローカライズ文言データ
        /// </summary>
        [Serializable]
        private sealed class LocalizeData
        {
            [SerializeField,Tooltip("日本語の文言")]
            public string JpText;
            [SerializeField,Tooltip("英語の文言")]
            public string EnText;
            [SerializeField,Tooltip("中国語の文言")]
            public string CnText;
            [SerializeField,Tooltip("文言キー")]
            public string Key;
        }
        
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            var newLocalizeDataTable = new List<LocalizeData>();
            
            _keyToLocalizeDataTable.Clear();
            
            foreach (var item in _localizeDataTable)
            {
                if (_keyToLocalizeDataTable.ContainsKey(item.Key))
                {
                    continue;
                }
                
                newLocalizeDataTable.Add(item);
                _keyToLocalizeDataTable.Add(item.Key, item);
            }
            
            _localizeDataTable = newLocalizeDataTable;
        }
        
        
        /// <summary>
        /// <see cref="SystemLanguage"/>を<see cref="LangType"/>に変換する
        /// </summary>
        /// <param name="systemLanguage">変換したいSystemLanguage値</param>
        /// <returns>変換した<see cref="LangType"/></returns>
        public static LangType ToLangType(in SystemLanguage systemLanguage)
        {
            return systemLanguage switch
            {
                SystemLanguage.Japanese => LangType.JP,
                SystemLanguage.Chinese => LangType.CN,
                SystemLanguage.ChineseSimplified => LangType.CN,
                SystemLanguage.ChineseTraditional => LangType.CN,
                _ => LangType.EN
            };
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
            in LangType langType,
            params object[] formatParams)
        {
            string rawText = GetRawText(key, langType);
            
            if (formatParams.Length == 0)
            {
                return rawText;
            }
            
            return !string.IsNullOrEmpty(rawText) ? string.Format(rawText, formatParams) : rawText;
        }
        
        /// <summary>
        /// 文言キーと言語種別から設定されている文言そのままを返す
        /// </summary>
        /// <param name="key">取得対象の文言キー</param>
        /// <param name="langType">取得対象の言語種別</param>
        /// <returns>設定されている文言</returns>
        /// <exception cref="ArgumentOutOfRangeException">非対応の言語種別が指定されると送出されます</exception>
        private string GetRawText(in string key, in LangType langType)
        {
            var localizeData = GetLocalizeDataFromKey(key);
            
            return langType switch
            {
                LangType.JP => localizeData?.JpText ?? string.Empty,
                LangType.EN => localizeData?.EnText ?? string.Empty,
                LangType.CN => localizeData?.CnText ?? string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(langType), langType, "This language type is not supported.")
            };
        }
        
        /// <summary>
        /// 文言キーからローカライズ文言データを返す
        /// </summary>
        /// <param name="key">取得対象の文言キー</param>
        /// <returns>ローカライズ文言データ</returns>
        private LocalizeData GetLocalizeDataFromKey(in string key)
            => _keyToLocalizeDataTable.ContainsKey(key) ? _keyToLocalizeDataTable[key] : null;
    }
}