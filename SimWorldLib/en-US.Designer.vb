﻿'------------------------------------------------------------------------------
' <auto-generated>
'     此代码由工具生成。
'     运行时版本:4.0.30319.42000
'
'     对此文件的更改可能会导致不正确的行为，并且如果
'     重新生成代码，这些更改将会丢失。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System


'此类是由 StronglyTypedResourceBuilder
'类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
'若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
'(以 /str 作为命令选项)，或重新生成 VS 项目。
'''<summary>
'''  一个强类型的资源类，用于查找本地化的字符串等。
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
 Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
 Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
Friend Class en_US
    
    Private Shared resourceMan As Global.System.Resources.ResourceManager
    
    Private Shared resourceCulture As Global.System.Globalization.CultureInfo
    
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
    Friend Sub New()
        MyBase.New
    End Sub
    
    '''<summary>
    '''  返回此类使用的缓存的 ResourceManager 实例。
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
        Get
            If Object.ReferenceEquals(resourceMan, Nothing) Then
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("SimWorldLib.en-US", GetType(en_US).Assembly)
                resourceMan = temp
            End If
            Return resourceMan
        End Get
    End Property
    
    '''<summary>
    '''  使用此强类型资源类，为所有资源查找
    '''  重写当前线程的 CurrentUICulture 属性。
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
    '''  查找类似 A 2nd-level object that tracks the states of the Creature.   的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property ActState() As String
        Get
            Return ResourceManager.GetString("ActState", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The age of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Age() As String
        Get
            Return ResourceManager.GetString("Age", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is alive.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Alive() As String
        Get
            Return ResourceManager.GetString("Alive", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Base level energy expenditure per second of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property BaseEExpend() As String
        Get
            Return ResourceManager.GetString("BaseEExpend", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Energy that the Creature&apos;s offsprings have at birth.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property BornEnergy() As String
        Get
            Return ResourceManager.GetString("BornEnergy", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The mass of the Creature&apos;s offsprings at birth.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property BornWeight() As String
        Get
            Return ResourceManager.GetString("BornWeight", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Date and time the World file is created.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property CreationTime() As String
        Get
            Return ResourceManager.GetString("CreationTime", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The population of the creatures in simulation in the World.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property CreatureCount() As String
        Get
            Return ResourceManager.GetString("CreatureCount", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 List of the Creatures within the World.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property CreatureList() As String
        Get
            Return ResourceManager.GetString("CreatureList", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The length of the daytime in second.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property DayLen() As String
        Get
            Return ResourceManager.GetString("DayLen", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The length of the whole day in second.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property DayTotal() As String
        Get
            Return ResourceManager.GetString("DayTotal", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Simulation time resolution/step in second.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property DT() As String
        Get
            Return ResourceManager.GetString("DT", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is eating.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Eating() As String
        Get
            Return ResourceManager.GetString("Eating", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Energy 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Energy() As String
        Get
            Return ResourceManager.GetString("Energy", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Environment 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Environment() As String
        Get
            Return ResourceManager.GetString("Environment", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 ？？？？？？？？？？？？ 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property EtoWRate() As String
        Get
            Return ResourceManager.GetString("EtoWRate", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Graphics 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Graphics() As String
        Get
            Return ResourceManager.GetString("Graphics", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Unique GUID of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property ID() As String
        Get
            Return ResourceManager.GetString("ID", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Identity 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Identity() As String
        Get
            Return ResourceManager.GetString("Identity", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Energy the Creature possesses.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Joule() As String
        Get
            Return ResourceManager.GetString("Joule", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Life 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Life() As String
        Get
            Return ResourceManager.GetString("Life", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The number of offsprings the Creature has for each mating.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property LitterSize() As String
        Get
            Return ResourceManager.GetString("LitterSize", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The probability that the offsprings of this Creature are male.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MaleRatio() As String
        Get
            Return ResourceManager.GetString("MaleRatio", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is shown highlighted.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Marked() As String
        Get
            Return ResourceManager.GetString("Marked", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Creature Reproduction 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MarkerSize() As String
        Get
            Return ResourceManager.GetString("MarkerSize", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is ready to mate and give birth.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MateReady() As String
        Get
            Return ResourceManager.GetString("MateReady", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Maximum energy stored by the Creature, i.e. its energy capacity.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MaxEnergyStorage() As String
        Get
            Return ResourceManager.GetString("MaxEnergyStorage", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The minimum age for the Creature to be able to mate.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MinMateAge() As String
        Get
            Return ResourceManager.GetString("MinMateAge", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is moving.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Move() As String
        Get
            Return ResourceManager.GetString("Move", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The 3D velocity vector of the Creature at the given instance.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property MovingVector() As String
        Get
            Return ResourceManager.GetString("MovingVector", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The length of the night in second.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property NightLen() As String
        Get
            Return ResourceManager.GetString("NightLen", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Rate of photosynthesis in energy per second per brightness received.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property PhotoSynRate() As String
        Get
            Return ResourceManager.GetString("PhotoSynRate", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is photosynthesizing.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Photosynthesize() As String
        Get
            Return ResourceManager.GetString("Photosynthesize", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The 3D position of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Position() As String
        Get
            Return ResourceManager.GetString("Position", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is pregnant.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Pregnant() As String
        Get
            Return ResourceManager.GetString("Pregnant", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The refresh rate of the Dashboard information and the Stage image in multiples of DT. In order to speed up the simulation running time, we recommend to set the value greater than 10.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property RefreshRate() As String
        Get
            Return ResourceManager.GetString("RefreshRate", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Reproduction 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Reproduction() As String
        Get
            Return ResourceManager.GetString("Reproduction", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The path of the Creature file.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property SettingPath() As String
        Get
            Return ResourceManager.GetString("SettingPath", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Sex of the Creature [Male is True]. For asexual organisms, this property is ignored.   的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Sex() As String
        Get
            Return ResourceManager.GetString("Sex", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Simulation 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Simulation() As String
        Get
            Return ResourceManager.GetString("Simulation", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The version of the SimWorld that created the World file.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property SimWorldVer() As String
        Get
            Return ResourceManager.GetString("SimWorldVer", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The size of the World (width x height x depth). [Positive double x Positive double x Non-negative double]. If the depth is zero, the simulation is 2-dimensional.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Size() As String
        Get
            Return ResourceManager.GetString("Size", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Whether the Creature is asleep.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Sleep() As String
        Get
            Return ResourceManager.GetString("Sleep", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The species number of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Species() As String
        Get
            Return ResourceManager.GetString("Species", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 State 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property State() As String
        Get
            Return ResourceManager.GetString("State", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The accumulated time since simulation started in second. 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property T() As String
        Get
            Return ResourceManager.GetString("T", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Environment temperature of the World in Celsius.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Temperature() As String
        Get
            Return ResourceManager.GetString("Temperature", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The length of each transition between day and night in second.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Twilight() As String
        Get
            Return ResourceManager.GetString("Twilight", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Utility 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Utility() As String
        Get
            Return ResourceManager.GetString("Utility", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The mass of the Creature.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property Weight() As String
        Get
            Return ResourceManager.GetString("Weight", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 The file name of the World file currently loaded. 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property WorldFile() As String
        Get
            Return ResourceManager.GetString("WorldFile", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 Folder directory that contains the World file and all its creature files. 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property WorldFileDir() As String
        Get
            Return ResourceManager.GetString("WorldFileDir", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 World File version.  的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property WorldVersion() As String
        Get
            Return ResourceManager.GetString("WorldVersion", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  查找类似 ？？？？？？？？？？？？ 的本地化字符串。
    '''</summary>
    Friend Shared ReadOnly Property WtoERate() As String
        Get
            Return ResourceManager.GetString("WtoERate", resourceCulture)
        End Get
    End Property
End Class