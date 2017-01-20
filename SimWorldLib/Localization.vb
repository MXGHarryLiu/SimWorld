Imports System.ComponentModel
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

    Public Class StatesConverter
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
            If (destinationType Is GetType(String) AndAlso TypeOf value Is Creature.States) Then
                Return CType(value, Creature.States).ToString
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

        'Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext,
        '                                        ByVal sourceType As Type) As Boolean
        '    If sourceType Is GetType(String) Then
        '        Return True
        '    End If
        '    Return MyBase.CanConvertFrom(context, sourceType)
        'End Function

        'Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext,
        '                                      ByVal culture As CultureInfo,
        '                                      ByVal value As Object) As Object
        '    If TypeOf value Is String Then
        '        Return New Creature.States(CBool(value))
        '    End If
        '    Return MyBase.ConvertFrom(context, culture, value)
        'End Function

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

    Public Class StructureConverter(Of T As Structure)
        Inherits System.ComponentModel.ExpandableObjectConverter

        Public Overrides Function GetCreateInstanceSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Function CreateInstance(ByVal context As ITypeDescriptorContext,
                                                 ByVal propertyValues As IDictionary) As Object
            Dim ret As T = context.PropertyDescriptor.GetValue(context.Instance)
            Dim NewT As ValueType = ret
            For Each Entry As DictionaryEntry In propertyValues
                Dim propinfo As Reflection.PropertyInfo = ret.GetType().GetProperty(Entry.Key.ToString())
                If propinfo IsNot Nothing And propinfo.CanWrite = True Then
                    propinfo.SetValue(NewT, Convert.ChangeType(Entry.Value, propinfo.PropertyType))
                End If
            Next
            Return CType(NewT, T)
        End Function

    End Class

    Public Class GenomeConverter
        Inherits ExpandableObjectConverter

        Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object,
                                                ByVal attributes() As Attribute) As PropertyDescriptorCollection
            Dim CurrentGenome As List(Of Gene) = TryCast(value, List(Of Gene))
            Dim props(CurrentGenome.Count - 1) As PropertyDescriptor
            For i As Integer = 0 To CurrentGenome.Count - 1 Step 1
                Dim AttributeList() As Attribute = {
                    New DescriptionAttribute(CurrentGenome(i).ToString()),
                    New TypeConverterAttribute(GetType(GeneConverter))
                }
                props(i) = New GenomePropertyDescriptor(CurrentGenome(i).ToString(), AttributeList, i)
            Next i
            Return New PropertyDescriptorCollection(props)
        End Function

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
            If (destinationType Is GetType(String) AndAlso TypeOf value Is List(Of Gene)) Then
                Return String.Format("{0} Gene(s)", TryCast(value, List(Of Gene)).Count)
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

    End Class

    Public Class GenomeUIEditor
        Inherits System.Drawing.Design.UITypeEditor

        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.None
        End Function

    End Class

    Public Class GeneConverter
        Inherits ExpandableObjectConverter

        Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object,
                                                ByVal attributes() As Attribute) As PropertyDescriptorCollection
            Dim CurrentGene As Gene = TryCast(value, Gene)
            Dim AttributeList() As Attribute
            Dim PropertyDescriptorList As PropertyDescriptorCollection = New PropertyDescriptorCollection(Nothing)
            AttributeList = {New System.ComponentModel.DefaultValueAttribute(True)} 'Not Working!!!
            PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Perceptible), AttributeList))
            Select Case CurrentGene.Model
                Case Gene.MathModels.CONSTANT
                    AttributeList = {New ReadOnlyAttribute(True)}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Minimum), AttributeList))
                    AttributeList = {New ReadOnlyAttribute(True)}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Maximum), AttributeList))
                    AttributeList = {New DescriptionAttribute(CurrentGene.Phenotype)}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(CurrentGene.Phenotype, AttributeList, 0))
                Case Gene.MathModels.UNIFORM
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Minimum), Nothing))
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Maximum), Nothing))
                Case Gene.MathModels.BINARY
                    AttributeList = {New ReadOnlyAttribute(True)}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Minimum), AttributeList))
                    AttributeList = {New ReadOnlyAttribute(True)}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Maximum), AttributeList))
                    AttributeList = {New DescriptionAttribute("p")}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor("p", AttributeList, 0))
                Case Gene.MathModels.NORMAL
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Minimum), Nothing))
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Maximum), Nothing))
                    AttributeList = {New DescriptionAttribute("Mu")}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor("Mu", AttributeList, 0))
                    AttributeList = {New DescriptionAttribute("Sigma")}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor("Sigma", AttributeList, 1))
                Case Gene.MathModels.EXPONENTIAL
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Minimum), Nothing))
                    PropertyDescriptorList.Add(New GenePropertyDescriptor(NameOf(Gene.Maximum), Nothing))
                    AttributeList = {New DescriptionAttribute("Lambda")}
                    PropertyDescriptorList.Add(New GenePropertyDescriptor("Lambda", AttributeList, 0))
                Case Else
                    ' Do nothing
            End Select
            Return PropertyDescriptorList
        End Function

        Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext,
                                                 ByVal sourceType As Type) As Boolean
            If sourceType Is GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext,
                                              ByVal culture As CultureInfo,
                                              ByVal value As Object) As Object
            If TypeOf value Is String Then
                Dim NewMathModel As Gene.MathModels
                [Enum].TryParse(value, NewMathModel)
                Return NewMathModel
            End If
            Return MyBase.ConvertFrom(context, culture, value)
        End Function

        Public Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim StandardValues As StandardValuesCollection = New StandardValuesCollection(System.Enum.GetNames(GetType(Gene.MathModels)))
            Return StandardValues
        End Function

        Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext,
                                               ByVal destinationType As Type) As Boolean
            If destinationType Is GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overloads Overrides Function ConvertTo(
                              ByVal context As ITypeDescriptorContext,
                              ByVal culture As CultureInfo,
                              ByVal value As Object,
                              ByVal destinationType As Type) As Object
            If destinationType Is GetType(String) AndAlso TypeOf value Is Gene Then
                Return TryCast(value, Gene).Model.ToString
            End If
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

    End Class

    Public Class GenomePropertyDescriptor
        Inherits PropertyDescriptor

        Private _Idx As Integer = 0

        Public Sub New(ByVal name As String, ByVal attrs As Attribute(), ByVal Idx As Integer)
            MyBase.New(name, attrs)
            Me._Idx = Idx
        End Sub

        Public Overrides ReadOnly Property ComponentType As Type
            Get
                Return GetType(List(Of Gene))
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType As Type
            Get
                Return GetType(Gene)
            End Get
        End Property

        Public Overrides Sub ResetValue(component As Object)
            TryCast(component, List(Of Gene))(_Idx).Model = Gene.MathModels.CONSTANT
        End Sub

        Public Overrides Sub SetValue(component As Object, value As Object)
            If TypeOf value IsNot String Then
                TryCast(component, List(Of Gene))(_Idx).Model = CType(value, Gene.MathModels)
            Else
                [Enum].TryParse(value, TryCast(component, List(Of Gene))(_Idx).Model)
            End If
        End Sub

        Public Overrides Function CanResetValue(component As Object) As Boolean
            Return True
        End Function

        Public Overrides Function GetValue(component As Object) As Object
            Return TryCast(component, List(Of Gene))(_Idx)
        End Function

        Public Overrides Function ShouldSerializeValue(component As Object) As Boolean
            Return False
        End Function

    End Class

    Public Class GenePropertyDescriptor
        Inherits PropertyDescriptor

        Private _Idx As Integer = -1

        Public Sub New(ByVal name As String, ByVal attrs() As Attribute, Optional ByVal Idx As Integer = -1)
            MyBase.New(name, attrs)
            Dim CurrentProp As Reflection.PropertyInfo = GetType(Gene).GetProperty(name)
            If CurrentProp IsNot Nothing Then
                Dim AttributeList As Attribute() = Me.AttributeArray
                For Each SubAttribute As Attribute In CurrentProp.GetCustomAttributes(False)
                    ReDim Preserve AttributeList(AttributeList.Count)
                    AttributeList(UBound(AttributeList)) = SubAttribute
                Next
                Me.AttributeArray = AttributeList
            End If
            Me._Idx = Idx
        End Sub

        Public Overrides ReadOnly Property ComponentType As Type
            Get
                Return GetType(Gene)
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                For Each SubAttribute As Attribute In Me.AttributeArray
                    If TypeOf SubAttribute Is ReadOnlyAttribute Then
                        Return TryCast(SubAttribute, ReadOnlyAttribute).IsReadOnly
                    End If
                Next
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType As Type
            Get
                Select Case Me.Name
                    Case NameOf(Gene.Perceptible)
                        Return GetType(Boolean)
                    Case Else
                        Return GetType(Double)
                End Select
            End Get
        End Property

        Public Overrides Sub ResetValue(component As Object)

        End Sub

        Public Overrides Sub SetValue(component As Object, value As Object)
            Dim CurrentGene As Gene = TryCast(component, Gene)
            If _Idx = -1 Then
                Select Case Me.Name
                    Case NameOf(Gene.Maximum)
                        CurrentGene.Maximum = value
                    Case NameOf(Gene.Minimum)
                        CurrentGene.Minimum = value
                    Case NameOf(Gene.Perceptible)
                        CurrentGene.Perceptible = value
                    Case Else

                End Select
            Else
                CurrentGene.ModelParameters(_Idx) = value
            End If
        End Sub

        Public Overrides Function CanResetValue(component As Object) As Boolean
            Return False
        End Function

        Public Overrides Function GetValue(component As Object) As Object
            Dim CurrentGene As Gene = TryCast(component, Gene)
            If _Idx = -1 Then
                Select Case Me.Name
                    Case NameOf(Gene.Maximum)
                        Return CurrentGene.Maximum
                    Case NameOf(Gene.Minimum)
                        Return CurrentGene.Minimum
                    Case NameOf(Gene.Perceptible)
                        Return CurrentGene.Perceptible
                    Case Else
                        Return -1
                End Select
            Else
                Return CurrentGene.ModelParameters(_Idx)
            End If
        End Function

        Public Overrides Function ShouldSerializeValue(component As Object) As Boolean
            Return False
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property)>
    Public Class GeneticApplicableAttribute
        Inherits Attribute

        <System.ComponentModel.DefaultValueAttribute(False)>
        Public Property Applicable As Boolean = False

        Public Sub New(ByVal Applicable As Boolean)
            Me.Applicable = Applicable
        End Sub

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
