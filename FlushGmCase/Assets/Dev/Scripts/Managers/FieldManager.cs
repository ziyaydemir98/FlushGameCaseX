using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Hucre icin prefab dosyasi koyuyorum.
    /// Hucrenin icerisindeki alt objeye materyal atamak icin materyal tanimliyorum.
    /// Tarlanin kac Satir olacagini, kac sütun olacağını belirliyorum.
    /// Tarlanin satir ve sütunları arasında kac birimlik bosluk olmasını istediğimi belirtiyorum.
    /// </summary>
    [Header("Cell Properties")]
    [SerializeField] CellManager cellPrefab;
    [SerializeField] Material cellMaterial;
    [SerializeField] int fieldLine;
    [SerializeField] int fieldColumn;
    [SerializeField] float offSetLineBetween;
    [SerializeField] float offSetColumnBetween;

    private List<CellManager> _cells = new List<CellManager>();
    #endregion

    private void Start()
    {
        GridCreate();
        IntroductionCell();
    }

    /// <summary>
    /// Hücrelerin olusma algorıtması.
    /// Önce Satırları gereklı X ve Z eksenı derinliğinde olusturuyorum.
    /// Daha sonra olusan satırın sütunlarını olusturuyorum.
    /// Tarla hücrelerini daha sonra kullanmak üzere bir listeye atiyorum.
    /// </summary>

    #region Functions
    private void GridCreate()
    {
        for (int a = 0; a < fieldLine; a++)
        {
            CellManager cellOfLine = Instantiate(cellPrefab, transform);
            Vector3 cellLocalScale = cellOfLine.CellBox.transform.localScale;
            float _posZBeginLine = ((fieldLine * cellLocalScale.z) / 2f) + (((fieldLine - 1f) * offSetLineBetween) / 2f);
            float _posXBeginLine = ((fieldColumn * cellLocalScale.x) / 2f) + (((fieldColumn - 1f) * offSetColumnBetween) / 2f);
            Vector3 _lineBegin = new Vector3((-_posXBeginLine) + cellLocalScale.x / 2f, 0, (-_posZBeginLine) + (a * (cellLocalScale.z + offSetLineBetween)) + cellLocalScale.z / 2f);
            cellOfLine.transform.localPosition = _lineBegin;
            _cells.Add(cellOfLine);

            for (int b = 1; b < fieldColumn; b++)
            {
                CellManager cellOfColumn = Instantiate(cellPrefab, transform);
                float _posXBeginColumn = (_lineBegin.x) + (b * (cellLocalScale.x + offSetColumnBetween));
                Vector3 _columnBegin = new Vector3(_posXBeginColumn, 0, _lineBegin.z);
                cellOfColumn.transform.localPosition = _columnBegin;
                _cells.Add(cellOfColumn);
            }

        }
    }


    /// <summary>
    /// Hucrelerin icerisinde gerekli fonksiyonlari hucrelerin hepsi olustuktan sonra baslatiyorum.
    /// </summary>
    private void IntroductionCell()
    {
        foreach (var cell in _cells)
        {
            cell.InstantCell();
            cell.CellBox.gameObject.GetComponent<MeshRenderer>().material = cellMaterial;
            cell.InstantGem();
        }
    }
    #endregion

}
