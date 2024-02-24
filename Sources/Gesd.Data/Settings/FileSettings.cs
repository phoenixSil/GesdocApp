﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Data.Settings
{
    public class FileSettings
    {
        public string CheminLocalFichier { get; set; }
        public string Environment { get; set; }
        public string BlobContainerName { get; set; }
        public string ChaineDeConnectionBlob { get; set; }
        public string FolderName { get; set; }
        public int TailleFichier { get; set; }
    }
}