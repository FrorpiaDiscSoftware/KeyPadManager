using System;
using UnityEngine;

namespace FDSoft.KeyPadInput
{
    /// <summary>
    /// 入力状態定数値
    /// </summary>
    public enum InputState : byte
    {
        FREE,     // 押されていない
        ON_PRESS, // 押した瞬間
        PRESS,    // 押しっぱなし
        ON_UP     // 放した瞬間
    }
}