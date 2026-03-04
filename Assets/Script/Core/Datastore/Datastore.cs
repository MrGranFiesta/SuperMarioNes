
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Datastore
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "datastore.json");

    public List<int> GetPoints() {
        if (!File.Exists(SavePath)) { 
            return new List<int>();
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            DatastoreDTO dto = JsonUtility.FromJson<DatastoreDTO>(json);

            return dto?.Points ?? new List<int>();
        }
        catch (Exception e)
        {
            Debug.LogError($"[Datastore] Error al parsear el JSON de Datastore: {e.Message}");
        }
        return new List<int>();
    }

    public void AddPoints(int point) {
        List<int> points = GetPoints();
        points.Add(point);

        DatastoreDTO datastoreDTO = new DatastoreDTO { Points = points };

        try
        {
            string json = JsonUtility.ToJson(datastoreDTO, true); // true = pretty print (más legible)
            File.WriteAllText(SavePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al GUARDAR el archivo: {e.Message}\nRuta: {SavePath}");
        }
    }
}
