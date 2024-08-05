using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public static class StreamExtensions
    {
        public static byte[] ReadAsByteArray(this Stream stream)
        {
            var data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            return data;
        }
    }
}