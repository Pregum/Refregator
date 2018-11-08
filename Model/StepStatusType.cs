using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// ステップの状態を表します
    /// </summary>
    public enum StepStatusType
    {
        /// <summary>
        /// 未定義
        /// </summary>
        None = 0,
        /// <summary>
        /// 完了
        /// </summary>
        Done = 1,
        /// <summary>
        /// 作業中
        /// </summary>
        Working = 2,
        /// <summary>
        ///  未着手
        /// </summary>
        New = 4
    }
}
