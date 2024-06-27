using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChessController))]
public class ChessDimensionsEditor : Editor
{
    public override void OnInspectorGUI()
    {

        ChessController boardManager = (ChessController)target;

        if (boardManager == null)
        {
            EditorGUILayout.LabelField("BoardManager is null.");
            return;
        }
        // Отображаем стандартный инспектор для других полей
        DrawDefaultInspector();
        if (boardManager.pieces != null)
        {
            // Динамічне налаштування розмірів масиву
            int rows = EditorGUILayout.IntField("Rows", boardManager.pieces.GetLength(0));
            int columns = EditorGUILayout.IntField("Columns", boardManager.pieces.GetLength(1));

            if (rows != boardManager.pieces.GetLength(0) || columns != boardManager.pieces.GetLength(1))
            {
                GameObject[,] newBoard = new GameObject[rows, columns];
                for (int i = 0; i < Mathf.Min(rows, boardManager.pieces.GetLength(0)); i++)
                {
                    for (int j = 0; j < Mathf.Min(columns, boardManager.pieces.GetLength(1)); j++)
                    {
                        newBoard[i, j] = boardManager.pieces[i, j];
                    }
                }
                boardManager.pieces = newBoard;
            }

            // Отображение массива в виде кубиков с поворотом на 90 градусов
            for (int i = 0; i < boardManager.pieces.GetLength(1); i++) // изменяем порядок циклов
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < boardManager.pieces.GetLength(0); j++) // изменяем порядок циклов
                {
                    // Поворот индексации на 90 градусов
                    bool hasObject = boardManager.pieces[j, boardManager.pieces.GetLength(1) - 1 - i] != null;
                    bool newHasObject = EditorGUILayout.Toggle(hasObject, GUILayout.Width(20), GUILayout.Height(20));

                    if (newHasObject != hasObject)
                    {
                        if (newHasObject)
                        {
                            // Create a new GameObject if the toggle was changed to true
                            boardManager.pieces[j, boardManager.pieces.GetLength(1) - 1 - i] = new GameObject("New Object");
                        }
                        else
                        {
                            // Destroy the GameObject if the toggle was changed to false
                            DestroyImmediate(boardManager.pieces[j, boardManager.pieces.GetLength(1) - 1 - i]);
                            boardManager.pieces[j, boardManager.pieces.GetLength(1) - 1 - i] = null;
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
    }
}

