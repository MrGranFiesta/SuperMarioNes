using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public Difficult Difficult { get; private set; }
    public string Languaje { get; private set; }
    public List<string> AvaibleLanguages { get; private set; } = new List<string>();

    /*private List<string> GetAvaibleLanguages(){
        return AvaibleLanguages;
    }*/
}