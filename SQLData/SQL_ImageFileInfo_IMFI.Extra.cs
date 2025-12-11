using System.Linq;
using System.Data.SqlClient;
using DBOFactory;
using System;
using System.Collections.Generic;
using DataObjects;
using System.Data;

namespace SQLData
{
    public partial class SQL_ImageFileInfo_IMFI : DBOFactoryObjectBase, IDBOFactoryObject
    {
        #region Static Methods

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfo_IMFI GetByName(string fullPath, string objConn)
        {
            return GetByName(fullPath, objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfo_IMFI GetByName(string fullPath, string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI() { FileFullPath = fullPath }, objConn);

            return imageFileInfo.GetByName(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetSingletons(bool isMoved, string objConn)
        {
            return GetSingletons(isMoved, objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetSingletons(bool isMoved, string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI() { IsMoved = isMoved }, objConn);

            return imageFileInfo.GetSingletons(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetMultiples(bool isMoved, string objConn, int withinDays = 30)
        {
            return GetMultiples(isMoved, objConn, null, withinDays);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetMultiples(bool isMoved, string objConn, DBOFactoryTransaction objTran, int withinDays = 30)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI() { IsMoved = isMoved }, objConn);

            return imageFileInfo.GetMultiples(objTran, withinDays);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int GetRecordCount(string objConn)
        {
            return GetRecordCount(objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int GetRecordCount(string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.GetRecordCount(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetYears(string objConn)
        {
            return GetYears(objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetYears(string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.GetYears(objTran);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetMonths(string objConn, int forYear)
        {
            return GetMonths(objConn, null, forYear);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetMonths(string objConn, DBOFactoryTransaction objTran, int forYear)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.GetMonths(objTran, forYear);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetDays(string objConn, int forYear, int forMonth)
        {
            return GetDays(objConn, null, forYear, forMonth);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static List<int> GetDays(string objConn, DBOFactoryTransaction objTran, int forYear, int forMonth)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.GetDays(objTran, forYear, forMonth);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetByDate(string objConn, DateTime date)
        {
            return GetByDate(objConn, null, date);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static ImageFileInfoList GetByDate(string objConn, DBOFactoryTransaction objTran, DateTime date)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.GetByDate(objTran, date);
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
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.DeleteAll(objTran);
        }


        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int MarkDuplicates(string objConn)
        {
            return MarkDuplicates(objConn, null);
        }

        /// <summary>
        /// This static method ...
        /// </summary>
        /// <returns></returns>
        public static int MarkDuplicates(string objConn, DBOFactoryTransaction objTran)
        {
            SQL_ImageFileInfo_IMFI imageFileInfo = new SQL_ImageFileInfo_IMFI(new ImageFileInfo_IMFI(), objConn);

            return imageFileInfo.MarkDuplicates(objTran);
        }

        #endregion

        #region Public Methods

        public ImageFileInfo_IMFI GetByName()
        {
            return GetByName((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public ImageFileInfo_IMFI GetByName(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@FileFullPath", FileFullPath);
            });

            QueryController objQueryController = new QueryController(null, $"SELECT {AllColumnsString} FROM {Table} WHERE FileFullPath = @FileFullPath", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetByName", null);

            ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            this.ResultDataSet = null;

            return objectList.FirstOrDefault();
        }

        public ImageFileInfoList GetSingletons()
        {
            return GetSingletons((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public ImageFileInfoList GetSingletons(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
            });

            QueryController objQueryController = new QueryController(null, $@"SELECT {AllColumnsString} FROM {Table} WHERE FileName IN ( SELECT FileName FROM {Table} WHERE IsMoved = @IsMoved GROUP BY FileName HAVING COUNT(1) = 1 ) AND IsMoved = @IsMoved AND FileFullPath NOT LIKE '%\Previews\%' AND FileFullPath NOT LIKE '%\Thumbnails\%'", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetSingletons", null);

            ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            return objectList;
        }

        public ImageFileInfoList GetMultiples(int withinDays = 30)
        {
            return GetMultiples((DBOFactoryTransaction)null, withinDays);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public ImageFileInfoList GetMultiples(DBOFactoryTransaction objTransaction, int withinDays = 30)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@IsMoved", IsMoved);
                objSQLCommand.Parameters.AddWithValue("@WithinDays", withinDays);
            });

            //QueryController objQueryController = new QueryController(null, $@"SELECT {AllColumnsString} FROM {Table} WHERE FileName IN ( SELECT FileName FROM {Table} GROUP BY FileName HAVING COUNT(1) > 1 ) AND IsMoved = @IsMoved", objFillParamsMethod, objGetResultsMethod);
            QueryController objQueryController = new QueryController(null, $@"WITH BaseImage(BASE_FileNameWithoutExtension, BASE_LikelyDateTime)
	 AS (SELECT FileNameWithoutExtension AS BASE_FileNameWithoutExtension, MIN(LikelyDateTime) AS BASE_LikelyDateTime
		 FROM {Table}
		 WHERE IsMoved = 0
		 GROUP BY FileNameWithoutExtension)  
	 SELECT {AllColumnsString}
	 FROM {Table}
		  INNER JOIN BaseImage ON BASE_FileNameWithoutExtension = FileNameWithoutExtension
                                  AND ABS(DATEDIFF(DAY, LikelyDateTime, BASE_LikelyDateTime)) < @WithinDays
	 WHERE IsMoved = @IsMoved AND FileFullPath NOT LIKE '%\Previews\%' AND FileFullPath NOT LIKE '%\Thumbnails\%'
	 ORDER BY FileNameWithoutExtension, LikelyDateTime, ImageModDateTime, FileName;", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetMultiples", null);

            ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            return objectList;
        }

        public ImageFileInfoList GetByDate(DateTime date)
        {
            return GetByDate((DBOFactoryTransaction)null, date);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public ImageFileInfoList GetByDate(DBOFactoryTransaction objTransaction, DateTime date)
        {
            GetResultsDataSetDelegate objGetResultsMethod = new GetResultsDataSetDelegate(GetSelectResultsDataSet);
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@Date", date.Date);
            });

            //QueryController objQueryController = new QueryController(null, $@"SELECT {AllColumnsString} FROM {Table} WHERE FileName IN ( SELECT FileName FROM {Table} GROUP BY FileName HAVING COUNT(1) > 1 ) AND IsMoved = @IsMoved", objFillParamsMethod, objGetResultsMethod);
            QueryController objQueryController = new QueryController(null, $@"
SELECT {AllColumnsString}
FROM {Table}
WHERE CONVERT(date, LikelyDateTime) = @Date AND FileExtension NOT IN ('.psd', '.NEF')
AND IsMoved = 1 AND NewFullPath != '*Duplicate*'
ORDER BY FileNameWithoutExtension, LikelyDateTime, ImageModDateTime, FileName;", objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetByDate", null);

            ImageFileInfoList objectList = GetObjectListFromDataset(this.ResultDataSet);

            return objectList;
        }


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public int GetRecordCount()
            => GetRecordCount((DBOFactoryTransaction)null);


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public int GetRecordCount(DBOFactoryTransaction objTransaction)
        {
            int recordCount = -1;

            GetResultsDataSetDelegate objGetResultsMethod =
                new GetResultsDataSetDelegate(delegate (SqlCommand objSQLCommand, DataSet objDataSet) {
                    if ((objDataSet != null) && (objDataSet.Tables != null) &&
                        (objDataSet.Tables.Count >= 1) && (objDataSet.Tables[0].Rows.Count >= 1))
                    {
                        DataRow row = objDataSet.Tables[0].Rows[0];

                        recordCount = (int)row["RecordCount"];
                    }
                });

            QueryController objQueryController = new QueryController(null, $@"SELECT COUNT(1) AS [RecordCount] FROM ImageFileInfo_IMFI", null, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "GetRecordCount", null);

            return recordCount;
        }


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetYears()
            => GetYears((DBOFactoryTransaction)null);


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetYears(DBOFactoryTransaction objTransaction)
        {
            List<int> years = new List<int>();

            GetResultsDataSetDelegate objGetResultsMethod =
                new GetResultsDataSetDelegate(delegate (SqlCommand objSQLCommand, DataSet objDataSet) {
                    if ((objDataSet != null) && (objDataSet.Tables != null) &&
                        (objDataSet.Tables.Count >= 1) && (objDataSet.Tables[0].Rows.Count >= 1))
                    {
                        foreach(DataRow row in objDataSet.Tables[0].Rows)
                        {
                            years.Add((int)row["Year"]);
                        }
                    }
                });

            QueryController objQueryController = new QueryController(null, $@"SELECT DISTINCT DATEPART(YEAR, LikelyDateTime) AS [Year]
FROM ImageFileInfo_IMFI
WHERE FileExtension NOT IN ('.psd', '.NEF')
AND IsMoved = 1 AND NewFullPath != '*Duplicate*'
ORDER BY 1", null, /*objGetResultsMethod*/ null);
            int intReturnCode = PerformQuery(objQueryController, "GetYears", null);

            return GetIntListFromDataset(this.ResultDataSet);
        }


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetMonths(int forYear)
            => GetMonths((DBOFactoryTransaction)null, forYear);


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetMonths(DBOFactoryTransaction objTransaction, int forYear)
        {
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@Year", forYear);
            });

            QueryController objQueryController = new QueryController(null, $@"SELECT DISTINCT DATEPART(MONTH, LikelyDateTime) AS [Month]
FROM ImageFileInfo_IMFI
WHERE DATEPART(YEAR, LikelyDateTime) = @Year AND FileExtension NOT IN ('.psd', '.NEF')
AND IsMoved = 1 AND NewFullPath != '*Duplicate*'
ORDER BY 1", objFillParamsMethod, null);
            int intReturnCode = PerformQuery(objQueryController, "GetMonths", null);

            return GetIntListFromDataset(this.ResultDataSet);
        }


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetDays(int forYear, int forMonth)
            => GetDays((DBOFactoryTransaction)null, forYear, forMonth);


        /// <summary>
        /// This method 
        /// <returns>The return value from the stored procedure.</returns>
        public List<int> GetDays(DBOFactoryTransaction objTransaction, int forYear, int forMonth)
        {
            FillCommandParamsDelegate objFillParamsMethod = new FillCommandParamsDelegate(delegate (SqlCommand objSQLCommand)
            {
                objSQLCommand.Parameters.AddWithValue("@Year", forYear);
                objSQLCommand.Parameters.AddWithValue("@Month", forMonth);
            });

            QueryController objQueryController = new QueryController(null, $@"SELECT DISTINCT DATEPART(DAY, LikelyDateTime) AS [Day]
FROM ImageFileInfo_IMFI
WHERE DATEPART(YEAR, LikelyDateTime) = @Year AND DATEPART(MONTH, LikelyDateTime) = @Month
AND FileExtension NOT IN ('.psd', '.NEF')
AND IsMoved = 1 AND NewFullPath != '*Duplicate*'
ORDER BY 1", objFillParamsMethod, null);
            int intReturnCode = PerformQuery(objQueryController, "GetDays", null);

            return GetIntListFromDataset(this.ResultDataSet);
        }



        private static List<int> GetIntListFromDataset(DataSet objDataSet)
        {
            List<int> intList = new List<int>();

            if ((objDataSet?.Tables?.Count >= 1) && (objDataSet?.Tables[0]?.Rows?.Count >= 1))
            {
                foreach (DataRow row in objDataSet.Tables[0].Rows)
                {
                    intList.Add((int)row[0]);
                }
            }
            return intList;
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
            int intReturnCode = PerformQuery(objQueryController, "DeleteAll", null);

            return intReturnCode;
        }

        public int MarkDuplicates()
        {
            return MarkDuplicates((DBOFactoryTransaction)null);
        }

        /// <summary>
        /// This method deletes a Vendor record
        /// <returns>The return value from the stored procedure.</returns>
        public int MarkDuplicates(DBOFactoryTransaction objTransaction)
        {
            GetResultsDataSetDelegate objGetResultsMethod = null;
            FillCommandParamsDelegate objFillParamsMethod = null;

            QueryController objQueryController = new QueryController("spImageFileInfo_RemoveDuplicates", null, objFillParamsMethod, objGetResultsMethod);
            int intReturnCode = PerformQuery(objQueryController, "MarkDuplicates", null);

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
            int intReturnCode = PerformQuery(objQueryController, "MarkMoved", null);
            this.ResultDataSet = null;

            return intReturnCode;
        }

        #endregion
    }
}