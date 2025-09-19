using System;
using System.Collections.Generic;
using UnityEngine;

namespace FDSoft.KeyPadInput
{
    /// <summary>
    /// 入力キーID基本
    /// </summary>
    /// <param name="Value">入力キーID</param>
    /// <param name="KeyCode">対応するキーコード</param>
    [Serializable]
    public record InputIdBase(in uint Value, in KeyCode KeyCode)
    {
        [SerializeField,Tooltip("入力キーID")]
        private uint _value;
        [SerializeField,Tooltip("対応するキーコード")]
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
        public static implicit operator uint(in InputIdBase instance) => instance._value;
    }
}