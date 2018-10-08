using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 表示されているページを表すEnum
    /// </summary>
    public enum DisplayPageStatus
    {
        /// <summary>
        /// ダッシュボード(最初のページ)
        /// </summary>
        DashBoard = 1,
        /// <summary>
        /// カレンダーページ
        /// </summary>
        Calendar = 2,
        /// <summary>
        /// 編集ページ
        /// </summary>
        Editor = 3
    }
}
