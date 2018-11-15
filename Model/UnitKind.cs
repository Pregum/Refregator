namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 単位の種類を表したクラス
    /// </summary>
    public enum UnitKind
    {
        /// <summary>
        /// 未定義
        /// </summary>
        Undefine = 0,
        /// <summary>
        /// キロカロリー
        /// </summary>
        kcal = 1,
        /// <summary>
        /// キロジュール
        /// </summary>
        kj = 2,
        /// <summary>
        /// グラム
        /// </summary>
        g = 4,
        /// <summary>
        /// ミリグラム
        /// </summary>
        mg = 8,
        /// <summary>
        /// マイクログラム
        /// </summary>
        micro_g = 16,
        /// <summary>
        /// パーセント
        /// </summary>
        percent = 32,
    }
}