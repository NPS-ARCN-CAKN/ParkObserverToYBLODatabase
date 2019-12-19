Imports Janus.Windows.GridEX

Public Class Form1

    'Dim ParkObserverDataset As New DataSet("ParkObserverDataset")
    'Dim  = "C:\Temp\YBLo\sdm"

    Dim GoodColor As Color = Color.White
    Dim BadColor As Color = Color.Red

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetParkObserverDataset("C:\Temp\YBLo\sdm")

        'Me.TracklogsDataGridView.DataSource = ParkObserverDataset.Tables("Tracklogs")
        'Me.LoonsDataGridView.DataSource = ParkObserverDataset.Tables("Loons")
        'validate the data in the park observer data grid
        'these columns should be required, highlight any missing data



    End Sub

    Private Sub GetParkObserverDataset(ParkObserverFilesDirectory As String)
        Dim ParkObserverDataset = New DataSet("ParkObserverDataset")
        If My.Computer.FileSystem.DirectoryExists(ParkObserverFilesDirectory) = True Then


            'TRACKLOGS: add the tracklogs to the park observer dataset
            Dim TracklogsDataTable As New DataTable()

            Dim TracklogFilePath As String = ParkObserverFilesDirectory & "\Tracklogs.csv"
            If My.Computer.FileSystem.FileExists(TracklogFilePath) Then
                TracklogsDataTable = CSVToDataTable(New IO.FileInfo(TracklogFilePath))
            Else
                MsgBox("File " & TracklogFilePath & " not found.")
            End If

            'clean the data
            CleanTimeColumn(TracklogsDataTable, "Start_Local")
            CleanTimeColumn(TracklogsDataTable, "End_Local")

            'clean up quoted text columns
            For Each Column As DataColumn In TracklogsDataTable.Columns
                'if the column is type string then clean the quotes out
                If Column.DataType.ToString.Contains("String") Then
                    CleanDoubleQuotes(TracklogsDataTable, Column.ColumnName)
                End If
            Next


            'TracklogsDataTable.Columns.Add(New DataColumn("Validation", GetType(String)))
            'TracklogsDataTable.Columns("Validation").SetOrdinal(0)

            'had to add these columns to the tracklogs table because the start_local and end_local columns were imported
            'as text fields instead of date. i couldn't do datetime calculations using them so I had to create 
            'these date columns
            TracklogsDataTable.Columns.Add(New DataColumn("EnteredPlot", GetType(DateTime)))
            TracklogsDataTable.Columns.Add(New DataColumn("ExitedPlot", GetType(DateTime)))
            'add some columns to use later
            With TracklogsDataTable.Columns
                .Add(New DataColumn("InsertQuery", GetType(String)))
                .Add(New DataColumn("TracklogID", GetType(String)))
            End With

            'add values to table to use later
            For Each Row As DataRow In TracklogsDataTable.Rows
                'tracklogid
                Row.Item("TracklogID") = Guid.NewGuid.ToString

                'EnteredPlot
                If Not Row.Item("Start_Local") Is Nothing Then
                    If IsDate(Row.Item("Start_Local")) Then
                        Row.Item("EnteredPlot") = CDate(Row.Item("Start_Local"))
                    End If
                End If

                'ExitedPlot
                If Not Row.Item("End_Local") Is Nothing Then
                    If IsDate(Row.Item("End_Local")) Then
                        Row.Item("ExitedPlot") = CDate(Row.Item("End_Local"))
                    End If
                End If

                'create an insert query
                Row.Item("InsertQuery") = "INSERT INTO [dbo].[Flights]([FlightDate]
