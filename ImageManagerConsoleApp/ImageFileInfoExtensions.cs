using SQLData;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public static class ImageFileInfoExtensions
    {

        #region PrivateMethods

        private static Regex _ThumbOrFacePattern = new Regex(@"(\w+_\d+)(_1024|face\d+)");

        private static string GetRootImageName(string imageName)
        {
            Match nameMatch = _ThumbOrFacePattern.Match(imageName);

            if (nameMatch.Success)
            {
                return nameMatch.Groups.Count == 3 ? nameMatch.Groups[1].Value : nameMatch.Value;
            }
            else
            {
                return imageName;
            }
        }

        #endregion

        public static DataObjects.ImageFileInfo AsDataObjectsImageFileInfo(this ImageFileInfo imageFileInfo)
        {
            return new DataObjects.ImageFileInfo()
            {
                ImageId = imageFileInfo.ImageId,
                FileCreateDate = imageFileInfo.FileCreateDate,
                FileExtension = imageFileInfo.FileExtension,
                FileFullPath = imageFileInfo.FileFullPath,
                FileLastWriteTime = imageFileInfo.FileLastWriteTime,
                FileName = imageFileInfo.FileName,
                FileNameWithoutExtension = imageFileInfo.FileNameWithoutExtension,
                FileSize = imageFileInfo.FileSize,
                ImageHeight = imageFileInfo.ImageHeight,
                ImageModDateTime = imageFileInfo.ImageModDateTime,
                ImageOriginalDateTime = imageFileInfo.ImageOriginalDateTime,
                ImageSize = imageFileInfo.ImageSize,
                ImageWidth = imageFileInfo.ImageWidth,
                LikelyDateTime = imageFileInfo.LikelyDateTime,
                IsMoved = imageFileInfo.IsMoved,
                NewFullPath = imageFileInfo.NewFullPath,
                UpdatedBy = imageFileInfo.UpdatedBy,
                LastUpdated = imageFileInfo.LastUpdated,
            };
        }

        public static ImageFileInfo AsImageFileInfo(this DataObjects.ImageFileInfo imageFileInfo)
        {
            return new ImageFileInfo()
            {
                ImageId = imageFileInfo.ImageId,
                FileCreateDate = imageFileInfo.FileCreateDate,
                FileExtension = imageFileInfo.FileExtension,
                FileFullPath = imageFileInfo.FileFullPath,
                FileLastWriteTime = imageFileInfo.FileLastWriteTime,
                FileName = imageFileInfo.FileName,
                FileNameWithoutExtension = imageFileInfo.FileNameWithoutExtension,
                FileSize = imageFileInfo.FileSize,
                ImageHeight = imageFileInfo.ImageHeight,
                ImageModDateTime = imageFileInfo.ImageModDateTime,
                ImageOriginalDateTime = imageFileInfo.ImageOriginalDateTime,
                ImageSize = imageFileInfo.ImageSize,
                ImageWidth = imageFileInfo.ImageWidth,
                IsMoved = imageFileInfo.IsMoved,
                NewFullPath = imageFileInfo.NewFullPath,
                UpdatedBy = imageFileInfo.UpdatedBy,
                LastUpdated = imageFileInfo.LastUpdated,
            };
        }

        public static SQL_ImageFileInfo AsSQLImageFileInfo(this DataObjects.ImageFileInfo imageFileInfo, string dbConnection)
        {
            return new SQL_ImageFileInfo()
            {
                Connection = dbConnection,
                ImageId = imageFileInfo.ImageId,
                FileCreateDate = imageFileInfo.FileCreateDate,
                FileExtension = imageFileInfo.FileExtension,
                FileFullPath = imageFileInfo.FileFullPath,
                FileLastWriteTime = imageFileInfo.FileLastWriteTime,
                FileName = imageFileInfo.FileName,
                FileNameWithoutExtension = imageFileInfo.FileNameWithoutExtension,
                FileSize = imageFileInfo.FileSize,
                ImageHeight = imageFileInfo.ImageHeight,
                ImageModDateTime = imageFileInfo.ImageModDateTime,
                ImageOriginalDateTime = imageFileInfo.ImageOriginalDateTime,
                ImageSize = imageFileInfo.ImageSize,
                ImageWidth = imageFileInfo.ImageWidth,
                IsMoved = imageFileInfo.IsMoved,
                NewFullPath = imageFileInfo.NewFullPath,
                UpdatedBy = imageFileInfo.UpdatedBy,
                LastUpdated = imageFileInfo.LastUpdated,
            };
        }

        public static SQL_ImageFileInfo AsSQLImageFileInfo(this ImageFileInfo imageFileInfo, string dbConnection)
        {
            return new SQL_ImageFileInfo()
            {
                Connection = dbConnection,
                ImageId = imageFileInfo.ImageId,
                FileCreateDate = imageFileInfo.FileCreateDate,
                FileExtension = imageFileInfo.FileExtension,
                FileFullPath = imageFileInfo.FileFullPath,
                FileLastWriteTime = imageFileInfo.FileLastWriteTime,
                FileName = imageFileInfo.FileName,
                FileNameWithoutExtension = imageFileInfo.FileNameWithoutExtension,
                FileSize = imageFileInfo.FileSize,
                ImageHeight = imageFileInfo.ImageHeight,
                ImageModDateTime = imageFileInfo.ImageModDateTime,
                ImageOriginalDateTime = imageFileInfo.ImageOriginalDateTime,
                ImageSize = imageFileInfo.ImageSize,
                ImageWidth = imageFileInfo.ImageWidth,
                IsMoved = imageFileInfo.IsMoved,
                NewFullPath = imageFileInfo.NewFullPath,
                UpdatedBy = imageFileInfo.UpdatedBy,
                LastUpdated = imageFileInfo.LastUpdated,
            };
        }

        public static void AddImageFileInfoToDictionary(this Dictionary<string, List<ImageFileInfo>> imageFiles, ImageFileInfo imageFileInfo, string connectionString = null)
        {
            string imageRootName = GetRootImageName(imageFileInfo.FileNameWithoutExtension);

            if (imageFiles.ContainsKey(imageRootName))
            {
                if (!imageFiles[imageRootName].Exists(i => i.FileFullPath == imageFileInfo.FileFullPath))
                {
                    imageFiles[imageRootName].Add(imageFileInfo);
                }
            }
            else
            {
                List<ImageFileInfo> imageFileInfos = new List<ImageFileInfo>() { imageFileInfo };
                imageFiles.Add(imageRootName, imageFileInfos);
            }
        }

        public static void InsertImageFileInfo(this ImageFileInfo imageFileInfo, string connectionString = null)
        {
            if (connectionString != null)
            {
                SQL_ImageFileInfo newSqlImageFileInfo = new SQL_ImageFileInfo(imageFileInfo.AsDataObjectsImageFileInfo(), connectionString);
                newSqlImageFileInfo.Save();
            }

        }

    }
}
