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
    
    /// <summary>
    /// 入力軸の種別
    /// </summary>
    public enum AxesType : byte
    {
        KEY_OR_MOUSE_BUTTON, // キーまたはマウスボタン
        MOUSE_MOVEMENT,      // マウス動作
        JOYSTICK_AXIS,       // ジョイスティック軸
    }
    
    /// <summary>
    /// ジョイスティック番号種別
    /// </summary>
    public enum JoyNumType : byte
    {
        GET_MOTION_FROM_ALL_JOYSTICKS, // 全てのジョイスティックの動きを取得(GetMotionFromAllJoysticks)
        JOYSTICK1,                     // ジョイスティック1(Joystick1)
        JOYSTICK2,                     // ジョイスティック2(Joystick2)
        JOYSTICK3,                     // ジョイスティック3(Joystick3)
        JOYSTICK4,                     // ジョイスティック4(Joystick4)
        JOYSTICK5,                     // ジョイスティック5(Joystick5)
        JOYSTICK6,                     // ジョイスティック6(Joystick6)
        JOYSTICK7,                     // ジョイスティック7(Joystick7)
        JOYSTICK8,                     // ジョイスティック8(Joystick8)
        JOYSTICK9,                     // ジョイスティック9(Joystick9)
        JOYSTICK10,                    // ジョイスティック10(Joystick10)
        JOYSTICK11,                    // ジョイスティック11(Joystick11)
        JOYSTICK12,                    // ジョイスティック12(Joystick12)
        JOYSTICK13,                    // ジョイスティック13(Joystick13)
        JOYSTICK14,                    // ジョイスティック14(Joystick14)
        JOYSTICK15,                    // ジョイスティック15(Joystick15)
        JOYSTICK16,                    // ジョイスティック16(Joystick16)
    }
}