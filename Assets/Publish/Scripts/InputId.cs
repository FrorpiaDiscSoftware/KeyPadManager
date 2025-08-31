using System;
using UnityEngine;

namespace FDSoft.KeyPadInput
{
    /// <summary>
    /// 入力キーID
    /// </summary>
    /// <param name="Value">入力キーID</param>
    /// <param name="KeyCode">対応するキーコード</param>
    [Serializable]
    public partial record InputId(in uint Value, in KeyCode KeyCode)
    {
        /// <summary>
        /// 未定義
        /// </summary>
        public static readonly InputId Undefined = new (0, KeyCode.None);
        
        
        /// <summary>
        /// 入力キーID
        /// </summary>
        [SerializeField]
        private uint _value;
        
        /// <summary>
        /// 対応するキーコード
        /// </summary>
        [SerializeField]
        private KeyCode _keyCode = KeyCode.None;
        
        
        /// <summary>
        /// 入力キーID
        /// </summary>
        public uint Value { get => _value; set => _value = value; }
        
        /// <summary>
        /// 対応するキーコード
        /// </summary>
        public KeyCode KeyCode { get => _keyCode; set => _keyCode = value; }
        
        
        /// <summary>
        /// InputId⇒uint変換
        /// </summary>
        /// <param name="instance">変換元のインスタンス</param>
        /// <returns>InputIdが持つ入力IDの値</returns>
        public static implicit operator uint(in InputId instance) => instance._value;
    }
}