﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System


'This class was auto-generated by the StronglyTypedResourceBuilder
'class via a tool like ResGen or Visual Studio.
'To add or remove a member, edit your .ResX file then rerun ResGen
'with the /str option, or rebuild your VS project.
'''<summary>
'''  A strongly-typed resource class, for looking up localized strings, etc.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"),  _
 Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
 Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
Friend Class zh_CN
    
    Private Shared resourceMan As Global.System.Resources.ResourceManager
    
    Private Shared resourceCulture As Global.System.Globalization.CultureInfo
    
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
    Friend Sub New()
        MyBase.New
    End Sub
    
    '''<summary>
    '''  Returns the cached ResourceManager instance used by this class.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
        Get
            If Object.ReferenceEquals(resourceMan, Nothing) Then
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("SimWorldLib.zh-CN", GetType(zh_CN).Assembly)
                resourceMan = temp
            End If
            Return resourceMan
        End Get
    End Property
    
    '''<summary>
    '''  Overrides the current thread's CurrentUICulture property for all
    '''  resource lookups using this strongly typed resource class.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set
            resourceCulture = value
        End Set
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 能量.
    '''</summary>
    Friend Shared ReadOnly Property Energy() As String
        Get
            Return ResourceManager.GetString("Energy", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 环境.
    '''</summary>
    Friend Shared ReadOnly Property Environment() As String
        Get
            Return ResourceManager.GetString("Environment", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 显示.
    '''</summary>
    Friend Shared ReadOnly Property Graphics() As String
        Get
            Return ResourceManager.GetString("Graphics", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 属性.
    '''</summary>
    Friend Shared ReadOnly Property Identity() As String
        Get
            Return ResourceManager.GetString("Identity", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 生物.
    '''</summary>
    Friend Shared ReadOnly Property Life() As String
        Get
            Return ResourceManager.GetString("Life", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 繁衍.
    '''</summary>
    Friend Shared ReadOnly Property Reproduction() As String
        Get
            Return ResourceManager.GetString("Reproduction", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 模拟.
    '''</summary>
    Friend Shared ReadOnly Property Simulation() As String
        Get
            Return ResourceManager.GetString("Simulation", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 状态.
    '''</summary>
    Friend Shared ReadOnly Property State() As String
        Get
            Return ResourceManager.GetString("State", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to 其他.
    '''</summary>
    Friend Shared ReadOnly Property Utility() As String
        Get
            Return ResourceManager.GetString("Utility", resourceCulture)
        End Get
    End Property
End Class
