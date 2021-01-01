using System;
using System.Data;
using DBOFactory;
using System.Collections.Generic;

namespace DataObjects
{

    public partial class ImageFileInfo : DBODataObjectBase
    {

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ImageFileInfo()
		{
			UpdatedBy = CurrentWindowsUser.Name;
		}

		#endregion
        public int ImageId { get; set; }
        public string FileFullPath { get; set; }
        public Int64 ImageSize { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public DateTime? ImageOriginalDateTime { get; set; }
        public DateTime? ImageModDateTime { get; set; }
        public string FileName { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string FileExtension { get; set; }
        public Int64 FileSize { get; set; }
        public DateTime? FileCreateDate { get; set; }
        public DateTime? FileLastWriteTime { get; set; }
        public DateTime LikelyDateTime { get; set; }
        public bool IsMoved { get; set; } = false;
        public string NewFullPath { get; set; }

    }

    #region ImageFileInfoList Class

    public partial class ImageFileInfoList :  List<ImageFileInfo>
	{

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ImageFileInfoList()
			: base()
		{
		}

		/// <summary>
		/// Alternative Copy Constructor
		/// </summary>
		public ImageFileInfoList(ImageFileInfoList sourceList)
			: base(sourceList)
		{
		}

		/// <summary>
		/// Alternative Constructor to initialize with a DataTable
		/// </summary>
		/// <param name="objTable">A DataTable containing rows of DataObjects</param>
		/// <param name="converter">A RowToObjectConverter to convert a DataRow to a APSDataObject</param>
		public ImageFileInfoList(DataTable objTable, RowToObjectConverter converter)
            : base()
        {
            if ((objTable != null) && (objTable.Rows != null))
            {
                foreach (DataRow objRow in objTable.Rows)
                {
                    Add((ImageFileInfo)converter(objRow));
                }
            }
        }

        /// <summary>
        /// Alternative Constructor to initialize with a DataRowCollection
        /// </summary>
        /// <param name="objRows">A DataRowCollection containing rows of APSDataObjects</param>
        /// <param name="converter">A RowToObjectConverter to convert a DataRow to a APSDataObject</param>
        public ImageFileInfoList(DataRowCollection objRows, RowToObjectConverter converter)
            : base()
        {
            if (objRows != null)
            {
                foreach (DataRow objRow in objRows)
                {
                    Add((ImageFileInfo)converter(objRow));
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// The indexed accessor is overridden to return a IPAddress object
        /// </summary>
        /// <param name="indexValue">The index</param>
        /// <returns>A IPAddress object</returns>
        public new ImageFileInfo this[int indexValue]
        {
            get { return (ImageFileInfo)base[indexValue]; }
        }

        #endregion
    }
	
    #endregion


}