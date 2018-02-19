using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;

// This class is part of OrangeNote™ for Windows (currently under development), and is provided openly and free of charge
// by The Little Software Company. If you appreciate this code, please consider purchasing a copy of OrangeNote to support my efforts!

namespace OrangeNote.Animation {
	[ContentProperty( "Frames" )]
	public class FrameBasedAnimation: ParallelTimeline {

		// We cannot use automatic variables here because it interferes with the ContentProperty attribute
		protected FrameCollection _frames;
		public FrameCollection Frames {
			get {
				if ( _frames == null )
					_frames = new FrameCollection();
				return _frames;
			}
		}

		public bool LoopAnimation { get; set; }

		public FrameBasedAnimation() {
			LoopAnimation = true; // default value
		}

		/// <summary>
		/// This method should be called immediately after InitializeComponent() in most cases, or whenever you modify the animation parameters.
		/// </summary>
		public void Render() {
			Dictionary<ObjectPropertyPair, AnimationTimeline> index = new Dictionary<ObjectPropertyPair, AnimationTimeline>();

			Frames.OrderBy<Frame, KeyTime>( delegate( Frame target ) {
				return target.KeyTime;
			} );

			// The trick here is to turn time-dominant form into property-dominant form.
			foreach ( Frame frame in Frames ) {
				foreach ( Setter setter in frame ) {

					ObjectPropertyPair pair = new ObjectPropertyPair( setter.TargetName, setter.Property );

					AnimationTimeline animation;
					if ( !index.ContainsKey( pair ) ) {
						animation = CreateAnimationFromType( setter.Property.PropertyType );
						Storyboard.SetTargetName( animation, setter.TargetName );
						Storyboard.SetTargetProperty( animation, new PropertyPath( setter.Property ) );
						animation.Duration = this.Duration;

						index.Add( pair, animation );
					}
					animation = index[ pair ];

					( (IKeyFrameAnimation)animation ).KeyFrames.Add( CreateKeyFrameFromType( setter.Property.PropertyType, setter.Value, frame.KeyTime ) );
				}
			}

			foreach ( AnimationTimeline animation in index.Values ) {
				if ( LoopAnimation ) {
					// Finally, tie each animation closed by projecting its initial frame into the future so that interpolations will be smooth.
					IKeyFrame firstFrame = (IKeyFrame)( (IKeyFrameAnimation)animation ).KeyFrames[ 0 ]; // Assume there will always be at least one frame.
					( (IKeyFrameAnimation)animation ).KeyFrames.Add( CreateKeyFrameFromType( firstFrame.Value.GetType(), firstFrame.Value.ToString(), KeyTime.FromTimeSpan( firstFrame.KeyTime.TimeSpan + this.Duration.TimeSpan ) ) );
				}

				this.Children.Add( animation );
			}
		}

		/// <summary>
		/// Extend this method to provide animation class instances for additional types.
		/// </summary>
		/// <param name="type">The type of the property being animated.</param>
		/// <returns>An instance of the appropriate concrete animation class (must be the UsingKeyFrames variant).</returns>
		private AnimationTimeline CreateAnimationFromType( Type type ) {
			switch ( type.ToString() ) {
				case "System.Double":
					return new DoubleAnimationUsingKeyFrames();

				// *** Add support for additional types here and below. ***

				default:
					throw new ArgumentException( String.Format( "The property type \"{0}\" is not currently supported by this implementation. You will need to modify the source code to support this type (don't worry, it's easy). For more information, examine the source code or see the CodeProject article.", type.ToString() ), "type" );
			}
		}

		/// <summary>
		/// Extend this method to instantiate key frames for additional animation types.
		/// </summary>
		/// <param name="type">The type of the property being animated.</param>
		/// <param name="value">The value of the key frame.</param>
		/// <param name="keyTime">The key time of the key frame.</param>
		/// <returns>An instance of the appropriate concrete key frame class.</returns>
		private IKeyFrame CreateKeyFrameFromType( Type type, object value, KeyTime keyTime ) {
			switch ( type.ToString() ) {
				case "System.Double":
					return new LinearDoubleKeyFrame( Double.Parse( (string)value ), keyTime );

				// *** Add support for additional types here and above. ***

				default:
					throw new ArgumentException( String.Format( "The property type \"{0}\" is not currently supported by this implementation. You will need to modify the source code to support this type (don't worry, it's easy). For more information, examine the source code or see the CodeProject article.", type.ToString() ), "type" );
			}
		}
	}

	public class Frame: ObservableCollection<Setter> {
		public KeyTime KeyTime { get; set; }
	}

	public class FrameCollection: ObservableCollection<Frame> { }

	internal class ObjectPropertyPair {
		public string TargetName { get; set; }
		public DependencyProperty TargetProperty { get; set; }

		public ObjectPropertyPair( string targetName, DependencyProperty targetProperty ) {
			this.TargetName = targetName;
			this.TargetProperty = targetProperty;
		}

		public override bool Equals( object obj ) {
			ObjectPropertyPair next = obj as ObjectPropertyPair;

			if ( next == null )
				return false;

			return this.TargetName == next.TargetName && this.TargetProperty == next.TargetProperty;
		}

		public override int GetHashCode() {
			return TargetName.GetHashCode() ^ TargetProperty.GetHashCode();
		}
	}
}
