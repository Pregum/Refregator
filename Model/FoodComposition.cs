namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食材栄養素
    /// </summary>
    public class FoodComposition
    {
        /// <summary>
        /// 食品群
        /// </summary>
        public int FoodGroup { get; private set; }
        /// <summary>
        /// 食品番号
        /// </summary>
        public int ItemNo { get; private set; }
        /// <summary>
        /// 索引番号
        /// </summary>
        public int IndexNo { get; private set; }
        /// <summary>
        /// 食品名
        /// </summary>
        public string FoodDscription { get; private set; }
        /// <summary>
        /// 廃棄率
        /// </summary>
        public Nutrient Refuse { get; private set; }
        /// <summary>
        /// エネルギー(kcal)
        /// </summary>
        public Nutrient Energy_kcal { get; private set; }
        /// <summary>
        /// エネルギー(kj)
        /// </summary>
        public Nutrient Energy_kj { get; private set; }
        /// <summary>
        /// 水分
        /// </summary>
        public Nutrient Water { get; private set; }
        /// <summary>
        /// タンパク質
        /// </summary>
        public Nutrient Protein { get; private set; }
        /// <summary>
        /// アミノ酸組成によるタンパク質
        /// </summary>
        public Nutrient Protein_AminoAcidResidues { get; private set; }
        /// <summary>
        /// 脂質
        /// </summary>
        public Nutrient Lipid { get; private set; }
        /// <summary>
        /// トリアシルグリセロール当量
        /// </summary>
        public Nutrient FattyAcid_TriacylGlycerol { get; private set; }
        /// <summary>
        /// 飽和脂肪酸
        /// </summary>
        public Nutrient FattyAcid_Saturated { get; private set; }
        /// <summary>
        /// 一価不飽和脂肪酸
        /// </summary>
        public Nutrient FattyAcid_MonoUnsaturated { get; private set; }
        /// <summary>
        /// 多価不飽和脂肪酸
        /// </summary>
        public Nutrient FattyAcid_PolyUnsaturated { get; private set; }
        /// <summary>
        /// コレステロール
        /// </summary>
        public Nutrient Cholesterol { get; private set; }
        /// <summary>
        /// 炭水化物
        /// </summary>
        public Nutrient Carbohydrate { get; private set; }
        /// <summary>
        /// 利用可能炭水化物
        /// </summary>
        public Nutrient Carbohydrate_Available { get; private set; }
        /// <summary>
        /// 水溶性食物繊維
        /// </summary>
        public Nutrient DietaryFiber_Soluble { get; private set; }
        /// <summary>
        /// 不溶性食物繊維
        /// </summary>
        public Nutrient DietaryFiber_Insoluble { get; private set; }
        /// <summary>
        /// 食物繊維総量
        /// </summary>
        public Nutrient DietaryFiber_Total { get; private set; }
        /// <summary>
        /// 灰分
        /// </summary>
        public Nutrient Ash { get; private set; }
        /// <summary>
        /// ナトリウム
        /// </summary>
        public Nutrient Sodium { get; private set; }
        /// <summary>
        /// カリウム
        /// </summary>
        public Nutrient Potassium { get; private set; }
        /// <summary>
        /// カルシウム
        /// </summary>
        public Nutrient Calcium { get; private set; }
        /// <summary>
        /// マグネシウム
        /// </summary>
        public Nutrient Magnesium { get; private set; }
        /// <summary>
        /// リン
        /// </summary>
        public Nutrient Phosphorus { get; private set; }
        /// <summary>
        /// 鉄
        /// </summary>
        public Nutrient Iron { get; private set; }
        /// <summary>
        /// 亜鉛
        /// </summary>
        public Nutrient Zinc { get; private set; }
        /// <summary>
        /// 銅
        /// </summary>
        public Nutrient Copper { get; private set; }
        /// <summary>
        /// マンガン
        /// </summary>
        public Nutrient Manganese { get; private set; }
        /// <summary>
        /// ヨウ素
        /// </summary>
        public Nutrient Iodine { get; private set; }
        /// <summary>
        /// セレン
        /// </summary>
        public Nutrient Selenium { get; private set; }
        /// <summary>
        /// クロム
        /// </summary>
        public Nutrient Chromium { get; private set; }
        /// <summary>
        /// モリブデン
        /// </summary>
        public Nutrient Molybdenum { get; private set; }
        /// <summary>
        /// レチノール
        /// </summary>
        public Nutrient Retinol { get; private set; }
        /// <summary>
        /// α-カロテン(ビタミンA）
        /// </summary>
        public Nutrient Alpha_Carotene { get; private set; }
        /// <summary>
        /// β-カロテン(ビタミンA)
        /// </summary>
        public Nutrient Beta_Carotene { get; private set; }
        /// <summary>
        /// β-クリプトサチン(ビタミンA)
        /// </summary>
        public Nutrient Beta_Cryptoxanthin { get; private set; }
        /// <summary>
        /// β-カロテン当量(ビタミンA)
        /// </summary>
        public Nutrient Beta_CaroteneEquivalents { get; private set; }
        /// <summary>
        /// レチノール活性当量(ビタミンA)
        /// </summary>
        public Nutrient Retinon_ActivityEquivalents { get; private set; }
        /// <summary>
        /// ビタミンD
        /// </summary>
        public Nutrient Vitamin_D { get; private set; }
        /// <summary>
        /// α-トコフェロール(ビタミンE)
        /// </summary>
        public Nutrient Alpha_Tocopherol { get; private set; }
        /// <summary>
        /// β-トコフェロール(ビタミンE)
        /// </summary>
        public Nutrient Beta_Tocopherol { get; private set; }
        /// <summary>
        /// γ-トコフェロール(ビタミンE)
        /// </summary>
        public Nutrient Gamma_Tocopherol { get; private set; }
        /// <summary>
        /// δ-トコフェロール(ビタミンE)
        /// </summary>
        public Nutrient Delta_Tocopherol { get; private set; }
        /// <summary>
        /// ビタミンK
        /// </summary>
        public Nutrient Vitamin_K { get; private set; }
        /// <summary>
        /// ビタミンB1
        /// </summary>
        public Nutrient Thiamin { get; private set; }
        /// <summary>
        /// ビタミンB2
        /// </summary>
        public Nutrient Riboflavin { get; private set; }
        /// <summary>
        /// ナイアシン
        /// </summary>
        public Nutrient Niacin { get; private set; }
        /// <summary>
        /// ビタミンB6
        /// </summary>
        public Nutrient Vitamin_B6 { get; private set; }
        /// <summary>
        /// ビタミンB12
        /// </summary>
        public Nutrient Vitamin_B12 { get; private set; }
        /// <summary>
        /// 葉酸
        /// </summary>
        public Nutrient Folate { get; private set; }
        /// <summary>
        /// パントテン酸
        /// </summary>
        public Nutrient Pantothenic_Acid { get; private set; }
        /// <summary>
        /// ビオチン
        /// </summary>
        public Nutrient Biotin { get; private set; }
        /// <summary>
        /// ビタミンC
        /// </summary>
        public Nutrient Ascorbic_Acid { get; private set; }
        /// <summary>
        /// 食塩相当量
        /// </summary>
        public Nutrient Salt_Equivalents { get; private set; }
        /// <summary>
        /// アルコール
        /// </summary>
        public Nutrient Alcohol { get; private set; }
        /// <summary>
        /// 硝酸イオン
        /// </summary>
        public Nutrient Nitrate_Ion { get; private set; }
        /// <summary>
        /// テオブロミン
        /// </summary>
        public Nutrient Theobromine { get; private set; }
        /// <summary>
        /// カフェイン
        /// </summary>
        public Nutrient Caffeine { get; private set; }
        /// <summary>
        /// タンニン
        /// </summary>
        public Nutrient Tannin { get; private set; }
        /// <summary>
        /// ポリフェノール
        /// </summary>
        public Nutrient Polyphenol { get; private set; }
        /// <summary>
        /// 酢酸
        /// </summary>
        public Nutrient Acetic_Acid { get; private set; }
        /// <summary>
        /// 調理油
        /// </summary>
        public Nutrient Cooking_Oil { get; private set; }
        /// <summary>
        /// 有機酸
        /// </summary>
        public Nutrient Organic_AcidsTotal { get; private set; }
        /// <summary>
        /// 重量変化率
        /// </summary>
        public Nutrient Yield { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="foodGroup">食品群</param>
        /// <param name="itemNo">食品番号</param>
        /// <param name="indexNo">索引番号</param>
        /// <param name="foodDescription">食品名</param>
        /// <param name="refuse">廃棄率</param>
        /// <param name="energy_kcal">エネルギー(kcal)</param>
        /// <param name="energy_kj">エネルギー(kJ)</param>
        /// <param name="water">水分</param>
        /// <param name="protein">タンパク質</param>
        /// <param name="protein_AminoAcidResidues">アミノ酸組成によるタンパク質</param>
        /// <param name="lipid">脂質</param>
        /// <param name="fattyAcid_TriacylGlycerol">トリアシルグリセロール当量</param>
        /// <param name="fattyAcid_Saturated">飽和脂肪酸</param>
        /// <param name="fattyAcid_MonoUnsaturated">一価不飽和脂肪酸</param>
        /// <param name="fattyAcid_polyUnsaturated">多価不飽和脂肪酸</param>
        /// <param name="cholesterol">コレステロール</param>
        /// <param name="carbohydrate">炭水化物</param>
        /// <param name="carbohydrate_Avaliable">利用可能炭水化物</param>
        /// <param name="dietaryFiber_Soluble">水溶性食物繊維</param>
        /// <param name="dietaryFiber_Insoluble">不溶性食物繊維</param>
        /// <param name="dietaryFiber_Total">食物繊維送料</param>
        /// <param name="ash">灰分</param>
        /// <param name="sodium">ナトリウム</param>
        /// <param name="potassium">カリウム</param>
        /// <param name="calcium">カルシウム</param>
        /// <param name="magnesium">マグネシウム</param>
        /// <param name="phosphorus">リン</param>
        /// <param name="iron">鉄</param>
        /// <param name="zinc">亜鉛</param>
        /// <param name="copper">銅</param>
        /// <param name="manganese">マンガン</param>
        /// <param name="iodine">ヨウ素</param>
        /// <param name="selenium">セレン</param>
        /// <param name="chromium">クロム</param>
        /// <param name="molybdenum">モリブデン</param>
        /// <param name="retinol">レチノール</param>
        /// <param name="alpha_Carotene">α-カロテン(ビタミンA)</param>
        /// <param name="beta_Carotene">β-カロテン(ビタミンA)</param>
        /// <param name="beta_Cryptoxathin">β-クリプトサチン(ビタミンA)</param>
        /// <param name="beta_CaroteneEquivalents">β-カロテン当量(ビタミンA)</param>
        /// <param name="retinon_ActivityEquivalents">レチノール活性当量(ビタミンA)</param>
        /// <param name="vitamin_D">ビタミンD</param>
        /// <param name="alpha_Tocopherol">α-トコフェロール(ビタミンE)</param>
        /// <param name="beta_Tocopherol">β-トコフェロール(ビタミンE)</param>
        /// <param name="gamma_Tocopherol">γ-トコフェロール(ビタミンE)</param>
        /// <param name="delta_Tocopherol">δ-トコフェロール(ビタミンE)</param>
        /// <param name="vitamin_K">ビタミンK</param>
        /// <param name="thiamin">ビタミンB1</param>
        /// <param name="riboflavin">ビタミンB2</param>
        /// <param name="niacin">ナイアシン</param>
        /// <param name="vitamin_B6">ビタミンB6</param>
        /// <param name="vitamin_B12">ビタミンB12</param>
        /// <param name="folate">葉酸</param>
        /// <param name="pantothenic_Acid">パントテン酸</param>
        /// <param name="biotin">ビオチン</param>
        /// <param name="ascorbic_Acid">ビタミンC</param>
        /// <param name="salt_Equivalents">食塩相当量</param>
        /// <param name="alcohol">アルコール</param>
        /// <param name="nitrate_Ion">硝酸イオン</param>
        /// <param name="Theobromine">テオブロミン</param>
        /// <param name="caffeine">カフェイン</param>
        /// <param name="tannin">タンイン</param>
        /// <param name="polyphenol">ポリフェノール</param>
        /// <param name="acetic_Acid">酢酸</param>
        /// <param name="cooking_Oil">調理油</param>
        /// <param name="organic_AcidsTotal">有機酸</param>
        /// <param name="yield">重量変化量</param>
        public FoodComposition(int foodGroup, int itemNo, int indexNo, string foodDescription, Nutrient refuse,
            Nutrient energy_kcal, Nutrient energy_kj, Nutrient water, Nutrient protein, Nutrient protein_AminoAcidResidues,
            Nutrient lipid, Nutrient fattyAcid_TriacylGlycerol, Nutrient fattyAcid_Saturated, Nutrient fattyAcid_MonoUnsaturated,
            Nutrient fattyAcid_polyUnsaturated, Nutrient cholesterol, Nutrient carbohydrate, Nutrient carbohydrate_Avaliable,
            Nutrient dietaryFiber_Soluble, Nutrient dietaryFiber_Insoluble, Nutrient dietaryFiber_Total,
            Nutrient ash, Nutrient sodium, Nutrient potassium, Nutrient calcium, Nutrient magnesium, Nutrient phosphorus,
            Nutrient iron, Nutrient zinc, Nutrient copper, Nutrient manganese, Nutrient iodine, Nutrient selenium,
            Nutrient chromium, Nutrient molybdenum, Nutrient retinol, Nutrient alpha_Carotene, Nutrient beta_Carotene,
            Nutrient beta_Cryptoxathin, Nutrient beta_CaroteneEquivalents, Nutrient retinon_ActivityEquivalents,
            Nutrient vitamin_D, Nutrient alpha_Tocopherol, Nutrient beta_Tocopherol, Nutrient gamma_Tocopherol,
            Nutrient delta_Tocopherol, Nutrient vitamin_K, Nutrient thiamin, Nutrient riboflavin,  Nutrient niacin, Nutrient vitamin_B6,
            Nutrient vitamin_B12, Nutrient folate, Nutrient pantothenic_Acid, Nutrient biotin, Nutrient ascorbic_Acid,
            Nutrient salt_Equivalents, Nutrient alcohol, Nutrient nitrate_Ion, Nutrient Theobromine, Nutrient caffeine,
            Nutrient tannin, Nutrient polyphenol, Nutrient acetic_Acid, Nutrient cooking_Oil, Nutrient organic_AcidsTotal,
            Nutrient yield)
        {
            this.FoodGroup = foodGroup;
            this.ItemNo = itemNo;
            this.IndexNo = indexNo;
            this.FoodDscription = foodDescription;
            this.Refuse = refuse;
            this.Energy_kcal = energy_kcal;
            this.Energy_kj = energy_kj;
            this.Water = water;
            this.Protein = protein;
            this.Protein_AminoAcidResidues = protein_AminoAcidResidues;
            this.Lipid = lipid;
            this.FattyAcid_TriacylGlycerol = fattyAcid_TriacylGlycerol;
            this.FattyAcid_Saturated = fattyAcid_Saturated;
            this.FattyAcid_MonoUnsaturated = fattyAcid_MonoUnsaturated;
            this.FattyAcid_PolyUnsaturated = fattyAcid_polyUnsaturated;
            this.Cholesterol = cholesterol;
            this.Carbohydrate = carbohydrate;
            this.Carbohydrate_Available = carbohydrate_Avaliable;
            this.DietaryFiber_Soluble = dietaryFiber_Soluble;
            this.DietaryFiber_Insoluble = dietaryFiber_Insoluble;
            this.DietaryFiber_Total = dietaryFiber_Total;
            this.Ash = ash;
            this.Sodium = sodium;
            this.Potassium = potassium;
            this.Calcium = calcium;
            this.Magnesium = magnesium;
            this.Phosphorus = phosphorus;
            this.Iron = iron;
            this.Zinc = zinc;
            this.Copper = copper;
            this.Manganese = manganese;
            this.Iodine = iodine;
            this.Selenium = selenium;
            this.Chromium = chromium;
            this.Molybdenum = molybdenum;
            this.Retinol = retinol;
            this.Alpha_Carotene = alpha_Carotene;
            this.Beta_Carotene = beta_Carotene;
            this.Beta_Cryptoxanthin = beta_Cryptoxathin;
            this.Beta_CaroteneEquivalents = beta_CaroteneEquivalents;
            this.Retinon_ActivityEquivalents = retinon_ActivityEquivalents;
            this.Vitamin_D = vitamin_D;
            this.Alpha_Tocopherol = alpha_Tocopherol;
            this.Beta_Tocopherol = beta_Tocopherol;
            this.Gamma_Tocopherol = gamma_Tocopherol;
            this.Delta_Tocopherol = delta_Tocopherol;
            this.Vitamin_K = vitamin_K;
            this.Thiamin = thiamin;
            this.Riboflavin = riboflavin; 
            this.Niacin = niacin;
            this.Vitamin_B6 = vitamin_B6;
            this.Vitamin_B12 = vitamin_B12;
            this.Folate = folate;
            this.Pantothenic_Acid = pantothenic_Acid;
            this.Biotin = biotin;
            this.Ascorbic_Acid = ascorbic_Acid;
            this.Salt_Equivalents = salt_Equivalents;
            this.Alcohol = alcohol;
            this.Nitrate_Ion = nitrate_Ion;
            this.Theobromine = Theobromine;
            this.Caffeine = caffeine;
            this.Tannin = tannin;
            this.Polyphenol = polyphenol;
            this.Acetic_Acid = acetic_Acid;
            this.Cooking_Oil = cooking_Oil;
            this.Organic_AcidsTotal = organic_AcidsTotal;
            this.Yield = yield;
        }

        public static FoodComposition operator+(FoodComposition a, FoodComposition b)
        {
            return new FoodComposition(0, 0, 0, "", 
                a.Refuse + b.Refuse,
                a.Energy_kcal + b.Energy_kcal,
                a.Energy_kj + b.Energy_kj,
                a.Water + b.Water,
                a.Protein + b.Protein,
                a.Protein_AminoAcidResidues + b.Protein_AminoAcidResidues,
                a.Lipid + b.Lipid,
                a.FattyAcid_TriacylGlycerol + b.FattyAcid_TriacylGlycerol,
                a.FattyAcid_Saturated + b.FattyAcid_Saturated,
                a.FattyAcid_MonoUnsaturated + b.FattyAcid_MonoUnsaturated,
                a.FattyAcid_PolyUnsaturated + b.FattyAcid_PolyUnsaturated,
                a.Cholesterol + b.Cholesterol,
                a.Carbohydrate + b.Carbohydrate,
                a.Carbohydrate_Available + b.Carbohydrate_Available,
                a.DietaryFiber_Soluble + b.DietaryFiber_Soluble,
                a.DietaryFiber_Insoluble + b.DietaryFiber_Insoluble,
                a.DietaryFiber_Total + b.DietaryFiber_Total,
                a.Ash + b.Ash,
                a.Sodium + b.Sodium,
                a.Potassium + b.Potassium,
                a.Calcium + b.Calcium,
                a.Magnesium + b.Magnesium,
                a.Phosphorus + b.Phosphorus,
                a.Iron + b.Iron,
                a.Zinc + b.Zinc,
                a.Copper + b.Copper,
                a.Manganese + b.Manganese,
                a.Iodine + b.Iodine,
                a.Selenium + b.Selenium,
                a.Chromium + b.Chromium,
                a.Molybdenum + b.Molybdenum,
                a.Retinol + b.Retinol,
                a.Alpha_Carotene + b.Alpha_Carotene,
                a.Beta_Carotene + b.Beta_Carotene,
                a.Beta_Cryptoxanthin + b.Beta_Cryptoxanthin,
                a.Beta_CaroteneEquivalents + b.Beta_CaroteneEquivalents,
                a.Retinon_ActivityEquivalents + b.Retinon_ActivityEquivalents,
                a.Vitamin_D + b.Vitamin_D,
                a.Alpha_Tocopherol + b.Alpha_Tocopherol,
                a.Beta_Tocopherol + b.Beta_Tocopherol,
                a.Gamma_Tocopherol + b.Gamma_Tocopherol,
                a.Delta_Tocopherol + b.Delta_Tocopherol,
                a.Vitamin_K + b.Vitamin_K,
                a.Thiamin + b.Thiamin,
                a.Riboflavin + b.Riboflavin,
                a.Niacin + b.Niacin,
                a.Vitamin_B6 + b.Vitamin_B6,
                a.Vitamin_B12 + b.Vitamin_B12,
                a.Folate + b.Folate,
                a.Pantothenic_Acid + b.Pantothenic_Acid,
                a.Biotin + b.Biotin,
                a.Ascorbic_Acid + b.Ascorbic_Acid,
                a.Salt_Equivalents + b.Salt_Equivalents,
                a.Alcohol + b.Alcohol,
                a.Nitrate_Ion + b.Nitrate_Ion,
                a.Theobromine + b.Theobromine,
                a.Caffeine + b.Caffeine,
                a.Tannin + b.Tannin,
                a.Polyphenol + b.Polyphenol,
                a.Acetic_Acid + b.Acetic_Acid,
                a.Cooking_Oil + b.Cooking_Oil,
                a.Organic_AcidsTotal + b.Organic_AcidsTotal,
                a.Yield + b.Yield);
        }
    }
}
