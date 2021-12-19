using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface ISettingsWindow<TWindowType>
{
    
     void AddToList(List<TWindowType> list);
     void RemoveFromList();
      
}
