'Special thanks to Josh Smith for his great teachings.  Without his below article, this control would have never been authored.
'Please read Josh's posting at: http://joshsmithonwpf.wordpress.com/2007/10/13/an-article-about-the-wpf-thought-process/
'
Imports System.ComponentModel

<TemplatePart(Name:="PART_IndicatorList", Type:=GetType(ItemsControl))> _
Public Class ListBoxWithSelectedItemIndicator
    Inherits System.Windows.Controls.ContentControl

#Region " Private Declarations "

    Private _objIndicatorList As ItemsControl
    Private _objIndicatorOffsets As System.Collections.ObjectModel.ObservableCollection(Of Double)
    Private _objListBox As ListBox

#End Region

#Region " Public Declarations "

    Public Shared ReadOnly IndicatorBrushProperty As DependencyProperty = DependencyProperty.Register("IndicatorBrush", GetType(Brush), GetType(ListBoxWithSelectedItemIndicator), New PropertyMetadata(New LinearGradientBrush(Colors.LightBlue, Colors.Blue, New Point(0.5, 0), New Point(0.5, 1))))
    Public Shared ReadOnly IndicatorHeightWidthProperty As DependencyProperty = DependencyProperty.Register("IndicatorHeightWidth", GetType(Double), GetType(ListBoxWithSelectedItemIndicator), New PropertyMetadata(16.0))

#End Region

#Region " Properties "

    <Description("Brush used to paint the indicator.  Defaults to a nice blue gradient brush"), Category("Custom")> _
    Public Property IndicatorBrush() As Brush
        Get
            Return CType(GetValue(IndicatorBrushProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(IndicatorBrushProperty, value)
        End Set
    End Property

    <Description("Size of indictor.  Indicator is rendered in a square so this value is the height and width of the indicator.  Default value is 16."), Category("Custom")> _
    Public Property IndicatorHeightWidth() As Double
        Get
            Return CType(GetValue(IndicatorHeightWidthProperty), Double)
        End Get
        Set(ByVal value As Double)
            SetValue(IndicatorHeightWidthProperty, value)
        End Set
    End Property

#End Region

#Region " Methods "

    Shared Sub New()
        'This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
        'This style is defined in themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(GetType(ListBoxWithSelectedItemIndicator), New FrameworkPropertyMetadata(GetType(ListBoxWithSelectedItemIndicator)))
    End Sub

    Public Sub New()

    End Sub

    Protected Overrides Sub OnContentChanged(ByVal oldContent As Object, ByVal newContent As Object)
        MyBase.OnContentChanged(oldContent, newContent)

        'this is our insurance policy that the developer does not add content that is not a ListBox

        If newContent Is Nothing OrElse TypeOf newContent Is ListBox Then
            'this ensures that our reference to the child ListBox is always corect or nothing.
            'if the child ListBox is removed, our reference is set to Nothing
            'if the child ListBox is swapped out, our reference is set to the newContent
            _objListBox = TryCast(newContent, ListBox)

            'this removes our references to the ListBox items
            If _objIndicatorOffsets IsNot Nothing AndAlso _objIndicatorOffsets.Count > 0 Then
                _objIndicatorOffsets.Clear()
            End If

            Exit Sub

        Else
            Throw New System.NotSupportedException("Invalid content.  ListBoxWithSelectedItemIndicator only accepts a ListBox control as its content.")

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

    Private Sub ListBoxWithSelectedItemIndicator_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        If _objIndicatorList Is Nothing Then
            'remember how much "fun" tabs were be in VB and Access?  Well...
            '
            'this is here because when you place a custom control in a tab, it loads the control once before it runs OnApplyTemplate
            'when the TabItem its in gets clicked (focus), OnApplyTemplate runs then Loaded runs agin.
            '
            'since OnApplyTemplate has not run yet, we are out of here
            Exit Sub
        End If

        _objIndicatorOffsets = New System.Collections.ObjectModel.ObservableCollection(Of Double)
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

        If _objIndicatorOffsets IsNot Nothing AndAlso _objIndicatorOffsets.Count > 0 Then
            _objIndicatorOffsets.Clear()
        End If

        If _objListBox.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim objGen As ItemContainerGenerator = _objListBox.ItemContainerGenerator
        If objGen.Status <> Primitives.GeneratorStatus.ContainersGenerated Then
            Exit Sub
        End If

        For Each objSelectedItem As Object In _objListBox.SelectedItems

            Dim lbi As ListBoxItem = TryCast(objGen.ContainerFromItem(objSelectedItem), ListBoxItem)
            If lbi Is Nothing Then
                Continue For
            End If

            Dim objTransform As GeneralTransform = lbi.TransformToAncestor(_objListBox)
            Dim pt As Point = objTransform.Transform(New Point(0, 0))

            Dim dblOffset As Double = pt.Y + (lbi.ActualHeight / 2) - (Me.IndicatorHeightWidth / 2)
            _objIndicatorOffsets.Add(dblOffset)

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

    Private Sub ListBoxWithSelectedItemIndicator_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        Me.RemoveHandler(ListBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf ListBox_SelectionChanged))
        Me.RemoveHandler(ScrollViewer.ScrollChangedEvent, New ScrollChangedEventHandler(AddressOf ListBox_ScrollViewer_ScrollChanged))
    End Sub

#End Region

End Class
