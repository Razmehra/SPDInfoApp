using System;
using System.Collections.Generic;
using System.Text;

namespace SPDInfoApp.Interfaces
{
    public interface IFileReadWrite
    {
        void WriteData(string fileName, string data);
        string ReadData(string filename);

    }
}
