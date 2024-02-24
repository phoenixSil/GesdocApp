﻿using Gesd.Entite;
using Gesd.Features.Dtos.Fichiers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Compute.Tools
{
    public static class ComputeFile
    {
        public static FileToAddDto GenererLeDtoDeSauvegarde(FileToAddDto file, string fileName)
        {
            var fileDto = new FileToAddDto
            {
                FileName = fileName,
                Description = "default Description",
                FileSize = GetFileSizeInMegabytes(file.File),
                FileType = GetFileType(file.File),
                Version = 1,
                File = file.File
            };
            return fileDto;
        }

        public static double GetFileSizeInMegabytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return 0;
            }
            else
            {
                return (double)file.Length / (1024 * 1024); // Convertit la taille en mégaoctets
            }
        }

        public static string GetFileType(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            else
            {
                return file.ContentType;
            }
        }

        public static EncryptedUrlFile GenererDtoEncFile(Entite.File blobdataResponse, string url)
        {
            var efile = new EncryptedUrlFile
            {
                FileId = blobdataResponse.Id,
                EncryptedUrl  = url
            };

            return efile;
        }

        public static string EncryptagedeLUrl(string url, string cleDeChiffrement)
        {
            byte[] iv;
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(cleDeChiffrement);
                iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(url);
                        }
                    }

                    array = memoryStream.ToArray();
                }
            }

            return Convert.ToBase64String(iv.Concat(array).ToArray());
        }

        public static string DecryptUrl(string encryptedUrl, string cleDeChiffrement)
        {
            byte[] buffer = Convert.FromBase64String(encryptedUrl);
            byte[] iv = buffer.Take(16).ToArray();
            byte[] array = buffer.Skip(16).ToArray();

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(cleDeChiffrement);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(array))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GenererCleDeChiffrement()
        {
            const int keySize = 128; // Taille de la clé en bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                var keyBytes = new byte[keySize / 8]; // Convertir en octets
                rng.GetBytes(keyBytes); // Remplir avec des octets aléatoires
                return Convert.ToBase64String(keyBytes); // Convertir en chaîne Base64 pour une utilisation facile
            }
        }

        public static KeyStore GenererDtoKeyStore(string generatedKey, Guid encUrlId)
        {
            return new KeyStore
            {
                GeneratedKey = generatedKey,
                EncryptedUrlId = encUrlId
            };
        }
    }
}
