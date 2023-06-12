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

    private void Start()
    {
        GridCreate();
    }
    private void GridCreate()
    {
        for (int a = 0; a < fieldLine; a++) // Satır oluşumu
        {
            float posX = fieldColumn / 2f;
            float posZ = fieldLine / 2f; 
            GameObject line = Instantiate(cell, Vector3.zero, Quaternion.identity);
            line.transform.SetParent(transform);
            Vector3 _lineVector = new Vector3(((-posX*cell.transform.localScale.x) + (cell.transform.localScale.x / 2f)), transform.position.y, ((-posZ * cell.transform.localScale.x) + (cell.transform.localScale.x / 2f))+(cell.transform.localScale.x *a));
            line.transform.localPosition = _lineVector;
            // Tarla hücrelerinin toplam satır sayısını ikiye bölüyorum.
            // Prefab dosyasını Instantiate edip child hale getiriyorum.
            // Child objeye atamak üzere Vector3 tipinde bir değişken tanımlıyorum. Sütunun başlayacağı konumu kaydırıyorum.
            // Buradaki amacım Tarla herhangi bir NxN boyutunde yapılmak istendiğinde Parent objeye her zaman ortalı bir şekilde oluşacak.
            // Grid icerisindeki Cell prefabinin Scale'ine gore ortalama yapiyorum.
            for (int b = 1; b < fieldColumn; b++) // Sütun oluşumu
            {
                GameObject column = Instantiate(cell, Vector3.zero, Quaternion.identity);
                column.transform.SetParent(transform);
                Vector3 _columnVector = new Vector3(line.transform.localPosition.x + (cell.transform.localScale.x * b), line.transform.localPosition.y, line.transform.localPosition.z);
                column.transform.localPosition = _columnVector;
            }
        }
    }
    
}
