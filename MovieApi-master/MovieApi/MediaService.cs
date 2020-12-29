using Microsoft.Extensions.FileProviders;
using MovieApi.Entities;
using MovieApi.Helpers;
using MovieApi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi
{
    public interface IMediaService
    {
        void DeleteFile(string fileName);
        string SaveFile(byte[] data, string fileName);
        string SaveFile(Stream data, string fileName);
        Task<UploadedFile> UploadFile(byte[] data, string fileName, bool isUsed = false);
        Task<UploadedFile> UploadFile(Stream data, string fileName, bool isUsed = false);
        Task<Tuple<List<ValidationResult>, IFileInfo>> GetFile(Guid fileId);

        Task<Tuple<List<ValidationResult>, FileViewModel>> GetFileAsBase64(Guid fileId);
    }

    public class MediaService : IMediaService
    {

        private readonly IFileStorageService _fileStorageService;
        private readonly DataContext _unitOfWork;
        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public MediaService(IFileStorageService fileStorageService, DataContext unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                _fileStorageService.DeleteFile(fileName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string SaveFile(byte[] data, string fileName)
        {
            try
            {
                fileName = GenerateFileName(fileName);
                _fileStorageService.SaveBytes(fileName, data);
                return fileName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string SaveFile(Stream data, string fileName)
        {
            try
            {
                fileName = GenerateFileName(fileName);
                _fileStorageService.TrySaveStream(fileName, data);
                return fileName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UploadedFile> UploadFile(byte[] data, string fileName, bool isUsed = false)
        {
            try
            {
                fileName = GenerateFileName(fileName);
                _fileStorageService.SaveBytes(fileName, data);
                var tempFile = new UploadedFile { FilePath = fileName, IsUsed = isUsed };
                _unitOfWork.UploadedFiles.Add(tempFile);
                await _unitOfWork.SaveChangesAsync();
                return tempFile;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UploadedFile> UploadFile(Stream data, string fileName, bool isUsed = false)
        {
            try
            {
                fileName = GenerateFileName(fileName);
                _fileStorageService.TrySaveStream(fileName, data);
                var tempFile = new UploadedFile { FilePath = fileName, IsUsed = isUsed };
                _unitOfWork.UploadedFiles.Add(tempFile);
                await _unitOfWork.SaveChangesAsync();
                return tempFile;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<List<ValidationResult>, IFileInfo>> GetFile(Guid fileId)
        {
            try
            {

                var file = _unitOfWork.UploadedFiles.Where(x => x.Id == fileId).FirstOrDefault();
                if (file == null)
                {
                    results.Add(new ValidationResult("File not found"));
                    return Tuple.Create(results, (IFileInfo)null);
                }
                var document = _fileStorageService.GetFile(file.FilePath);
                return Tuple.Create(results, document);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<List<ValidationResult>, FileViewModel>> GetFileAsBase64(Guid fileId)
        {
            var file = _unitOfWork.UploadedFiles.Where(x => x.Id == fileId).FirstOrDefault();

            if (file == null)
            {
                results.Add(new ValidationResult("File not found"));
                return Tuple.Create(results, (FileViewModel)null);
            }

            var result = new FileViewModel
            {
                FileId = file.Id,
                File = Base64File(file),
                Name = file.FilePath,
            };

            return Tuple.Create(results, result);
        }

        private string Base64File(UploadedFile file)
        {
            if (file is null || string.IsNullOrWhiteSpace(file.FilePath))
                return string.Empty;

            try
            {
                var fileObj = _fileStorageService.GetFile(file.FilePath);

                if (fileObj is null)
                    return string.Empty;

                var stream = fileObj.CreateReadStream();
                byte[] bytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GenerateFileName(string fileName)
        {
            string ext = fileName.Split(".")[fileName.Split(".").Length - 1];
            string path = $"{fileName.Substring(0, fileName.Length - ext.Length)}_{DateTime.Now.Ticks}.{ext}";
            path = path.Replace(" ", "");
            path = GetSafeFileName(path);
            return path;
        }

        static string GetSafeFileName(string name, char replace = '_')
        {
            char[] invalids = Path.GetInvalidFileNameChars();
            return new string(name.Select(c => invalids.Contains(c) ? replace : c).ToArray());
        }
    }
}
