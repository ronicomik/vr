using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;//��� ���������� ������ ����������
using UnityEngine.Networking;//�������� � ���������, ������� �������� � ���������� 
using UnityEngine.EventSystems; //��� ��������� �������

public class NewBehaviourScript : MonoBehaviour
{
    public Text nm;
    public Text lvl;
    public Text dat1;//����� �������� ������������
    public string jsonURL;//������ �� ������
    public Jsonclass jsnData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());//����������� �������������, ��� ������ ����� ��������
    }
    IEnumerator getData()
    {
        Debug.Log("��������...");
        var uwr = new UnityWebRequest(jsonURL);//����������� �� ������ ������
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");//������ � ��������� ���������
        var dh = new DownloadHandlerFile(resultFile);//������ ������ ��  ������� � ����
        dh.removeFileOnAbort = true;//��� ������� ��������
        uwr.downloadHandler = dh;//������������
        yield return uwr.SendWebRequest();//�����, ��� ���������� ����� �������������� � ������������ � ��������� �����

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            nm.text = "������!";
        }
        else
        {
            Debug.Log("���� �������� �� ����: " + resultFile);
            jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));//�������� �� ������ �� �����
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