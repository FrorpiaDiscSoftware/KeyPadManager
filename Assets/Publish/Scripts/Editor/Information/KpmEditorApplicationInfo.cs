using System;
using UnityEngine;
using UnityEditor;
using FDSoft.KeyPadInput.Editor.Profile;

namespace FDSoft.KeyPadInput.Editor
{
    /// <summary>
    /// 本ライブラリのエディタアプリケーション関連情報
    /// </summary>
    public sealed class KpmEditorApplicationInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private const string PREFS_KEY_LANG_TYPE = nameof(KpmEditorApplicationInfo) + "." + nameof(_langType);
        
        
        /// <summary>
        /// 
        /// </summary>
        private KpmEditorLocalizeProfile.LangType _langType = KpmEditorLocalizeProfile.LangType.JP;
        
        
        /// <summary>
        /// 
        /// </summary>
        public KpmEditorLocalizeProfile.LangType LangType
        {
            get => GetLangType();
            set => SetLangType(value);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="langType"></param>
        private void SetLangType(in KpmEditorLocalizeProfile.LangType langType)
        {
            EditorPrefs.SetInt(PREFS_KEY_LANG_TYPE, (int)langType);
            _langType = langType;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private KpmEditorLocalizeProfile.LangType GetLangType()
        {
            _langType = (KpmEditorLocalizeProfile.LangType)EditorPrefs.GetInt(PREFS_KEY_LANG_TYPE);
            return _langType;
        }
    }
}