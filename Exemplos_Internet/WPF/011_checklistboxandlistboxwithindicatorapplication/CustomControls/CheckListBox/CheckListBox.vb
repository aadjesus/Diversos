'Special thanks to Josh Smith for his great teachings.  Without his below article, this control would have never been authored.
'Please read Josh's posting at: http://joshsmithonwpf.wordpress.com/2007/10/13/an-article-about-the-wpf-thought-process/
'
Imports System.ComponentModel

<TemplatePart(Name:="PART_IndicatorList", Type:=GetType(ItemsControl))> _
Public Class CheckListBox
    Inherits System.Windows.Controls.ContentControl

#Region " Private Declarations "

    Private _objIndicatorList As ItemsControl
    Private _objIndicatorOffsets As System.Collections.ObjectModel.ObservableCollection(Of CheckListBoxIndicatorItem)
    Private _objListBox As ListBox

#End Region

#Region " Public Declarations "

    Public Shared ReadOnly CheckBrushProperty As DependencyProperty = DependencyProperty.Register("CheckBrush", GetType(Brush), GetType(CheckListBox), New PropertyMetadata(New SolidColorBrush(System.Windows.Media.Colors.Black)))
    Public Shared ReadOnly CheckBrushStrokeThicknessProperty As DependencyProperty = DependencyProperty.Register("CheckBrushStrokeThickness", GetType(Double), GetType(CheckListBox), New PropertyMetadata(2.0))
    Public Shared ReadOnly CheckHeightWidthProperty As DependencyProperty = DependencyProperty.Register("CheckHeightWidth", GetType(Double), GetType(CheckListBox), New PropertyMetadata(13.0))

#End Region

