using System;
using System.Collections.Generic;

namespace MobileApi.Models
{
    // Input database files

    public class DataBaseInputFile
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string RootFolder { get; set; }
        public string PathName { get; set; }
    }
}
