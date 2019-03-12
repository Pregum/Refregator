using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 現在の作業の種類
    /// </summary>
    public enum WorkType
    {
        /// <summary>
        /// 未定義
        /// </summary>
        None = 0,
        /// <summary>
        /// 待機中
        /// </summary>
        StandBy = 1,
        /// <summary>
        /// 作成
        /// </summary>
        Create = 2,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 4,
        /// <summary>
        /// 削除
        /// </summary>
        Delete = 8,
        /// <summary>
        /// 使用
        /// </summary>
        Use = 16,
    }
}
