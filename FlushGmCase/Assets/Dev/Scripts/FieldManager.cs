using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [Header("Cell Properties")]
    [SerializeField] GameObject cell;
    [SerializeField] Material cellMaterial;    
    [SerializeField] int fieldLine;
    [SerializeField] int fieldColumn;
    [SerializeField] float offSetLine;
    [SerializeField] float offSetColumn;

    private List<CellManager> _cells = new List<CellManager>();
    private void Start()
    {
        GridCreate();
    }
    private void GridCreate()
    {
        GameObject cellParent = new GameObject("CellField"); // Parent nesnesi oluşturulur
        cellParent.transform.SetParent(transform);
        cellParent.transform.localPosition = Vector3.zero;

        for (int a = 0; a < fieldLine; a++) // Satır oluşumu
        {
            for (int b = 0; b < fieldColumn; b++) // Sütun oluşumu
            {
                GameObject newCell = Instantiate(cell, cellParent.transform); // Hücre prefabı parent nesnesi altında oluşturulur
                float posX = b * offSetColumn - ((fieldColumn - 1) * offSetColumn / 2f);
                float posZ = a * offSetLine - ((fieldLine - 1) * offSetLine / 2f);
                newCell.transform.localPosition = new Vector3(posX, transform.position.y, posZ);
                GetMaterial(newCell);
                _cells.Add(newCell.GetComponent<CellManager>()); // Oluşturulan hücreler, hücre listesine atılıyor.
            }
        }
        // Tarla hücrelerinin toplam satır sayısını ikiye bölüyorum.
        // Prefab dosyasını Instantiate edip child hale getiriyorum.
        // Child objeye atamak üzere Vector3 tipinde bir değişken tanımlıyorum. Sütunun başlayacağı konumu kaydırıyorum.
        // Buradaki amacım Tarla herhangi bir NxN boyutunde yapılmak istendiğinde Parent objeye her zaman ortalı bir şekilde oluşacak.
        // Grid icerisindeki Cell prefabinin Scale'ine gore ortalama yapiyorum.
    }
    private void GetMaterial(GameObject obj)
    {

        obj.GetComponent<MeshRenderer>().material = cellMaterial;
    }
    
}
