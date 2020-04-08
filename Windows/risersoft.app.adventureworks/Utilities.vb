Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Infragistics.Win
Imports System.Data
Imports System.Diagnostics
Imports System.Reflection
Imports System.Drawing
Imports System.Windows.Forms
Imports Infragistics.Win.UltraWinToolbars
Imports System.Resources

''' <summary>
''' Utility class to perform context-independent functionality
''' </summary>
Friend Class Utilities


    Private Shared m_executingAssembly As Assembly

	Private Shared ReadOnly Property ExecutingAssembly() As Assembly
		Get
			If m_executingAssembly Is Nothing Then
				m_executingAssembly = Assembly.GetExecutingAssembly()
			End If
			Return m_executingAssembly
		End Get
	End Property


    ''' <summary>
    ''' Sets the Enabled property of the tools within the specified RibbonGroup.
    ''' </summary>
    ''' <param name="group">The group.</param>
    ''' <param name="enabledState">if set to <c>true</c> [enabled state].</param>
    Friend Shared Sub SetRibbonGroupToolsEnabledState(group As RibbonGroup, enabled As Boolean)
		If group Is Nothing Then
			Return
		End If

		For Each tool As ToolBase In group.Tools
			tool.SharedProps.Enabled = enabled
		Next
	End Sub



    ''' <summary>
    ''' Creates new images using a pixel-based color replacement on the image from the provided imagelist. 
    ''' </summary>
    ''' <param name="oldColor">The old Color.</param>
    ''' <param name="colors">The dictionary containing the new resolved colors</param>
    ''' <param name="defaultImageList">The default ImageList.</param>
    ''' <param name="colorizedImageList">The colorized ImageList.</param>
    Friend Shared Sub ColorizeImages(oldColor As Color, colors As Dictionary(Of String, Color), ByRef defaultImageList As ImageList, ByRef colorizedImageList As ImageList)
		' loop through the default imageliss, colorize the image and put it into the colorized imagelist
		For Each key As String In defaultImageList.Images.Keys
			Dim bitmap As Bitmap = TryCast(defaultImageList.Images(key), Bitmap)
			If bitmap IsNot Nothing Then
				bitmap = TryCast(bitmap.Clone(), Bitmap)
				For Each colorKey As String In colors.Keys
					If key.EndsWith(colorKey) Then
						Infragistics.Win.DrawUtility.ReplaceColor(bitmap, oldColor, colors(colorKey))
						Exit For
					End If
				Next

				Dim index As Integer = colorizedImageList.Images.IndexOfKey(key)
				If index >= 0 Then
					Dim oldImage As Image = colorizedImageList.Images(index)
					colorizedImageList.Images.RemoveByKey(key)
					If oldImage IsNot Nothing Then
						oldImage.Dispose()
					End If
				End If
				colorizedImageList.Images.Add(key, bitmap)
			End If
		Next
	End Sub



    ''' <summary>
    ''' Retrieves the string representation of the request AssemblyAttribute from the project's AssemblyInfo.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value">The value.</param>
    ''' <returns></returns>
    Friend Shared Function GetAssemblyAttribute(Of T As Attribute)(value As Func(Of T, String)) As String
		Dim attribute__1 As T = DirectCast(Attribute.GetCustomAttribute(Utilities.ExecutingAssembly, GetType(T)), T)
		Return value.Invoke(attribute__1)
	End Function




    ''' <summary>
    ''' Gets the embedded resource stream.
    ''' </summary>
    ''' <param name="resourceName">Name of the resource.</param>
    ''' <returns></returns>
    Friend Shared Function GetEmbeddedResourceStream(resourceName As String) As System.IO.Stream
		Dim stream As System.IO.Stream = Utilities.ExecutingAssembly.GetManifestResourceStream(resourceName)
		Debug.Assert(stream IsNot Nothing, "Unable to locate embedded resource.", "Resource name: {0}", resourceName)
		Return stream
	End Function


    ''' <summary>
    ''' Localizes a string using the ResourceManager.
    ''' </summary>
    ''' <param name="currentString"></param>
    ''' <returns></returns>
    Friend Shared Function GetLocalizedString(currentString As String) As String
        Dim rm As ResourceManager = Resources.ResourceManager
        Dim localizedString As String = rm.GetString(currentString)
		Return (If(String.IsNullOrEmpty(localizedString), currentString, localizedString)).Replace("_"C, " "C)
	End Function


    ''' <summary>
    ''' Gets an array of  style library resource names.
    ''' </summary>
    ''' <returns></returns>
    Friend Shared Function GetStyleLibraryResourceNames() As String()
		Dim resourceStrings As New List(Of String)(Utilities.ExecutingAssembly.GetManifestResourceNames())

		Return resourceStrings.FindAll(Function(i) i.EndsWith(".isl")).ToArray()
	End Function

    ''' <summary>
    ''' Colorizes the images in the large and small imagelists containing the default images, and place the new images in the colorized imagelists.
    ''' </summary>
    Friend Shared Sub ColorizeImages(bar As UltraToolbarsManager, ILDefaultSm As ImageList, ILDefaultLg As ImageList, ILColorSm As ImageList, ILColorLg As ImageList)
        ' Suspend painting to the UltraToolbarsManager
        Dim shouldSuspendPainting As Boolean = Not bar.IsUpdating
        If shouldSuspendPainting Then
            bar.BeginUpdate()
        End If

        Dim largeImageList As ImageList = bar.ImageListLarge
        Dim smallImageList As ImageList = bar.ImageListSmall

        Try
            bar.ImageListLarge = Nothing
            bar.ImageListSmall = Nothing

            Dim resolveTool As ToolBase = Nothing

            If bar.Tools.Exists("Insert_Task") Then
                resolveTool = bar.Tools("Insert_Task")

                ' loop through all instances looking for the tool in the RibbonGroup.
                For Each instanceTool As ToolBase In resolveTool.SharedProps.ToolInstances
                    If instanceTool.OwnerIsRibbonGroup Then
                        resolveTool = instanceTool
                        Exit For
                    End If
                Next
            End If

            If resolveTool Is Nothing Then
                Return
            End If

            ' Get the resolved colors
            Dim colors As New Dictionary(Of String, Color)()
            Dim appData As New AppearanceData()
            Dim requestedProps As AppearancePropFlags = AppearancePropFlags.ForeColor
            resolveTool.ResolveAppearance(appData, requestedProps)
            colors("Normal") = appData.ForeColor

            appData = New AppearanceData()
            requestedProps = AppearancePropFlags.ForeColor Or AppearancePropFlags.BackColor
            resolveTool.ResolveAppearance(appData, requestedProps, True, False)
            colors("Active") = appData.ForeColor

            If appData.BackColor.IsEmpty OrElse appData.BackColor.Equals(Color.Transparent) Then
                appData = New AppearanceData()
                requestedProps = AppearancePropFlags.BackColor
                bar.Ribbon.Tabs(0).ResolveTabItemAppearance(appData, requestedProps)
                colors("Disabled") = appData.BackColor
            Else
                colors("Disabled") = appData.BackColor
            End If

            Dim replacementColor As Color = Color.Magenta

            Utilities.ColorizeImages(replacementColor, colors, ILDefaultLg, ILColorLg)
            Utilities.ColorizeImages(replacementColor, colors, ILDefaultSm, ILColorSm)

            ' Make sure the UltraToolbarsManager is using the new colorized images
            largeImageList = ILColorLg
            smallImageList = ILColorSm
        Catch
            ' Make sure the UltraToolbarsManager is using the new colorized images
            largeImageList = ILDefaultLg
            smallImageList = ILDefaultSm
        Finally
            bar.ImageListLarge = largeImageList
            bar.ImageListSmall = smallImageList

            ' Resume painting on the UltraToolbarsManager
            If shouldSuspendPainting Then
                bar.EndUpdate()
            End If
        End Try
    End Sub


End Class
