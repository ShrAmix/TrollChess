using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(TabelController))]
public class GridDimensionsEditor : Editor
{
   /* public override void OnInspectorGUI()
    {
        // �������� ������ �� ������� ������
        TabelController gridDimensions = (TabelController)target;

        if (gridDimensions == null)
        {
            EditorGUILayout.LabelField("GridDimensions is null.");
            return;
        }

        // ���������� ����������� ��������� ��� ������ �����
        DrawDefaultInspector();

        // ������ ��� ��������� _width
        EditorGUILayout.LabelField("Width Tabel: " + gridDimensions.Width);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-8")) gridDimensions.Width -= 8;
        if (GUILayout.Button("-6")) gridDimensions.Width -= 6;
        if (GUILayout.Button("-4")) gridDimensions.Width -= 4;
        if (GUILayout.Button("-2")) gridDimensions.Width -= 2;
        if (GUILayout.Button("+2")) gridDimensions.Width += 2;
        if (GUILayout.Button("+4")) gridDimensions.Width += 4;
        if (GUILayout.Button("+6")) gridDimensions.Width += 6;
        if (GUILayout.Button("+8")) gridDimensions.Width += 8;
        EditorGUILayout.EndHorizontal();

        // ������ ��� ��������� _height
        EditorGUILayout.LabelField("Height Tabel: " + gridDimensions.Height);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-8")) gridDimensions.Height -= 8;
        if (GUILayout.Button("-6")) gridDimensions.Height -= 6;
        if (GUILayout.Button("-4")) gridDimensions.Height -= 4;
        if (GUILayout.Button("-2")) gridDimensions.Height -= 2;
        if (GUILayout.Button("+2")) gridDimensions.Height += 2;
        if (GUILayout.Button("+4")) gridDimensions.Height += 4;
        if (GUILayout.Button("+6")) gridDimensions.Height += 6;
        if (GUILayout.Button("+8")) gridDimensions.Height += 8;
        EditorGUILayout.EndHorizontal();

        // ���������� ���������
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        
        
        
        
        
        /*TabelController boardManager = (TabelController)target;

        if (boardManager == null)
        {
            EditorGUILayout.LabelField("BoardManager is null.");
            return;
        }

        if (boardManager._boardCells != null)
        {
            // �������� ������������ ������ ������
            int rows = EditorGUILayout.IntField("Rows", boardManager._boardCells.GetLength(0));
            int columns = EditorGUILayout.IntField("Columns", boardManager._boardCells.GetLength(1));

            if (rows != boardManager._boardCells.GetLength(0) || columns != boardManager._boardCells.GetLength(1))
            {
                GameObject[,] newBoard = new GameObject[rows, columns];
                for (int i = 0; i < Mathf.Min(rows, boardManager._boardCells.GetLength(0)); i++)
                {
                    for (int j = 0; j < Mathf.Min(columns, boardManager._boardCells.GetLength(1)); j++)
                    {
                        newBoard[i, j] = boardManager._boardCells[i, j];
                    }
                }
                boardManager._boardCells = newBoard;
            }

            // ����������� ������� � ���� ������� � ��������� �� 90 ��������
            for (int i = 0; i < boardManager._boardCells.GetLength(1); i++) // �������� ������� ������
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < boardManager._boardCells.GetLength(0); j++) // �������� ������� ������
                {
                    // ������� ���������� �� 90 ��������
                    bool hasObject = boardManager._boardCells[j, boardManager._boardCells.GetLength(1) - 1 - i] != null;
                    bool newHasObject = EditorGUILayout.Toggle(hasObject, GUILayout.Width(20), GUILayout.Height(20));

                    if (newHasObject != hasObject)
                    {
                        if (newHasObject)
                        {
                            // Create a new GameObject if the toggle was changed to true
                            boardManager._boardCells[j, boardManager._boardCells.GetLength(1) - 1 - i] = new GameObject("New Object");
                        }
                        else
                        {
                            // Destroy the GameObject if the toggle was changed to false
                            DestroyImmediate(boardManager._boardCells[j, boardManager._boardCells.GetLength(1) - 1 - i]);
                            boardManager._boardCells[j, boardManager._boardCells.GetLength(1) - 1 - i] = null;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("BoardCells is null.");
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }*/

}
