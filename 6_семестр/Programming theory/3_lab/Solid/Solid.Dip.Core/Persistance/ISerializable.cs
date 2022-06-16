using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Dip.Persistance;

public interface ISerializable<T>
{
    string Serialize(T item);
}
