using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// progress trackerの1手順を表します
    /// </summary>
    public interface IStep
    {
        /// <summary>
        /// ステップ名を取得します
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ステップの状態を表します
        /// </summary>
        StepStatusType StepStatus { get; }

        /// <summary>
        /// 引数の食材の更新を行います
        /// </summary>
        /// <param name="food">操作中の食材</param>
        void Update(FoodModel food);

        /// <summary>
        ///  初期化を行います
        /// </summary>
        void Init();

        /// <summary>
        /// 画面を遷移し、作業を始めます。
        /// </summary>
        /// <param name="navigation">画面遷移を行う為のサービス</param>
        void Navigate(NavigationService navigation);

        /// <summary>
        /// 元の状態に戻します
        /// </summary>
        void Revert();
    }
}
