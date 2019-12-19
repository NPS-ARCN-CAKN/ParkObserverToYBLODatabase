Module Utilities
    ''' <summary>
    ''' Converts a comma separated values file into a DataTable
    ''' </summary>
    ''' <param name="CSVFileInfo">Input comma separated values file. FileInfo</param>
    ''' <returns>Output DataTable</returns>
    Public Function CSVToDataTable(ByVal CSVFileInfo As IO.FileInfo) As System.Data.DataTable
        Dim CSVDataTable As New DataTable() 'The output DataTable
        Try
            'Build a StreamReader and read the contents of the CSVFile into it
            Dim CSVStreamReader As New IO.StreamReader(CSVFileInfo.FullName)
            Dim CSVFullFileStream As String = CSVStreamReader.ReadToEnd()
            CSVStreamReader.Close()
            CSVStreamReader.Dispose()

            'Build a String array and read the lines into it
            Dim CSVLines As String() = CSVFullFileStream.Split(ControlChars.Lf)

            'Assume the first line contains column headers. Load them into the DataTable as DataColumns
            Dim ColumnNames As String() = CSVLines(0).Split(","c)
            For Each ColumnName As String In ColumnNames
                CSVDataTable.Columns.Add(New DataColumn(ColumnName.Trim))
            Next

            'Make data rows
            Dim NewRow As DataRow
            Dim CleanedCSVLine As String = ""

            'We need to skip the first line which is assumed to be headers so we need a line counter
            Dim Counter As Integer = 0

            'Loop through the CSV lines
            For Each CSVLine As String In CSVLines
                If Counter <> 0 Then
                    'Make a new DataRow
                    NewRow = CSVDataTable.NewRow()

                    'Replace any carriage returns with blanks
                    CleanedCSVLine = CSVLine.Replace(Convert.ToString(ControlChars.Cr), "")

                    'Split the cleaned line by commas and load them into NewRow DataRow
                    NewRow.ItemArray = CleanedCSVLine.Split(","c)

                    'Add the new row to the output DataTable
                    CSVDataTable.Rows.Add(NewRow)
                End If

                'Increment the counter
                Counter = Counter + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return CSVDataTable
    End Function
End Module