,[TailNo]
,[Pilot]
,[Observer]
,[Aircraft]
,[ProtocolVersion]
,[ProtocolIRMAReference]
,[FlightID])
VALUES('" & Row.Item("EnteredPlot") & "','" & Row.Item("TailNo") & "','" & Row.Item("Pilot") & "','" & Row.Item("Observer") & "','" & Row.Item("Aircraft") & "',1.0,0,'" & Row.Item("TracklogID") & "');"

            Next
            TracklogsDataTable.TableName = "Tracklogs"

            'add the tracklogs data table to the dataset
            ParkObserverDataset.Tables.Add(TracklogsDataTable)
            Me.TracklogsBindingSource.DataSource = ParkObserverDataset.Tables("Tracklogs")
            Me.TracklogsDataGridView.DataSource = TracklogsBindingSource
            'Me.TracklogsBindingSource.DataMember = "Tracklogs"




            'LOONS
            Dim LoonsDataTable As New DataTable()

            Dim LoonsFilePath As String = ParkObserverFilesDirectory & "\Loons.csv"
            If My.Computer.FileSystem.FileExists(LoonsFilePath) Then
                LoonsDataTable = CSVToDataTable(New IO.FileInfo(LoonsFilePath))
            Else
                MsgBox("File " & LoonsFilePath & " Not found.")
            End If
            LoonsDataTable.Columns.Add(New DataColumn("ObservationID", GetType(String)))
            LoonsDataTable.Columns.Add(New DataColumn("TracklogID", GetType(String)))
            For Each Row As DataRow In LoonsDataTable.Rows
                Row.Item("ObservationID") = Guid.NewGuid.ToString

            Next
            LoonsDataTable.TableName = "Loons"

            'clean up time columns
            CleanTimeColumn(LoonsDataTable, "Timestamp_Local")

            'clean up quoted text columns
            For Each Column As DataColumn In LoonsDataTable.Columns
                'if the column is type string then clean the quotes out
                If Column.DataType.ToString.Contains("String") Then
                    CleanDoubleQuotes(LoonsDataTable, Column.ColumnName)
                End If
            Next

            'add in the tracklogids
            'match loon observations to tracklog records
            For Each LoonRow As DataRow In LoonsDataTable.Rows
                If Not IsDBNull(LoonRow.Item("Timestamp_Local")) Then
                    If IsDate(LoonRow.Item("Timestamp_Local")) Then
                        Dim Timestamp_Local As Date = CDate(LoonRow.Item("Timestamp_Local"))
                        Dim Filter As String = "(EnteredPlot < #" & Timestamp_Local & "#) And (ExitedPlot > #" & Timestamp_Local & "#)"

                        Dim DV As New DataView(ParkObserverDataset.Tables("Tracklogs"), Filter, "EnteredPlot", DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            Dim EnteredPlot As String = DV(0).Item("EnteredPlot")
                            Dim ExitedPlot As String = DV(0).Item("ExitedPlot")
                            LoonRow.Item("TracklogID") = DV.Item(0).Item("TracklogID")
                            'LoonRow.Item("Plot") = DV.Item(0).Item("Plot")
                            'LoonRow.Item("Observer") = DV.Item(0).Item("Observer")
                            'LoonRow.Item("TailNo") = DV.Item(0).Item("TailNo")
                        End If
                    End If
                End If
            Next

            'add the loons datatable to the dataset
            ParkObserverDataset.Tables.Add(LoonsDataTable)

            'create a datarelation between tracklogs and loon observations
            Dim TracklogsToLoonsDataRelation As New DataRelation("TracklogsToLoonsDataRelation", ParkObserverDataset.Tables("Tracklogs").Columns("TracklogID"), ParkObserverDataset.Tables("Loons").Columns("TracklogID"), False)
            ParkObserverDataset.Relations.Add(TracklogsToLoonsDataRelation)

            'create a bindingsource for the master data table
            LoonsBindingSource.DataSource = TracklogsBindingSource ' ParkObserverDataset.Tables("Loons") ' 
            LoonsBindingSource.DataMember = "TracklogsToLoonsDataRelation"
            Me.LoonsDataGridView.DataSource = LoonsBindingSource

            'create a bindingsource for the detail table
            ' using the DataRelation name to filter the information in the 
            ' details table based on the current row in the master table. 
            'WorkLogBindingSource.DataSource = VitalSignsBindingSource
            'WorkLogBindingSource.DataMember = "VitalSignToWorkLogRelation"

            ''set the datagridviews data sources to the binding sources
            'Me.VitalSignsDataGridView.DataSource = VitalSignsBindingSource
            'Me.WorkLogDataGridView.DataSource = WorkLogBindingSource





            ValidateTracklogsGrid()

        Else
            MsgBox("Directory " & ParkObserverFilesDirectory & " does Not exist")
        End If
    End Sub

    Private Function DateIsBetween(DateToCompare As Date, StartDate As Date, EndDate As Date) As Boolean
        If StartDate <= DateToCompare Then
            Debug.Print(StartDate <= DateToCompare)
            Return True
        Else
            Return False
        End If
    End Function



    ''' <summary>
    ''' Highlights any recommended fields that are null such as Plot,Observer,TailNo,Start_Local,End_Local
    ''' </summary>
    Private Sub ValidateTracklogsGrid()
        'loop through the list of required columns and highlight any cells in the grid that are missing data
        Dim RequiredColumns As String = "Plot,Observer,TailNo,Start_Local,End_Local"
        Dim RequiredColumnsList() As String = RequiredColumns.Split(",")
        For Each ColumnName As String In RequiredColumnsList
            'loop through the Datagridview rows and examine the contents of each row's ColumnName cell value
            'if null or blank highlight it.
            For Each Row As DataGridViewRow In Me.TracklogsDataGridView.Rows

                If Not Row Is Nothing Then
                    If Not Row.Cells Is Nothing Then
                        'distinguish Observing="Yes" from No
                        If Not Row.Cells("Observing") Is Nothing Then
                            If Not Row.Cells("Observing").Value Is Nothing Then
                                If Not IsDBNull(Row.Cells("Observing").Value) Then
                                    If Row.Cells("Observing").Value <> "Yes" Then
                                        Row.DefaultCellStyle.ForeColor = Color.Gray
                                    End If
                                End If
                            End If
                        End If


                        'highlight nulls and blanks
                        If Not Row.Cells(ColumnName) Is Nothing Then
                            If Not Row.Cells(ColumnName).Value Is Nothing Then
                                If Row.Cells(ColumnName).Value.ToString.Trim = "" Then
                                    'cell is blank
                                    Row.Cells(ColumnName).Style.BackColor = BadColor
                                ElseIf IsDBNull(Row.Cells(ColumnName).Value) = True Then
                                    'cell is null
                                    Row.Cells(ColumnName).Style.BackColor = BadColor
                                Else
                                    'cell is not null
                                    Row.Cells(ColumnName).Style.BackColor = GoodColor
                                End If
                            End If
                        End If
                    End If
                End If

                'provide feedback on problems
                'With Row.Cells("Validation")
                '    If ErrorCount = 0 Then
                '        .Style.BackColor = Color.Green
                '        .Value = ErrorCount & "OK"
                '    Else
                '        .Style.BackColor = Color.Red
                '        .Value = ErrorCount & "Error"
                '    End If
                'End With
            Next
        Next

        'validate the loons grid
        ValidateLoonsGrid()
    End Sub

    Private Sub ValidateLoonsGrid()

        'loop through the list of required columns and highlight any cells in the grid that are missing data
        Dim RequiredColumns As String = "Lake,SpeciesAcronym,Timestamp_Local,Feature_Latitude,Feature_Longitude" ', SpeciesAcronym"
        Dim RequiredColumnsList() As String = RequiredColumns.Split(",")
        For Each ColumnName As String In RequiredColumnsList
            'loop through the Datagridview rows and examine the contents of each row's ColumnName cell value
            'if null or blank highlight it.
            For Each Row As DataGridViewRow In Me.LoonsDataGridView.Rows
                If Not Row Is Nothing Then
                    'highlight nulls and blanks
                    If Not Row.Cells(ColumnName) Is Nothing Then
                        If Not Row.Cells(ColumnName).Value Is Nothing Then
                            If Row.Cells(ColumnName).Value.ToString.Trim = "" Then
                                'cell is blank
                                Row.Cells(ColumnName).Style.BackColor = BadColor
                            ElseIf IsDBNull(Row.Cells(ColumnName).Value) = True Then
                                'cell is null
                                Row.Cells(ColumnName).Style.BackColor = BadColor
                            Else
                                'cell is not null
                                Row.Cells(ColumnName).Style.BackColor = GoodColor
                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Cleans double quotes out of Park Observer text columns
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <param name="ColumnName"></param>
    Private Sub CleanDoubleQuotes(DataTable As DataTable, ColumnName As String)
        Try
            For Each Row As DataRow In DataTable.Rows
                Dim CellValue As String = Row.Item(ColumnName).ToString.Trim
                Dim CleanedValue As String = CellValue.Replace("""", "")
                Row.Item(ColumnName) = CleanedValue
            Next

        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    ''' <summary>
    ''' Cleans 'AKDT' and 'T' out of Park Observer time columns
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <param name="ColumnName"></param>
    Private Sub CleanTimeColumn(DataTable As DataTable, ColumnName As String)
        Try
            For Each Row As DataRow In DataTable.Rows
                Dim CellValue As String = Row.Item(ColumnName).ToString.Trim
                Dim CleanedValue As String = CellValue.Replace("AKDT", "").Replace("T", " ").Replace("Z", " ") 'do the akdt part first since the t part will convert akdt to akd
                CleanedValue = CleanedValue.Replace("AKST", "")
                CleanedValue = CleanedValue.Replace("AKS", "")
                CleanedValue = CleanedValue.Replace("AKD", "")
                Row.Item(ColumnName) = CleanedValue
            Next
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenParkObserverFilesDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenParkObserverFilesDirectoryToolStripMenuItem.Click
        Dim FB As New FolderBrowserDialog
        With FB
            .Description = "Select a directory where a Park Observer .poz (.zip) archive has been decompressed."
            .RootFolder = Environment.SpecialFolder.MyComputer
        End With

        If FB.ShowDialog = DialogResult.OK Then
            If My.Computer.FileSystem.DirectoryExists(FB.SelectedPath) Then
                GetParkObserverDataset(FB.SelectedPath)
            Else
                MsgBox("Directory " & FB.SelectedPath & " does not exist.")
            End If
        End If
    End Sub

    Private Sub TracklogsDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TracklogsDataGridView.CellEndEdit
        ValidateTracklogsGrid()
    End Sub

    Private Sub TracklogsDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles TracklogsDataGridView.SelectionChanged
        ValidateLoonsGrid()

        'For Each Row As DataGridViewRow In LoonsDataGridView.Rows
        '    If TracklogsDataGridView.CurrentRow.Cells("TracklogID").Value = Row.Cells("TracklogID").Value Then
        '        If Row.DefaultCellStyle.BackColor <> Color.Red Then
        '            Row.DefaultCellStyle.BackColor = Color.AliceBlue
        '            Row.DefaultCellStyle.ForeColor = Color.Black
        '        End If
        '    Else
        '        Row.DefaultCellStyle.BackColor = Color.White
        '        Row.DefaultCellStyle.ForeColor = Color.Gray
        '    End If
        'Next


    End Sub

    Private Sub LoonsDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles LoonsDataGridView.CellEndEdit
        ValidateLoonsGrid()
    End Sub
End Class
