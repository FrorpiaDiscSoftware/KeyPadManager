using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace FDSoft.KeyPadInput
{
    /// <summary>
    /// 入力情報設定
    /// </summary>
    [CreateAssetMenu(fileName = "InputProfile", menuName = "FDSoft/KeyPadInput/InputProfile", order = 0)]
    public class InputProfile
        : ScriptableObject
    {
        [SerializeField,Tooltip("入力情報設定リスト")]
        private List<InputProfileData> _inputProfiles = new ();
        
        
        /// <summary>
        /// 入力キー名から入力関連情報データ検索用辞書テーブル
        /// </summary>
        private readonly Dictionary<string, InputProfileData> _nameToProfileDataTable = new ();
        
        /// <summary>
        /// 入力キーIDから入力関連情報データ検索用辞書テーブル
        /// </summary>
        private readonly Dictionary<uint, InputProfileData> _inputIdToProfileDataTable = new ();
        
        
        /// <summary>
        /// 入力キー情報の最大数
        /// </summary>
        public int InputLength => _inputProfiles.Count;
        
        
        /// <summary>
        /// 入力情報設定データ(1要素分)
        /// </summary>
        [Serializable]
        public class InputProfileData
        {
            [SerializeField,Tooltip("入力キー名(日本語名)")]
            public string Jname;
            [SerializeField,Tooltip("入力キー名(英名/Unityに渡す名前)")]
            public string Name;
            [SerializeField,Tooltip("入力キーID※設定ファイル向けのID指定値")]
            public InputId InputID;
        }
        
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            var newInputProfiles = new List<InputProfileData>();
            
            foreach (var item in _inputProfiles)
            {
                if (_nameToProfileDataTable.ContainsKey(item.Name)
                    || _inputIdToProfileDataTable.ContainsKey(item.InputID))
                {
                    continue;
                }
                
                newInputProfiles.Add(item);
                _nameToProfileDataTable.Add(item.Name, item);
                _inputIdToProfileDataTable.Add(item.InputID, item);
                
                OnInitializeTableAdded(item);
            }
            
            _inputProfiles = newInputProfiles;
            
            // 初期化イベント実行
            OnInitialize();
        }
        
        /// <summary>
        /// 初期化時テーブルに設定項目を追加する際のイベント
        /// </summary>
        /// <param name="profileData">追加する設定項目</param>
        protected virtual void OnInitializeTableAdded(in InputProfileData profileData) { }
        
        /// <summary>
        /// 初期化イベント<br/>
        /// ※本クラスを継承した際に初期化時に何かやりたいことがあれば、この関数をオーバーライドして初期化処理を書いてください。
        /// </summary>
        protected virtual void OnInitialize() { }
        
        
        /// <summary>
        /// 指定した入力キー名(英名)の設定項目があるかどうかを返す
        /// </summary>
        /// <param name="inputName">設定項目があるか確認したい入力キー名(英名)</param>
        /// <returns>trueで設定項目あり</returns>
        public bool IsProfileData(in string inputName) => _nameToProfileDataTable.ContainsKey(inputName);
        
        /// <summary>
        /// 指定したIndexの設定項目があるかどうかを返す
        /// </summary>
        /// <param name="index">設定項目があるか確認したい設定リストのIndex</param>
        /// <returns>trueで設定項目あり</returns>
        public bool IsProfileData(in int index) => index >= 0 && index < _inputProfiles.Count;
        
        /// <summary>
        /// 指定した入力キーIDの設定項目があるかどうかを返す
        /// </summary>
        /// <param name="inputId">設定項目があるか確認したい入力キーID</param>
        /// <returns>trueで設定項目あり</returns>
        public bool IsProfileData(in InputId inputId) => _inputIdToProfileDataTable.ContainsKey(inputId);
        
        
        /// <summary>
        /// 指定した入力キー名(英名)の設定項目を返す
        /// </summary>
        /// <param name="inputName">取得したい設定項目の入力キー名(英名)</param>
        /// <returns>取得した設定項目(存在しなければnull)</returns>
        public InputProfileData GetProfileData(in string inputName)
            => IsProfileData(inputName) ? _nameToProfileDataTable[inputName] : null;
        
        /// <summary>
        /// 指定したIndexの設定項目を返す
        /// </summary>
        /// <param name="index">取得したい設定項目の設定リストのIndex</param>
        /// <returns>取得した設定項目(存在しなければnull)</returns>
        public InputProfileData GetProfileData(in int index)
            => IsProfileData(index) ? _inputProfiles[index] : null;
        
        /// <summary>
        /// 指定した入力キーIDの設定項目を返す
        /// </summary>
        /// <param name="inputID">取得したい設定項目の入力キーID</param>
        /// <returns>取得した設定項目(存在しなければnull)</returns>
        public InputProfileData GetProfileData(in InputId inputID) =>
            IsProfileData(inputID) ? _inputIdToProfileDataTable[inputID] : null;
        
        /// <summary>
        /// 指定した入力キー名(英名)の入力キーIDを返す
        /// </summary>
        /// <param name="inputName">取得したい入力キーIDに対応する入力キー名(英名)</param>
        /// <returns>取得した入力キーID(存在しなければ<see cref="InputId.Undefined"/>)</returns>
        public InputId GetInputId(in string inputName)
            => IsProfileData(inputName) ? _nameToProfileDataTable[inputName].InputID : InputId.Undefined;
        
        /// <summary>
        /// 指定したIndexの入力キー名(英名)を返す
        /// </summary>
        /// <param name="index">取得したい入力キー名(英名)に対応する設定リストのIndex</param>
        /// <returns>取得した入力キー名(英名)(存在しなければ<see cref="string.Empty"/>)</returns>
        public string GetInputName(in int index)
            => IsProfileData(index) ? _inputProfiles[index].Name : string.Empty;
        
        /// <summary>
        /// 指定した入力キーIDの入力キー名(英名)を返す
        /// </summary>
        /// <param name="inputID">取得したい入力キー名(英名)に対応する入力キーID</param>
        /// <returns>取得した入力キー名(英名)(存在しなければ<see cref="string.Empty"/>)</returns>
        public string GetInputName(in InputId inputID)
            => IsProfileData(inputID) ? _inputIdToProfileDataTable[inputID].Name : string.Empty;
    }
}