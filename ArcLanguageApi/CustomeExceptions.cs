using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ARCLanguageApi
{
    public class DuplicateDataInsertException : IOException
    {
        public string Message { get; }
        public int StatusCode { get; }

        public DuplicateDataInsertException(string message)
        {
            StatusCode = 601;
            Message = message;
        }
    }

}
