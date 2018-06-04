using System;
using System.Collections.Generic;
using System.Text;
using LogExpert;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace LogExpert
{

    public class HybrisColumnizer : ILogLineColumnizer
    {
        private Regex _lineRegex = new Regex("(.*)(\\|\\s*.*)(\\|\\s*.*)(\\|\\s*.*)(\\|\\s*.*)(\\s*\\[.*\\])(\\s*\\[.*\\])(\\s*.*)");
        //INFO   		| jvm 1    		| main   		| 2018/03/19 11:26:07.706 		| INFO  [WrapperSimpleAppMain] [hybrisserver] Starting up hybris server
       

        public string GetName()
        {
            return "Hybris Columnizer";
        }

        public string GetDescription()
        {
            return "Logfile Format used by SAP Hybris.";
        }

        public int GetColumnCount()
        {
            return 6;
        }

        public string[] GetColumnNames()
        {
            return new string[] { "Log Level", "JVM", "   ", "Date","Time", "Message" };
        }

        public string[] SplitLine(ILogLineColumnizerCallback callback, string line)
        {
            string[] cols = new string[6] { "", "", "", "", "","" };
            if (line.Length > 1024)
            {
                // spam
                line = line.Substring(0, 1024);
                cols[4] = line;
                return cols;
            }
            String[] res = Regex.Split(line, "\\|");
            //if (_lineRegex.IsMatch(line))
            if (true)
            {
                /*
                Match match = _lineRegex.Match(line);
                GroupCollection groups = match.Groups;
                if (groups.Count == 9)
                {
                    cols[0] = groups[1].Value;
                    cols[1] = groups[2].Value;
                    cols[2] = groups[3].Value;
                    cols[3] = groups[4].Value;
                    cols[4] = groups[5].Value;
                    cols[5] = groups[6].Value;
                    cols[6] = groups[7].Value;
                    cols[7] = groups[8].Value;
                }
                else
                {
                    cols[7] = line;
                }
                */
                if (res.Length == 5) {
                    cols[0] = res[0];
                    cols[1] = res[1];
                    cols[2] = res[2];
                    String[] dateTime= Regex.Split(res[3], "\\s");
                    cols[3] = dateTime[1];
                    cols[4] = dateTime[2];
                    cols[5] = res[4];
                }
                else
                {
                    cols[4] = line;
                }
            }
            else
            {
                cols[4] = line;
            }
            return cols;
        }

        public IColumnizedLogLine SplitLine(ILogLineColumnizerCallback callback, ILogLine line)
        {
            ColumnizedLogLine columnizedLogLine = new ColumnizedLogLine();
            columnizedLogLine.LogLine = line; // Add the reference to the LogLine 
            Column[] columns = Column.CreateColumns(GetColumnCount(), columnizedLogLine);
            columnizedLogLine.ColumnValues = columns.Select(a => a as IColumn).ToArray();
            String[] tmp=SplitLine(callback, line.FullLine);

            for(int i = 0; i < columns.Length; i++)
            {
                columns[i].FullValue = tmp[i];
            }
            return columnizedLogLine;
        }
        public bool IsTimeshiftImplemented()
        {
            return false;
        }

        public void SetTimeOffset(int msecOffset)
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public int GetTimeOffset()
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public DateTime GetTimestamp(ILogLineColumnizerCallback callback, string line)
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public void PushValue(ILogLineColumnizerCallback callback, int column, string value, string oldValue)
        {
            throw new NotImplementedException(); //Nothing to do here
        }
        public DateTime GetTimestamp(ILogLineColumnizerCallback callback, ILogLine line)
        {
            throw new NotImplementedException();
        }
    }
}