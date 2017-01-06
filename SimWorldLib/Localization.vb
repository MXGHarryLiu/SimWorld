Imports System.Drawing
Imports System.Drawing.Design
Imports System.Globalization
Imports System.Windows.Forms

Namespace Localization

    Public Class CategoryAttribute
        Inherits System.ComponentModel.CategoryAttribute

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal category As String)
            MyBase.New(category)
        End Sub

        Protected Overrides Function GetLocalizedString(ByVal value As String) As String
            Dim resx As New Resources.ResourceManager(
            System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Culture.ToString,
            System.Reflection.Assembly.GetExecutingAssembly)
            Try
                Return resx.GetString(value)
            Catch ex As Resources.MissingManifestResourceException
                Return value
            End Try
        End Function

    End Class

    Public Class DescriptionAttribute
        Inherits System.ComponentModel.DescriptionAttribute

        Public Sub New(ByVal description As String)
            MyBase.New(description)
            Dim resx As New Resources.ResourceManager(
                System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Culture.ToString,
                System.Reflection.Assembly.GetExecutingAssembly)
            Try
                Me.DescriptionValue = resx.GetString(description)
            Catch ex As Resources.MissingManifestResourceException
                ' Nothing happens
            End Try
        End Sub
    End Class

    Public Class MapConverter
        Inherits System.ComponentModel.ExpandableObjectConverter

        Public Overloads Overrides Function CanConvertTo(
                              ByVal context As System.ComponentModel.ITypeDescriptorContext,
                              ByVal destinationType As Type) As Boolean
            If destinationType Is GetType(Single) Or destinationType Is GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overloads Overrides Function ConvertTo(
                              ByVal context As System.ComponentModel.ITypeDescriptorContext,
                              ByVal culture As CultureInfo,
                              ByVal value As Object,
                              ByVal destinationType As Type) As Object
            If (destinationType Is GetType(Single) AndAlso TypeOf value Is Map) Then
                Return CType(value, Map).Sum
            End If
            If (destinationType Is GetType(String) AndAlso TypeOf value Is Map) Then
                Return CType(value, Map).Sum.ToString
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

        'Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext,
        '                          ByVal value As Object,
        '                          ByVal attributes() As Attribute) As PropertyDescriptorCollection
        '    Return MyBase.GetProperties(context, value, attributes)
        'End Function

    End Class

    Public Class MapUIEditor
        Inherits System.Drawing.Design.UITypeEditor

        Public Overrides Function GetPaintValueSupported(
                         ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Sub PaintValue(ByVal pe As Drawing.Design.PaintValueEventArgs)
            Dim b As Bitmap = New Bitmap(CType(pe.Value, Map).ColorImg)
            pe.Graphics.DrawImage(b, pe.Bounds)
            b.Dispose()
        End Sub

        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function

        Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext,
                                            ByVal provider As IServiceProvider,
                                            ByVal value As Object) As Object
            Return MyBase.EditValue(context, provider, value)
        End Function

    End Class

    Public Class ActStatesConverter
        Inherits System.ComponentModel.ExpandableObjectConverter

        Public Overloads Overrides Function CanConvertTo(
                              ByVal context As System.ComponentModel.ITypeDescriptorContext,
                              ByVal destinationType As Type) As Boolean
            If destinationType Is GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overloads Overrides Function ConvertTo(
                              ByVal context As System.ComponentModel.ITypeDescriptorContext,
                              ByVal culture As CultureInfo,
                              ByVal value As Object,
                              ByVal destinationType As Type) As Object
            If (destinationType Is GetType(String) AndAlso TypeOf value Is Creature.ActStates) Then
                Select Case CType(value, Creature.ActStates).Alive
                    Case True
                        Return "Alive"                 'Localization!!!!!!!!!!!!!!
                    Case False
                        Return "Dead"
                End Select
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

    End Class

    Public Class CreatureCollectionEditor
        Inherits System.ComponentModel.Design.CollectionEditor

        Public Sub New(type As Type)
            MyBase.New(type)
        End Sub

        ' Name shown the editor for individual creature
        Protected Overrides Function GetDisplayText(ByVal value As Object) As String
            Dim thisCreature As Creature = CType(value, Creature)
            Return String.Format("{0}: {1} s @ ({2}, {3})",
                thisCreature.Species, thisCreature.Age, CInt(thisCreature.Position.X), CInt(thisCreature.Position.Y))
        End Function

        Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
            Select Case itemType
                Case GetType(Creature)
                    ' Do Nothing => Mutator !!!!!!!!!!
                Case GetType(CreatureFromFile)
                    Dim OpenFileDiag As New OpenFileDialog
                    OpenFileDiag.Filter = "SimWorld Creature Files|*.smc"
                    'OpenFileDiag.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory()
                    If OpenFileDiag.ShowDialog() <> DialogResult.OK Then
                        Return Nothing
                    Else
                        '!!！！！！！！！！！ to be optimized!!!!!!!!!!!!!!!!!!!!!!!!!

                        'Check id duplication!!!!
                        Return New CreatureFromFile(OpenFileDiag.FileName).ToCreature()
                    End If
                Case Else
                    Throw New Exception("Unsupported type on creating an instance. ")
            End Select
            Return MyBase.CreateInstance(itemType)
        End Function

        Protected Overrides Function CreateNewItemTypes() As Type()
            Dim TypeList As List(Of Type) = New List(Of Type)
            TypeList.Add(GetType(Creature))
            TypeList.Add(GetType(CreatureFromFile))
            Return TypeList.ToArray() ' MyBase.CreateNewItemTypes()
        End Function

        Protected Overrides Function CreateCollectionForm() As CollectionForm
            Dim CreatureCollectionForm As CollectionForm = MyBase.CreateCollectionForm()
            Dim propG As PropertyGrid = CType(CreatureCollectionForm.Controls("overArchingTableLayoutPanel").Controls("propertyBrowser"), PropertyGrid)
            propG.HelpVisible = True
            'Dim propTab As PropertyGrid.
            'propG.PropertyTabs.AddTabType(GetType(CustomTab), PropertyTabScope.Global)
            'propG.PropertyTabs.Clear(PropertyTabScope.Document)
            'AddHandler Dashboard.PropertyGrid1.PropertyValueChanged, MainForm.CurrentDashboard.PropertyGrid1_PropertyValueChanged(Me, New PropertyValueChangedEventArgs(Nothing, Nothing))
            CreatureCollectionForm.HelpButton = False
            CreatureCollectionForm.MinimizeBox = True
            CreatureCollectionForm.Height = 1.5 * CreatureCollectionForm.Height
            Return CreatureCollectionForm
        End Function

    End Class

    'Public Class CustomTab
    '    Inherits Windows.Forms.Design.PropertyTab

    '    Public Overrides ReadOnly Property TabName As String
    '        Get
    '            Return "CustomTab"
    '        End Get
    '    End Property

    '    'If (_bitmap == null)
    '    '    {
    '    '        // 1. create a file named "CustomTab.bmp". It must be a 16x16, 8-bit bitmap. Transparency pixel Is magenta.
    '    '        // 2. place it in the project aside this .cs
    '    '        // 3. configure its build action to "Embedded Resource"
    '    '        _bitmap = New Bitmap(GetType(), GetType().Name + ".bmp");
    '    '    }
    '    Private _bitmap As Bitmap
    '    Public Overrides ReadOnly Property Bitmap As Bitmap
    '        Get
    '            Return MyBase.Bitmap
    '        End Get
    '    End Property

    '    Public Overrides Function GetProperties(context As ITypeDescriptorContext, component As Object, attributes() As Attribute) As PropertyDescriptorCollection
    '        Return New PropertyDescriptorCollection(Nothing)
    '        'Return MyBase.GetProperties(context, component, attributes)
    '    End Function

    '    Public Overrides Function GetProperties(component As Object, attributes() As Attribute) As PropertyDescriptorCollection
    '        Return GetProperties(Nothing, component, attributes)
    '    End Function

    'End Class

End Namespace
