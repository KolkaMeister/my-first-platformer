using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemRenderer<TDataType>
{
     void SetData(TDataType data,int index);
        
}
