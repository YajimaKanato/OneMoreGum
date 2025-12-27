using UnityEngine;

public static class SaveManager
{
    const string JSON = ".json";

    /// <summary>
    /// Json形式でデータをセーブする関数
    /// </summary>
    /// <typeparam name="T">セーブするデータの型</typeparam>
    /// <param name="fileName">セーブするファイルの名前</param>
    /// <param name="saveData">セーブするデータ</param>
    public static void SaveDataPrefs<T>(string fileName, T saveData)
    {
        //var filePath = Path.Combine(Application.persistentDataPath, fileName + JSON);
        //var filePath = Application.persistentDataPath + $"/{fileName}";
        //これらは同じ

        PlayerPrefs.SetString(fileName + JSON, JsonUtility.ToJson(saveData));
        PlayerPrefs.Save();

        //Debug.Log($"{filePath}に保存");
    }

    /// <summary>
    /// Json形式でデータをロードする関数
    /// </summary>
    /// <typeparam name="T">ロードするデータの型</typeparam>
    /// <param name="fileName">ロードするファイルの名前</param>
    /// <returns></returns>
    public static T LoadDataPrefs<T>(string fileName)
    {
        //var filePath = Path.Combine(Application.persistentDataPath, fileName + JSON);
        var filePath = fileName + JSON;

        if (PlayerPrefs.HasKey(filePath))
        {
            var json = PlayerPrefs.GetString(filePath);
            var loaded = JsonUtility.FromJson<T>(json);
            Debug.Log("データをロードできました");
            return loaded;
        }
        else
        {
            Debug.Log("ファイルが見つかりませんでした");
            return default;
        }
    }
}
