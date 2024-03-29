﻿using System.ComponentModel.DataAnnotations;

namespace Gesd.Entite
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModify { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public int Version { get; set; }
        public EncryptedUrlFile EncryptedUrlFile { get; set; }
    }
}
