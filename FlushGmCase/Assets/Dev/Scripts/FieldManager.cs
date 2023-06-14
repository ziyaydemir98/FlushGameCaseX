using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [Header("Cell Properties")]
    [SerializeField] CellManager cellPrefab;
    [SerializeField] Material cellMaterial;
    [SerializeField] int fieldLine;
    [SerializeField] int fieldColumn;
    [SerializeField] float offSetLineBetween;
    [SerializeField] float offSetColumnBetween;

    private List<CellManager> _cells = new List<CellManager>();
    private void Start()
    {
        GridCreate();
        IntroductionCell();
    }
    private void GridCreate()
    {
        for (int a = 0; a < fieldLine; a++)
        {
            CellManager cellOfLine = Instantiate(cellPrefab, transform);
            Vector3 cellLocalScale = cellOfLine.CellBox.transform.localScale;
            float _posZBeginLine = ((fieldLine * cellLocalScale.z) / 2f) + (((fieldLine - 1f) * offSetLineBetween) / 2f);
            float _posXBeginLine = ((fieldColumn * cellLocalScale.x) / 2f) + (((fieldColumn - 1f) * offSetColumnBetween) / 2f);
            Vector3 _lineBegin = new Vector3((-_posXBeginLine) +cellLocalScale.x/2f, 0, (-_posZBeginLine) + (a * (cellLocalScale.z+offSetLineBetween))+cellLocalScale.z/2f); 
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
    
    private void IntroductionCell()
    {
        foreach (var cell in _cells)
        {
            cell.InstantCell();
            cell.CellBox.gameObject.GetComponent<MeshRenderer>().material = cellMaterial;
            cell.InstantGem();
        }
    }
}
