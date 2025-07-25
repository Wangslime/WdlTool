﻿using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Drsoft.Tools.PdfLibrary
{
    public static class Pdfcrypt
    {
        static string key = "20250620202506202025062020250620";
        static string iv = "2025062020250620"; // 固定16字节

        public static string Decrypt(string sourcePath)
        {
            string inputPath = Path.Combine(Path.GetTempPath(), $"DrDecrypt/1111.txt");
            string outputPath = inputPath.Replace(".txt", "_New.txt");
            if (File.Exists(inputPath))
            {
                File.Delete(inputPath);
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            if (File.Exists(outputPath.Replace(".txt", ".pdf")))
            {
                File.Delete(outputPath.Replace(".txt", ".pdf"));
            }
            if (File.Exists(sourcePath))
            {
                var dirInfo = new DirectoryInfo(inputPath).Parent!;
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                File.Copy(sourcePath, inputPath);
            }
            if (File.Exists(inputPath))
            {
                DecryptFile(inputPath, outputPath, key, iv);
            }
            if (File.Exists(outputPath))
            {
                File.Move(outputPath, outputPath.Replace(".txt", ".pdf"));
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            if (File.Exists(inputPath))
            {
                File.Delete(inputPath);
            }
            return outputPath.Replace(".txt", ".pdf");
        }

        public static void Encrypt(string sourcePath)
        {
            string copyPath = sourcePath.Replace(".pdf", ".txt");
            string drPath = copyPath.Replace(".txt", "_Dr.txt");
            if (File.Exists(copyPath))
            {
                File.Delete(copyPath);
            }
            if (File.Exists(drPath))
            {
                File.Delete(drPath);
            }
            if (File.Exists(drPath.Replace(".txt", ".pdf")))
            {
                File.Delete(drPath.Replace(".txt", ".pdf"));
            }
            File.Copy(sourcePath, copyPath);
            if (File.Exists(copyPath))
            {
                EncryptFile(copyPath, drPath, key, iv);
                if (File.Exists(drPath))
                {
                    File.Move(drPath, drPath.Replace(".txt", ".pdf"));
                }
            }
            if (File.Exists(copyPath))
            {
                File.Delete(copyPath);
            }
            if (File.Exists(drPath))
            {
                File.Delete(drPath);
            }
        }

        // 加密TXT文件内容
        public static void EncryptFile(string inputPath, string outputPath, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                using (FileStream fsInput = new FileStream(inputPath, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputPath, FileMode.Create))
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsInput.CopyTo(cs); // 流式加密，避免内存溢出[5,8](@ref)
                }
            }
        }

        // 解密TXT文件内容
        public static void DecryptFile(string inputPath, string outputPath, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                using (FileStream fsInput = new FileStream(inputPath, FileMode.Open))
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                using (CryptoStream cs = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                using (FileStream fsOutput = new FileStream(outputPath, FileMode.Create))
                {
                    cs.CopyTo(fsOutput); // 流式解密，支持大文件[6,8](@ref)
                }
            }
        }
    }
}
