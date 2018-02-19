
namespace WpfScrollContent
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class ScrollAnimation
    {
        #region Members
        private int _nFrameInterval = 50;
        private double _dbFrom = 0;
        private double _dbTo = 0;
        private TimeSpan _duration = TimeSpan.FromSeconds(5);
        private double _dbIncrement = 0;
        private Thread _workerThread = null;
        private bool _bStopThread = false;
        private bool _bIsRunning = false;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ScrollAnimation()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns true if the animation thread is running
        /// </summary>
        public bool IsRunning
        {
            get { return this._bIsRunning; }
        }

        /// <summary>
        /// Set/get the starting value of this animation
        /// </summary>
        public double From
        {
            set
            {
                if (this._bIsRunning == false)
                    this._dbFrom = value;
            }
            get { return this._dbFrom; }
        }

        /// <summary>
        /// Set/get the ending value of this animation
        /// </summary>
        public double To
        {
            set
            {
                if (this._bIsRunning == false)
                    this._dbTo = value;
            }
            get { return this._dbTo; }
        }

        /// <summary>
        /// Set/get the duration value of this animation
        /// </summary>
        public TimeSpan Duration
        {
            set
            {
                // Set maximum duration to be five minutes, this can easily be changed
                // just didn't want to get some ridiculously high duration
                if (this._bIsRunning == false && value < TimeSpan.FromMinutes(5))
                    this._duration = value;
            }
            get { return this._duration; }
        }

        /// <summary>
        /// Set/get the frame interval value of this animation, must be in milliseconds
        /// </summary>
        public int FrameInterval
        {
            set
            {
                if (this._bIsRunning == false)
                    this._nFrameInterval = value;
            }
            get { return this._nFrameInterval; }
        }
        #endregion

        #region Start Method
        /// <summary>
        /// Start method first makes certain that the thread isn't currently running.
        /// Then it calculates values, sets all initial values, and starts animation thread. 
        /// </summary>
        public void Start()
        {
            // Only allow call if the thread isn't currently running
            if (this._bIsRunning == true)
                return;

            // Calc values
            double dbDelta = this._dbTo - this._dbFrom;

            // Calculate how many steps will be in this animation based on the 
            // total duration divided by the frame interval (default 50 milliseconds)
            int nFrames = Convert.ToInt32(this._duration.TotalMilliseconds / this._nFrameInterval);

            // Calculate and set the increment value for the animation based on the 
            // total delta divided by the number of frames
            // This is a linear calculation.  An improvement would be to utilize
            // an acceleration value to make animation more realistic.
            this._dbIncrement = dbDelta / nFrames;

            // Start thread
            this._workerThread = new Thread(new ThreadStart(Worker));
            this._workerThread.Start();
        }
        #endregion

        #region Stop Method
        /// <summary>
        /// Stop can be used to manually stop animation while it is running.
        /// This method does not always have to be called because the thread will
        /// automatically stop when the duration has been exceeded.
        /// </summary>
        public void Stop()
        {
            this._bStopThread = true;
        }
        #endregion

        #region Worker Method
        /// <summary>
        /// The worker method is executed by the thread and is used to generated
        /// the calculated values of the animation.  When a new value is calculated, it
        /// uses the UpdateAnimation callback to notify the UI that the value has been
        /// updated.
        /// </summary>
        public void Worker()
        {
            this._bIsRunning = true;
            try
            {
                double dbCurrentValue = this._dbFrom;
                DateTime startTime = DateTime.Now;
                while (this._bStopThread == false)
                {
                    // Check elapsed time
                    TimeSpan elapsed = DateTime.Now - startTime;
                    if (elapsed > this._duration)
                    {
                        // if user is not trying to stop thread
                        if (this._bStopThread == false)
                        {
                            // Guarantee that the final value is the To value.
                            OnUpdateAnimation(this, new AnimationEventArgs(this._dbTo));
                        }

                        break;
                    }
                    else
                    {
                        // The first frame should have the initial From value.
                        // Subsequent frames will have the incremented value.
                        OnUpdateAnimation(this, new AnimationEventArgs(dbCurrentValue));
                    }

                    // update current value
                    dbCurrentValue += this._dbIncrement;

                    Thread.Sleep(this._nFrameInterval);
                }
            }
            catch (Exception ex)
            {
                // Ignore exception for now
            }
            finally
            {
                this._bIsRunning = false;
            }
        }
        #endregion

        #region UpdateAnimation EventHandler and Method
        /// <summary>
        /// UpdateAnimation event used for callback
        /// </summary>
        public event UpdateAnimationHandler UpdateAnimation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSender"></param>
        /// <param name="animArgs"></param>
        public void OnUpdateAnimation(Object objSender, AnimationEventArgs animArgs)
        {
            if (this.UpdateAnimation != null)
            {
                this.UpdateAnimation(objSender, animArgs);
            }
        }
        #endregion
    } // end of class

    /// <summary>
    /// Custom delegate for the animation callback
    /// </summary>
    /// <param name="objSender"></param>
    /// <param name="animationArgs"></param>
    public delegate void UpdateAnimationHandler(Object objSender, AnimationEventArgs animationArgs);
} // end of namespace