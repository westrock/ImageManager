using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace ConsoleApp
{
    public class ImageFileInfo : ColumnOrientedRecord, IEquatable<ImageFileInfo>
    {
        #region Private Properties

        private DateTime _DEFAULT_DATE = new DateTime(1980, 01, 01);
        private string _FileFullPath = null;
        private FileInfo _FileInfo = null;

        #endregion

        #region Static Properties

        //private static readonly List<(string Header, int Order)> _Headers;
        //private static readonly List<(PropertyInfo Property, string Format, int Order)> _Formats;

        //static ImageFileInfo()
        //{
        //    Type infoType = typeof(ImageFileInfo);
        //    var items = infoType
        //        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .Select(p => new { Prop = p, Attribute = p.GetCustomAttributes(typeof(ColumnOutputAttribute)).OfType<ColumnOutputAttribute>().FirstOrDefault() })
        //        .Where(a => a.Attribute != null)
        //        .ToList();

        //    _Headers = items
        //        .OrderBy(i => i.Attribute.Order)
        //        .Select(v => (v.Attribute.ColumnHeader, v.Attribute.Order))
        //        .ToList();

        //    _Formats = items
        //        .OrderBy(i => i.Attribute.Order)
        //        .Select(v => (v.Prop, v.Attribute.Format, v.Attribute.Order))
        //        .ToList();
        //}

        //public static IEnumerable<string> GetPropertyValues(ImageFileInfo info)
        //{
        //    return _Formats
        //        .Select
        //        (
        //            f =>
        //            {
        //                var value = f.Property.GetValue(info);
        //                if (value == null)
        //                {
        //                    return "";
        //                }
        //                else
        //                {
        //                    return (f.Format != null) ? string.Format($"{{0:{f.Format}}}", value) : value.ToString();
        //                }
        //            }
        //        );
        //}

        //public static IEnumerable<string> GetPropertyColumnHeaders()
        //{
        //    return _Headers
        //        .Select(h => h.Header);
        //}


        #endregion

        #region Public Properties

        //public ImageFileInfo() : base() { }

        public FileInfo FileInfo
        {
            get
            {
                if ((_FileInfo == null && !string.IsNullOrWhiteSpace(_FileFullPath)) || (_FileInfo?.FullName != _FileFullPath))
                {
                    _FileInfo = new FileInfo(_FileFullPath);
                }
                else
                {
                    //_FileInfo = null;
                }
                return _FileInfo;
            }
        }

        public int ImageId { get; set; }

        [ColumnOutput(columnHeader: "FileFullPath", order: 10)]
        public string FileFullPath
        {
            get => _FileFullPath;
            set
            {
                if (_FileFullPath != value)
                {
                    _FileFullPath = string.IsNullOrWhiteSpace(value) ? null : value;
                }
            }
        }

        [ColumnOutput(columnHeader: "ImageSize", order: 3)]
        public long ImageSize { get; set; }

        [ColumnOutput(columnHeader: "ImageWidth", order: 4)]
        public int ImageWidth { get; set; }

        [ColumnOutput(columnHeader: "ImageHeight", order: 5)]
        public int ImageHeight { get; set; }

        [ColumnOutput(columnHeader: "ImageOriginalDateTime", order: 2, stringFormat: "yyyyMMdd hh:mm:ss")]
        public DateTime? ImageOriginalDateTime { get; set; }

        [ColumnOutput(columnHeader: "ImageModDateTime", order: 6, stringFormat: "yyyyMMdd hh:mm:ss")]
        public DateTime? ImageModDateTime { get; set; }

        [ColumnOutput(columnHeader: "FileName", order: 1)]
        public string FileName { get; set; }

        public string FileNameWithoutExtension { get; set; }

        public string FileExtension { get; set; }

        [ColumnOutput(columnHeader: "FileSize", order: 7)]
        public long FileSize { get; set; }

        [ColumnOutput(columnHeader: "FileCreateDate", order: 8, stringFormat: "yyyyMMdd hh:mm:ss")]
        public DateTime? FileCreateDate { get; set; }

        [ColumnOutput(columnHeader: "FileLastWriteTime", order: 9, stringFormat: "yyyyMMdd hh:mm:ss")]
        public DateTime? FileLastWriteTime { get; set; }

        [ColumnOutput(columnHeader: "LikelyDateTime", order: 0, stringFormat: "yyyyMMdd hh:mm:ss")]
        public DateTime LikelyDateTime
        {
            get
            {
                if ((ImageOriginalDateTime != null) && (ImageOriginalDateTime.Value.Date != _DEFAULT_DATE))
                {
                    return ImageOriginalDateTime.Value;
                }
                else
                {
                    if (ImageModDateTime != null)
                    {
                        return ImageModDateTime.Value;
                    }
                    else
                    {
                        return (FileLastWriteTime < FileCreateDate) ? FileLastWriteTime.Value : FileCreateDate.Value;
                    }
                }
            }
        }

        [ColumnOutput(columnHeader: "IsMoved", order: 11)]
        public bool IsMoved { get; set; }

        [ColumnOutput(columnHeader: "NewFullPath", order: 12)]
        public string NewFullPath { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }

        #endregion

        #region Public Methods

        public void SetFileSystemInfo()
        {
            if (!string.IsNullOrWhiteSpace(_FileFullPath))
            {
                FileName = Path.GetFileName(_FileFullPath);
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(_FileFullPath);
                FileExtension = Path.GetExtension(_FileFullPath);
                FileSize = FileInfo?.Length ?? 0;
                FileCreateDate = FileInfo?.CreationTime ?? null;
                FileLastWriteTime = FileInfo?.LastWriteTime ?? null;
            }
            else
            {
                _FileInfo = null;
                FileName = null;
                FileNameWithoutExtension = null;
                FileExtension = null;
                FileSize = 0;
                FileCreateDate = null;
                FileLastWriteTime = null;
            }
        }

        public bool Equals(ImageFileInfo other)
        {
            if (other == null)
                return false;

            return (ImageSize == other.ImageSize &&
                    ImageWidth == other.ImageWidth &&
                    ImageHeight == other.ImageHeight &&
                    FileSize == other.FileSize &&
                    FileName.Equals(other.FileName, StringComparison.OrdinalIgnoreCase));
        }

        public override int GetHashCode()
        {
            int hash = new { ImageSize, ImageWidth, ImageHeight, FileSize, FileName, ImageOriginalDateTime }.GetHashCode();
            return hash;
        }


        public override string ToString()
        {
            return $"{ImageOriginalDateTime},{FileName},{FileNameWithoutExtension},{FileExtension},{ImageSize},{ImageWidth},{ImageHeight},{ImageModDateTime},{FileSize},{FileCreateDate},{FileLastWriteTime},{FileFullPath}";
        }

        public string DelimitedString()
        {
            return $"{LikelyDateTime},{ImageOriginalDateTime},{FileName},{FileExtension},{ImageSize},{ImageWidth},{ImageHeight},{ImageModDateTime},{FileSize},{FileCreateDate},{FileLastWriteTime},{FileFullPath}";
        }

        #endregion

    }

    public class ImageFileInfo_Comparer : IEqualityComparer<ImageFileInfo>
    {
        public bool Equals(ImageFileInfo image1, ImageFileInfo image2)
        {
            if (image1 == null && image2 == null)
            {
                return true;
            }
            else
            {
                if (image1 == null || image2 == null)
                {
                    return false;
                }
                else
                {
                    return (image1.ImageSize == image2.ImageSize &&
                            image1.ImageWidth == image2.ImageWidth &&
                            image1.ImageHeight == image2.ImageHeight &&
                            image1.FileSize == image2.FileSize &&
                            image1.FileName.Equals(image2.FileName, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        public int GetHashCode(ImageFileInfo image)
        {
            if (image == null)
            { return 0;
            }
            else
            {
                int hash = new { image.ImageSize, image.ImageWidth, image.ImageHeight, image.FileSize, image.FileName, image.ImageOriginalDateTime }.GetHashCode();
                return hash;
            }
        }
    }
}
