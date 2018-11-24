using System;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 栄養素の情報
    /// </summary>
    public class Nutrient
    {
        /// <summary>
        /// 栄養素の値
        /// </summary>
        public double Value { get; private set; }
        /// <summary>
        /// 単位
        /// </summary>
        public UnitKind UnitKind { get; private set; }
        /// <summary>
        /// 推定値か
        /// </summary>
        public bool IsEstimateValue { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">栄養素の成分値</param>
        /// <param name="unitKind">単位の種類 e.g) mg, g, kcal, etc...</param>
        /// <param name="isEstimateValue">成分値は推定値か</param>
        public Nutrient(double value, UnitKind unitKind, bool isEstimateValue)
        {
            this.Value = value;
            this.UnitKind = unitKind;
            this.IsEstimateValue = isEstimateValue;
        }

        /// <summary>
        /// 栄養素クラスを作成する
        /// </summary>
        /// <param name="value">栄養素</param>
        /// <param name="unitKind">単位の種類</param>
        /// <returns>栄養素クラス</returns>
        public static Nutrient Parse(string value, UnitKind unitKind)
        {
            double nutrientValue = 0.0;
            bool isEstimateValue = false;
            string nutrientValueStr = value.Trim('\"');

            // 値が最小単位以下(Tr)の場合NaNに設定
            if (nutrientValueStr == "Tr" || nutrientValueStr == "(Tr)" || nutrientValueStr == "-" || nutrientValueStr == "*")
            {
                //nutrientValue = Double.NaN;
                nutrientValue = 0.0d;
                isEstimateValue = nutrientValueStr.Contains("(");
            }
            // カッコがあれば推定値とみなす
            else if (nutrientValueStr.Contains("("))
            {
                nutrientValue = double.Parse(nutrientValueStr.Trim('(', ')'));
                isEstimateValue = true;
            }
            // 数値ならばdouble型にParse
            else
            {
                nutrientValue = Double.Parse(value.Trim('(', ')'));
            }

            return new Nutrient(nutrientValue, unitKind, isEstimateValue);
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }

        public static Nutrient operator+(Nutrient a, Nutrient b)
        {
            return new Nutrient(a.Value + b.Value, a.UnitKind, a.IsEstimateValue || b.IsEstimateValue);
        }
    }
}
