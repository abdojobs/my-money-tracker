'
' Created by SharpDevelop.
' User: Jeff Walsh
' Date: 14/07/2012
' Time: 12:09 PM
' Jeff Walsh - Game Associated Programming®
' 
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.menuStripMainForm = New System.Windows.Forms.MenuStrip()
		Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
		Me.tsmiFileCSVLoad = New System.Windows.Forms.ToolStripMenuItem()
		Me.tsmiFileMainLoad = New System.Windows.Forms.ToolStripMenuItem()
		Me.viewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.rawDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.searchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.menuStripMainForm.SuspendLayout
		Me.SuspendLayout
		'
		'menuStripMainForm
		'
		Me.menuStripMainForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.viewToolStripMenuItem})
		Me.menuStripMainForm.Location = New System.Drawing.Point(0, 0)
		Me.menuStripMainForm.Name = "menuStripMainForm"
		Me.menuStripMainForm.Size = New System.Drawing.Size(792, 24)
		Me.menuStripMainForm.TabIndex = 1
		'
		'tsmiFile
		'
		Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFileCSVLoad, Me.tsmiFileMainLoad})
		Me.tsmiFile.Name = "tsmiFile"
		Me.tsmiFile.Size = New System.Drawing.Size(35, 20)
		Me.tsmiFile.Text = "File"
		'
		'tsmiFileCSVLoad
		'
		Me.tsmiFileCSVLoad.Name = "tsmiFileCSVLoad"
		Me.tsmiFileCSVLoad.Size = New System.Drawing.Size(122, 22)
		Me.tsmiFileCSVLoad.Text = "CSV Load"
		AddHandler Me.tsmiFileCSVLoad.Click, AddressOf Me.TsmiFileCSVLoadClick
		'
		'tsmiFileMainLoad
		'
		Me.tsmiFileMainLoad.Name = "tsmiFileMainLoad"
		Me.tsmiFileMainLoad.Size = New System.Drawing.Size(122, 22)
		Me.tsmiFileMainLoad.Text = "Main Load"
		AddHandler Me.tsmiFileMainLoad.Click, AddressOf Me.tsmiFileMainLoadClick
		'
		'viewToolStripMenuItem
		'
		Me.viewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.rawDataToolStripMenuItem, Me.searchToolStripMenuItem})
		Me.viewToolStripMenuItem.Name = "viewToolStripMenuItem"
		Me.viewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
		Me.viewToolStripMenuItem.Text = "View"
		'
		'rawDataToolStripMenuItem
		'
		Me.rawDataToolStripMenuItem.Name = "rawDataToolStripMenuItem"
		Me.rawDataToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.rawDataToolStripMenuItem.Text = "Raw Data"
		AddHandler Me.rawDataToolStripMenuItem.Click, AddressOf Me.RawDataToolStripMenuItemClick
		'
		'searchToolStripMenuItem
		'
		Me.searchToolStripMenuItem.Name = "searchToolStripMenuItem"
		Me.searchToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.searchToolStripMenuItem.Text = "Search"
		AddHandler Me.searchToolStripMenuItem.Click, AddressOf Me.SearchToolStripMenuItemClick
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(792, 573)
		Me.Controls.Add(Me.menuStripMainForm)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.IsMdiContainer = true
		Me.MainMenuStrip = Me.menuStripMainForm
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "Money Tracker v"
		Me.menuStripMainForm.ResumeLayout(false)
		Me.menuStripMainForm.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private searchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private rawDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private viewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private tsmiFileMainLoad As System.Windows.Forms.ToolStripMenuItem
	Private tsmiFileCSVLoad As System.Windows.Forms.ToolStripMenuItem
	Private tsmiFile As System.Windows.Forms.ToolStripMenuItem
	Private menuStripMainForm As System.Windows.Forms.MenuStrip
End Class
