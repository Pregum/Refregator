using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Newtonsoft.Json;

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
            T fff = JsonConvert.DeserializeObject<T>(targetPath);
            return fff;
        }

        public static string SaveJsonTo<T>(T sourceObject, string destinationPath = @"food_data.json")
        {
            var jsonString = JsonConvert.SerializeObject(sourceObject, Formatting.Indented);
            File.WriteAllText(destinationPath, jsonString);
            return destinationPath;
        }
    }
}
