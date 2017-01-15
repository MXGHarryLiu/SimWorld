''' <summary>
''' SimWorld Error Message Class
''' </summary>
Public Class ErrMsg

    Public Enum ErrType As Byte
        UNKNOWN = &B0
        NOTNEGATIVE = &B1
        MUSTPOSITIVE = &B10
        PROBABILITY = &B11
        UPPERBOUNDED = &B100
        LOWERBOUNDED = &B101
    End Enum

    Public Shared Sub ShowErrMsg(ByVal Msg As String)
        MsgBox(Msg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SimWorld")
    End Sub

    Public Shared Sub ShowErrMsg(ByVal PropertyName As String, ByVal ErrType As ErrType,
                                 Optional ByVal RelatedPropertyName As String = "")
        Select Case ErrType
            Case ErrType.MUSTPOSITIVE
                ShowErrMsg(PropertyName & " must be positive. ")
            Case ErrType.NOTNEGATIVE
                ShowErrMsg(PropertyName & " must be non-negative. ")
            Case ErrType.PROBABILITY
                ShowErrMsg(PropertyName & " is a probability, therefore is from 0 to 1. ")
            Case ErrType.UPPERBOUNDED
                ShowErrMsg(String.Format("{0} is bounded by the maximum {1}. ", PropertyName, RelatedPropertyName))
            Case ErrType.LOWERBOUNDED
                ShowErrMsg(String.Format("{0} is bounded by the minimum {1}. ", PropertyName, RelatedPropertyName))
            Case Else
                ShowErrMsg(PropertyName & "'s value is invalid. ")
        End Select
    End Sub

End Class
