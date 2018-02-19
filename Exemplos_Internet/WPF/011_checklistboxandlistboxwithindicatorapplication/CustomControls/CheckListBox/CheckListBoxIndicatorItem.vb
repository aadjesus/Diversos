Public Class CheckListBoxIndicatorItem

#Region " Private Declarations "

    Private _dblOffset As Double
    Private _bolIsSelected As Boolean = False
    Private _lbiRelatedListBoxItem As ListBoxItem

#End Region

#Region " Properties "

    Public Property Offset() As Double
        Get
            Return _dblOffset
        End Get
        Set(ByVal Value As Double)
            _dblOffset = Value
        End Set
    End Property

    Public Property IsSelected() As Boolean
        Get
            Return _bolIsSelected
        End Get
        Set(ByVal Value As Boolean)
            _bolIsSelected = Value
        End Set
    End Property

    Public Property RelatedListBoxItem() As ListBoxItem
        Get
            Return _lbiRelatedListBoxItem
        End Get
        Set(ByVal Value As ListBoxItem)
            _lbiRelatedListBoxItem = Value
        End Set
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal dblOffset As Double, ByVal bolIsSelected As Boolean, ByVal lbiRelatedListBoxItem As ListBoxItem)

        _dblOffset = dblOffset
        _bolIsSelected = bolIsSelected
        _lbiRelatedListBoxItem = lbiRelatedListBoxItem

    End Sub

#End Region

End Class

