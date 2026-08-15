using System;

namespace FDSoft.KeyPadInput.Editor
{
    /// <summary>
    /// 本ライブラリのエディタ情報
    /// </summary>
    public static class KpmEditorInfo
    {
        /// <summary>
        /// エディタパス情報
        /// </summary>
        public static readonly KpmEditorPathInfo PathInfo = new ();
        
        /// <summary>
        /// ローカライズ文言情報
        /// </summary>
        public static readonly KpmEditorLocalizeInfo LocalizeInfo = new ();
    }
}