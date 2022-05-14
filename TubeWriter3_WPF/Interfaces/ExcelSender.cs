using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using TubeWriter3_WPF.Models;
using TubeWriter3_WPF.ViewModels;

namespace TubeWriter3_WPF.Interfaces
{
    public static class ExcelSender
    {
        public static Report Send(MainViewModel ViewModel)
        {
            //Prepare starting variables
            int start;
            try
            {
                start = Convert.ToInt32(ViewModel.BoundStart) % 96;
            }
            catch
            {
                start = 1; 
            }
            int rowCount = 0;

            //Create sheet to fill with data
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Tubes Sheet");

            //Repeat fill process foreach active date
            for (int i = 0; i <= ViewModel.DateCount; i++)
            {
                var Date = ViewModel.BoundDate[i];

                //Stop if no date
                if (Date == null)
                {
                    return new Report(false, "A date was missing.");
                }

                //Adjust date length
                while (Date.Length < 4)
                {
                    Date = " " + Date;
                }

                //Convert strings to ranges
                List<List<int>> ranges = new List<List<int>>();
                ranges.Add(Range2Ints(ViewModel.BoundDogs[i]));
                ranges.Add(Range2Ints(ViewModel.BoundHorses[i]));
                ranges.Add(Range2Ints(ViewModel.BoundBirds[i]));
                ranges.Add(Range2Ints(ViewModel.BoundDoubles[i]));

                //Fill sheet with ranges
                for (int j = 0; j < ranges.Count; j++)
                {
                    //Make sure range converted correctly
                    if (ranges[j] == null)
                    {
                        return new Report(false, $"Invalid range for {IntToAnimalString(j)}.");
                    }

                    foreach (int number in ranges[j])
                    {
                        //Create string for current number
                        string numString = Convert.ToString(number);
                        if (number < 1000)
                        {
                            numString = " " + numString;
                        }

                        //Create and fill row for current number
                        IRow row = sheet.CreateRow(rowCount);

                        //Incrementing Column
                        ICell countCell = row.CreateCell(0);
                        countCell.SetCellValue(start);

                        //Character Column
                        ICell charCell = row.CreateCell(1);
                        charCell.SetCellValue(IntToAnimalChar(j, ViewModel.BoundRetest[i]));

                        //Number Column
                        ICell numCell = row.CreateCell(2);
                        numCell.SetCellValue(number);

                        //Date Column
                        ICell dateCell = row.CreateCell(3);
                        dateCell.SetCellValue(Date);

                        rowCount++;
                        start++;
                    }
                }
            }

            //Try to save workbook to file
            if (!FileHandler.Save(workbook))
            {
                return new Report(false, "Please close the \"Tubes.xlsx\" file");
            }


            //Try to open excel
            if (!FileHandler.Open())
            {
                return new Report(false, "Unable to open Excel, is it already running?");
            }

            return new Report(true);
        }

        //Converts entrybox range into list of ints
        private static List<int> Range2Ints(string str)
        {
            char[] delimChars = new char[] { ',', '+' };
            List<int> result = new List<int>();

            if (str == "")
            {
                return result;
            }

            try
            {
                foreach (var subStr in str.Split(delimChars))
                {

                    if (subStr.Contains('-'))
                    {
                        int lower = Convert.ToInt32(subStr.Split('-')[0]);
                        int upper = Convert.ToInt32(subStr.Split('-')[1]);

                        //Check for invalid range entry
                        if (lower > upper)
                        {
                            return null;
                        }

                        //Add range to final list
                        var range = Enumerable.Range(lower, (upper + 1) - lower);
                        result.AddRange(range);
                    }
                    else
                    {
                        int number = Convert.ToInt32(subStr);
                        result.Add(number);
                    }
                }
            }
            catch (System.FormatException)
            {
                return null;
            }

            return result;
        }

        //Converts int to string to be used in error messages
        private static string IntToAnimalString(int Number)
        {
            switch (Number)
            {
                case 0:
                    return "Dogs";
                case 1:
                    return "Horses";
                case 2:
                    return "Birds";
                case 3:
                    return "Doubles";
                default:
                    return "oh uh sorry I'm really not sure which one, wow you messed up buddy.";
            }
        }

        //Converts int to string to be printed on tubes
        private static string IntToAnimalChar(int Number, bool Retest)
        {
            string result = "";
            switch (Number)
            {
                case 0:
                    result = "C";
                    break;
                case 1:
                    result = "E";
                    break;
                case 2:
                    result = "A";
                    break;
                case 3:
                    result = "AS";
                    break;
                default:
                    return "oh uh sorry I'm really not sure which one, wow you messed up buddy.";
            }

            return Retest ? result + "R": result;
        }
    }
}
