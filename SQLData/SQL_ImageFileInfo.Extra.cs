using System.Linq;
using System.Data.SqlClient;
using DBOFactory;
using System;

namespace SQLData
{
    public partial class SQL_ImageFileInfo : DBOFactoryObjectBase, IDBOFactoryObject
    {
        #region Static Methods

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfo GetByName(string fullPath, string objConn)
        {
            return GetByName(fullPath, objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfo GetByName(string fullPath, string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo imageFileInfo = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo() { FileFullPath = fullPath }, objConn);

            return imageFileInfo.GetByName(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfoList GetSingletons(bool isMoved, string objConn)
        {
            return GetSingletons(isMoved, objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfoList GetSingletons(bool isMoved, string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo imageFileInfo = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo() { IsMoved = isMoved }, objConn);

            return imageFileInfo.GetSingletons(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfoList GetMultiples(bool isMoved, string objConn)
        {
            return GetMultiples(isMoved, objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static DataObjects.ImageFileInfoList GetMultiples(bool isMoved, string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo imageFileInfo = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo() { IsMoved = isMoved }, objConn);

            return imageFileInfo.GetMultiples(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int DeleteAll(string objConn)
        {
            return DeleteAll(objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int DeleteAll(string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo imageFileInfo = new SQL_ImageFileInfo(new DataObjects.ImageFileInfo(), objConn);

            return imageFileInfo.DeleteAll(objTran);
        }


        #endregion

        #region Public Methods

        public DataObjects.ImageFileInfo GetByName()
        {
            return GetByName((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public DataObjects.ImageFileInfo GetByName(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@FileFullPath", FileFullPath);
            });

            QueryController objQueryController = new QueryController(null, $"SELECT {AllColumnsString} FROM {Table} WHERE FileFullPath = @FileFullPath", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetByName", null);

            DataObjects.ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            this.ResultDataSet = null;

            return objectList.FirstOrDefault();
        }

        public DataObjects.ImageFileInfoList GetSingletons()
        {
            return GetSingletons((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public DataObjects.ImageFileInfoList GetSingletons(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
            });

            QueryController objQueryController = new QueryController(null, $"SELECT {AllColumnsString} FROM {Table} WHERE FileName IN ( SELECT FileName FROM {Table} GROUP BY FileName HAVING COUNT(1) = 1 ) AND IsMoved = @IsMoved", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetSingledtons", null);

            DataObjects.ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            return objectList;
        }

        public DataObjects.ImageFileInfoList GetMultiples()
        {
            return GetMultiples((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public DataObjects.ImageFileInfoList GetMultiples(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
            });

            QueryController objQueryController = new QueryController(null, $"SELECT {AllColumnsString} FROM {Table} WHERE FileName IN ( SELECT FileName FROM {Table} GROUP BY FileName HAVING COUNT(1) > 1 ) AND IsMoved = @IsMoved", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetSingledtons", null);

            DataObjects.ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            return objectList;
        }


        public int DeleteAll()
        {
            return DeleteAll((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public int DeleteAll(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = null;
            FillCommandParamsDelegate objFillParamsMethod = null;

            QueryController objQueryController = new QueryController(null, $"DELETE {Table}", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetByName", null);

            return intReturnCode;
        }

        public int MarkMoved()
        {
            return MarkMoved((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public int MarkMoved(DBOFactoryTransaction objTransaction)
        {
            LastUpdated = DateTime.Now;

            if (string.IsNullOrWhiteSpace(UpdatedBy))
            {
                UpdatedBy = CurrentWindowsUser.Name;
            }

            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(
                delegate (SqlCommand objSQLCommand)
                {
                    objSQLCommand.Parameters.AddWithValue($"@{Identity}", IdValue);
                    objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
                    objSQLCommand.Parameters.AddWithValue("@NewFullPath", NewFullPath);
                    objSQLCommand.Parameters.AddWithValue("@LastUpdated", LastUpdated);
                    objSQLCommand.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
                }
            );

            QueryController objQueryController = new QueryController(null, $"UPDATE {Table} SET IsMoved = @IsMoved, NewFullPath = @NewFullPath, LastUpdated = @LastUpdated, UpdatedBy = @UpdatedBy  WHERE {Identity} = @{Identity}", objFillParamsMethod, null);
            int intReturnCode = PerformQuery(objQueryController, "GetByName", null);
            this.ResultDataSet = null;

            return intReturnCode;
        }

        #endregion
    }
}