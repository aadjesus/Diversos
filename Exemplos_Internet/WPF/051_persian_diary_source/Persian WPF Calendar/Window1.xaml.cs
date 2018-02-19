using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using System.Data.SqlClient;

namespace Persian_WPF_Calendar
{
    public partial class Window1 : Window
    {
        #region fields

        PersianCalendar persianCalendar = new PersianCalendar();
        HijriCalendar hijriCalendar = new HijriCalendar();

        //اطلاعات تاریخ امروز 
        readonly int currentYear = 1387;
        readonly int currentMonth = 10;
        readonly int currentDay = 1;

        //برای حرکت بین ماه ها
        //به شمسی
        int yearForNavigating = 1387;
        int monthForNavigating = 10;

        //اطلاعات روزی که کاربر روی آن کلیک کرده
        //Miladi
        int yearMiladi = 2009;
        int monthMiladi = 01;
        int dayMiladi = 01;

        //shamsi
        int yearShamsi = 1387;
        int monthShamsi = 01;
        int dayShamsi = 01;

        //ghamari
        int yearHijri = 1387;
        int monthHijri = 01;
        int dayHijri = 01;
        //\\

        //SqlConnection connection;
        DataClasses1DataContext db = new DataClasses1DataContext();

        #endregion

        public Window1()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point
            this.currentYear = persianCalendar.GetYear(DateTime.Now);
            this.currentMonth = persianCalendar.GetMonth(DateTime.Now);
            this.currentDay = persianCalendar.GetDayOfMonth(DateTime.Now);

            hijriCalendar.HijriAdjustment = -2;
            TextBoxHijriAdjustment.Text = hijriCalendar.HijriAdjustment.ToString();

            //string connString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True";
            //connection = new SqlConnection(connString);

            calculateMonth(currentYear, currentMonth);
        }

        #region SQL Queries

        /// <summary>
        /// Searchs the Database shamsi table
        /// </summary>
        /// <returns>If the input datas find in the table return true else return false</returns>
        bool searchShamsiTable(int year, int month, int day)
        {
            bool result = false;
            try
            {
                int? id = null;
                id = (from q in db.shamsis
                      where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                      select q.id).First();
                if (id != null) result = true;
            }
            catch { result = false; }
            return result;
        }

        /// <summary>
        /// Searchs the Database miladi table
        /// </summary>
        /// <returns>If the input datas find in the table return true else return false</returns>
        bool searchMiladiTable(int year, int month, int day)
        {
            bool result = false;
            try
            {
                int? id = (from q in db.miladis
                           where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                           select q.id).First();
                if (id != null) result = true;
            }
            catch { result = false; }
            return result;
        }

        /// <summary>
        /// Searchs the Database hijri table
        /// </summary>
        /// <returns>If the input datas find in the table return true else return false</returns>
        bool searchHijriTable(int year, int month, int day)
        {
            bool result = false;
            try
            {
                int? id = (from q in db.hijris
                           where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                           select q.id).First();
                if (id != null) result = true;
            }
            catch { result = false; }
            return result;
        }

        /// <summary>
        /// Serachs `whichCalendar` table for finding, whether the input date is a holiday or not
        /// </summary>
        /// <param name="whichCalendar">Type of calendar => "Hijri", "Miladi", "Shamsi"</param>
        /// <returns>If the date was a holiday return true else return false</returns>
        bool isHoliday(int year, int month, int day, string whichCalendar)
        {
            bool isHoliday = false;
            try
            {
                if (whichCalendar == "Hijri")
                {
                    isHoliday = (from q in db.hijris
                                 where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                                 select q.holiday).First().Value;
                }
                else if (whichCalendar == "Shamsi")
                {
                    isHoliday = (from q in db.shamsis
                                 where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                                 select q.holiday).First().Value;
                }
                else if (whichCalendar == "Miladi")
                {
                    isHoliday = (from q in db.miladis
                                 where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                                 select q.holiday).First().Value;
                }
            }
            catch { isHoliday = false; }
            return isHoliday;
        }

        /// <summary>
        /// Serachs `whichCalendar` table for finding, whether the input date is an anniersary or not
        /// </summary>
        /// <param name="whichCalendar">Type of calendar => "Hijri", "Miladi", "Shamsi"</param>
        /// <returns>If the date was an anniersary return true else return false</returns>
        bool isAnniersary(int year, int month, int day, string whichCalendar)
        {
            bool IsAnniversary = false;
            try
            {
                if (whichCalendar == "Hijri")
                {
                    IsAnniversary = (from q in db.hijris
                                     where ((q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day))
                                     select q.anniversary).First().Value;
                }
                else if (whichCalendar == "Shamsi")
                {
                    IsAnniversary = (from q in db.shamsis
                                     where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                                     select q.anniversary).First().Value;
                }
                else if (whichCalendar == "Miladi")
                {
                    IsAnniversary = (from q in db.miladis
                                     where (q.year == year && q.day == day && q.month == month) || (q.anniversary == true && q.month == month && q.day == day)
                                     select q.anniversary).First().Value;
                }
            }
            catch { IsAnniversary = false; }
            return IsAnniversary;
        }

        /// <summary>
        /// Gets the content of `text` field from `whichCalendar` table
        /// </summary>
        /// <param name="whichCalendar">Type of calendar => "Hijri", "Miladi", "Shamsi"</param>
        /// <returns>content of `text` field</returns>
        string GetTextOfMemo(int year, int month, int day, string whichCalendar)
        {
            try
            {
                if (whichCalendar == "Hijri")
                {
                    return (from q in db.hijris.AsEnumerable()
                            where (q.year == year && q.month == month && q.day == day) || (q.anniversary == true && q.month == month && q.day == day)
                            select q.text).First().Trim();
                }
                else if (whichCalendar == "Miladi")
                {
                    return (from q in db.miladis.AsEnumerable()
                            where (q.year == year && q.month == month && q.day == day) || (q.anniversary == true && q.month == month && q.day == day)
                            select q.text).First().Trim();
                }
                else if (whichCalendar == "Shamsi")
                {
                    return (from q in db.shamsis.AsEnumerable()
                            where (q.year == year && q.month == month && q.day == day) || (q.anniversary == true && q.month == month && q.day == day)
                            select q.text).First().Trim();
                }
                else return "";
            }
            catch { return ""; }
        }

        #endregion

        #region calculating and showing the calendar

