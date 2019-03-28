
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MVVM_Refregator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System;
using System.Threading.Tasks;
using Windows.Storage;

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

        public static T LoadJsonFrom<T>(Func<string, string> pathToTextFunc, string path)
        {
            T deserializedObject = JsonConvert.DeserializeObject<T>(pathToTextFunc(path));
            return deserializedObject;
        }

        public static async Task<T> LoadJsonFromAsync<T>(string targetFilePath = @"food_data.json")
        {
            try
            {
                var storage = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await storage.GetFileAsync(targetFilePath);
                var text = await Windows.Storage.FileIO.ReadTextAsync(file);

                T deserializedObject;
                if (string.IsNullOrWhiteSpace(text) == false)
                {
                    deserializedObject = JsonConvert.DeserializeObject<T>(text);
                }
                else
                {
                    deserializedObject = default(T);
                }
                return deserializedObject;
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("ファイルが破損しています。", "食材管理アプリ");
                throw;
            }
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

        public static void SaveJsonTo<T>(T sourceObject, Action<string, string> pathToTextFunc, string path)
        {
            var jsonString = JsonConvert.SerializeObject(sourceObject, Formatting.Indented);
            pathToTextFunc(path, jsonString);
        }

        public static async Task<bool> SaveJsonToAsync<T>(T sourceObject, string path)
        {
            var jsonString = JsonConvert.SerializeObject(sourceObject, Formatting.Indented);
            StorageFolder storage = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await storage.GetFileAsync(path);
                await Windows.Storage.FileIO.WriteTextAsync(file, jsonString);
            }
            catch (Exception)
            {
            }

            return true;
        }

        public static ObservableCollection<FoodComposition> ReadJson(Func<string, string> readToTextFunc, string targetPath)
        {
            JToken foodComposition = JToken.Parse(readToTextFunc(targetPath));
            return ConvertToList(foodComposition);
        }

        /// <summary>
        /// 食品栄養標準成分表を読み込む
        /// </summary>
        /// <param name="targetPath">読み込むファイル名</param>
        /// <returns></returns>
        public static ObservableCollection<FoodComposition> ReadJson(string targetPath = @"food_composition_japanese.json")
        {
            JToken foodComposition = JToken.Parse(File.ReadAllText(targetPath, Encoding.GetEncoding("shift-jis")));
            return ConvertToList(foodComposition);
        }

        private static ObservableCollection<FoodComposition> ConvertToList(JToken foodComposition)
        {
            IList<JToken> compositionList = foodComposition.Children().ToList();
            var list = new ObservableCollection<FoodComposition>();
            for (int i = 0; i < compositionList.Count; i++)
            {
                JToken children = compositionList[i];
                var foodGroup = children["Food Group"].Value<int>();
                var itemNo = children["Item No."].Value<int>();
                var indexNo = children["Index No."].Value<int>();
                var foodAndDescription = children["Food and Description"].Value<string>();
                var refuse = Nutrient.Parse(children["Refuse"].Value<string>(), UnitKind.percent);
                var energy_kcal = Nutrient.Parse(children["Energy (kcal)"].Value<string>(), UnitKind.kcal);
                var energy_kj = Nutrient.Parse(children["Energy (kJ)"].Value<string>(), UnitKind.kj);
                var water = Nutrient.Parse(children["Water"].Value<string>(), UnitKind.g);
                var protein = Nutrient.Parse(children["Protein, calculated from  reference nitrogen"].Value<string>(), UnitKind.g);
                var protein_AminoAcidResidues = Nutrient.Parse(children["Protein, calculated as  the sum of amino acid residues"].Value<string>(), UnitKind.g);
                var lipid = Nutrient.Parse(children["Lipid"].Value<string>(), UnitKind.g);
                var fattyAcid_TriacylGlycerol = Nutrient.Parse(children["Fatty acid, triacyl-glycerol equivalents"].Value<string>(), UnitKind.g);
                var fattyAcid_Saturated = Nutrient.Parse(children["Fatty acid, saturated"].Value<string>(), UnitKind.g);
                var fattyAcid_MonoUnsaturated = Nutrient.Parse(children["Fatty acid, mono-unsaturated"].Value<string>(), UnitKind.g);
                var fattyAcid_PolyUnsaturated = Nutrient.Parse(children["Fatty acid, poly-unsaturated"].Value<string>(), UnitKind.g);
                var cholesterol = Nutrient.Parse(children["Cholesterol"].Value<string>(), UnitKind.g);
                var carbohydrate = Nutrient.Parse(children["Carbohydrate, total, calculated by difference"].Value<string>(), UnitKind.g);
                //var carbohydrate_Available = Nutrient.Parse(children["Carbohydrate, available; expressed in mono-saccharide equivalents"].Value<string>(), UnitKind.g);
                var carbohydrate_Available = Nutrient.Parse(children["Carbohydrate, available, expressed in mono-saccharide equivalents"].Value<string>(), UnitKind.g);
                var dietaryFiber_Soluble = Nutrient.Parse(children["Dietary fiber, soluble"].Value<string>(), UnitKind.g);
                var dietaryFiber_Insoluble = Nutrient.Parse(children["Dietary fiber, insoluble"].Value<string>(), UnitKind.g);
                var dietaryFiber_Total = Nutrient.Parse(children["Dietary fiber, total"].Value<string>(), UnitKind.g);
                var ash = Nutrient.Parse(children["Ash"].Value<string>(), UnitKind.g);
                var sodium = Nutrient.Parse(children["Sodium"].Value<string>(), UnitKind.mg);
                var potassium = Nutrient.Parse(children["Potassium"].Value<string>(), UnitKind.mg);
                var calcium = Nutrient.Parse(children["Calcium"].Value<string>(), UnitKind.mg);
                var magnesium = Nutrient.Parse(children["Magnesium"].Value<string>(), UnitKind.mg);
                var phosphorus = Nutrient.Parse(children["Phosphorus"].Value<string>(), UnitKind.mg);
                var iron = Nutrient.Parse(children["Iron"].Value<string>(), UnitKind.mg);
                var zinc = Nutrient.Parse(children["Zinc"].Value<string>(), UnitKind.mg);
                var copper = Nutrient.Parse(children["Copper"].Value<string>(), UnitKind.mg);
                var manganese = Nutrient.Parse(children["Manganese"].Value<string>(), UnitKind.mg);
                var iodine = Nutrient.Parse(children["Iodine"].Value<string>(), UnitKind.micro_g);
                var selenium = Nutrient.Parse(children["Selenium"].Value<string>(), UnitKind.micro_g);
                var chromium = Nutrient.Parse(children["Chromium"].Value<string>(), UnitKind.micro_g);
                var molybdenum = Nutrient.Parse(children["Molybdenum"].Value<string>(), UnitKind.micro_g);
                var retinol = Nutrient.Parse(children["Retinol"].Value<string>(), UnitKind.micro_g);
                var alpha_Carotene = Nutrient.Parse(children["alpha-Carotene"].Value<string>(), UnitKind.micro_g);
                var beta_Carotene = Nutrient.Parse(children["beta-Carotene"].Value<string>(), UnitKind.micro_g);
                var beta_Cryptoxathin = Nutrient.Parse(children["beta-Cryptoxanthin"].Value<string>(), UnitKind.micro_g);
                var beta_Caroteneequivalents = Nutrient.Parse(children["beta-Caroteneequivalents"].Value<string>(), UnitKind.micro_g);
                var retinon_ActivityEquivalents = Nutrient.Parse(children["Retinol activityequivalents"].Value<string>(), UnitKind.micro_g);
                var vitamin_D = Nutrient.Parse(children["Vitamin D"].Value<string>(), UnitKind.micro_g);
                var alpha_Tocopherol = Nutrient.Parse(children["alpha-Tocopherol"].Value<string>(), UnitKind.mg);
                var beta_Tocopherol = Nutrient.Parse(children["beta-Tocopherol"].Value<string>(), UnitKind.mg);
                var gamma_Tocopherol = Nutrient.Parse(children["gamma-Tocopherol"].Value<string>(), UnitKind.mg);
                var delta_Tocopherol = Nutrient.Parse(children["delta-Tocopherol"].Value<string>(), UnitKind.mg);
                var vitamin_K = Nutrient.Parse(children["Vitamin K"].Value<string>(), UnitKind.micro_g);
                var thiamin = Nutrient.Parse(children["Thiamin"].Value<string>(), UnitKind.mg);
                var riboflavin = Nutrient.Parse(children["Riboflavin"].Value<string>(), UnitKind.mg);
                var niacin = Nutrient.Parse(children["Niacin"].Value<string>(), UnitKind.mg);
                var vitamin_B6 = Nutrient.Parse(children["Vitamin B-6"].Value<string>(), UnitKind.mg);
                var vitamin_B12 = Nutrient.Parse(children["Vitamin B-12"].Value<string>(), UnitKind.micro_g);
                var folate = Nutrient.Parse(children["Folate"].Value<string>(), UnitKind.micro_g);
                var pantothenic_Acid = Nutrient.Parse(children["Pantothenic acid"].Value<string>(), UnitKind.mg);
                var biotin = Nutrient.Parse(children["Biotin"].Value<string>(), UnitKind.micro_g);
                var ascorbic_Acid = Nutrient.Parse(children["Ascorbic acid"].Value<string>(), UnitKind.mg);
                var salt_Equivalents = Nutrient.Parse(children["Salt equivalents"].Value<string>(), UnitKind.g);
                var alcohol = Nutrient.Parse(children["Alcohol"].Value<string>(), UnitKind.g);
                var nitrate_Ion = Nutrient.Parse(children["Nitrate ion "].Value<string>(), UnitKind.g);
                var theobromine = Nutrient.Parse(children["Theobromine"].Value<string>(), UnitKind.g);
                var caffeine = Nutrient.Parse(children["Caffeine"].Value<string>(), UnitKind.g);
                var tannin = Nutrient.Parse(children["Tannin"].Value<string>(), UnitKind.g);
                var polyphenol = Nutrient.Parse(children["Polyphenol"].Value<string>(), UnitKind.g);
                var acetic_Acid = Nutrient.Parse(children["Acetic acid"].Value<string>(), UnitKind.g);
                var cooking_Oil = Nutrient.Parse(children["Cooking oil"].Value<string>(), UnitKind.g);
                var organic_Acids = Nutrient.Parse(children["Organic acids, total"].Value<string>(), UnitKind.g);
                var yield = Nutrient.Parse(children["Yield"].Value<string>(), UnitKind.percent);

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
                                        thiamin, riboflavin, niacin, vitamin_B6, vitamin_B12, folate, pantothenic_Acid,
                                        biotin, ascorbic_Acid, salt_Equivalents, alcohol, nitrate_Ion, theobromine, caffeine,
                                        tannin, polyphenol, acetic_Acid, cooking_Oil, organic_Acids, yield)
                                        );
            }

            return list;
        }

        public static string SaveFoodComposition(ObservableCollection<FoodComposition> foodCompositions, string destinationPath = @"food_composition_data.json")
        {
            var seri = foodCompositions.Select(x =>
            {

                return new JObject(
                    new JProperty("Food Group", x.FoodGroup),
                    new JProperty("Item No.", x.ItemNo),
                    new JProperty("Index No.", x.IndexNo),
                    new JProperty("Food and Description", x.FoodDscription),
                    new JProperty("Refuse", x.Refuse.Value),
                    new JProperty("Energy (kcal)", x.Energy_kcal.Value),
                    new JProperty("Energy (kJ)", x.Energy_kj.Value),
                    new JProperty("Water", x.Water.Value),
                    new JProperty("Protein, calculated from  reference nitrogen", x.Protein.Value),
                    new JProperty("Protein, calculated as  the sum of amino acid residues", x.Protein_AminoAcidResidues.Value),
                    new JProperty("Lipid", x.Lipid.Value),
                    new JProperty("Fatty acid, triacyl-glycerol equivalents", x.FattyAcid_TriacylGlycerol.Value),
                    new JProperty("Fatty acid, saturated", x.FattyAcid_Saturated.Value),
                    new JProperty("Fatty acid, mono-unsaturated", x.FattyAcid_MonoUnsaturated.Value),
                    new JProperty("Fatty acid, poly-unsaturated", x.FattyAcid_PolyUnsaturated.Value),
                    new JProperty("Cholesterol", x.Cholesterol.Value),
                    new JProperty("Carbohydrate, total, calculated by difference", x.Carbohydrate.Value),
                    new JProperty("Carbohydrate, available, expressed in mono-saccharide equivalents", x.Carbohydrate_Available.Value),
                    new JProperty("Dietary fiber, soluble", x.DietaryFiber_Soluble.Value),
                    new JProperty("Dietary fiber, insoluble", x.DietaryFiber_Insoluble.Value),
                    new JProperty("Dietary fiber, total", x.DietaryFiber_Total.Value),
                    new JProperty("Ash", x.Ash.Value),
                    new JProperty("Sodium", x.Sodium.Value),
                    new JProperty("Potassium", x.Potassium.Value),
                    new JProperty("Calcium", x.Calcium.Value),
                    new JProperty("Magnesium", x.Magnesium.Value),
                    new JProperty("Phosphorus", x.Phosphorus.Value),
                    new JProperty("Iron", x.Iron.Value),
                    new JProperty("Zinc", x.Zinc.Value),
                    new JProperty("Copper", x.Copper.Value),
                    new JProperty("Manganese", x.Manganese.Value),
                    new JProperty("Iodine", x.Iodine.Value),
                    new JProperty("Selenium", x.Selenium.Value),
                    new JProperty("Chromium", x.Chromium.Value),
                    new JProperty("Molybdenum", x.Molybdenum.Value),
                    new JProperty("Retinol", x.Retinol.Value),
                    new JProperty("alpha-Carotene", x.Alpha_Carotene.Value),
                    new JProperty("beta-Carotene", x.Beta_Carotene.Value),
                    new JProperty("beta-Cryptoxanthin", x.Beta_Cryptoxanthin.Value),
                    new JProperty("beta-Caroteneequivalents", x.Beta_CaroteneEquivalents.Value),
                    new JProperty("Retinol activityequivalents", x.Retinon_ActivityEquivalents.Value),
                    new JProperty("Vitamin D", x.Vitamin_D.Value),
                    new JProperty("alpha-Tocopherol", x.Alpha_Tocopherol.Value),
                    new JProperty("beta-Tocopherol", x.Beta_Tocopherol.Value),
                    new JProperty("gamma-Tocopherol", x.Gamma_Tocopherol.Value),
                    new JProperty("delta-Tocopherol", x.Delta_Tocopherol.Value),
                    new JProperty("Vitamin K", x.Vitamin_K.Value),
                    new JProperty("Thiamin", x.Thiamin.Value),
                    new JProperty("Riboflavin", x.Riboflavin.Value),
                    new JProperty("Niacin", x.Niacin.Value),
                    new JProperty("Vitamin B-6", x.Vitamin_B6.Value),
                    new JProperty("Vitamin B-12", x.Vitamin_B12.Value),
                    new JProperty("Folate", x.Folate.Value),
                    new JProperty("Pantothenic acid", x.Pantothenic_Acid.Value),
                    new JProperty("Biotin", x.Biotin.Value),
                    new JProperty("Ascorbic acid", x.Ascorbic_Acid.Value),
                    new JProperty("Salt equivalents", x.Salt_Equivalents.Value),
                    new JProperty("Alcohol", x.Alcohol.Value),
                    new JProperty("Nitrate ion ", x.Nitrate_Ion.Value),
                    new JProperty("Theobromine", x.Theobromine.Value),
                    new JProperty("Caffeine", x.Caffeine.Value),
                    new JProperty("Tannin", x.Tannin.Value),
                    new JProperty("Polyphenol", x.Polyphenol.Value),
                    new JProperty("Acetic acid", x.Acetic_Acid.Value),
                    new JProperty("Cooking oil", x.Cooking_Oil.Value),
                    new JProperty("Organic acids, total", x.Organic_AcidsTotal.Value),
                    new JProperty("Yield", x.Yield.Value)
                    );

            }).ToList();

            return JsonManager.SaveJsonTo(seri, destinationPath);
            //return await JsonManager.SaveJsonToAsync(seri, destinationPath);
        }

    }
}
