using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;//для корректной работы интерфейса
using UnityEngine.Networking;//связанны с событиями, которые связанны с интернетом 
using UnityEngine.EventSystems; //для обработки событий

public class NewBehaviourScript : MonoBehaviour
{
    public Text nm;
    public Text lvl;
    public Text dat1;//чтобы показать соответствие
    public string jsonURL;//ссылка на облако
    public Jsonclass jsnData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());//выполняется автоматически, как только будет запущено
    }
    IEnumerator getData()
    {
        Debug.Log("Загрузка...");
        var uwr = new UnityWebRequest(jsonURL);//запрашиваем по ссылке данные
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");//запись в локальное хранилище
        var dh = new DownloadHandlerFile(resultFile);//запись данных из  массива в файл
        dh.removeFileOnAbort = true;//при ошибках загрузки
        uwr.downloadHandler = dh;//буфферизация
        yield return uwr.SendWebRequest();//точка, где выполнение будет приостановлено и возобновится в следующем кадре

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            nm.text = "Ошибка!";
        }
        else
        {
            Debug.Log("Файл сохранен по пути: " + resultFile);
            jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));//отвечает за чтение из файла
            nm.text = jsnData.Name.ToString();
            lvl.text = jsnData.Level.ToString();
            dat1.text = jsnData.Parametr.ToString();
            yield return StartCoroutine(getData());
        }
    }
    [System.Serializable]
    public class Jsonclass
    {
        public string Name;
        public int Level;
        public int Parametr;
    }
    // Update is called once per frame
    void Update()
    {

    }
}