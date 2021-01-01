using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.FileSystem;
using MetadataExtractor.Formats.FileType;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Png;
using SQLData;

namespace ConsoleApp
{
    public class ImageFileProcessor
    {
        private string _DBOConn = null;

        public string DBOConn
        {
            get
            {
                if (_DBOConn == null)
                {
                    ConnectionStringSettings objConnString = ConfigurationManager.ConnectionStrings["Westrock_PROD"];

                    if (objConnString != null)
                    {
                        _DBOConn = objConnString.ConnectionString;
                    }
                }
                return _DBOConn;
            }
            set
            {
                _DBOConn = value;
            }
        }

        public Dictionary<string, List<ImageFileInfo>> ImageFiles { get; set; } = new Dictionary<string, List<ImageFileInfo>>();

        protected Dictionary<string, Regex> _SearchPatterns { get; set; } = new Dictionary<string, Regex>();

        protected Regex GetRegex(string searchPattern)
        {
            Regex theRegex;

            if (!_SearchPatterns.TryGetValue(searchPattern, out theRegex))
            {
                theRegex = new Regex(searchPattern, RegexOptions.IgnoreCase);
                _SearchPatterns.Add(searchPattern, theRegex);
            }

            return theRegex;
        }

        public IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
        {
            Queue<string> pending = new Queue<string>();
            IEnumerable<string> tmp = new List<string>();
            Regex patternRegex = GetRegex(fileSearchPattern);

            pending.Enqueue(rootFolderPath);
            while (pending.Count > 0)
            {
                rootFolderPath = pending.Dequeue();
                try
                {
                    tmp = System.IO.Directory.GetFiles(rootFolderPath).ToList().Where(f => patternRegex.IsMatch(f));
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                foreach (string fileName in tmp)
                {
                    yield return fileName;
                }
                tmp = System.IO.Directory.GetDirectories(rootFolderPath);
                foreach (string directoryPath in tmp)
                {
                    pending.Enqueue(directoryPath);
                }
            }
        }

        public int AnalyzeFiles(string fileSearchPattern, string rootFolderPath)
        {
            try
            {
                IEnumerable<string> imageFiles = GetFileList(fileSearchPattern, rootFolderPath);

                foreach (string filename in imageFiles)
                {
                    AnalyzeFile(filename);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in AnalyzeFiles(): {ex.Message}");
            }

            return ImageFiles?.Sum(k => k.Value?.Count ?? 0) ?? 0;
        }


        public void AnalyzeFile(string filename)
        {
            DateTime? originalDateTime = null;
            int imageHeight = 0;
            int imageWidth = 0;
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            IEnumerable<MetadataExtractor.Directory> imageDirectories = null;

            try
            {
                imageDirectories = ImageMetadataReader.ReadMetadata(filename);
                FileMetadataDirectory fileDirectory = imageDirectories.OfType<FileMetadataDirectory>().FirstOrDefault();
                FileTypeDirectory fileTypeDirectory = imageDirectories.OfType<FileTypeDirectory>().FirstOrDefault();

                //var fileSize = fileDirectory?.GetDescription(FileMetadataDirectory.TagFileSize);
                long fileSize = fileDirectory?.GetInt64(FileMetadataDirectory.TagFileSize) ?? 0;
                var modDate = fileDirectory?.GetDateTime(FileMetadataDirectory.TagFileModifiedDate);

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        {
                            JpegDirectory jpegDirectory = imageDirectories.OfType<JpegDirectory>().FirstOrDefault();
                            ExifSubIfdDirectory exifSubIfdDirectory = imageDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault(d => d.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal));

                            originalDateTime = exifSubIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

                            if (jpegDirectory != null)
                            {
                                jpegDirectory.TryGetInt32(JpegDirectory.TagImageHeight, out imageHeight);
                                jpegDirectory.TryGetInt32(JpegDirectory.TagImageWidth, out imageWidth);
                            }

                            ImageFileInfo ifi = new ImageFileInfo()
                            {
                                FileFullPath = filename,
                                ImageHeight = imageHeight,
                                ImageSize = fileSize,
                                ImageWidth = imageWidth,
                                ImageModDateTime = modDate,
                                ImageOriginalDateTime = originalDateTime,
                            };

                            ifi.SetFileSystemInfo();
                            ImageFiles.AddImageFileInfoToDictionary(ifi);
                            ifi.InsertImageFileInfo(DBOConn);
                        }
                        break;
                    case ".png":
                        {
                            PngDirectory pngDirectory = imageDirectories.OfType<PngDirectory>().FirstOrDefault(d => d.Name == "PNG-IHDR");

                            if (pngDirectory != null)
                            {
                                pngDirectory.TryGetInt32(PngDirectory.TagImageHeight, out imageHeight);
                                pngDirectory.TryGetInt32(PngDirectory.TagImageWidth, out imageWidth);
                            }

                            ImageFileInfo ifi = new ImageFileInfo()
                            {
                                FileFullPath = filename,
                                ImageHeight = imageHeight,
                                ImageSize = fileSize,
                                ImageWidth = imageWidth,
                                ImageModDateTime = modDate,
                                ImageOriginalDateTime = originalDateTime,
                            };

                            ifi.SetFileSystemInfo();
                            ImageFiles.AddImageFileInfoToDictionary(ifi);
                            ifi.InsertImageFileInfo(DBOConn);
                        }
                        break;
                    case ".nef":
                        {
                            List<ExifSubIfdDirectory> exifSubIfdDirectories = imageDirectories.OfType<ExifSubIfdDirectory>().ToList();
                            ExifSubIfdDirectory exifSubIfdDirectory = imageDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault(d => d.GetDescription(ExifDirectoryBase.TagNewSubfileType) == "Full-resolution image");

                            if (exifSubIfdDirectory != null)
                            {
                                exifSubIfdDirectory.TryGetInt32(ExifDirectoryBase.TagImageHeight, out imageHeight);
                                exifSubIfdDirectory.TryGetInt32(ExifDirectoryBase.TagImageWidth, out imageWidth);
                            }

                            exifSubIfdDirectory = imageDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault(d => d.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal));
                            originalDateTime = exifSubIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

                            ImageFileInfo ifi = new ImageFileInfo()
                            {
                                FileFullPath = filename,
                                ImageHeight = imageHeight,
                                ImageSize = fileSize,
                                ImageWidth = imageWidth,
                                ImageModDateTime = modDate,
                                ImageOriginalDateTime = originalDateTime,
                            };

                            ifi.SetFileSystemInfo();
                            ImageFiles.AddImageFileInfoToDictionary(ifi);
                            ifi.InsertImageFileInfo(DBOConn);
                        }
                        break;
                    case ".tiff":
                        {
                            List<ExifSubIfdDirectory> exifSubIfdDirectories = imageDirectories.OfType<ExifSubIfdDirectory>().ToList();
                            ExifIfd0Directory exifIfd0Directory = imageDirectories.OfType<ExifIfd0Directory>().FirstOrDefault();

                            if (exifIfd0Directory != null)
                            {
                                exifIfd0Directory.TryGetInt32(ExifDirectoryBase.TagImageHeight, out imageHeight);
                                exifIfd0Directory.TryGetInt32(ExifDirectoryBase.TagImageWidth, out imageWidth);
                            }

                            ExifSubIfdDirectory exifSubIfdDirectory = imageDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault(d => d.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal));
                            originalDateTime = exifSubIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

                            ImageFileInfo ifi = new ImageFileInfo()
                            {
                                FileFullPath = filename,
                                ImageHeight = imageHeight,
                                ImageSize = fileSize,
                                ImageWidth = imageWidth,
                                ImageModDateTime = modDate,
                                ImageOriginalDateTime = originalDateTime,
                            };

                            ifi.SetFileSystemInfo();
                            ImageFiles.AddImageFileInfoToDictionary(ifi);
                            ifi.InsertImageFileInfo(DBOConn);
                        }
                        break;
                    case ".psd":
                        {
                            List<ExifSubIfdDirectory> exifSubIfdDirectories = imageDirectories.OfType<ExifSubIfdDirectory>().ToList();

                            ExifSubIfdDirectory exifSubIfdDirectory = imageDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault(d => d.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal));
                            originalDateTime = exifSubIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                            exifSubIfdDirectory?.TryGetInt32(ExifDirectoryBase.TagExifImageHeight, out imageHeight);
                            exifSubIfdDirectory?.TryGetInt32(ExifDirectoryBase.TagExifImageWidth, out imageWidth);

                            ImageFileInfo ifi = new ImageFileInfo()
                            {
                                FileFullPath = filename,
                                ImageHeight = imageHeight,
                                ImageSize = fileSize,
                                ImageWidth = imageWidth,
                                ImageModDateTime = modDate,
                                ImageOriginalDateTime = originalDateTime,
                            };

                            ifi.SetFileSystemInfo();
                            ImageFiles.AddImageFileInfoToDictionary(ifi);
                            ifi.InsertImageFileInfo(DBOConn);
                        }
                        break;
                    case ".bmp":
                        break;
                    case ".gif":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ImageFileInfo ifi = new ImageFileInfo()
                {
                    FileFullPath = filename,
                };

