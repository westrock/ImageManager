using DBOFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLData
{
    public partial class SQL_ImageFileInfo : DBOFactoryObjectBase, IDBOFactoryObject
    {
        #region Private Fields

        private static DataSet m_objAllImageFileInfoDS = null;

        protected override object IdValue
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageId;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageId = (int)value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SQL_ImageFileInfo()
        {
            this.DataObject = new DataObjects.ImageFileInfo();
            base.UpdatedBy = CurrentWindowsUser.Name;

            Table = "ImageFileInfo";
            Identity = "ImageId";
            Columns = new List<string>
            {
                "FileFullPath",
                "FileFullPath",
                "ImageSize",
                "ImageWidth",
                "ImageHeight",
                "ImageOriginalDateTime",
                "ImageModDateTime",
                "FileName",
                "FileNameWithoutExtension",
                "FileExtension",
                "FileSize",
                "FileCreateDate",
                "FileLastWriteTime",
                "LikelyDateTime",
                "IsMoved",
                "NewFullPath",
                "UpdatedBy",
                "LastUpdated"
            };
            Orders = new List<string>()
            {
                Identity // Identity
            };

        }



        /// <summary>
        /// Complete Object-based Constructor
        /// </summary>
        /// <param name="ImageFileInfoObject">A DataObjects.SQL_ImageFileInfo object</param>
        public SQL_ImageFileInfo(DataObjects.ImageFileInfo ImageFileInfoObject)
            : this(ImageFileInfoObject, null)
        {
        }



        /// <summary>
        /// Full Object-based Constructor with string
        /// </summary>
        /// <param name="clientsObject">A DataObjects.SQL_ImageFileInfo object</param>
        /// <param name="connection">A Connection String</param>
        public SQL_ImageFileInfo(DataObjects.ImageFileInfo ImageFileInfoObject, string connection)
            :base(connection)
        {
            this.DataObject = ImageFileInfoObject;

            Table = "ImageFileInfo";
            Identity = "ImageId";
            Columns = new List<string>
            {
                "FileFullPath",
                "ImageSize",
                "ImageWidth",
                "ImageHeight",
                "ImageOriginalDateTime",
                "ImageModDateTime",
                "FileName",
                "FileNameWithoutExtension",
                "FileExtension",
                "FileSize",
                "FileCreateDate",
                "FileLastWriteTime",
                "LikelyDateTime",
                "IsMoved",
                "NewFullPath",
                "UpdatedBy",
                "LastUpdated"
            };
            Orders = new List<string>()
            {
                Identity // Identity
            };

        }


        #endregion

        #region Static Queries with Connections


        /// <summary>
        /// This static method is the preferred technique for obtaining a ImageFileInfo record via its IdValue.
        /// </summary>
        /// <param name="id">The ImageFileInfo's IdValue value</param>
        /// <returns>A ImageFileInfo record contained in a ImageFileInfo object, or null</returns>
        public static SQL_ImageFileInfo GetByID(int id, string connectionString)
        {
            SQL_ImageFileInfo objImageFileInfo = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo() { ImageId = id }, connectionString);

            return (0 == objImageFileInfo.Get()) ? objImageFileInfo : null;
        }


        /// <summary>
        /// This static method is the preferred technique for obtaining all SQL_ImageFileInfo records.
        /// </summary>
        /// <returns>A collection of SQL_ImageFileInfo objects, or null</returns>
        public static DataObjects.ImageFileInfoList GetAll(string connectionString)
        {
            return GetAll(false, connectionString);
        }


        /// <summary>
        /// This static method is the preferred technique for obtaining all SQL_ImageFileInfo records.
        /// </summary>
        /// <returns>A collection of SQL_ImageFileInfo objects, or null</returns>
        public static DataObjects.ImageFileInfoList GetAll(bool refresh, string connectionString)
        {
            return GetObjectListFromDataset(GetAllDS(refresh, connectionString));
        }


        /// <summary>
        /// This static method is the preferred technique for obtaining all SQL_ImageFileInfo records.
        /// </summary>
        /// <returns>A DataSet containing a collection of SQL_ImageFileInfo objects, or null</returns>
        public static DataSet GetAllDS(string connectionString)
        {
            return GetAllDS(false, connectionString);
        }


        /// <summary>
        /// This static method is the preferred technique for obtaining all SQL_ImageFileInfo records.
        /// </summary>
        /// <returns>A DataSet containing a collection of SQL_ImageFileInfo objects, or null</returns>
        public static DataSet GetAllDS(bool refresh, string connectionString)
        {
            SQL_ImageFileInfo ImageFileInfoObject = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo(), connectionString);

            if (0 == ImageFileInfoObject.GetAll(refresh))
                return m_objAllImageFileInfoDS;
            else
                return null;
        }

        #endregion

        #region Private Static Methods


        private static DataObjects.ImageFileInfoList GetObjectListFromDataset(DataSet ImageFileInfoObject)
        {
            DataObjects.ImageFileInfoList ImageFileInfoObjectList = new DataObjects.ImageFileInfoList(ImageFileInfoObject.Tables[0], RowConverter);

            return ImageFileInfoObjectList;
        }

        #endregion

        #region QueryInitializers

        public QueryController QueryInitializationDELETE()
        {
            /*--------------------------------------------------------------------------+
			|	Set the DatabaseObjectFactory's DELETEQueryController.					|
			+--------------------------------------------------------------------------*/
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(FillDeleteCommandParameters);

            return new QueryController(null, $"DELETE {Table} WHERE {Identity} = @Id", objFillParamsMethod, null);
        }



        public QueryController QueryInitializationSELECT()
        {
            /*--------------------------------------------------------------------------+
			|	Set the DatabaseObjectFactory's SELECTQueryController.					|
			+--------------------------------------------------------------------------*/
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(FillSelectCommandParameters);
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);

            return new QueryController(null, $"SELECT {AllColumnsString} FROM {Table} WHERE {Identity} = @Id", objFillParamsMethod, objGetResultsMethod);
        }


        public QueryController QueryInitializationSELECTALL()
        {
            /*--------------------------------------------------------------------------+
			|	Set the DatabaseObjectFactory's SELECTALLQueryController.   			|
			+--------------------------------------------------------------------------*/
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);

            return new QueryController(null, $"SELECT {AllColumnsString} FROM {Table}", null, objGetResultsMethod);
        }


        public QueryController QueryInitializationINSERT()
        {
            /*--------------------------------------------------------------------------+
			|	Set the DatabaseObjectFactory's INSERTQueryController.					|
			+--------------------------------------------------------------------------*/
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(FillInsertCommandParameters);
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetInsertResultsDataSet);

            return new QueryController(null, $"INSERT INTO {Table} ({ColumnsString}) OUTPUT INSERTED.{Identity} AS IdValue VALUES({ColumnsParametersString})", objFillParamsMethod, objGetResultsMethod);
        }



        public QueryController QueryInitializationUPDATE()
        {
            /*--------------------------------------------------------------------------+
			|	Set the DatabaseObjectFactory's UPDATEQueryController.					|
			+--------------------------------------------------------------------------*/
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(FillUpdateCommandParameters);

            return new QueryController(null, $"UPDATE {Table} SET {UpdateParametersString} WHERE {Identity} = @{Identity}", objFillParamsMethod, null);
        }



        /// <summary>
        /// Looks for a method named AdditionalInitialization.  If it exists it will invoke it. The
        /// method should call AddReplaceQueryController() to add additional QueryController.
        /// </summary>
		public void QueryInitializationXTRA()
        {
            System.Reflection.MethodInfo objMethodInfo = this.GetType().GetMethod("AdditionalInitialization");

            objMethodInfo?.Invoke(this, null);
        }


        /// <summary>
        /// Fills the SqlCommand Parameters for an INSERT stored proc.  This method is invoked by the
        /// DatabaseObjectFactory, by way of the INSERTQueryController's FillCommandParametersMethod delegate.
        /// </summary>
        /// <param name="objSQLCommand">An active SqlCommand</param>
        private void FillInsertCommandParameters(SqlCommand objSQLCommand)
        {
            FillUpdateInsertCommandParameters(objSQLCommand);
        }


        /// <summary>
        /// Fills the SqlCommand Parameters for an UPDATE stored proc.  This method is invoked by the
        /// DatabaseObjectFactory, by way of the UPDATEQueryController's FillCommandParametersMethod delegate.
        /// </summary>
        /// <param name="objSQLCommand">An active SqlCommand</param>
        private void FillUpdateCommandParameters(SqlCommand objSQLCommand)
        {
            FillUpdateInsertCommandParameters(objSQLCommand);
        }


        /// <summary>
        /// Fills the SqlCommand Parameters for a DELETE stored proc.  This method is invoked by the
        /// DatabaseObjectFactory, by way of the DELETEQueryController's FillCommandParametersMethod delegate.
        /// </summary>
        /// <param name="objSQLCommand">An active SqlCommand</param>
        private void FillDeleteCommandParameters(SqlCommand objSQLCommand)
        {
            objSQLCommand.Parameters.AddWithValue($"@{Identity}", IdValue);

            if (objSQLCommand.CommandType == CommandType.StoredProcedure)
            {
                objSQLCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            }
        }


        /// <summary>
        /// Fills the SqlCommand Parameters for a SELECT stored proc.  This method is invoked by the
        /// DatabaseObjectFactory, by way of the SELECTQueryController's FillCommandParametersMethod delegate.
        /// </summary>
        /// <param name="objSQLCommand">An active SqlCommand</param>
        private void FillSelectCommandParameters(SqlCommand objSQLCommand)
        {
            objSQLCommand.Parameters.AddWithValue($"@{Identity}", IdValue);

            if (objSQLCommand.CommandType == CommandType.StoredProcedure)
            {
                objSQLCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            }
        }



        /// <summary>
        /// Fills the SqlCommand Parameters for either an INSERT or UPDATE stored proc.
        /// This method is invoked by the DatabaseObjectFactory, by way of the either the
        /// INSERTQueryController's or INSERTQueryController's FillCommandParametersMethod delegate.
        /// </summary>
        /// <param name="objSQLCommand">An active SqlCommand</param>
        private void FillUpdateInsertCommandParameters(SqlCommand objSQLCommand)
        {
            // All parameters common to the UPDATE and INSERT follow
            objSQLCommand.Parameters.AddWithValue("@FileFullPath", FileFullPath);
            objSQLCommand.Parameters.AddWithValue("@ImageSize", ImageSize);
            objSQLCommand.Parameters.AddWithValue("@ImageWidth", ImageWidth);
            objSQLCommand.Parameters.AddWithValue("@ImageHeight", ImageHeight);
            objSQLCommand.Parameters.AddWithValue("@ImageOriginalDateTime", ImageOriginalDateTime == null ? (object)DBNull.Value : ImageOriginalDateTime);
            objSQLCommand.Parameters.AddWithValue("@ImageModDateTime", ImageModDateTime == null ? (object)DBNull.Value : ImageModDateTime);
            objSQLCommand.Parameters.AddWithValue("@FileName", FileName);
            objSQLCommand.Parameters.AddWithValue("@FileNameWithoutExtension", FileNameWithoutExtension);
            objSQLCommand.Parameters.AddWithValue("@FileExtension", FileExtension == null ? (object)DBNull.Value : FileExtension);
            objSQLCommand.Parameters.AddWithValue("@FileSize", FileSize);
            objSQLCommand.Parameters.AddWithValue("@FileCreateDate", FileCreateDate == null ? (object)DBNull.Value : FileCreateDate);
            objSQLCommand.Parameters.AddWithValue("@FileLastWriteTime", FileLastWriteTime == null ? (object)DBNull.Value : FileLastWriteTime);
            objSQLCommand.Parameters.AddWithValue("@LikelyDateTime", LikelyDateTime);
            objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
            objSQLCommand.Parameters.AddWithValue("@NewFullPath", NewFullPath == null ? (object)DBNull.Value : NewFullPath);
            objSQLCommand.Parameters.AddWithValue("@UpdatedBy", UpdatedBy == null ? (object)DBNull.Value : UpdatedBy);
            objSQLCommand.Parameters.AddWithValue("@LastUpdated", LastUpdated == null ? (object)DBNull.Value : LastUpdated);


            // All parameters that are nullable follow
            objSQLCommand.Parameters["@ImageOriginalDateTime"].IsNullable = true;
            objSQLCommand.Parameters["@ImageModDateTime"].IsNullable = true;
            objSQLCommand.Parameters["@FileExtension"].IsNullable = true;
            objSQLCommand.Parameters["@FileCreateDate"].IsNullable = true;
            objSQLCommand.Parameters["@FileLastWriteTime"].IsNullable = true;
            objSQLCommand.Parameters["@UpdatedBy"].IsNullable = true;
            objSQLCommand.Parameters["@LastUpdated"].IsNullable = true;

        }

        #endregion


        #region Public Methods


        /// <summary>
        /// This method gets the ImageFileInfo based on the value of the instance's key.
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            return PerformSelect();
        }


        /// <summary>
        /// This method Saves a ImageFileInfo record
        /// </summary>
        /// <returns>The return value from the INSERT.</returns>
        public int Save()
        {
            return PerformInsert();
        }


        /// <summary>
        /// This method updates Saves a ImageFileInfo record using a transaction
        /// </summary>
        /// <returns>The return value from the INSERT.</returns>
        public int Save(DBOFactoryTransaction objTransaction)
        {
            return PerformInsert(objTransaction);
        }


        /// <summary>
        /// This method updates a ImageFileInfo record
        /// </summary>
        /// <returns>The return value from the UPDATE.</returns>
        public int Update()
        {
            return PerformUpdate();
        }


        /// <summary>
        /// This method updates a ImageFileInfo record
        /// </summary>
        /// <returns>The return value from the UPDATE.</returns>
        public int Update(DBOFactoryTransaction objTransaction)
        {
            return PerformUpdate(objTransaction);
        }


        /// <summary>
        /// This method deletes a ImageFileInfo record
        /// </summary>
        /// <returns>The return value from the DELETE.</returns>
        public int Delete()
        {
            return PerformDelete();
        }


        /// <summary>
        /// This method deletes a ImageFileInfo record
        /// </summary>
        /// <returns>The return value from the DELETE.</returns>
        public int Delete(DBOFactoryTransaction objTransaction)
        {
            return PerformDelete(objTransaction);
        }


        #endregion


        #region Private Methods

        /// <summary>
        /// Gets all of the currently defined ImageFileInfo records from either the local
        /// copy in the private member m_objAllImageFileInfoDS, or a fresh copy.
        /// </summary>
        /// <remarks>Do we really need a lock here?</remarks>
        /// <param name="refresh">If true, will force a fresh copy of the data</param>
        /// <returns>If 0, all went well.  Otherwise, an error code.</returns>
        private int GetAll(bool refresh)
        {
            int intReturnCode = 0;

            using (DBOFactoryLock objLock = new DBOFactoryLock())
            {
                lock (objLock)
                {
                    if (refresh || (m_objAllImageFileInfoDS == null))
                    {
                        intReturnCode = PerformSelectAll("GetAll");

                        m_objAllImageFileInfoDS = this.ResultDataSet;//.Copy();
                    }
                }
            }

            return intReturnCode;
        }

        #endregion


        #region Public Properties


        /// <summary>
        /// Gets the SQL_ImageFileInfo APSDataObject
        /// </summary>
        public DataObjects.ImageFileInfo ImageFileInfoObject => (DataObjects.ImageFileInfo)DataObject;




        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public int ImageId
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageId;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageId = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public string FileFullPath
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileFullPath;
            set => ((DataObjects.ImageFileInfo)DataObject).FileFullPath = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public Int64 ImageSize
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageSize;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageSize = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public int ImageWidth
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageWidth;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageWidth = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public int ImageHeight
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageHeight;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageHeight = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public DateTime? ImageOriginalDateTime
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageOriginalDateTime;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageOriginalDateTime = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public DateTime? ImageModDateTime
        {
            get => ((DataObjects.ImageFileInfo)DataObject).ImageModDateTime;
            set => ((DataObjects.ImageFileInfo)DataObject).ImageModDateTime = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public string FileName
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileName;
            set => ((DataObjects.ImageFileInfo)DataObject).FileName = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public string FileNameWithoutExtension
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileNameWithoutExtension;
            set => ((DataObjects.ImageFileInfo)DataObject).FileNameWithoutExtension = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public string FileExtension
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileExtension;
            set => ((DataObjects.ImageFileInfo)DataObject).FileExtension = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public Int64 FileSize
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileSize;
            set => ((DataObjects.ImageFileInfo)DataObject).FileSize = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public DateTime? FileCreateDate
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileCreateDate;
            set => ((DataObjects.ImageFileInfo)DataObject).FileCreateDate = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public DateTime? FileLastWriteTime
        {
            get => ((DataObjects.ImageFileInfo)DataObject).FileLastWriteTime;
            set => ((DataObjects.ImageFileInfo)DataObject).FileLastWriteTime = value;
        }



        /// <summary>
        /// Gets and sets the DataObject's ImageFileInfo property
        /// </summary>
        public DateTime LikelyDateTime
        {
            get => ((DataObjects.ImageFileInfo)DataObject).LikelyDateTime;
            set => ((DataObjects.ImageFileInfo)DataObject).LikelyDateTime = value;
        }


        /// <summary>
        /// Gets and sets the DataObject's IsMoved property
        /// </summary>
        public bool IsMoved
        {
            get => ((DataObjects.ImageFileInfo)DataObject).IsMoved;
            set => ((DataObjects.ImageFileInfo)DataObject).IsMoved = value;
        } 


        /// <summary>
        /// Gets and sets the DataObject's NewFullPath property
        /// </summary>
        public string NewFullPath
        {
            get => ((DataObjects.ImageFileInfo)DataObject).NewFullPath;
            set => ((DataObjects.ImageFileInfo)DataObject).NewFullPath = value;
        }


        #endregion



        protected void GetSelectResultsDataSet(SqlCommand objSQLCommand, DataSet objDataSet)
        {
            if ((objDataSet != null) && (objDataSet.Tables != null) &&
                (objDataSet.Tables.Count >= 1) && (objDataSet.Tables[0].Rows.Count >= 1))
            {
                DataRow objRow = objDataSet.Tables[0].Rows[0];
                ImageId = (int)objRow["ImageId"];
                FileFullPath = (string)objRow["FileFullPath"];
                ImageSize = (Int64)objRow["ImageSize"];
                ImageWidth = (int)objRow["ImageWidth"];
                ImageHeight = (int)objRow["ImageHeight"];
                ImageOriginalDateTime = objRow["ImageOriginalDateTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["ImageOriginalDateTime"];
                ImageModDateTime = objRow["ImageModDateTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["ImageModDateTime"];
                FileName = (string)objRow["FileName"];
                FileNameWithoutExtension = (string)objRow["FileNameWithoutExtension"];
                FileExtension = objRow["FileExtension"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["FileExtension"];
                FileSize = (Int64)objRow["FileSize"];
                FileCreateDate = objRow["FileCreateDate"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["FileCreateDate"];
                FileLastWriteTime = objRow["FileLastWriteTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["FileLastWriteTime"];
                LikelyDateTime = (DateTime)objRow["LikelyDateTime"];
                IsMoved = objRow["IsMoved"].GetType().Equals(typeof(DBNull)) ? false : (bool)objRow["IsMoved"];
                NewFullPath = objRow["NewFullPath"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["NewFullPath"];
                UpdatedBy = objRow["UpdatedBy"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["UpdatedBy"];
                LastUpdated = objRow["LastUpdated"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["LastUpdated"];

            }
        }

        protected static DBODataObjectBase RowConverter(DataRow objRow)
        {
            return new DataObjects.ImageFileInfo()
            {
                ImageId = (int)objRow["ImageId"],
                FileFullPath = (string)objRow["FileFullPath"],
                ImageSize = (Int64)objRow["ImageSize"],
                ImageWidth = (int)objRow["ImageWidth"],
                ImageHeight = (int)objRow["ImageHeight"],
                ImageOriginalDateTime = objRow["ImageOriginalDateTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["ImageOriginalDateTime"],
                ImageModDateTime = objRow["ImageModDateTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["ImageModDateTime"],
                FileName = (string)objRow["FileName"],
                FileNameWithoutExtension = (string)objRow["FileNameWithoutExtension"],
                FileExtension = objRow["FileExtension"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["FileExtension"],
                FileSize = (Int64)objRow["FileSize"],
                FileCreateDate = objRow["FileCreateDate"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["FileCreateDate"],
                FileLastWriteTime = objRow["FileLastWriteTime"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["FileLastWriteTime"],
                LikelyDateTime = (DateTime)objRow["LikelyDateTime"],
                IsMoved = objRow["IsMoved"].GetType().Equals(typeof(DBNull)) ? false : (bool)objRow["IsMoved"],
                NewFullPath = objRow["NewFullPath"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["NewFullPath"],
                UpdatedBy = objRow["UpdatedBy"].GetType().Equals(typeof(DBNull)) ? null : (string)objRow["UpdatedBy"],
                LastUpdated = objRow["LastUpdated"].GetType().Equals(typeof(DBNull)) ? null : (DateTime?)objRow["LastUpdated"],
            };
        }

    }
}