using System;
using UnityEngine;
using System.Collections.Generic;

namespace Hsy.Core
{
  public class Log
  {
    public Log()
    {
    }

    public static void Println(string s)
    {
      Console.WriteLine(s);
      Debug.Log(s);
    }
  }
}