                ifi.SetFileSystemInfo();
                ImageFiles.AddImageFileInfoToDictionary(ifi);
            }

        }

        public int LoadFromDatabase(FileStatus fileStatus, FileGrouping fileGrouping)
        {
            DataObjects.ImageFileInfoList sqlImageFileInfos = new DataObjects.ImageFileInfoList();

            switch (fileGrouping)
            {
                case FileGrouping.All:
                    if (fileStatus == FileStatus.All)
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetAll(DBOConn);
                    }
                    else
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetSingletons(fileStatus == FileStatus.Moved, DBOConn);
                        sqlImageFileInfos.AddRange(SQL_ImageFileInfo.GetMultiples(fileStatus == FileStatus.Moved, DBOConn));
                    }
                    break;
                case FileGrouping.Singleton:
                    if (fileStatus == FileStatus.All)
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetSingletons(true, DBOConn);
                        sqlImageFileInfos.AddRange(SQL_ImageFileInfo.GetSingletons(false, DBOConn));
                    }
                    else
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetSingletons(fileStatus == FileStatus.Moved, DBOConn);
                    }
                    break;
                case FileGrouping.Multiple:
                    if (fileStatus == FileStatus.All)
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetMultiples(true, DBOConn);
                        sqlImageFileInfos.AddRange(SQL_ImageFileInfo.GetMultiples(false, DBOConn));
                    }
                    else
                    {
                        sqlImageFileInfos = SQL_ImageFileInfo.GetMultiples(fileStatus == FileStatus.Moved, DBOConn);
                    }
                    break;
                default:
                    break;
            }

            foreach (DataObjects.ImageFileInfo sqlImageFileInfo in sqlImageFileInfos)
            {
                ImageFileInfo imageFileInfo = sqlImageFileInfo.AsImageFileInfo();

                ImageFiles.AddImageFileInfoToDictionary(imageFileInfo, DBOConn);
            }

            return ImageFiles.Count;
        }

        public void ProcessSingletonImages(string repository)
        {
            List<ImageFileInfo> singletonFiles = ImageFiles.Where(i => i.Value.Count == 1).SelectMany(i => i.Value).ToList();
            int uniqueNamesCount = ImageFiles.Count;
            int allFilesCount = ImageFiles.SelectMany(i => i.Value).Count();
            int singletonFilesCount = singletonFiles.Count;

            if (!System.IO.Directory.Exists(repository))
            {
                System.IO.Directory.CreateDirectory(repository);
            }

            foreach (ImageFileInfo imageFile in singletonFiles)
            {
                string yearDirectory = Path.Combine(repository, $"{imageFile.LikelyDateTime.Year}");

                if (!System.IO.Directory.Exists(yearDirectory))
                {
                    System.IO.Directory.CreateDirectory(yearDirectory);
                }

                string monthDirectory = Path.Combine(yearDirectory, $"{imageFile.LikelyDateTime.Month:D2}");

                if (!System.IO.Directory.Exists(monthDirectory))
                {
                    System.IO.Directory.CreateDirectory(monthDirectory);
                }

                string dayDirectory = Path.Combine(monthDirectory, $"{imageFile.LikelyDateTime.Day:D2}");

                if (!System.IO.Directory.Exists(dayDirectory))
                {
                    System.IO.Directory.CreateDirectory(dayDirectory);
                }

                try
                {
                    File.Copy(imageFile.FileFullPath, Path.Combine(dayDirectory, imageFile.FileName));
                    imageFile.NewFullPath = Path.Combine(dayDirectory, imageFile.FileName);
                    imageFile.IsMoved = true;
                    imageFile.AsSQLImageFileInfo(_DBOConn).MarkMoved();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            GenerateReport($@"E:SingletonPictureReport_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv", singletonFiles, ",");

            foreach (string fileName in singletonFiles.Select(u => u.FileName))
            {
                ImageFiles.Remove(fileName);
            }
        }

        public void FindOriginalsDupesAndCollissions(string repository)
        {
            List<ImageFileInfo> singletonFiles = ImageFiles.Where(i => i.Value.Count == 1).SelectMany(i => i.Value).ToList();
            int uniqueNamesCount = ImageFiles.Count;
            int allFilesCount = ImageFiles.SelectMany(i => i.Value).Count();
            int singletonFilesCount = singletonFiles.Count;

            if (!System.IO.Directory.Exists(repository))
            {
                System.IO.Directory.CreateDirectory(repository);
            }


            GenerateReport($@"E:MultiplePictureReport_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv", ImageFiles.SelectMany(s => s.Value), ",");

            int filenamesWithDupesCount = ImageFiles.Count;

            foreach (List<ImageFileInfo> imageList in ImageFiles.Values)
            {
                string filenameWithoutExtension = imageList.First().FileNameWithoutExtension;
                int landscapeCount = imageList.Where(i => i.ImageWidth > i.ImageHeight).Count();
                int portraitCount = imageList.Count() - landscapeCount;

                var allGroups = imageList.GroupBy(g => (LikelyDateTime: g.LikelyDateTime, ImageModDateTime: g.ImageModDateTime, ImageSize: g.ImageSize), (key, g) => new { GroupKey = key, Images = g.ToList() });
                var duplicateGroups = allGroups.Where(d => d.Images.Count > 1);
                var simpleGroups = allGroups.Where(d => d.Images.Count == 1);

                //var groupings = imageList.GroupBy(g =>  (LikelyDateTime: g.LikelyDateTime, ImageModDateTime: g.ImageModDateTime, ImageSize: g.ImageSize )).OrderBy(o => o.Key.ImageModDateTime).ThenByDescending(o => o.Key.ImageSize).ToList();
                //int numberOfGroupings = groupings.Count;
                //var poop = groupings.Where(g => g.Count() == 1);
                //var doop = groupings.Where(g => g.Count() > 1);


                foreach (var grouping in allGroups)
                {
                    int groupingImageCount = grouping.Images.Count();
                    bool isSimpleGroup = groupingImageCount == 1;
                    bool isDuplicateGroup = groupingImageCount > 1;

                    var groupingKey = grouping.GroupKey;

                    if (isSimpleGroup)
                    {
                        ImageFileInfo image = grouping.Images[0];

                        if (image.FileFullPath.Contains(@"\Thumbnails\"))
                        {

                        }
                        else if (image.FileFullPath.Contains(@"\Previews\"))
                        {

                        }
                        else if (image.FileFullPath.Contains(@"\Masters\") || image.FileFullPath.Contains(@"\PhotoMasters\"))
                        {

                        }
                    }
                    else
                    {

                    }


                    var thumbImages = grouping.Images.Where(g => g.FileFullPath.Contains(@"\Thumbnails\"));
                    var previewImages = grouping.Images.Where(g => g.FileFullPath.Contains(@"\Previews\"));
                }


                foreach (var grouping in duplicateGroups)
                {
                    int fileNameCount = grouping.Images.Select(i => i.FileName).Distinct().Count();

                    if (fileNameCount == 1)
                    {
                        // All the images in this group have the same name, likelyDate, modDate and size.  They also have the same name.
                        // So we will pick one and be done.  We will rename the others "...Dup-n..."
                    }
                    var groupingKey = grouping.GroupKey;
                    var images = grouping.Images;
                    var thumbImages = grouping.Images.Where(g => g.FileFullPath.Contains(@"\Thumbnails"));
                    var previewImages = grouping.Images.Where(g => g.FileFullPath.Contains(@"\Preview"));

                }


                //foreach (var grouping in groupings)
                //{
                //    var groupingKey = grouping.Key;
                //    var images = grouping.ToList();
                //    var thumbImages = grouping.Where(g => g.FileFullPath.Contains(@"\Thumbnails"));
                //    var previewImages = grouping.Where(g => g.FileFullPath.Contains(@"\Preview"));

                //}


                //var distinctImages = imageList.Value.Distinct(new ImageFileInfo_Comparer()).ToList();

                //foreach (var extraItem in imageList.Value.Where(v => !distinctImages.ToList().Exists(d => d.FileFullPath == v.FileFullPath)).ToList())
                //{
                //    imageList.Value.Remove(extraItem);
                //}
            }

        }

        public void GenerateReport(string fileName, string delimiter = "|")
        {
            GenerateReport(fileName, ImageFiles.SelectMany(s => s.Value), delimiter);
        }


        public void GenerateReport(string fileName, IEnumerable<ImageFileInfo> imageFileInfoList, string delimiter = "|")
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (TextWriter writer = new StreamWriter(fileStream))
                {
                    //writer.WriteLine(string.Join(delimiter, ImageFileInfo.PropertyNames().Select(p => p.Contains(delimiter) ? $"\"{p}\"" : p)));
                    writer.WriteLine(string.Join(delimiter, ImageFileInfo.GetPropertyColumnHeaders().Select(p => p.Contains(delimiter) ? $"\"{p}\"" : p)));

                    foreach (ImageFileInfo imageFile in imageFileInfoList.OrderBy(i => i.ImageOriginalDateTime).ThenBy(i => i.ImageModDateTime ?? DateTime.MinValue))
                    {
                        //writer.WriteLine(string.Join(delimiter, imageFile.PropertyValues().Select(p => p.Contains(delimiter) ? $"\"{p}\"" : p)));
                        writer.WriteLine(string.Join(delimiter, ImageFileInfo.GetPropertyValues(imageFile).Select(p => p.Contains(delimiter) ? $"\"{p}\"" : p)));
                    }
                }
            }
        }

    }



    public static class ImageFileHelpers
    {
    }
}
