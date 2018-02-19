using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Media;

namespace middleEast
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region Fileds

        public Storyboard StoryboardMouseEnter;
        public Storyboard StoryboardMouseLeave;
        public Storyboard StoryboardInfo1;
        public Storyboard StoryboardInfo2;

        string enterColorString = "#FFDCDCDC";
        string leaveColorString = "#FF727272";
        string trueAnswerColorString = "#FF09FD1A";
        string falseAnswerColorString = "#FFFF3A3A";

        XElement xmlFile;

        Random rand = new Random(DateTime.Now.Second.GetHashCode());

        string answer;

        #endregion

        public Window1()
        {
            try
            {
                this.InitializeComponent();

                // Insert code required on object creation below this point.

                StoryboardMouseEnter = TryFindResource("StoryboardMouseEnter") as Storyboard;
                StoryboardMouseLeave = (Storyboard)TryFindResource("StoryboardMouseLeave");

                StoryboardInfo1 = TryFindResource("StoryboardInfo1") as Storyboard;
                StoryboardInfo2 = TryFindResource("StoryboardInfo2") as Storyboard;

                StoryboardInfo1.Completed += new EventHandler(StoryboardInfo1_Completed);

                xmlFile = XElement.Load("XMLcountriesInfo.xml");

                changeQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
                this.Close();
            }
        }

        #region MouseEvents

        void country_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Path senderPath = (Path)sender;

                changeColorValue(senderPath.Name);
                //updateCountryInfo(senderPath.Name);

                StoryboardMouseEnter.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        void country_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                StoryboardMouseLeave.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        private void country_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Path senderPath = (Path)sender;
                BrushConverter bc = new BrushConverter();

                SoundPlayer chord_wav = new SoundPlayer("media\\chord.wav");
                SoundPlayer ding_wav = new SoundPlayer("media\\ding.wav");

                if (senderPath.Name == this.answer)
                {
                    ding_wav.Play();
                    senderPath.Fill = bc.ConvertFromString(trueAnswerColorString) as Brush;
                    Score.Text = (Int32.Parse(Score.Text) + 10).ToString();
                    changeQuestion();
                }
                else
                {
                    chord_wav.Play();
                    senderPath.Fill = bc.ConvertFromString(falseAnswerColorString) as Brush;
                    Score.Text = (Int32.Parse(Score.Text) - 10).ToString();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        #endregion

        void changeColorValue(string countryName)
        {
            try
            {
                /////////////////////////////////
                //changing color of target path
                Color newColor = (Color)ColorConverter.ConvertFromString(this.enterColorString);

                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = (from c in StoryboardMouseEnter.Children
                                                                             where (Storyboard.GetTargetName(c) == countryName)
                                                                             select c).First() as ColorAnimationUsingKeyFrames;
                colorAnimationUsingKeyFrames.KeyFrames[0].Value = newColor;

                //////////////////////////////////////////////            
                //changing color of others path except target
                Color grayColor = (Color)ColorConverter.ConvertFromString(this.leaveColorString);

                var othersColorAnimationUsingKeyFrames = from c in StoryboardMouseEnter.Children
                                                         where (Storyboard.GetTargetName(c) != countryName)
                                                         select c;

                foreach (ColorAnimationUsingKeyFrames tmp in othersColorAnimationUsingKeyFrames)
                    tmp.KeyFrames[0].Value = grayColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        void updateCountryInfo(string countryName)
        {
            try
            {
                var countryNode = from c in xmlFile.Descendants(countryName)
                                  select c;

                var Area = (from q in countryNode.Descendants("Area")
                            select q).First().Value;

                var Population = (from q in countryNode.Descendants("Population")
                                  select q).First().Value;

                var Capital = (from q in countryNode.Descendants("Capital")
                               select q).First().Value;

                var OfficialLanguage = (from q in countryNode.Descendants("OfficialLanguage")
                                        select q).First().Value;

                var flagName = (from q in countryNode.Descendants("flagName")
                                select q).First().Value;

                //////////////////////////////////////////////
                this.NameOfCountry.Text = countryName;
                this.Flag.Source = new BitmapImage(new Uri("Flags\\" + flagName, UriKind.Relative));
                this.area.Text = Area + " KM2";
                this.population.Text = Population;
                this.capital.Text = Capital;
                this.languages.Text = OfficialLanguage;
            }
            catch { }
        }

        void changeQuestion()
        {
            try
            {
                string[] countries = new string[] { "Iran", "Turkey", "Iraq", "Kuwait", "Bahrain", "Oman", "Qatar", "SaudiArabia", "UAE", "Yemen", "Israel", "Jordan", "Lebanon", "Syria", "Egypt", "Palestine" };
                
                this.answer = countries[rand.Next(0, countries.Count() - 1)];

                StoryboardInfo1.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        void StoryboardInfo1_Completed(object sender, EventArgs e)
        {
            try
            {
                updateCountryInfo(this.answer);
                StoryboardInfo2.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hyperlink source = sender as Hyperlink;
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }
    }
}