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
        : InputIdBase(Value, KeyCode)
    {
        /// <summary>
        /// 未定義
        /// </summary>
        public static readonly InputId Undefined = new (0, KeyCode.None);
    }
}