#Region " Properties "

    <Description("Brush used to paint the check inside the checkbox.  Defaults to black."), Category("Custom")> _
    Public Property CheckBrush() As Brush
        Get
            Return CType(GetValue(CheckBrushProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(CheckBrushProperty, value)
        End Set
    End Property

    <Description("Stroke thickness for the check inside the checkbox.  Defaults to 2."), Category("Custom")> _
    Public Property CheckBrushStrokeThickness() As Double
        Get
            Return CType(GetValue(CheckBrushStrokeThicknessProperty), Double)
        End Get
        Set(ByVal value As Double)
            SetValue(CheckBrushStrokeThicknessProperty, value)
        End Set
    End Property

    <Description("Size of CheckBox.  CheckBox is rendered in a square so this value is the height and width of the CheckBox.  Default value is 13."), Category("Custom")> _
    Public Property CheckHeightWidth() As Double
        Get
            Return CType(GetValue(CheckHeightWidthProperty), Double)
        End Get
        Set(ByVal value As Double)
            SetValue(CheckHeightWidthProperty, value)
        End Set
    End Property
#End Region

#Region " Methods "

    Shared Sub New()
        'This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
        'This style is defined in themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(GetType(CheckListBox), New FrameworkPropertyMetadata(GetType(CheckListBox)))
    End Sub

    Public Sub New()
        EventManager.RegisterClassHandler(GetType(CheckBox), CheckBox.ClickEvent, New RoutedEventHandler(AddressOf CheckBox_Clicked))
    End Sub

    Private Sub CheckBox_Clicked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim cb As CheckBox = TryCast(e.OriginalSource, CheckBox)
        If cb Is Nothing Then
            Exit Sub
        End If

        Dim objCheckListBoxIndicatorItem As CheckListBoxIndicatorItem = TryCast(cb.DataContext, CheckListBoxIndicatorItem)
        If objCheckListBoxIndicatorItem Is Nothing Then
            Exit Sub
        End If

        objCheckListBoxIndicatorItem.RelatedListBoxItem.IsSelected = CType(cb.IsChecked, Boolean)
    End Sub

    Protected Overrides Sub OnContentChanged(ByVal oldContent As Object, ByVal newContent As Object)
        MyBase.OnContentChanged(oldContent, newContent)

        'this is our insurance policy that the developer does not add content that is not a ListBox

        If newContent Is Nothing OrElse TypeOf newContent Is ListBox Then
            'this ensures that our reference to the child ListBox is always corect or nothing.
            'if the child ListBox is removed, our reference is set to Nothing
            'if the child ListBox is swapped out, our reference is set to the newContent
            _objListBox = TryCast(Me.Content, ListBox)

            'this removes our references to the ListBox items
            If _objIndicatorOffsets IsNot Nothing AndAlso _objIndicatorOffsets.Count > 0 Then
                _objIndicatorOffsets.Clear()
            End If

            Exit Sub

        Else
            Throw New System.NotSupportedException("Invalid content.  CheckListBox only accepts a ListBox control as its content.")

        End If

    End Sub

    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        'when the template is applied, this give the developer the oppurtunity to get references to name objects in the control template.
        'in our case, we need a reference to the ItemsControl that holds the indicator arrows.
        '
        'what your control does in the absence of an expected object in the control template is up to the control develeper.
        'in my case here, without the items control, we are dead in the water.
        '
        'remember that custom controls are supposed to be Lookless.  Meaning the visual and code are highly decoupled.  
        'Any designer using Blend fully expects to be able edit the control template anyway they want.
        'My using the "PART_" naming convention, you indicate that this object is probably necessary for the conrol to work, but this is not true in all cases.
        '
        _objIndicatorList = TryCast(GetTemplateChild("PART_IndicatorList"), ItemsControl)
        If _objIndicatorList Is Nothing Then
            Throw New Exception("Hey!  The PART_IndicatorList is missing from the template or is not an ItemsControl.  Sorry but this ItemsControl is required.")
        End If

    End Sub

    Private Sub ListBoxSelectedItemIndicator_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        If _objIndicatorList Is Nothing Then
            'remember how much "fun" tabs were be in VB and Access?  Well...
            '
            'this is here because when you place a custom control in a tab, it loads the control once before it runs OnApplyTemplate
            'when the TabItem its in gets clicked (focus), OnApplyTemplate runs then Loaded runs agin.
            Exit Sub
        End If

        _objIndicatorOffsets = New System.Collections.ObjectModel.ObservableCollection(Of CheckListBoxIndicatorItem)
        _objIndicatorList.ItemsSource = _objIndicatorOffsets

        'How cool are routed events!  We can listen into the child ListBoxes activities and act accordingly.
        Me.AddHandler(ListBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf ListBox_SelectionChanged))
        Me.AddHandler(ScrollViewer.ScrollChangedEvent, New ScrollChangedEventHandler(AddressOf ListBox_ScrollViewer_ScrollChanged))

        UpdateIndicators()

    End Sub

    Private Sub UpdateIndicators()

        'This is the awesome procedure that Josh Smith authored with a few modifications

        If _objIndicatorOffsets Is Nothing Then
            Exit Sub
        End If

        If _objListBox Is Nothing Then
            Exit Sub
        End If

        If _objIndicatorOffsets.Count > 0 Then
            _objIndicatorOffsets.Clear()
        End If

        Dim objGen As ItemContainerGenerator = _objListBox.ItemContainerGenerator
        If objGen.Status <> Primitives.GeneratorStatus.ContainersGenerated Then
            Exit Sub
        End If

        For Each objSelectedItem As Object In _objListBox.Items

            Dim lbi As ListBoxItem = TryCast(objGen.ContainerFromItem(objSelectedItem), ListBoxItem)
            If lbi Is Nothing Then
                Continue For
            End If

            Dim objTransform As GeneralTransform = lbi.TransformToAncestor(_objListBox)
            Dim pt As Point = objTransform.Transform(New Point(0, 0))

            Dim dblOffset As Double = pt.Y + (lbi.ActualHeight / 2) - (CheckHeightWidth / 2)
            _objIndicatorOffsets.Add(New CheckListBoxIndicatorItem(dblOffset, lbi.IsSelected, lbi))

        Next

    End Sub

    Private Sub ListBox_ScrollViewer_ScrollChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.ScrollChangedEventArgs)
        'if the user is scrolling horizontality, no reason to run any of our attached behavior code
        If e.VerticalChange = 0 Then
            Exit Sub
        End If

        UpdateIndicators()
    End Sub

    Private Sub ListBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)
        UpdateIndicators()
    End Sub

    Private Sub ListBoxSelectedItemIndicator_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        Me.RemoveHandler(ListBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf ListBox_SelectionChanged))
        Me.RemoveHandler(ScrollViewer.ScrollChangedEvent, New ScrollChangedEventHandler(AddressOf ListBox_ScrollViewer_ScrollChanged))
    End Sub

#End Region

End Class

