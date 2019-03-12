using System;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 栄養素の情報
    /// </summary>
    public class Nutrient
    {
        /// <summary>
        /// 栄養価
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
        /// 引数の単位に栄養価を変換する
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public double ConvertValue(UnitKind unit)
        {
            if (unit == UnitKind.Undefine)
            {
                throw new ArgumentException("UnitKindが未定義です。");
            }

            switch (this.UnitKind)
            {
                case UnitKind.kcal:
                    if (unit == UnitKind.kj)
                    {
                        return this.Value * 4.184;
                    }
                    else
                    {
                        throw new ArgumentException($"{this.UnitKind}を{unit}に変換できません。");
                    }
                case UnitKind.kj:
                    if (unit == UnitKind.kcal)
                    {
                        return this.Value / 4.184;
                    }
                    else
                    {
                        throw new ArgumentException($"{this.UnitKind}を{unit}に変換できません。");
                    }
                case UnitKind.g:
                    if (unit == UnitKind.mg)
                    {
                        return this.Value * 1000;
                    }
                    else if(unit == UnitKind.micro_g)
                    {
                        return this.Value * 1000000;
                    }
                    else
                    {
                        throw new ArgumentException($"{this.UnitKind}を{unit}に変換できません。");
                    }
                case UnitKind.mg:
                    if (unit == UnitKind.g)
                    {
                        return this.Value * 0.001;
                    }
                    else if(unit == UnitKind.micro_g)
                    {
                        return this.Value * 1000;
                    }
                    else
                    {
                        throw new ArgumentException($"{this.UnitKind}を{unit}に変換できません。");
                    }
                case UnitKind.micro_g:
                    if (unit == UnitKind.g)
                    {
                        return this.Value * 0.000001;
                    }
                    else if(unit == UnitKind.mg)
                    {
                        return this.Value * 0.001;
                    }
                    else
                    {
                        throw new ArgumentException($"{this.UnitKind}を{unit}に変換できません。");
                    }
                case UnitKind.percent:
                    throw new ArgumentException("%は変換先がありません。");
                case UnitKind.Undefine:
                default:
                    throw new ArgumentException("UnitKindが未定義です。");
            }
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
