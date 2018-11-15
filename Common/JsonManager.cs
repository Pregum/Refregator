
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MVVM_Refregator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace MVVM_Refregator.Common
{
    public static class JsonManager
    {

        /// <summary>
        /// 指定したJsonファイルからオブジェクトを生成します
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="createClassType"></param>
        /// <returns></returns>
        public static T LoadJsonFrom<T>(string targetPath = @"food_data.json")
        {
            System.Diagnostics.Debug.WriteLine($"{typeof(T).ToString()}");
            T deserialized_object = JsonConvert.DeserializeObject<T>(File.ReadAllText(targetPath));
            return deserialized_object;
        }

        /// <summary>
        /// 現在のオブジェクトをjsonファイルに保存します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">保存したいオブジェクト</param>
        /// <param name="destinationPath">保存先のパス</param>
        /// <returns></returns>
        public static string SaveJsonTo<T>(T sourceObject, string destinationPath = @"food_data.json")
        {
            var jsonString = JsonConvert.SerializeObject(sourceObject, Formatting.Indented);
            File.WriteAllText(destinationPath, jsonString);
            return destinationPath;
        }

        /// <summary>
        /// 食品栄養標準成分表を読み込む
        /// </summary>
        /// <param name="targetPath">読み込むファイル名</param>
        /// <returns></returns>
        public static ObservableCollection<FoodComposition> ReadJson(string targetPath = @"food_composition_japanese.json")
        {
            var foodComposition = JToken.Parse(File.ReadAllText(targetPath, Encoding.GetEncoding("shift-jis")));
            IList<JToken> compositionList = foodComposition.Children().ToList();
            var list = new ObservableCollection<FoodComposition>();
            for (int i = 0; i < compositionList.Count; i++)
            {
                JToken children = compositionList[i];
                var foodGroup = children["Food Group"].Value<int>();
                var itemNo = children["Item No."].Value<int>();
                var indexNo = children["Index No."].Value<int>();
                var foodAndDescription = children["Food and Description"].Value<string>();
                var refuse = Nutrient.Create(children["Refuse"].Value<string>(), UnitKind.percent);
                var energy_kcal = Nutrient.Create(children["Energy (kcal)"].Value<string>(), UnitKind.kcal);
                var energy_kj = Nutrient.Create(children["Energy (kJ)"].Value<string>(), UnitKind.kj);
                var water = Nutrient.Create(children["Water"].Value<string>(), UnitKind.g);
                var protein = Nutrient.Create(children["Protein, calculated from  reference nitrogen"].Value<string>(), UnitKind.g);
                var protein_AminoAcidResidues = Nutrient.Create(children["Protein, calculated as  the sum of amino acid residues"].Value<string>(), UnitKind.g);
                var lipid = Nutrient.Create(children["Lipid"].Value<string>(), UnitKind.g);
                var fattyAcid_TriacylGlycerol = Nutrient.Create(children["Fatty acid, triacyl-glycerol equivalents"].Value<string>(), UnitKind.g);
                var fattyAcid_Saturated = Nutrient.Create(children["Fatty acid, saturated"].Value<string>(), UnitKind.g);
                var fattyAcid_MonoUnsaturated = Nutrient.Create(children["Fatty acid, mono-unsaturated"].Value<string>(), UnitKind.g);
                var fattyAcid_PolyUnsaturated = Nutrient.Create(children["Fatty acid, poly-unsaturated"].Value<string>(), UnitKind.g);
                var cholesterol = Nutrient.Create(children["Cholesterol"].Value<string>(), UnitKind.g);
                var carbohydrate = Nutrient.Create(children["Carbohydrate, total, calculated by difference"].Value<string>(), UnitKind.g);
                var carbohydrate_Available = Nutrient.Create(children["Carbohydrate, available; expressed in mono-saccharide equivalents"].Value<string>(), UnitKind.g);
                var dietaryFiber_Soluble = Nutrient.Create(children["Dietary fiber, soluble"].Value<string>(), UnitKind.g);
                var dietaryFiber_Insoluble = Nutrient.Create(children["Dietary fiber, insoluble"].Value<string>(), UnitKind.g);
                var dietaryFiber_Total = Nutrient.Create(children["Dietary fiber, total"].Value<string>(), UnitKind.g);
                var ash = Nutrient.Create(children["Ash"].Value<string>(), UnitKind.g);
                var sodium = Nutrient.Create(children["Sodium"].Value<string>(), UnitKind.mg);
                var potassium = Nutrient.Create(children["Potassium"].Value<string>(), UnitKind.mg);
                var calcium = Nutrient.Create(children["Calcium"].Value<string>(), UnitKind.mg);
                var magnesium = Nutrient.Create(children["Magnesium"].Value<string>(), UnitKind.mg);
                var phosphorus = Nutrient.Create(children["Phosphorus"].Value<string>(), UnitKind.mg);
                var iron = Nutrient.Create(children["Iron"].Value<string>(), UnitKind.mg);
                var zinc = Nutrient.Create(children["Zinc"].Value<string>(), UnitKind.mg);
                var copper = Nutrient.Create(children["Copper"].Value<string>(), UnitKind.mg);
                var manganese = Nutrient.Create(children["Manganese"].Value<string>(), UnitKind.mg);
                var iodine = Nutrient.Create(children["Iodine"].Value<string>(), UnitKind.micro_g);
                var selenium = Nutrient.Create(children["Selenium"].Value<string>(), UnitKind.micro_g);
                var chromium = Nutrient.Create(children["Chromium"].Value<string>(), UnitKind.micro_g);
                var molybdenum = Nutrient.Create(children["Molybdenum"].Value<string>(), UnitKind.micro_g);
                var retinol = Nutrient.Create(children["Retinol"].Value<string>(), UnitKind.micro_g);
                var alpha_Carotene = Nutrient.Create(children["alpha-Carotene"].Value<string>(), UnitKind.micro_g);
                var beta_Carotene = Nutrient.Create(children["beta-Carotene"].Value<string>(), UnitKind.micro_g);
                var beta_Cryptoxathin = Nutrient.Create(children["beta-Cryptoxanthin"].Value<string>(), UnitKind.micro_g);
                var beta_Caroteneequivalents = Nutrient.Create(children["beta-Caroteneequivalents"].Value<string>(), UnitKind.micro_g);
                var retinon_ActivityEquivalents = Nutrient.Create(children["Retinol activityequivalents"].Value<string>(), UnitKind.micro_g);
                var vitamin_D = Nutrient.Create(children["Vitamin D"].Value<string>(), UnitKind.micro_g);
                var alpha_Tocopherol = Nutrient.Create(children["alpha-Tocopherol"].Value<string>(), UnitKind.mg);
                var beta_Tocopherol = Nutrient.Create(children["beta-Tocopherol"].Value<string>(), UnitKind.mg);
                var gamma_Tocopherol = Nutrient.Create(children["gamma-Tocopherol"].Value<string>(), UnitKind.mg);
                var delta_Tocopherol = Nutrient.Create(children["delta-Tocopherol"].Value<string>(), UnitKind.mg);
                var vitamin_K = Nutrient.Create(children["Vitamin K"].Value<string>(), UnitKind.micro_g);
                var thiamin = Nutrient.Create(children["Thiamin"].Value<string>(), UnitKind.mg);
                var riboflavin = Nutrient.Create(children["Riboflavin"].Value<string>(), UnitKind.mg);
                var niacin = Nutrient.Create(children["Niacin"].Value<string>(), UnitKind.mg);
                var vitamin_B6 = Nutrient.Create(children["Vitamin B-6"].Value<string>(), UnitKind.mg);
                var vitamin_B12 = Nutrient.Create(children["Vitamin B-12"].Value<string>(), UnitKind.micro_g);
                var folate = Nutrient.Create(children["Folate"].Value<string>(), UnitKind.micro_g);
                var pantothenic_Acid = Nutrient.Create(children["Pantothenic acid"].Value<string>(), UnitKind.mg);
                var biotin = Nutrient.Create(children["Biotin"].Value<string>(), UnitKind.micro_g);
                var ascorbic_Acid = Nutrient.Create(children["Ascorbic acid"].Value<string>(), UnitKind.mg);
                var salt_Equivalents = Nutrient.Create(children["Salt equivalents"].Value<string>(), UnitKind.g);
                var alcohol = Nutrient.Create(children["Alcohol"].Value<string>(), UnitKind.g);
                var nitrate_Ion = Nutrient.Create(children["Nitrate ion "].Value<string>(), UnitKind.g);
                var theobromine = Nutrient.Create(children["Theobromine"].Value<string>(), UnitKind.g);
                var caffeine = Nutrient.Create(children["Caffeine"].Value<string>(), UnitKind.g);
                var tannin = Nutrient.Create(children["Tannin"].Value<string>(), UnitKind.g);
                var polyphenol = Nutrient.Create(children["Polyphenol"].Value<string>(), UnitKind.g);
                var acetic_Acid = Nutrient.Create(children["Acetic acid"].Value<string>(), UnitKind.g);
                var cooking_Oil = Nutrient.Create(children["Cooking oil"].Value<string>(), UnitKind.g);
                var organic_Acids = Nutrient.Create(children["Organic acids, total"].Value<string>(), UnitKind.g);
                var yield = Nutrient.Create(children["Yield"].Value<string>(), UnitKind.percent);

                list.Add(
                    new FoodComposition(foodGroup, itemNo, indexNo, foodAndDescription, refuse,
                                        energy_kcal, energy_kj, water, protein, protein_AminoAcidResidues,
                                        lipid, fattyAcid_TriacylGlycerol, fattyAcid_Saturated,
                                        fattyAcid_MonoUnsaturated, fattyAcid_PolyUnsaturated, cholesterol, carbohydrate,
                                        carbohydrate_Available, dietaryFiber_Soluble, dietaryFiber_Insoluble, dietaryFiber_Total,
                                        ash, sodium, potassium, calcium, magnesium, phosphorus,
                                        iron, zinc, copper, manganese, iodine, selenium,
                                        chromium, molybdenum, retinol, alpha_Carotene, beta_Carotene,
                                        beta_Cryptoxathin, beta_Caroteneequivalents, retinon_ActivityEquivalents, vitamin_D,
                                        alpha_Tocopherol, beta_Tocopherol, gamma_Tocopherol, delta_Tocopherol, vitamin_K,
                                        thiamin, niacin, vitamin_B6, vitamin_B12, folate, pantothenic_Acid,
                                        biotin, ascorbic_Acid, salt_Equivalents, alcohol, nitrate_Ion, theobromine, caffeine,
                                        tannin, polyphenol, acetic_Acid, cooking_Oil, organic_Acids, yield)
                                        );
            }

            return list;
        }
    }
}
