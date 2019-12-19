<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TracklogsDataGridView = New System.Windows.Forms.DataGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.LoonsDataGridView = New System.Windows.Forms.DataGridView()
        Me.LoonsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TracklogsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenpozArchiveFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenParkObserverFilesDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.TracklogsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.LoonsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoonsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TracklogsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TracklogsDataGridView
        '
        Me.TracklogsDataGridView.AllowUserToOrderColumns = True
        Me.TracklogsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.TracklogsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TracklogsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TracklogsDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.TracklogsDataGridView.Name = "TracklogsDataGridView"
        Me.TracklogsDataGridView.RowTemplate.Height = 28
        Me.TracklogsDataGridView.Size = New System.Drawing.Size(2320, 376)
        Me.TracklogsDataGridView.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 33)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TracklogsDataGridView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(2320, 1365)
        Me.SplitContainer1.SplitterDistance = 376
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.LoonsDataGridView)
        Me.SplitContainer2.Size = New System.Drawing.Size(2320, 985)
        Me.SplitContainer2.SplitterDistance = 500
        Me.SplitContainer2.TabIndex = 0
        '
        'LoonsDataGridView
        '
        Me.LoonsDataGridView.AllowUserToOrderColumns = True
        Me.LoonsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.LoonsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.LoonsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoonsDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.LoonsDataGridView.Name = "LoonsDataGridView"
        Me.LoonsDataGridView.RowTemplate.Height = 28
        Me.LoonsDataGridView.Size = New System.Drawing.Size(2320, 500)
        Me.LoonsDataGridView.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(2320, 33)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenpozArchiveFileToolStripMenuItem, Me.OpenParkObserverFilesDirectoryToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(50, 29)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenpozArchiveFileToolStripMenuItem
        '
        Me.OpenpozArchiveFileToolStripMenuItem.Enabled = False
        Me.OpenpozArchiveFileToolStripMenuItem.Name = "OpenpozArchiveFileToolStripMenuItem"
        Me.OpenpozArchiveFileToolStripMenuItem.Size = New System.Drawing.Size(378, 30)
        Me.OpenpozArchiveFileToolStripMenuItem.Text = "Open .poz archive file..."
        '
        'OpenParkObserverFilesDirectoryToolStripMenuItem
        '
        Me.OpenParkObserverFilesDirectoryToolStripMenuItem.Name = "OpenParkObserverFilesDirectoryToolStripMenuItem"
        Me.OpenParkObserverFilesDirectoryToolStripMenuItem.Size = New System.Drawing.Size(378, 30)
        Me.OpenParkObserverFilesDirectoryToolStripMenuItem.Text = "Open Park Observer files directory..."
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2320, 1398)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.TracklogsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.LoonsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoonsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TracklogsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TracklogsDataGridView As DataGridView
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents LoonsDataGridView As DataGridView
    Friend WithEvents LoonsBindingSource As BindingSource
    Friend WithEvents TracklogsBindingSource As BindingSource
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenpozArchiveFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenParkObserverFilesDirectoryToolStripMenuItem As ToolStripMenuItem
End Class
