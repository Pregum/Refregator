using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 栄養素情報
    /// </summary>
    public class Nutrient
    {
        /// <summary>
        /// エネルギー
        /// </summary>
        public float Energy{ get; private set; } = 0.0f;
        /// <summary>
        /// タンパク質
        /// </summary>
        public float Protein{ get; private set; } = 0.0f;
        /// <summary>
        /// 脂質
        /// </summary>
        public float Lipid{ get; private set; } = 0.0f;
        /// <summary>
        /// 飽和脂肪酸
        /// </summary>
        public float SaturatedFattyAcids{ get; private set; } = 0.0f;
        /// <summary>
        /// n-6系脂肪酸
        /// </summary>
        public float N_6FattyAcid{ get; private set; } = 0.0f;
        /// <summary>
        /// n-3系脂肪酸
        /// </summary>
        public float N_3FattyAcids{ get; private set; } = 0.0f;
        /// <summary>
        /// 炭水化物
        /// </summary>
        public float Carbohydrate{ get; private set; } = 0.0f;
        /// <summary>
        /// 食物繊維
        /// </summary>
        public float DietaryFiber{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンA
        /// </summary>
        public float Vitamin_A{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンD
        /// </summary>
        public float Vitamin_D{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンE
        /// </summary>
        public float Vitamin_E{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンK
        /// </summary>
        public float Vitamin_K{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンB1
        /// </summary>
        public float Vitamin_B1{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンB2
        /// </summary>
        public float Vitamin_B2{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンB6
        /// </summary>
        public float Vitamin_B6{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンB12
        /// </summary>
        public float Vitamin_B12{ get; private set; } = 0.0f;
        /// <summary>
        /// ビタミンC
        /// </summary>
        public float Vitamin_C{ get; private set; } = 0.0f;
        /// <summary>
        /// ナイアシン
        /// </summary>
        public float Niacin{ get; private set; } = 0.0f;
        /// <summary>
        /// 葉酸
        /// </summary>
        public float FolicAcid{ get; private set; } = 0.0f;
        /// <summary>
        /// パントテン酸
        /// </summary>
        public float PantothenicAcid{ get; private set; } = 0.0f;
        /// <summary>
        /// ビオチン
        /// </summary>
        public float Biotin{ get; private set; } = 0.0f;
        /// <summary>
        /// ナトリウム
        /// </summary>
        public float Sodium{ get; private set; } = 0.0f;
        /// <summary>
        /// カリウム
        /// </summary>
        public float Potassium{ get; private set; } = 0.0f;
        /// <summary>
        /// カルシウム
        /// </summary>
        public float Calcium{ get; private set; } = 0.0f;
        /// <summary>
        /// マグネシウム
        /// </summary>
        public float Magnesium{ get; private set; } = 0.0f;
        /// <summary>
        /// リン
        /// </summary>
        public float Rin{ get; private set; } = 0.0f;
        /// <summary>
        /// 鉄
        /// </summary>
        public float Iron{ get; private set; } = 0.0f;
        /// <summary>
        /// 亜鉛
        /// </summary>
        public float Zinc{ get; private set; } = 0.0f;
        /// <summary>
        /// 銅
        /// </summary>
        public float Copper{ get; private set; } = 0.0f;
        /// <summary>
        /// マンガン
        /// </summary>
        public float Manganese{ get; private set; } = 0.0f;
        /// <summary>
        /// ヨウ素
        /// </summary>
        public float Lodine{ get; private set; } = 0.0f;
        /// <summary>
        /// セレン
        /// </summary>
        public float Selenium{ get; private set; } = 0.0f;
        /// <summary>
        /// クロム
        /// </summary>
        public float Chromium{ get; private set; } = 0.0f;
        /// <summary>
        /// モリブデン
        /// </summary>
        public float Molybdenum{ get; private set; } = 0.0f;


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="energy">エネルギー</param>
        /// <param name="protein">タンパク質</param>
        /// <param name="lipid">脂質</param>
        /// <param name="saturatedFattyAcids">飽和脂肪酸</param>
        /// <param name="n_6FattyAcid">n-6系脂肪酸</param>
        /// <param name="n_3FattyAcids">n-3系脂肪酸</param>
        /// <param name="carbohydrate">炭水化物</param>
        /// <param name="dietaryFiber">食物繊維</param>
        /// <param name="vitamin_A">ビタミンA</param>
        /// <param name="vitamin_D">ビタミンD</param>
        /// <param name="vitamin_E">ビタミンE</param>
        /// <param name="vitamin_K">ビタミンK</param>
        /// <param name="vitamin_B1">ビタミンB1</param>
        /// <param name="vitamin_B2">ビタミンB2</param>
        /// <param name="vitamin_B6">ビタミンB6</param>
        /// <param name="vitamin_B12">ビタミンB12</param>
        /// <param name="vitamin_C">ビタミンC</param>
        /// <param name="niacin">ナイアシン</param>
        /// <param name="folicAcid">葉酸</param>
        /// <param name="pantothenicAcid">パントテン酸</param>
        /// <param name="biotin">ビオチン</param>
        /// <param name="sodium">ナトリウム</param>
        /// <param name="potassium">カリウム</param>
        /// <param name="calcium">カルシウム</param>
        /// <param name="magnesium">マグネシウム</param>
        /// <param name="rin">リン</param>
        /// <param name="iron">鉄</param>
        /// <param name="zinc">亜鉛</param>
        /// <param name="copper">銅</param>
        /// <param name="manganese">マンガン</param>
        /// <param name="lodine">ヨウ素</param>
        /// <param name="selenium">セレン</param>
        /// <param name="chromium">クロム</param>
        /// <param name="molybdenum">モリブデン</param>
        public Nutrient(float energy, float protein, float lipid, float saturatedFattyAcids, float n_6FattyAcid,
                        float n_3FattyAcids, float carbohydrate, float dietaryFiber, float vitamin_A, float vitamin_D,
                        float vitamin_E, float vitamin_K, float vitamin_B1, float vitamin_B2, float vitamin_B6,
                        float vitamin_B12, float vitamin_C, float niacin, float folicAcid, float pantothenicAcid, 
                        float biotin, float sodium, float potassium, float calcium, float magnesium, float rin,
                        float iron, float zinc, float copper, float manganese, float lodine, float selenium,
                        float chromium, float molybdenum)
        {
            this.Energy = energy;
            this.Protein = protein;
            this.Lipid = lipid;
            this.SaturatedFattyAcids = saturatedFattyAcids;
            this.N_6FattyAcid = n_6FattyAcid;
            this.N_3FattyAcids = n_3FattyAcids;
            this.Carbohydrate = carbohydrate;
            this.DietaryFiber = dietaryFiber;
            this.Vitamin_A = vitamin_A;
            this.Vitamin_D = vitamin_D;
            this.Vitamin_E = vitamin_E;
            this.Vitamin_K = vitamin_K;
            this.Vitamin_B1 = vitamin_B1;
            this.Vitamin_B2 = vitamin_B2;
            this.Vitamin_B6 = vitamin_B6;
            this.Vitamin_B12 = vitamin_B12;
            this.Vitamin_C = vitamin_C;
            this.FolicAcid = folicAcid;
            this.PantothenicAcid = pantothenicAcid;
            this.Biotin = biotin;
            this.Sodium = sodium;
            this.Potassium = potassium;
            this.Calcium = calcium;
            this.Magnesium = magnesium;
            this.Rin = rin;
            this.Iron = iron;
            this.Zinc = zinc;
            this.Copper = copper;
            this.Manganese = manganese;
            this.Lodine = lodine;
            this.Selenium = selenium;
            this.Chromium = chromium;
            this.Molybdenum = molybdenum;
        }

        /// <summary>
        /// ctorプロパティ初期化詞で初期化済み
        /// </summary>
        public Nutrient()
        {
        }
    }
}
