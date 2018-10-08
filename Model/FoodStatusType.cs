using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食材(の消費期限)の状態
    /// </summary>
    public enum FoodStatusType
    {
        /// <summary>
        /// 未定義
        /// </summary>
        None,
        /// <summary>
        /// 新鮮
        /// </summary>
        Fresh,
        /// <summary>
        /// 期限ぎりぎり
        /// </summary>
        BestLimit,
        /// <summary>
        /// アウト
        /// </summary>
        Rotten
    }
}