        /// <summary>
        /// The main method to show the calendar
        /// This method shows `thisMonth` in `thisYear`
        /// </summary>
        void calculateMonth(int thisYear, int thisMonth)
        {
            try
            {
                yearForNavigating = thisYear;
                monthForNavigating = thisMonth;

                DateTime tempDateTime = persianCalendar.ToDateTime(yearForNavigating, monthForNavigating, 15, 01, 01, 01, 01);

                int thisDay = 1;
                TextBlockThisMonth.Text = "";
                TextBlockThisMonth.Text =
                    monthForNavigating.convertToPersianMonth() + " " +
                    yearForNavigating.convertToPersianNumber();

                //Different between first place of calendar and first place of this month
                //اختلاف بین خانه شروع ماه و اولین خانه تقویم            
                string DayOfWeek = persianCalendar.GetDayOfWeek(persianCalendar.ToDateTime(thisYear, thisMonth, 01, 01, 01, 01, 01)).ToString();
                int span = calculatePersianSpan(DayOfWeek.convertToPersianDay());

                decreasePersianDay(ref thisYear, ref thisMonth, ref thisDay, span);

                string persianDate;//حاوی تاریخ روزهای شمسی Contains the date of Persian
                string miladiDate;//حاوی تاریخ روزهای میلادی Contains the date of Christian
                string hijriDate;//حاوی تاریخ روزهای قمری Contains the date of Hijri

                string tooltip_context = "";//Contains the text of tooltip

                ////////////////////////////////////

                for (int i = 0; i < 6 * 7; i++)
                {
                    tempDateTime = persianCalendar.ToDateTime(thisYear, thisMonth, thisDay, 01, 01, 01, 01);

                    miladiDate = tempDateTime.Day.ToString() + " " + englishMonthName(tempDateTime.Month) + " " + tempDateTime.Year.ToString();
                    hijriDate = hijriCalendar.GetDayOfMonth(tempDateTime).convertToPersianNumber() + " " + hijriCalendar.GetMonth(tempDateTime).convertToHigriMonth() + " " + hijriCalendar.GetYear(tempDateTime).convertToPersianNumber();
                    persianDate = thisDay.convertToPersianNumber();

                    DayOfWeek = persianCalendar.GetDayOfWeek(tempDateTime).ToString();

                    if (thisMonth == monthForNavigating)
                    {
                        tooltip_context = "";
                        if (thisDay == currentDay && thisMonth == currentMonth && thisYear == currentYear)//بررسی تاریخ امروز Friday
                        {
                            tooltip_context = GetTextOfMemo(thisYear, thisMonth, thisDay, "Shamsi");
                            if (tooltip_context == "")
                                tooltip_context = GetTextOfMemo(tempDateTime.Year, tempDateTime.Month, tempDateTime.Day, "Miladi");
                            if (tooltip_context == "")
                                tooltip_context = GetTextOfMemo(hijriCalendar.GetYear(tempDateTime), hijriCalendar.GetMonth(tempDateTime), hijriCalendar.GetDayOfMonth(tempDateTime), "Hijri");

                            if (DayOfWeek.convertToPersianDay() == "جمعه")//بررسی جمعه بودن روز Friday
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyleForHolydays_CurrentDay", "TextBlockStyle3", "TextBlockStyle12", "TextBlockStyle8", tooltip_context);
                            else
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyleToday", "TextBlockStyle5", "TextBlockStyle10", "TextBlockStyle6", tooltip_context);
                        }
                        else if (searchShamsiTable(thisYear, thisMonth, thisDay))
                        {
                            tooltip_context = GetTextOfMemo(thisYear, thisMonth, thisDay, "Shamsi");
                            if (isHoliday(thisYear, thisMonth, thisDay, "Shamsi"))
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle5", "TextBlockStyle3", "TextBlockStyle12", "TextBlockStyle8", tooltip_context);
                            else
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle4", "TextBlockStyle1", "TextBlockStyle10", "TextBlockStyle6", tooltip_context);
                        }
                        else if (searchHijriTable(hijriCalendar.GetYear(tempDateTime), hijriCalendar.GetMonth(tempDateTime), hijriCalendar.GetDayOfMonth(tempDateTime)))
                        {
                            tooltip_context = GetTextOfMemo(hijriCalendar.GetYear(tempDateTime), hijriCalendar.GetMonth(tempDateTime), hijriCalendar.GetDayOfMonth(tempDateTime), "Hijri");
                            if (isHoliday(hijriCalendar.GetYear(tempDateTime), hijriCalendar.GetMonth(tempDateTime), hijriCalendar.GetDayOfMonth(tempDateTime), "Hijri"))
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle5", "TextBlockStyle3", "TextBlockStyle12", "TextBlockStyle8", tooltip_context);
                            else
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle4", "TextBlockStyle5", "TextBlockStyle10", "TextBlockStyle6", tooltip_context);
                        }
                        else if (searchMiladiTable(tempDateTime.Year, tempDateTime.Month, tempDateTime.Day))
                        {
                            tooltip_context = GetTextOfMemo(tempDateTime.Year, tempDateTime.Month, tempDateTime.Day, "Miladi");
                            if (isHoliday(tempDateTime.Year, tempDateTime.Month, tempDateTime.Day, "Miladi"))
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle5", "TextBlockStyle3", "TextBlockStyle12", "TextBlockStyle8", tooltip_context);
                            else
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle4", "TextBlockStyle5", "TextBlockStyle10", "TextBlockStyle6", tooltip_context);
                        }
                        else
                        {
                            if (DayOfWeek.convertToPersianDay() == "جمعه")//بررسی جمعه بودن روز Friday
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyleForHolydays", "TextBlockStyle3", "TextBlockStyle12", "TextBlockStyle8", tooltip_context);
                            else
                                changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle2", "TextBlockStyle5", "TextBlockStyle10", "TextBlockStyle6", tooltip_context);
                        }
                    }
                    else
                    {
                        if (DayOfWeek.convertToPersianDay() == "جمعه")//بررسی جمعه بودن روز Friday
                            changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyleForOtherHolydays", "TextBlockStyle4", "TextBlockStyle13", "TextBlockStyle9", tooltip_context);
                        else
                            changeProperties(i, persianDate, hijriDate, miladiDate, "RectangleStyle3", "TextBlockStyle2", "TextBlockStyle11", "TextBlockStyle7", tooltip_context);
                    }

                    increasePersianDay(ref thisYear, ref thisMonth, ref thisDay, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// اضافه کردن تعداد مشخصی ماه به ورودی
        /// Adds some months (`number`) to the input date
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="number">تعداد ماهی که باید به ورودی اضافه شود</param>
        void increasePersianMonth(ref int year, ref int month, int number)
        {
            month += number;
            if (month > 12)
            {
                month = 1;
                year++;
            }
        }

        /// <summary>
        /// کاهش تعداد مشخصی ماه از ورودی
        /// Decreases some months (`number`) from the input date
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="number">تعداد ماهی که باید از ورودی کم شود</param>
        void decreasePersianMonth(ref int year, ref int month, int number)
        {
            month -= number;
            if (month < 1)
            {
                month = 12;
                year--;
            }
        }

        /// <summary>
        /// اضافه کردن تعداد معلومی روز به ورودی
        /// Adds some days(`number`) to the input date
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">روز</param>
        /// <param name="number">تعداد روزی که باید به ورودی اضافه شود</param>
        void increasePersianDay(ref int year, ref int month, ref int day, int number)
        {
            int tempDay = day;
            tempDay += number;
            //شش ماه اول سال
            if (month <= 6 && tempDay > 31)
            {
                day = number;
                increasePersianMonth(ref year, ref month, 1);
            }
            //5 ماه دوم سال 
            else if (month > 6 && month < 12 && tempDay > 30)
            {
                day = number;
                increasePersianMonth(ref year, ref month, 1);
            }
            //اسفند در سال کبیسه
            else if (month == 12 && persianCalendar.IsLeapYear(year) && tempDay > 30)
            {
                day = number;
                increasePersianMonth(ref year, ref month, 1);
            }
            //اسفند در سال غیر کبیسه
            else if (month == 12 && !persianCalendar.IsLeapYear(year) && tempDay > 29)
            {
                day = number;
                increasePersianMonth(ref year, ref month, 1);
            }
            else
                day += number;
        }

        /// <summary>
        /// کم کردن تعداد معلومی روز از ورودی
        /// Decreases some days (`number`) from the input date
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">روز</param>
        /// <param name="number">تعداد روزی که باید از ورودی کم شود</param>
        void decreasePersianDay(ref int year, ref int month, ref int day, int number)
        {
            int tempDay = day;
            tempDay -= number;
            //شش ماه اول سال
            if (month == 1 && tempDay < 1)
            {
                if (persianCalendar.IsLeapYear(year - 1))
                    day = 30 - number + 1;//+1 رو باید اضافه کرد در غیر این صورت محاسبات اشتباه میشوند ، تجربی
                else
                    day = 29 - number + 1;
                decreasePersianMonth(ref year, ref month, 1);
            }
            else if (month <= 7 && month > 1 && tempDay < 1)
            {
                day = 31 - number + 1;
                month--;
            }
            //6 ماه دوم سال 
            else if (month > 7 && month <= 12 && tempDay < 1)
            {
                day = 30 - number + 1;
                decreasePersianMonth(ref year, ref month, 1);
            }
            else
                day -= number;

        }

        /// <summary>
        /// Converts a Persian weekday to equal of it in integer
        /// </summary>
        int calculatePersianSpan(string weekday)
        {
            switch (weekday)
            {
                case "شنبه":
                    return 0;

                case "یک شنبه":
                    return 1;

                case "دو شنبه":
                    return 2;

                case "سه شنبه":
                    return 3;

                case "چهار شنبه":
                    return 4;

                case "پنج شنبه":
                    return 5;

                case "جمعه":
                    return 6;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Converts the input month number to short equal of it in Christian Calendar
        /// </summary>
        string englishMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 01:
                    return "Jan";

                case 02:
                    return "Feb";

                case 03:
                    return "Mar";

                case 04:
                    return "Apr";

                case 05:
                    return "May";

                case 06:
                    return "Jun";

                case 07:
                    return "Jul";

                case 08:
                    return "Aug";

                case 09:
                    return "Sep";

                case 10:
                    return "Oct";

                case 11:
                    return "Nov";

                case 12:
                    return "Dec";

                default:
                    return "";
            }
        }

        /// <summary>
        /// Converts the input persian month name to equal of it in integer
        /// </summary>
        int numberOfMonth(string persianMonthName)
        {
            switch (persianMonthName)
            {
                case "فروردین":
                    return 1;

                case "اردیبهشت":
                    return 2;

                case "خرداد":
                    return 3;

                case "تیر":
                    return 4;

                case "مرداد":
                    return 5;

                case "شهریور":
                    return 6;

                case "مهر":
                    return 7;

                case "آبان":
                    return 8;

                case "آذر":
                    return 9;

                case "دی":
                    return 10;

                case "بهمن":
                    return 11;

                case "اسفند":
                    return 12;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Chnages the purpose Grid (`which`) properties with the input data
        /// </summary>
        /// <param name="which">Purpose Grid</param>
        /// <param name="persianDate">Text of Persian date</param>
        /// <param name="hijriDate">Text of Hijri date</param>
        /// <param name="miladiDate">Text of Christian date</param>
        /// <param name="rectangleResourceName">New name of rectangle resource</param>
        /// <param name="persianTextBlockResourceName">New name of Persian date resource</param>
        /// <param name="hijriTextBlockResourceName">New name of Hijri date resource</param>
        /// <param name="miladiTextBlockResourceName">New name of Christian date resource</param>
        /// <param name="tooltip_context">Text of tooltip</param>
        void changeProperties(int which, string persianDate, string hijriDate, string miladiDate, string rectangleResourceName, string persianTextBlockResourceName, string hijriTextBlockResourceName, string miladiTextBlockResourceName, string tooltip_context)
        {
            switch (which)
            {
                case 0:
                    TextBlockShanbe0.Text = persianDate;
                    TextBlockShanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe0Miladi.Text = miladiDate;
                    TextBlockShanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe0Hijri.Text = hijriDate;
                    TextBlockShanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe0.ToolTip = tooltip_context;
                    else GridShanbe0.ToolTip = null;
                    break;

                case 1:
                    TextBlock1Shanbe0.Text = persianDate;
                    TextBlock1Shanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe0Miladi.Text = miladiDate;
                    TextBlock1Shanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe0Hijri.Text = hijriDate;
                    TextBlock1Shanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe0.ToolTip = tooltip_context;
                    else Grid1Shanbe0.ToolTip = null;
                    break;

                case 2:
                    TextBlock2Shanbe0.Text = persianDate;
                    TextBlock2Shanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe0Miladi.Text = miladiDate;
                    TextBlock2Shanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe0Hijri.Text = hijriDate;
                    TextBlock2Shanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe0.ToolTip = tooltip_context;
                    else Grid2Shanbe0.ToolTip = null;
                    break;

                case 3:
                    TextBlock3Shanbe0.Text = persianDate;
                    TextBlock3Shanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe0Miladi.Text = miladiDate;
                    TextBlock3Shanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe0Hijri.Text = hijriDate;
                    TextBlock3Shanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe0.ToolTip = tooltip_context;
                    else Grid3Shanbe0.ToolTip = null;
                    break;

                case 4:
                    TextBlock4Shanbe0.Text = persianDate;
                    TextBlock4Shanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe0Miladi.Text = miladiDate;
                    TextBlock4Shanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe0Hijri.Text = hijriDate;
                    TextBlock4Shanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe0.ToolTip = tooltip_context;
                    else Grid4Shanbe0.ToolTip = null;
                    break;

                case 5:
                    TextBlock5Shanbe0.Text = persianDate;
                    TextBlock5Shanbe0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe0Miladi.Text = miladiDate;
                    TextBlock5Shanbe0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe0Hijri.Text = hijriDate;
                    TextBlock5Shanbe0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe0.ToolTip = tooltip_context;
                    else Grid5Shanbe0.ToolTip = null;
                    break;

                case 6:
                    TextBlockJome0.Text = persianDate;
                    TextBlockJome0.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome0Miladi.Text = miladiDate;
                    TextBlockJome0Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome0Hijri.Text = hijriDate;
                    TextBlockJome0Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome0.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome0.ToolTip = tooltip_context;
                    else GridJome0.ToolTip = null;
                    break;

                ///////////////////////////////////////////

                case 7:
                    TextBlockShanbe1.Text = persianDate;
                    TextBlockShanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe1Miladi.Text = miladiDate;
                    TextBlockShanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe1Hijri.Text = hijriDate;
                    TextBlockShanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe1.ToolTip = tooltip_context;
                    else GridShanbe1.ToolTip = null;
                    break;

                case 8:
                    TextBlock1Shanbe1.Text = persianDate;
                    TextBlock1Shanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe1Miladi.Text = miladiDate;
                    TextBlock1Shanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe1Hijri.Text = hijriDate;
                    TextBlock1Shanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe1.ToolTip = tooltip_context;
                    else Grid1Shanbe1.ToolTip = null;
                    break;

                case 9:
                    TextBlock2Shanbe1.Text = persianDate;
                    TextBlock2Shanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe1Miladi.Text = miladiDate;
                    TextBlock2Shanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe1Hijri.Text = hijriDate;
                    TextBlock2Shanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe1.ToolTip = tooltip_context;
                    else Grid2Shanbe1.ToolTip = null;
                    break;

                case 10:
                    TextBlock3Shanbe1.Text = persianDate;
                    TextBlock3Shanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe1Miladi.Text = miladiDate;
                    TextBlock3Shanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe1Hijri.Text = hijriDate;
                    TextBlock3Shanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe1.ToolTip = tooltip_context;
                    else Grid3Shanbe1.ToolTip = null;
                    break;

                case 11:
                    TextBlock4Shanbe1.Text = persianDate;
                    TextBlock4Shanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe1Miladi.Text = miladiDate;
                    TextBlock4Shanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe1Hijri.Text = hijriDate;
                    TextBlock4Shanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe1.ToolTip = tooltip_context;
                    else Grid4Shanbe1.ToolTip = null;
                    break;

                case 12:
                    TextBlock5Shanbe1.Text = persianDate;
                    TextBlock5Shanbe1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe1Miladi.Text = miladiDate;
                    TextBlock5Shanbe1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe1Hijri.Text = hijriDate;
                    TextBlock5Shanbe1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe1.ToolTip = tooltip_context;
                    else Grid5Shanbe1.ToolTip = null;
                    break;

                case 13:
                    TextBlockJome1.Text = persianDate;
                    TextBlockJome1.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome1Miladi.Text = miladiDate;
                    TextBlockJome1Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome1Hijri.Text = hijriDate;
                    TextBlockJome1Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome1.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome1.ToolTip = tooltip_context;
                    else GridJome1.ToolTip = null;
                    break;

                ///////////////////////////////////////////

                case 14:
                    TextBlockShanbe2.Text = persianDate;
                    TextBlockShanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe2Miladi.Text = miladiDate;
                    TextBlockShanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe2Hijri.Text = hijriDate;
                    TextBlockShanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe2.ToolTip = tooltip_context;
                    else GridShanbe2.ToolTip = null;
                    break;

                case 15:
                    TextBlock1Shanbe2.Text = persianDate;
                    TextBlock1Shanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe2Miladi.Text = miladiDate;
                    TextBlock1Shanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe2Hijri.Text = hijriDate;
                    TextBlock1Shanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe2.ToolTip = tooltip_context;
                    else Grid1Shanbe2.ToolTip = null;
                    break;

                case 16:
                    TextBlock2Shanbe2.Text = persianDate;
                    TextBlock2Shanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe2Miladi.Text = miladiDate;
                    TextBlock2Shanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe2Hijri.Text = hijriDate;
                    TextBlock2Shanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe2.ToolTip = tooltip_context;
                    else Grid2Shanbe2.ToolTip = null;
                    break;

                case 17:
                    TextBlock3Shanbe2.Text = persianDate;
                    TextBlock3Shanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe2Miladi.Text = miladiDate;
                    TextBlock3Shanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe2Hijri.Text = hijriDate;
                    TextBlock3Shanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe2.ToolTip = tooltip_context;
                    else Grid3Shanbe2.ToolTip = null;
                    break;

                case 18:
                    TextBlock4Shanbe2.Text = persianDate;
                    TextBlock4Shanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe2Miladi.Text = miladiDate;
                    TextBlock4Shanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe2Hijri.Text = hijriDate;
                    TextBlock4Shanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe2.ToolTip = tooltip_context;
                    else Grid4Shanbe2.ToolTip = null;
                    break;

                case 19:
                    TextBlock5Shanbe2.Text = persianDate;
                    TextBlock5Shanbe2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe2Miladi.Text = miladiDate;
                    TextBlock5Shanbe2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe2Hijri.Text = hijriDate;
                    TextBlock5Shanbe2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe2.ToolTip = tooltip_context;
                    else Grid5Shanbe2.ToolTip = null;
                    break;

                case 20:
                    TextBlockJome2.Text = persianDate;
                    TextBlockJome2.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome2Miladi.Text = miladiDate;
                    TextBlockJome2Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome2Hijri.Text = hijriDate;
                    TextBlockJome2Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome2.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome2.ToolTip = tooltip_context;
                    else GridJome2.ToolTip = null;
                    break;

                ///////////////////////////////////////////

                case 21:
                    TextBlockShanbe3.Text = persianDate;
                    TextBlockShanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe3Miladi.Text = miladiDate;
                    TextBlockShanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe3Hijri.Text = hijriDate;
                    TextBlockShanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe3.ToolTip = tooltip_context;
                    else GridShanbe3.ToolTip = null;
                    break;

                case 22:
                    TextBlock1Shanbe3.Text = persianDate;
                    TextBlock1Shanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe3Miladi.Text = miladiDate;
                    TextBlock1Shanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe3Hijri.Text = hijriDate;
                    TextBlock1Shanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe3.ToolTip = tooltip_context;
                    else Grid1Shanbe3.ToolTip = null;
                    break;

                case 23:
                    TextBlock2Shanbe3.Text = persianDate;
                    TextBlock2Shanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe3Miladi.Text = miladiDate;
                    TextBlock2Shanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe3Hijri.Text = hijriDate;
                    TextBlock2Shanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe3.ToolTip = tooltip_context;
                    else Grid2Shanbe3.ToolTip = null;
                    break;

                case 24:
                    TextBlock3Shanbe3.Text = persianDate;
                    TextBlock3Shanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe3Miladi.Text = miladiDate;
                    TextBlock3Shanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe3Hijri.Text = hijriDate;
                    TextBlock3Shanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe3.ToolTip = tooltip_context;
                    else Grid3Shanbe3.ToolTip = null;
                    break;

                case 25:
                    TextBlock4Shanbe3.Text = persianDate;
                    TextBlock4Shanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe3Miladi.Text = miladiDate;
                    TextBlock4Shanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe3Hijri.Text = hijriDate;
                    TextBlock4Shanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe3.ToolTip = tooltip_context;
                    else Grid4Shanbe3.ToolTip = null;
                    break;

                case 26:
                    TextBlock5Shanbe3.Text = persianDate;
                    TextBlock5Shanbe3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe3Miladi.Text = miladiDate;
                    TextBlock5Shanbe3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe3Hijri.Text = hijriDate;
                    TextBlock5Shanbe3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe3.ToolTip = tooltip_context;
                    else Grid5Shanbe3.ToolTip = null;
                    break;

                case 27:
                    TextBlockJome3.Text = persianDate;
                    TextBlockJome3.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome3Miladi.Text = miladiDate;
                    TextBlockJome3Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome3Hijri.Text = hijriDate;
                    TextBlockJome3Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome3.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome3.ToolTip = tooltip_context;
                    else GridJome3.ToolTip = null;
                    break;

                ///////////////////////////////////////////

                case 28:
                    TextBlockShanbe4.Text = persianDate;
                    TextBlockShanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe4Miladi.Text = miladiDate;
                    TextBlockShanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe4Hijri.Text = hijriDate;
                    TextBlockShanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe4.ToolTip = tooltip_context;
                    else GridShanbe4.ToolTip = null;
                    break;

                case 29:
                    TextBlock1Shanbe4.Text = persianDate;
                    TextBlock1Shanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe4Miladi.Text = miladiDate;
                    TextBlock1Shanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe4Hijri.Text = hijriDate;
                    TextBlock1Shanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe4.ToolTip = tooltip_context;
                    else Grid1Shanbe4.ToolTip = null;
                    break;

                case 30:
                    TextBlock2Shanbe4.Text = persianDate;
                    TextBlock2Shanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe4Miladi.Text = miladiDate;
                    TextBlock2Shanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe4Hijri.Text = hijriDate;
                    TextBlock2Shanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe4.ToolTip = tooltip_context;
                    else Grid2Shanbe4.ToolTip = null;
                    break;

                case 31:
                    TextBlock3Shanbe4.Text = persianDate;
                    TextBlock3Shanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe4Miladi.Text = miladiDate;
                    TextBlock3Shanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe4Hijri.Text = hijriDate;
                    TextBlock3Shanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe4.ToolTip = tooltip_context;
                    else Grid3Shanbe4.ToolTip = null;
                    break;

                case 32:
                    TextBlock4Shanbe4.Text = persianDate;
                    TextBlock4Shanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe4Miladi.Text = miladiDate;
                    TextBlock4Shanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe4Hijri.Text = hijriDate;
                    TextBlock4Shanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe4.ToolTip = tooltip_context;
                    else Grid4Shanbe4.ToolTip = null;
                    break;

                case 33:
                    TextBlock5Shanbe4.Text = persianDate;
                    TextBlock5Shanbe4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe4Miladi.Text = miladiDate;
                    TextBlock5Shanbe4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe4Hijri.Text = hijriDate;
                    TextBlock5Shanbe4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe4.ToolTip = tooltip_context;
                    else Grid5Shanbe4.ToolTip = null;
                    break;

                case 34:
                    TextBlockJome4.Text = persianDate;
                    TextBlockJome4.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome4Miladi.Text = miladiDate;
                    TextBlockJome4Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome4Hijri.Text = hijriDate;
                    TextBlockJome4Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome4.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome4.ToolTip = tooltip_context;
                    else GridJome4.ToolTip = null;
                    break;

                ///////////////////////////////////////////

                case 35:
                    TextBlockShanbe5.Text = persianDate;
                    TextBlockShanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockShanbe5Miladi.Text = miladiDate;
                    TextBlockShanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockShanbe5Hijri.Text = hijriDate;
                    TextBlockShanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleShanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridShanbe5.ToolTip = tooltip_context;
                    else GridShanbe5.ToolTip = null;
                    break;

                case 36:
                    TextBlock1Shanbe5.Text = persianDate;
                    TextBlock1Shanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock1Shanbe5Miladi.Text = miladiDate;
                    TextBlock1Shanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock1Shanbe5Hijri.Text = hijriDate;
                    TextBlock1Shanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle1Shanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid1Shanbe5.ToolTip = tooltip_context;
                    else Grid1Shanbe5.ToolTip = null;
                    break;

                case 37:
                    TextBlock2Shanbe5.Text = persianDate;
                    TextBlock2Shanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock2Shanbe5Miladi.Text = miladiDate;
                    TextBlock2Shanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock2Shanbe5Hijri.Text = hijriDate;
                    TextBlock2Shanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle2Shanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid2Shanbe5.ToolTip = tooltip_context;
                    else Grid2Shanbe5.ToolTip = null;
                    break;

                case 38:
                    TextBlock3Shanbe5.Text = persianDate;
                    TextBlock3Shanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock3Shanbe5Miladi.Text = miladiDate;
                    TextBlock3Shanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock3Shanbe5Hijri.Text = hijriDate;
                    TextBlock3Shanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle3Shanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid3Shanbe5.ToolTip = tooltip_context;
                    else Grid3Shanbe5.ToolTip = null;
                    break;

                case 39:
                    TextBlock4Shanbe5.Text = persianDate;
                    TextBlock4Shanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock4Shanbe5Miladi.Text = miladiDate;
                    TextBlock4Shanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock4Shanbe5Hijri.Text = hijriDate;
                    TextBlock4Shanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle4Shanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid4Shanbe5.ToolTip = tooltip_context;
                    else Grid4Shanbe5.ToolTip = null;
                    break;

                case 40:
                    TextBlock5Shanbe5.Text = persianDate;
                    TextBlock5Shanbe5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlock5Shanbe5Miladi.Text = miladiDate;
                    TextBlock5Shanbe5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlock5Shanbe5Hijri.Text = hijriDate;
                    TextBlock5Shanbe5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    Rectangle5Shanbe5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") Grid5Shanbe5.ToolTip = tooltip_context;
                    else Grid5Shanbe5.ToolTip = null;
                    break;

                case 41:
                    TextBlockJome5.Text = persianDate;
                    TextBlockJome5.Style = (Style)FindResource(persianTextBlockResourceName);
                    TextBlockJome5Miladi.Text = miladiDate;
                    TextBlockJome5Miladi.Style = (Style)FindResource(miladiTextBlockResourceName);
                    TextBlockJome5Hijri.Text = hijriDate;
                    TextBlockJome5Hijri.Style = (Style)FindResource(hijriTextBlockResourceName);
                    RectangleJome5.Style = (Style)FindResource(rectangleResourceName);
                    if (tooltip_context != "") GridJome5.ToolTip = tooltip_context;
                    else GridJome5.ToolTip = null;
                    break;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// نمایش ماه بعد
        /// Shows the next month
        /// </summary>
        private void nextMonth_Click(object sender, RoutedEventArgs e)
        {
            increasePersianMonth(ref yearForNavigating, ref monthForNavigating, 1);
            calculateMonth(yearForNavigating, monthForNavigating);
        }

        /// <summary>
        /// نمایش ماه قبل
        /// Shows the previous month
        /// </summary>
        private void previousMonth_Click(object sender, RoutedEventArgs e)
        {
            decreasePersianMonth(ref yearForNavigating, ref monthForNavigating, 1);
            calculateMonth(yearForNavigating, monthForNavigating);
        }

        /// <summary>
        /// پرش به سال قبل
        /// Shows the previous year
        /// </summary>
        private void previousYear_Click(object sender, RoutedEventArgs e)
        {
            this.yearForNavigating--;
            calculateMonth(yearForNavigating, monthForNavigating);
        }

        /// <summary>
        /// پرش به سال بعد
        /// Shows the next year
        /// </summary>
        private void nextYear_Click(object sender, RoutedEventArgs e)
        {
            this.yearForNavigating++;
            calculateMonth(yearForNavigating, monthForNavigating);
        }

        /// <summary>
        /// پرش به تاریخ امروز
        /// Shows the month of today
        /// </summary>
        private void goToToday_Click(object sender, RoutedEventArgs e)
        {
            calculateMonth(this.currentYear, this.currentMonth);
        }

        /// <summary>
        /// پرش به تاریخ وارد شده
        /// Shows the input date
        /// </summary>
        private void goToDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateMonth(int.Parse(textBoxYear.Text), comboBoxMonths.SelectedIndex + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// تنظیم ماه قمری
        /// Adjusts the Hijri calendar 
        /// </summary>
        private void ButttonHijriAdjustment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hijriCalendar.HijriAdjustment = int.Parse(TextBoxHijriAdjustment.Text);
                calculateMonth(this.currentYear, this.currentMonth);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard hideEventGrid = (Storyboard)TryFindResource("hideEventGrid");
            hideEventGrid.Begin();
        }

        string baseName;

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.baseName = Regex.Replace((sender as Grid).Name, @"Grid", "", RegexOptions.IgnoreCase);

            //If selected Grid wasn't for this month do nothing
            Rectangle rect = FindName("Rectangle" + this.baseName) as Rectangle;
            if (rect.Style == FindResource("RectangleStyleForOtherHolydays")
                || rect.Style == FindResource("RectangleStyle3"))
                return;
            //\\

            TextBoxEventText.Text = "";

            //////////////////////////////////////////////////////////
            //بدست آوردن اطلاعات امروز
            this.yearShamsi = this.yearForNavigating;
            this.monthShamsi = this.monthForNavigating;
            this.dayShamsi = (FindName("TextBlock" + this.baseName) as TextBlock).Text.convertToInteger();

            DateTime tempDateTime = persianCalendar.ToDateTime(this.yearShamsi, this.monthShamsi, this.dayShamsi, 01, 01, 01, 01);

            this.dayMiladi = tempDateTime.Day;
            this.monthMiladi = tempDateTime.Month;
            this.yearMiladi = tempDateTime.Year;

            this.yearHijri = hijriCalendar.GetYear(tempDateTime);
            this.monthHijri = hijriCalendar.GetMonth(tempDateTime);
            this.dayHijri = hijriCalendar.GetDayOfMonth(tempDateTime);

            ////////////////////////////////////////////////////////            

            TextBlockSelectedDateShamsi.Text = (FindName("TextBlock" + this.baseName) as TextBlock).Text + "  " + TextBlockThisMonth.Text;
            TextBlockSelectedDateMiladi.Text = (FindName("TextBlock" + this.baseName + "Miladi") as TextBlock).Text;
            TextBlockSelectedDateHijri.Text = (FindName("TextBlock" + this.baseName + "Hijri") as TextBlock).Text;

            ////////////////////////////////////////////////////////
            //اگر یادآوری ثبت شده باشد آن را پیدا و نشان دهد
            try
            {
                if (searchShamsiTable(this.yearShamsi, this.monthShamsi, this.dayShamsi))
                {
                    TextBoxEventText.Text = GetTextOfMemo(this.yearShamsi, this.monthShamsi, this.dayShamsi, "Shamsi");
                    IsHoliday.IsChecked = isHoliday(this.yearShamsi, this.monthShamsi, this.dayShamsi, "Shamsi");
                    IsAnniversary.IsChecked = isAnniersary(this.yearShamsi, this.monthShamsi, this.dayShamsi, "Shamsi");
                    RadioButtonShamsi.IsChecked = true;
                }
                else if (searchHijriTable(this.yearHijri, this.monthHijri, this.dayHijri))
                {
                    TextBoxEventText.Text = GetTextOfMemo(this.yearHijri, this.monthHijri, this.dayHijri, "Hijri");
                    IsHoliday.IsChecked = isHoliday(this.yearHijri, this.monthHijri, this.dayHijri, "Hijri");
                    IsAnniversary.IsChecked = isAnniersary(this.yearHijri, this.monthHijri, this.dayHijri, "Hijri");
                    RadioButtonHijri.IsChecked = true;
                }
                else if (searchMiladiTable(this.yearMiladi, this.monthMiladi, this.dayMiladi))
                {
                    TextBoxEventText.Text = GetTextOfMemo(this.yearMiladi, this.monthMiladi, this.dayMiladi, "Miladi");
                    IsHoliday.IsChecked = isHoliday(this.yearMiladi, this.monthMiladi, this.dayMiladi, "Miladi");
                    IsAnniversary.IsChecked = isAnniersary(this.yearMiladi, this.monthMiladi, this.dayMiladi, "Miladi");
                    RadioButtonMiladi.IsChecked = true;
                }
            }
            catch
            {
                TextBoxEventText.Text = "";
                RadioButtonShamsi.IsChecked = true;
                IsAnniversary.IsChecked = false;
                IsHoliday.IsChecked = false;
            }

            //////////////////\\\\\\\\\\\\\\\\\\\\

            Storyboard hideEventGrid = (Storyboard)TryFindResource("showEventGrid");
            hideEventGrid.Begin();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxEventText.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter the text !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TextBoxEventText.Focus();
                return;
            }

            ///////////////////////////////////////////
            //اگر یادآوری در دیتی بیس موجود بود آن را به روز کند
            bool insert = true;
            bool? Is_Holiday = this.IsHoliday.IsChecked;
            bool? Is_Anniversary = this.IsAnniversary.IsChecked;

            try
            {
                if (searchShamsiTable(this.yearShamsi, this.monthShamsi, this.dayShamsi))
                {
                    shamsi row = db.shamsis.Single(q => (q.year == this.yearShamsi && q.month == this.monthShamsi && q.day == this.dayShamsi) || (q.anniversary == true && q.month == this.monthShamsi && q.day == this.dayShamsi));
                    if (RadioButtonShamsi.IsChecked.Value)
                    {
                        row.text = TextBoxEventText.Text.Trim();
                        row.holiday = Is_Holiday.Value;
                        row.anniversary = Is_Anniversary.Value; 
                        db.SubmitChanges();
                        insert = false;
                    }
                    else
                    {
                        db.shamsis.DeleteOnSubmit(row);
                        insert = true;
                    }
                }
                else if (searchHijriTable(this.yearHijri, this.monthHijri, this.dayHijri))
                {
                    hijri row = db.hijris.Single(q => (q.year == this.yearHijri && q.month == this.monthHijri && q.day == this.dayHijri) || (q.anniversary == true && q.month == this.monthHijri && q.day == this.dayHijri));
                    if (RadioButtonHijri.IsChecked.Value)
                    {
                        row.text = TextBoxEventText.Text.Trim();                        
                        row.holiday = Is_Holiday.Value;
                        row.anniversary = Is_Anniversary.Value;
                        db.SubmitChanges();
                        insert = false;
                    }
                    else
                    {
                        db.hijris.DeleteOnSubmit(row);
                        insert = true;
                    }
                }
                else if (searchMiladiTable(this.yearMiladi, this.monthMiladi, this.dayMiladi))
                {
                    miladi row = db.miladis.Single(q => (q.year == this.yearMiladi && q.month == this.monthMiladi && q.day == this.dayMiladi) || (q.anniversary == true && q.month == this.monthMiladi && q.day == this.dayMiladi));
                    if (RadioButtonMiladi.IsChecked.Value)
                    {
                        row.text = TextBoxEventText.Text.Trim();
                        row.holiday = Is_Holiday.Value;
                        row.anniversary = Is_Anniversary.Value;
                        db.SubmitChanges();
                        insert = false;
                    }
                    else
                    {
                        db.miladis.DeleteOnSubmit(row);
                        insert = true;
                    }
                }
                db.SubmitChanges();
            }
            catch { insert = true; }
            ///////////////////////////////////////////
            try
            {                
                if (insert)
                {
                    if (RadioButtonMiladi.IsChecked.Value)
                    {
                        miladi newRow = new miladi();
                        newRow.text = TextBoxEventText.Text.Trim();
                        newRow.year = (short)this.yearMiladi;
                        newRow.month = (byte)this.monthMiladi;
                        newRow.day = (byte)this.dayMiladi;
                        newRow.holiday = Is_Holiday.Value;
                        newRow.anniversary = Is_Anniversary.Value;

                        db.miladis.InsertOnSubmit(newRow);
                        db.SubmitChanges();
                    }
                    else if (RadioButtonShamsi.IsChecked.Value)
                    {
                        shamsi newRow = new shamsi();
                        newRow.text = TextBoxEventText.Text.Trim();
                        newRow.year = (short)this.yearShamsi;
                        newRow.month = (byte)this.monthShamsi;
                        newRow.day = (byte)this.dayShamsi;
                        newRow.holiday = Is_Holiday.Value;
                        newRow.anniversary = Is_Anniversary.Value;

                        db.shamsis.InsertOnSubmit(newRow);
                        db.SubmitChanges();
                    }
                    else if (RadioButtonHijri.IsChecked.Value)
                    {
                        hijri newRow = new hijri();
                        newRow.text = TextBoxEventText.Text.Trim();
                        newRow.year = (short)this.yearHijri;
                        newRow.month = (byte)this.monthHijri;
                        newRow.day = (byte)this.dayHijri;
                        newRow.holiday = Is_Holiday.Value;
                        newRow.anniversary = Is_Anniversary.Value;

                        db.hijris.InsertOnSubmit(newRow);
                        db.SubmitChanges();
                    }
                }


                Rectangle destinationRectangle = FindName("Rectangle" + this.baseName) as Rectangle;
                if (Is_Holiday.Value)
                    destinationRectangle.Style = FindResource("RectangleStyle5") as Style;
                else
                    destinationRectangle.Style = FindResource("RectangleStyle4") as Style;

                Storyboard hideEventGrid = (Storyboard)TryFindResource("hideEventGrid");
                hideEventGrid.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure ?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;

                if (searchShamsiTable(this.yearShamsi, this.monthShamsi, this.dayShamsi))
                {
                    shamsi row = db.shamsis.Single(q => (q.year == this.yearShamsi && q.month == this.monthShamsi && q.day == this.dayShamsi) || (q.anniversary == true && q.month == this.monthShamsi && q.day == this.dayShamsi));
                    db.shamsis.DeleteOnSubmit(row);
                }
                else if (searchHijriTable(this.yearHijri, this.monthHijri, this.dayHijri))
                {
                    hijri row = db.hijris.Single(q => (q.year == this.yearHijri && q.month == this.monthHijri && q.day == this.dayHijri) || (q.anniversary == true && q.month == this.monthHijri && q.day == this.dayHijri));
                    db.hijris.DeleteOnSubmit(row);
                }
                else if (searchMiladiTable(this.yearMiladi, this.monthMiladi, this.dayMiladi))
                {
                    miladi row = db.miladis.Single(q => (q.year == this.yearMiladi && q.month == this.monthMiladi && q.day == this.dayMiladi) || (q.anniversary == true && q.month == this.monthMiladi && q.day == this.dayMiladi));
                    db.miladis.DeleteOnSubmit(row);
                }

                db.SubmitChanges();

                Rectangle destinationRectangle = FindName("Rectangle" + this.baseName) as Rectangle;
                if (Regex.IsMatch(this.baseName, @"Jome", RegexOptions.IgnoreCase))
                    destinationRectangle.Style = FindResource("RectangleStyleForHolydays") as Style;
                else
                    destinationRectangle.Style = FindResource("RectangleStyle2") as Style;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Storyboard hideEventGrid = (Storyboard)TryFindResource("hideEventGrid");
            hideEventGrid.Begin();
        }

        #endregion
    }

    public static class Extensions
    {
        /// <summary>
        /// متدی برای تبدیل اعداد انگلیسی به فارسی
        /// </summary>
        public static string convertToPersianNumber(this int input)
        {
            //۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
            string temp = input.ToString();

            temp = Regex.Replace(temp, "0", "۰");
            temp = Regex.Replace(temp, "1", "۱");
            temp = Regex.Replace(temp, "2", "۲");
            temp = Regex.Replace(temp, "3", "۳");
            temp = Regex.Replace(temp, "4", "۴");
            temp = Regex.Replace(temp, "5", "۵");
            temp = Regex.Replace(temp, "6", "۶");
            temp = Regex.Replace(temp, "7", "۷");
            temp = Regex.Replace(temp, "8", "۸");
            temp = Regex.Replace(temp, "9", "۹");

            return temp;
        }

        /// <summary>
        /// تبدیل اعداد فارسی به معادلش به صورت عدد integer
        /// </summary>
        public static int convertToInteger(this string input)
        {
            input = Regex.Replace(input, "۰", "0");
            input = Regex.Replace(input, "۱", "1");
            input = Regex.Replace(input, "۲", "2");
            input = Regex.Replace(input, "۳", "3");
            input = Regex.Replace(input, "۴", "4");
            input = Regex.Replace(input, "۵", "5");
            input = Regex.Replace(input, "۶", "6");
            input = Regex.Replace(input, "۷", "7");
            input = Regex.Replace(input, "۸", "8");
            input = Regex.Replace(input, "۹", "9");

            input = Regex.Replace(input, @"\D*", "");

            return int.Parse(input);
        }

        /// <summary>
        /// تبدیل نام روزهای هفته میلادی به شمسی
        /// </summary>
        public static string convertToPersianDay(this string input)
        {
            switch (input)
            {
                case "Saturday":
                    return "شنبه";

                case "Sunday":
                    return "یک شنبه";

                case "Monday":
                    return "دو شنبه";

                case "Tuesday":
                    return "سه شنبه";

                case "Wednesday":
                    return "چهار شنبه";

                case "Thursday":
                    return "پنج شنبه";

                case "Friday":
                    return "جمعه";
            }
            return "خطا";
        }

        /// <summary>
        /// تبدیل عدد ماه به معادل نام ماه شمسی
        /// </summary>
        public static string convertToPersianMonth(this int input)
        {
            string FarsiMonthName = "هیچ کدام";
            //تعیین نام ماه شمسی  
            switch (input)
            {
                //فروردین
                case 01:
                    FarsiMonthName = "فروردین";
                    break;

                //اردیبهشت
                case 02:
                    FarsiMonthName = "اردیبهشت";
                    break;

                //خرداد
                case 03:
                    FarsiMonthName = "خرداد";
                    break;

                //تیر
                case 04:
                    FarsiMonthName = "تیر";
                    break;

                //مرداد
                case 05:
                    FarsiMonthName = "مرداد";
                    break;

                //شهریور
                case 06:
                    FarsiMonthName = "شهریور";
                    break;

                //مهر
                case 07:
                    FarsiMonthName = "مهر";
                    break;

                //آبان
                case 08:
                    FarsiMonthName = "آبان";
                    break;

                //آذر
                case 09:
                    FarsiMonthName = "آذر";
                    break;

                //دی
                case 10:
                    FarsiMonthName = "دی";
                    break;

                //بهمن
                case 11:
                    FarsiMonthName = "بهمن";
                    break;

                //اسفند
                case 12:
                    FarsiMonthName = "اسفند";
                    break;
            }

            return FarsiMonthName;
        }

        /// <summary>
        /// تبدیل عدد ماه به معادل نام ماه قمری
        /// </summary>
        public static string convertToHigriMonth(this int input)
        {
            string higriMonthName = "هیچ کدام";
            //تعیین نام ماه هجری قمری  
            switch (input)
            {
                case 01:
                    higriMonthName = "محرم";
                    break;

                case 02:
                    higriMonthName = "صفر";
                    break;

                case 03:
                    higriMonthName = "ربیع الاول";
                    break;

                case 04:
                    higriMonthName = "ربیع الثانی";
                    break;

                case 05:
                    higriMonthName = "جمادی الاول";
                    break;

                case 06:
                    higriMonthName = "جمادی الثانی";
                    break;

                case 07:
                    higriMonthName = "رجب";
                    break;

                case 08:
                    higriMonthName = "شعبان";
                    break;

                case 09:
                    higriMonthName = "رمضان";
                    break;

                case 10:
                    higriMonthName = "شوال";
                    break;

                case 11:
                    higriMonthName = "ذیقعده";
                    break;

                case 12:
                    higriMonthName = "ذیحجه";
                    break;
            }

            return higriMonthName;
        }
    }
}
