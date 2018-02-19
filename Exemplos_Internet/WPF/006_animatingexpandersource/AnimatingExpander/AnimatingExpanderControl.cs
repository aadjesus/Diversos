using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using AnimatingExpander;

namespace WhiteBoxSecurity {
    /// <summary>
    /// This control holds content and can expand/collapse. The control is composed of
    /// its content, a checkbox for expanding/collapsing the content and a
    /// "gridsplitter" which can resize the height/width of the content while it is 
    /// expanded. The content is initially expanded, to the InitialExpandedLength DP. 
    /// The control "remembers" the last "size" value and will expand to that value.
    /// To use this element define it and set it's content. 
    /// </summary>
    // Created: 16/03/2009
    // Author: Guy Shtub
    // FileName: AnimatingExpanderControl.cs
    [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Thumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_CheckBox", Type = typeof(CheckBox))]
    public partial class AnimatingExpanderControl : ContentControl {

        #region members

        private double _expandedValue;
        private FrameworkElement _actualContent;
        private CheckBox _checkBox;
        private Thumb _thumb;

        //constants
        private const double _defaultLength = 100.0;
        private const int _animationDuration = 200;

        #endregion

        #region Dependency Properties

        /// <summary>
        /// This DP can be set by the user to control the initial value (width or height) 
        /// that the control will expand to. 
        /// </summary>
        public double InitialExpandedLength{
            get {
                return (double)GetValue(InitialExpandedLengthProperty);
            }
            set {
                SetValue(InitialExpandedLengthProperty, value);
            }
        }
        public static readonly DependencyProperty InitialExpandedLengthProperty =
            DependencyProperty.Register(
                "InitialExpandedLength", typeof(double),
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata(_defaultLength)
        );


        /// <summary>
        /// Determines the direction to expand to. Values can be Up, Down, Left and Right.
        /// Default is up.
        /// </summary>
        public ExpandDirection ExpandDirection {
            get {
                return (ExpandDirection)GetValue(ExpandDirectionProperty);
            }
            set {
                SetValue(ExpandDirectionProperty, value);
            }
        }
        public static readonly DependencyProperty ExpandDirectionProperty =
            DependencyProperty.Register(
                "ExpandDirection", typeof(ExpandDirection),
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata(ExpandDirection.Up)
        );

        /// <summary>
        /// Flag which enables or disables the expand and collapse animation on the 
        /// control
        /// </summary>
        public bool AnimationEnabled {
            get {
                return (bool)GetValue(AnimationEnabledProperty);
            }
            set {
                SetValue(AnimationEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty AnimationEnabledProperty =
            DependencyProperty.Register(
                "AnimationEnabled", typeof(bool),
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata(true)
            );

        /// <summary>
        /// Flag which shows if the control is collapsed or not. When collapsed the 
        /// gridsplitter is disabled. If not set by user this DP is defaulted to true.
        /// </summary>
        public bool IsCollapsed {
            get {
                return (bool)GetValue(IsCollapsedProperty);
            }
            set {
                SetValue(IsCollapsedProperty, value);
            }
        }

        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.Register(
                "IsCollapsed", typeof(bool),
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata(true)
            );

        /// <summary>
        /// This property sets the title of the control which is displayed next to the 
        /// expand/collapse checkbox.
        /// </summary>
        public string Title {
            get {
                return (string)GetValue(TitleProperty);
            }
            set {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string),
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata("")
            );

        #endregion

        #region Properties

        /// <summary>
        /// The height/width value of the control, when expanded. 
        /// It is initially set to the InitialExpandedLength and then reset to the value
        /// which is set with the "gridsplitter".
        /// </summary>
        public double ExpandedValue {
            get {
                return _expandedValue;
            }
            private set {
                _expandedValue = value;
            }
        }

        /// <summary>
        /// a reference to the content the user set
        /// </summary>
        public FrameworkElement ActualContent {
            get {
                return _actualContent;
            }
            set {
                _actualContent = value;
            }
        }

        /// <summary>
        /// a reference to the CheckBox_PART
        /// </summary>
        public CheckBox CheckBoxPART{
            get {
                return _checkBox;
            }
            private set {
                _checkBox = value;
            }
        }

        /// <summary>
        /// a reference to the Thumb part ("GridSplitter")
        /// </summary>
        public Thumb ThumbPART {
            get {
                return _thumb;
            }
            private set {
                _thumb = value;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default CTOR. Initializes the control, and add an initialized event handler.
        /// </summary>
        public AnimatingExpanderControl() {
            Initialized += new EventHandler(AnimatingExpanderControl_Initialized);
            InitializeComponent();
        }

        /// <summary>
        /// Allow for setting the style.
        /// </summary>
        static AnimatingExpanderControl() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AnimatingExpanderControl),
                new FrameworkPropertyMetadata(typeof(AnimatingExpanderControl))
            );
        }
        #endregion

        #region methods

        /// <summary>
        /// Verify that the template is applied to the control.
        /// </summary>
        void AnimatingExpanderControl_Initialized(object sender, EventArgs e) {
            this.ApplyTemplate();
        }

        /// <summary>
        /// Occurs when the template is applied. Sets the AcutalContent and ExpandedValue
        /// properties. 
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            ControlTemplate ct = this.Template;
            ContentPresenter contentPresenter = 
                            Template.FindName("PART_Content", this) as ContentPresenter;
            if (null != contentPresenter && null != contentPresenter.Content) {
                this.ActualContent = contentPresenter.Content as FrameworkElement;

                //determine the DP that needs to be changed
                DependencyProperty actualContentLengthDP;
                this.DetermineLengthDPToChange(out actualContentLengthDP);

                this.ExpandedValue = this.InitialExpandedLength;

                Thumb thumb = Template.FindName("PART_Thumb", this) as Thumb;
                if (null != thumb) {
                    this.ThumbPART = thumb;
                }

                CheckBox cb = Template.FindName("PART_CheckBox", this) as CheckBox;
                if (null != cb) {
                    this.CheckBoxPART = cb;
                    //if started as expanded, expand.
                    if (!this.IsCollapsed) {
                        cb.IsChecked = !cb.IsChecked;
                    }
                    else {
                        //set Width/Height to zero, i.e. collapse.
                        this.ActualContent.SetValue(actualContentLengthDP,0.0);
                    }
                }
            }
        }

        /// <summary>
        /// Event handler, called when the user presses the "button" to expand the
        /// control.
        /// </summary>
        private void BottomCB_Checked(object sender, RoutedEventArgs e) {
            //determine the DP that needs to be changed
            DependencyProperty actualContentLengthDP;
            this.DetermineLengthDPToChange(out actualContentLengthDP);
            
            //change the determined DP with animation
            if (AnimationEnabled) {
                this.ExpandWithAnimation(actualContentLengthDP);
            }
            //change the determined DP without animation
            else {
                this.ActualContent.SetValue(actualContentLengthDP, this.ExpandedValue);
            }
            this.IsCollapsed = false;
        }
        /// <summary>
        /// Event handler, called when the user presses the "button" to collapse the
        /// control.
        /// </summary>
        private void BottomCB_UnChecked(object sender, RoutedEventArgs e) {
            //determine the DP that needs to be changed
            DependencyProperty actualContentLengthDP;
            this.DetermineLengthDPToChange(out actualContentLengthDP);

            //change the determined DP with animation
            if (AnimationEnabled) {
                this.CollapseWithAnimation(actualContentLengthDP);
            }
            //change the determined DP with out animation
            else {
                this.ActualContent.SetValue(actualContentLengthDP, 0.0);
            }
            this.IsCollapsed = true;
        }

        /// <summary>
        /// Event handler for "GridSplitter" drag event. 
        /// This event can occur only if control is not collapsed. This is enforced by
        /// binding the IsEnabled Property of the control to the IsChecked DP of the
        /// CheckBox.
        /// </summary>
        private void GridSplitter_DragDelta(object sender, DragDeltaEventArgs e) {
            
            if (null != this.ActualContent) {
                //set this.ExpandedValue to new dragged value, then resize the content.
                //if new value is smaller then zero, or bigger the Content's Max Length,
                //reset length accordingly
                switch (this.ExpandDirection) {
                    case ExpandDirection.Down:
                        this.ExpandedValue = this.ActualContent.Height + e.VerticalChange;
                        this.CheckExpandedValue(this.ActualContent.MaxHeight);
                        this.ActualContent.Height = this.ExpandedValue; 
                        break;
                    case ExpandDirection.Up:
                        this.ExpandedValue = this.ActualContent.Height - e.VerticalChange;
                        this.CheckExpandedValue(this.ActualContent.MaxHeight);
                        this.ActualContent.Height = this.ExpandedValue; 
                        break;
                    case ExpandDirection.Left:
                        this.ExpandedValue = this.ActualContent.Width - e.HorizontalChange;
                        this.CheckExpandedValue(this.ActualContent.MaxWidth);
                        this.ActualContent.Width = this.ExpandedValue;
                        break;
                    case ExpandDirection.Right:
                        this.ExpandedValue = this.ActualContent.Width + e.HorizontalChange;
                        this.CheckExpandedValue(this.ActualContent.MaxWidth);
                        this.ActualContent.Width = this.ExpandedValue;
                        break;
                }
            }
        }

        /// <summary>
        /// Helper method for verifying that the ExpandedValue that was set with the 
        /// "GridSplitter" is legal. If it is lower then zero, or higher then the 
        /// Max Width/Height, it is reset.
        /// </summary>
        /// <param name="MaxLength">
        /// The control's content MaxWidth or MaxHeight.
        /// </param>
        private void CheckExpandedValue(double MaxLength) {
            if (this.ExpandedValue < 0) {
                this.ExpandedValue = 0;
            }
            if (this.ExpandedValue > MaxLength) {
                this.ExpandedValue = MaxLength;
            }
        }

        /// <summary>
        /// Expands the control, invoked when a user checks the checkbox.
        /// </summary>
        /// <param name="actualContentLengthDP">
        /// The Height or Width DP.
        /// </param>
        private void ExpandWithAnimation(
            DependencyProperty actualContentLengthDP
        ) {
            Storyboard sb = new Storyboard();
            DoubleAnimation expandAnimation = new DoubleAnimation();
            expandAnimation.From = 0;
            //Content is collapsed. Expand to last saved this.ExpandedValue.
            expandAnimation.To = this.ExpandedValue;
            expandAnimation.Duration = new TimeSpan(0, 0, 0, 0, _animationDuration);
            sb.Children.Add(expandAnimation);
            sb.FillBehavior = FillBehavior.Stop;
            this.BeginChangeSizeAnimation(
                sb, 
                expandAnimation,
                actualContentLengthDP
            );    
        }

        /// <summary>
        /// Collapses the control, invoked when a user unchecks the checkbox.
        /// </summary>
        /// <param name="actualContentLengthDP">
        /// The Height or Width DP.
        /// </param>
        private void CollapseWithAnimation(
            DependencyProperty actualContentLengthDP
        ) {
            Storyboard sb = new Storyboard();
            DoubleAnimation collapseAnimation = new DoubleAnimation();
            //Here the content is expanded, the ExpandedValue is the height/width of the 
            //content
            collapseAnimation.From = this.ExpandedValue;
            collapseAnimation.To = 0;
            collapseAnimation.Duration = new TimeSpan(0, 0, 0, 0, _animationDuration);
            sb.Children.Add(collapseAnimation);
            sb.FillBehavior = FillBehavior.Stop;
            this.BeginChangeSizeAnimation(
                sb, 
                collapseAnimation,
                actualContentLengthDP
            );
        }
        
        /// <summary>
        /// Execute the expand/collapse animation according to the ExpandDirection
        /// </summary>
        /// <param name="sb">
        /// The storyboard that is the "parent" of the given animation.
        /// </param>
        /// <param name="lengthAnimation">
        /// The Animation used to expand/collapse the content.
        /// </param>
        /// <param name="actualContentLengthDP">
        /// The Height or Width DP.
        /// </param>
        private void BeginChangeSizeAnimation(
            Storyboard sb, 
            DoubleAnimation lengthAnimation,
            DependencyProperty actualContentLengthDP
        ) {
            //execute Animation
            Storyboard.SetTargetProperty(lengthAnimation, new PropertyPath(actualContentLengthDP.Name));
            if (null != this.ActualContent) {
                sb.Completed += delegate {
                    this.ActualContent.SetValue(actualContentLengthDP, lengthAnimation.To.Value);
                };
                sb.Begin(this.ActualContent);
            }
        }

        /// <summary>
        /// Helper method for determining the DP to change (width or height), according
        /// to the ExpandDirection DP.
        /// </summary>
        /// <param name="actualContentLengthDP">
        /// The Dependency property that needs to be changed: width / height. 
        /// </param>
        private void DetermineLengthDPToChange(
            out DependencyProperty actualContentLengthDP
        ) {
            //variable that determine the properties for the animation according to 
            //ExpandDirection DP
            actualContentLengthDP = FrameworkElement.HeightProperty;

            //set variables according to ExpandDirection
            if (this.ExpandDirection.Equals(ExpandDirection.Up) ||
                this.ExpandDirection.Equals(ExpandDirection.Down)) {
                actualContentLengthDP =  FrameworkElement.HeightProperty;
            }
            else if (this.ExpandDirection.Equals(ExpandDirection.Left) ||
                this.ExpandDirection.Equals(ExpandDirection.Right)) {
                actualContentLengthDP = FrameworkElement.WidthProperty;
            }
        }
        #endregion
    }
}
