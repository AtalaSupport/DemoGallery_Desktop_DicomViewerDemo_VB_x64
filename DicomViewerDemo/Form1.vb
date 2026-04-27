Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO

Namespace DicomViewerDemo
    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1 : Inherits System.Windows.Forms.Form
        Private _currentFile As String = ""
        Private _currentIndex As Integer
        Private mainMenu1 As System.Windows.Forms.MainMenu
        Private menuFile As System.Windows.Forms.MenuItem
        Private WithEvents menuFileOpen As System.Windows.Forms.MenuItem
        Private menuItem3 As System.Windows.Forms.MenuItem
        Private WithEvents menuExit As System.Windows.Forms.MenuItem
        Private menuHelp As System.Windows.Forms.MenuItem
        Private WithEvents menuAbout As System.Windows.Forms.MenuItem
        Private workspaceViewer1 As Atalasoft.Imaging.WinControls.WorkspaceViewer
        Private WithEvents thumbnailView1 As Atalasoft.Imaging.WinControls.ThumbnailView
        Private panel1 As System.Windows.Forms.Panel
        Private tvMetadata As System.Windows.Forms.TreeView
        Private menuTool As System.Windows.Forms.MenuItem
        Private WithEvents menuToolMagnifier As System.Windows.Forms.MenuItem
        Private WithEvents menuToolZoom As System.Windows.Forms.MenuItem
        Private WithEvents menuToolPan As System.Windows.Forms.MenuItem
        Private WithEvents menuToolNone As System.Windows.Forms.MenuItem
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            ' Register the DICOM decoder.
            Try
                Atalasoft.Imaging.Codec.RegisteredDecoders.Decoders.Insert(0, New Atalasoft.Imaging.Codec.Dicom.DicomDecoder)
            Catch e1 As Atalasoft.Imaging.AtalasoftLicenseException
                Me.menuFile.Enabled = False
                ShowLicenseMessage("DICOM Add-on")
            End Try
        End Sub

        Private Sub ShowLicenseMessage(ByVal product As String)
            If MessageBox.Show("This demo requires a " & product & " license." & Constants.vbCrLf & "An evaluation license can be requested with our activation utility." & Constants.vbCrLf & Constants.vbCrLf & "Would you like to run this utility now?", product & " License Required", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Dim activationUtil As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) & "\Atalasoft\DotImage "

                ' Use reflection to find out which version of DotImage we are using.
                Dim assemblies As System.Reflection.AssemblyName() = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                For Each name As System.Reflection.AssemblyName In assemblies
                    If name.Name = "Atalasoft.dotImage" Then
                        activationUtil &= name.Version.ToString(2)
                        Exit For
                    End If
                Next name

                activationUtil &= "\AtalasoftToolkitActivation.exe"

                If System.IO.File.Exists(activationUtil) Then
                    System.Diagnostics.Process.Start(activationUtil)
                Else
                    MessageBox.Show("We were unable to location the activation utility on this system." & Constants.vbCrLf & "Please run it from the start menu.", "AtalasoftToolkitActivation.exe Not Found")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
            Me.mainMenu1 = New System.Windows.Forms.MainMenu
            Me.menuFile = New System.Windows.Forms.MenuItem
            Me.menuFileOpen = New System.Windows.Forms.MenuItem
            Me.menuItem3 = New System.Windows.Forms.MenuItem
            Me.menuExit = New System.Windows.Forms.MenuItem
            Me.menuTool = New System.Windows.Forms.MenuItem
            Me.menuToolMagnifier = New System.Windows.Forms.MenuItem
            Me.menuToolPan = New System.Windows.Forms.MenuItem
            Me.menuToolZoom = New System.Windows.Forms.MenuItem
            Me.menuHelp = New System.Windows.Forms.MenuItem
            Me.menuAbout = New System.Windows.Forms.MenuItem
            Me.workspaceViewer1 = New Atalasoft.Imaging.WinControls.WorkspaceViewer
            Me.thumbnailView1 = New Atalasoft.Imaging.WinControls.ThumbnailView
            Me.panel1 = New System.Windows.Forms.Panel
            Me.tvMetadata = New System.Windows.Forms.TreeView
            Me.menuToolNone = New System.Windows.Forms.MenuItem
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' mainMenu1
            ' 
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuTool, Me.menuHelp})
            ' 
            ' menuFile
            ' 
            Me.menuFile.Index = 0
            Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFileOpen, Me.menuItem3, Me.menuExit})
            Me.menuFile.Text = "&File"
            ' 
            ' menuFileOpen
            ' 
            Me.menuFileOpen.Index = 0
            Me.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
            Me.menuFileOpen.Text = "&Open"
            '			Me.menuFileOpen.Click += New System.EventHandler(Me.menuFileOpen_Click);
            ' 
            ' menuItem3
            ' 
            Me.menuItem3.Index = 1
            Me.menuItem3.Text = "-"
            ' 
            ' menuExit
            ' 
            Me.menuExit.Index = 2
            Me.menuExit.Text = "E&xit"
            '			Me.menuExit.Click += New System.EventHandler(Me.menuExit_Click);
            ' 
            ' menuTool
            ' 
            Me.menuTool.Index = 1
            Me.menuTool.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuToolNone, Me.menuToolMagnifier, Me.menuToolPan, Me.menuToolZoom})
            Me.menuTool.Text = "&Tool"
            ' 
            ' menuToolMagnifier
            ' 
            Me.menuToolMagnifier.Index = 1
            Me.menuToolMagnifier.RadioCheck = True
            Me.menuToolMagnifier.Text = "&Magnifier"
            '			Me.menuToolMagnifier.Click += New System.EventHandler(Me.menuToolMagnifier_Click);
            ' 
            ' menuToolPan
            ' 
            Me.menuToolPan.Index = 2
            Me.menuToolPan.RadioCheck = True
            Me.menuToolPan.Text = "&Pan"
            '			Me.menuToolPan.Click += New System.EventHandler(Me.menuToolPan_Click);
            ' 
            ' menuToolZoom
            ' 
            Me.menuToolZoom.Index = 3
            Me.menuToolZoom.RadioCheck = True
            Me.menuToolZoom.Text = "&Zoom"
            '			Me.menuToolZoom.Click += New System.EventHandler(Me.menuToolZoom_Click);
            ' 
            ' menuHelp
            ' 
            Me.menuHelp.Index = 2
            Me.menuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAbout})
            Me.menuHelp.Text = "&Help"
            ' 
            ' menuAbout
            ' 
            Me.menuAbout.Index = 0
            Me.menuAbout.Text = "&About..."
            '			Me.menuAbout.Click += New System.EventHandler(Me.menuAbout_Click);
            ' 
            ' workspaceViewer1
            ' 
            Me.workspaceViewer1.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray
            Me.workspaceViewer1.BackColor = System.Drawing.SystemColors.Window
            Me.workspaceViewer1.Centered = True
            Me.workspaceViewer1.DisplayProfile = Nothing
            Me.workspaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.workspaceViewer1.Location = New System.Drawing.Point(0, 0)
            Me.workspaceViewer1.Magnifier.BackColor = System.Drawing.Color.White
            Me.workspaceViewer1.Magnifier.BorderColor = System.Drawing.Color.Black
            Me.workspaceViewer1.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me.workspaceViewer1.Name = "workspaceViewer1"
            Me.workspaceViewer1.OutputProfile = Nothing
            Me.workspaceViewer1.Selection = Nothing
            Me.workspaceViewer1.Size = New System.Drawing.Size(536, 449)
            Me.workspaceViewer1.TabIndex = 0
            Me.workspaceViewer1.Text = "workspaceViewer1"
            ' 
            ' thumbnailView1
            ' 
            Me.thumbnailView1.AutoDragDrop = False
            Me.thumbnailView1.BackColor = System.Drawing.SystemColors.Window
            Me.thumbnailView1.CaptionLines = 0
            Me.thumbnailView1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.thumbnailView1.DragDistanceTrigger = 20
            Me.thumbnailView1.DragSelectionColor = System.Drawing.Color.Red
            Me.thumbnailView1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.thumbnailView1.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight
            Me.thumbnailView1.HighlightTextColor = System.Drawing.SystemColors.HighlightText
            Me.thumbnailView1.LoadErrorMessage = ""
            Me.thumbnailView1.LoadMethod = Atalasoft.Imaging.WinControls.ThumbLoadMethod.EntireFolder
            Me.thumbnailView1.Location = New System.Drawing.Point(0, 449)
            Me.thumbnailView1.Margins = New Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4)
            Me.thumbnailView1.MaxWorkerThreads = 3
            Me.thumbnailView1.Name = "thumbnailView1"
            Me.thumbnailView1.SelectedItemStyle = Atalasoft.Imaging.WinControls.SelectedItemRenderStyle.Extended
            Me.thumbnailView1.SelectionRectangleBackColor = System.Drawing.Color.Transparent
            Me.thumbnailView1.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            Me.thumbnailView1.SelectionRectangleLineColor = System.Drawing.Color.Black
            Me.thumbnailView1.Size = New System.Drawing.Size(816, 128)
            Me.thumbnailView1.TabIndex = 1
            Me.thumbnailView1.Text = "thumbnailView1"
            Me.thumbnailView1.ThumbnailBackground = Nothing
            Me.thumbnailView1.ThumbnailLayout = Atalasoft.Imaging.WinControls.ThumbnailLayout.Horizontal
            Me.thumbnailView1.ThumbnailOffset = New System.Drawing.Point(0, 0)
            Me.thumbnailView1.ThumbnailSize = New System.Drawing.Size(100, 100)
            '			Me.thumbnailView1.SelectedIndexChanged += New System.EventHandler(Me.thumbnailView1_SelectedIndexChanged);
            ' 
            ' panel1
            ' 
            Me.panel1.Controls.Add(Me.tvMetadata)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(536, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(280, 449)
            Me.panel1.TabIndex = 2
            ' 
            ' tvMetadata
            ' 
            Me.tvMetadata.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.tvMetadata.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvMetadata.ImageIndex = -1
            Me.tvMetadata.Location = New System.Drawing.Point(0, 0)
            Me.tvMetadata.Name = "tvMetadata"
            Me.tvMetadata.SelectedImageIndex = -1
            Me.tvMetadata.Size = New System.Drawing.Size(280, 449)
            Me.tvMetadata.TabIndex = 0
            ' 
            ' menuToolNone
            ' 
            Me.menuToolNone.Checked = True
            Me.menuToolNone.Index = 0
            Me.menuToolNone.RadioCheck = True
            Me.menuToolNone.Text = "None"
            '			Me.menuToolNone.Click += New System.EventHandler(Me.menuToolNone_Click);
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(816, 577)
            Me.Controls.Add(Me.workspaceViewer1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.thumbnailView1)
            Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
            Me.Menu = Me.mainMenu1
            Me.MinimumSize = New System.Drawing.Size(600, 488)
            Me.Name = "Form1"
            Me.Text = "Atalasoft DICOM Viewer"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()>
        Shared Sub Main()
            Application.Run(New Form1)
        End Sub

#Region "File Menu"

        Private Sub menuFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFileOpen.Click
            'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
            '			using (OpenFileDialog dlg = New OpenFileDialog())
            Dim dlg As OpenFileDialog = New OpenFileDialog
            Try
                dlg.Filter = "All Files (*.*)|*.*"
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                    '					using (FileStream fs = New FileStream(dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    Dim fs As FileStream = New FileStream(dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                    Try
                        'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                        '						using (Atalasoft.Imaging.Codec.Dicom.DicomDecoder decoder = New Atalasoft.Imaging.Codec.Dicom.DicomDecoder())
                        Dim decoder As Atalasoft.Imaging.Codec.Dicom.DicomDecoder = New Atalasoft.Imaging.Codec.Dicom.DicomDecoder
                        Try

                            ' This demo only cares about DICOM images.
                            If (Not decoder.IsValidFormat(fs)) Then
                                MessageBox.Show(Me, "The image selected is not a recognized DICOM format.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If

                            ' Reset controls.
                            Me._currentFile = dlg.FileName
                            Me._currentIndex = 0
                            Me.workspaceViewer1.Images.Clear()
                            Me.thumbnailView1.Items.Clear()
                            Me.tvMetadata.Nodes.Clear()

                            fs.Seek(0, SeekOrigin.Begin)
                            LoadMetadata(fs)

                            ' Only keep one image in memory at a time.
                            fs.Seek(0, SeekOrigin.Begin)
                            Me.workspaceViewer1.Open(fs, 0)

                            ' Add the thumbnails.
                            fs.Seek(0, SeekOrigin.Begin)
                            Dim count As Integer = decoder.GetFrameCount(fs)
                            Dim thumbs As Atalasoft.Imaging.WinControls.Thumbnail() = New Atalasoft.Imaging.WinControls.Thumbnail(count - 1) {}
                            Dim i As Integer = 0
                            'ORIGINAL LINE: for (int i = 0; i < count; i += 1)
                            'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
                            Do While i < count
                                thumbs(i) = New Atalasoft.Imaging.WinControls.Thumbnail(dlg.FileName, i, "", "")
                                i += 1
                            Loop
                            Me.thumbnailView1.Items.AddRange(thumbs)
                        Finally
                            Dim disp As IDisposable = decoder
                            disp.Dispose()
                        End Try
                        'INSTANT VB NOTE: End of the original C# 'using' block
                    Finally
                        Dim disp As IDisposable = fs
                        disp.Dispose()
                    End Try
                    'INSTANT VB NOTE: End of the original C# 'using' block
                End If
            Finally
                Dim disp As IDisposable = dlg
                disp.Dispose()
            End Try
            'INSTANT VB NOTE: End of the original C# 'using' block
        End Sub

        Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
            Me.Close()
        End Sub

#End Region

#Region "Help Menu"

        Private Sub menuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAbout.Click
            Dim frm As AtalaDemos.AboutBox.About = New AtalaDemos.AboutBox.About("About...", "Atalasoft DICOM Viewer")
            frm.Description = "This demo loads DICOM images and metadata using the Atalasoft.dotImage.Dicom assembly."
            frm.ShowDialog(Me)
            frm.Dispose()
        End Sub

#End Region

#Region "Tool Menu"

        Private Sub menuToolNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuToolNone.Click
            Me.workspaceViewer1.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None

            Me.menuToolNone.Checked = True
            Me.menuToolMagnifier.Checked = False
            Me.menuToolPan.Checked = False
            Me.menuToolZoom.Checked = False
        End Sub

        Private Sub menuToolMagnifier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuToolMagnifier.Click
            Me.workspaceViewer1.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Magnifier
            Me.workspaceViewer1.Magnifier.Zoom = Me.workspaceViewer1.Zoom * 4

            Me.menuToolNone.Checked = False
            Me.menuToolMagnifier.Checked = True
            Me.menuToolPan.Checked = False
            Me.menuToolZoom.Checked = False
        End Sub

        Private Sub menuToolPan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuToolPan.Click
            Me.workspaceViewer1.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Pan

            Me.menuToolNone.Checked = False
            Me.menuToolMagnifier.Checked = False
            Me.menuToolPan.Checked = True
            Me.menuToolZoom.Checked = False
        End Sub

        Private Sub menuToolZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuToolZoom.Click
            Me.workspaceViewer1.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Zoom

            Me.menuToolNone.Checked = False
            Me.menuToolMagnifier.Checked = False
            Me.menuToolPan.Checked = False
            Me.menuToolZoom.Checked = True
        End Sub

#End Region

#Region "Events"

        Private Sub thumbnailView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles thumbnailView1.SelectedIndexChanged
            ' Load the selected thumbnail.
            If Not Me.thumbnailView1.FocusedItem Is Nothing Then
                Dim index As Integer = Me.thumbnailView1.SelectedIndices(0)
                If index <> Me._currentIndex Then
                    Me._currentIndex = index
                    Me.workspaceViewer1.Open(Me._currentFile, index)
                End If
            End If
        End Sub

#End Region

#Region "Metadata"

        Private Sub LoadMetadata(ByVal fs As FileStream)
            Me.tvMetadata.Nodes.Clear()

            Try
                'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                '				using (Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser parser = New Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser(fs))
                Dim parser As Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser = New Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser(fs)
                Try

                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Acquisition)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Command)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Identifying)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.ImagePresentation)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Patient)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Relationship)
                    LookupTagData(parser, Atalasoft.Imaging.Codec.Dicom.DicomGroup.Text)
                Finally
                    Dim disp As IDisposable = parser
                    disp.Dispose()
                End Try
                'INSTANT VB NOTE: End of the original C# 'using' block
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "Metadata Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If Me.tvMetadata.Nodes.Count > 0 Then
                Me.tvMetadata.ExpandAll()
                Me.tvMetadata.SelectedNode = Me.tvMetadata.Nodes(0)
            End If
        End Sub

        Private Sub LookupTagData(ByVal parser As Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser, ByVal group As Atalasoft.Imaging.Codec.Dicom.DicomGroup)
            Dim parentNode As TreeNode = New TreeNode(group.ToString())

            Select Case group
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Acquisition
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionContrastBolusAgent, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionEchoTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionGantryTilt, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionInversionTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionKVP, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionRadionuclide, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionRepetitionTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.AcquisitionSliceThickness, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Command
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Identifying
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingAcquisitionDate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingAcquisitionTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingCreationDate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingCreationTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingImageDate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingImageTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingImageType, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingModality, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingRecognitionCode, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingSeriesDate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingSeriesTime, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingStudyDate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.IdentifyingStudyTime, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.ImagePresentation
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationBitsAllocated, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationBitsStored, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationColumns, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationHighBit, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationNumberOfFrames, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationPixelRepresentation, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.ImagePresentationPixelSize, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Patient
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.PatientAge, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.PatientBirthdate, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.PatientID, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.PatientName, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.PatientSex, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Relationship
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.RelationshipPositionReference, parentNode)
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.RelationshipSliceLocation, parentNode)
                Case Atalasoft.Imaging.Codec.Dicom.DicomGroup.Text
                    LookupTagData(parser, group, Atalasoft.Imaging.Codec.Dicom.DicomTag.GroupComments, parentNode)
            End Select

            If parentNode.Nodes.Count > 0 Then
                Me.tvMetadata.Nodes.Add(parentNode)
            End If
        End Sub

        Private Sub LookupTagData(ByVal parser As Atalasoft.Imaging.Codec.Dicom.DicomHeaderParser, ByVal group As Atalasoft.Imaging.Codec.Dicom.DicomGroup, ByVal tag As Atalasoft.Imaging.Codec.Dicom.DicomTag, ByVal parentNode As TreeNode)
            Dim zero As UInt32 = Convert.ToUInt32(0)
            Dim s As String = parser.GetString(group, zero, tag, zero)
            If s.Length > 0 Then
                Dim node As TreeNode = New TreeNode(tag.ToString())
                node.Nodes.Add(s)

                For i As System.Int32 = 1 To 99
                    s = parser.GetString(group, zero, tag, Convert.ToUInt32(i))
                    If s.Length = 0 Then
                        Exit For
                    End If
                    node.Nodes.Add(s)
                Next i

                parentNode.Nodes.Add(node)
            End If
        End Sub

#End Region

    End Class
End Namespace
