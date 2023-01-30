using Hsy.GyresMesh;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsy.Core
{
    public class HS_ProgressReporter : HS_Thread
    {
        public HS_ProgressTracker tracker = HS_ProgressTracker.instance();
        public HS_ProgressStatus status;
        public StreamWriter output;

        String path;
        public HS_ProgressReporter(String path):this(0,path,false)
        {

        }
        public HS_ProgressReporter(int depth, String path, bool Enqueue):base()
        {
            
            tracker.setMaxLevel(depth);
            this.path = path;
            try
            {

                //if (Enqueue)
                //{
                output = new StreamWriter(path, Enqueue, Encoding.UTF8);
                //}
                //            else
                //            {
                //	output = new StreamWriter(new BufferedStream(new FileStream(path, FileMode.OpenOrCreate)), Encoding.UTF8);
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //e.printStackTrace();
            }
        
        }
        public HS_ProgressReporter(int depth, int consoleDepth, String path) : this(depth, path, false)
        {
            //this(depth, path, false);
        }

        public override void start()
        {
            base.start();
            Console.WriteLine("Starting HS_ProgressTracker: " + path);
            Console.WriteLine("");

        }

        public override void run()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                try
                {
                    while (tracker.isUpdated())
                    {
                        status = tracker.getStatus();
                        if (status != null)
                        {
                            String s = status.getText();
                            if (s != null)
                            {
                                output.WriteLine(s);
                                if (status.getLevel() < 2)
                                {
                                    Console.WriteLine(status.getConsoleText());
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public class HS_ProgressCounter
        {
            protected internal volatile int count;
            protected internal volatile String caller;
            protected internal volatile String text;
            private int limit;
            private int percentageStep;
            private volatile int currentPercentage;
            private volatile int nextUpdate;
            static HS_ProgressTracker tracker = HS_ProgressTracker.instance();

            public HS_ProgressCounter(int limit, int percentageStep)
            {
                this.count = 0;
                this.limit = limit;
                this.percentageStep = percentageStep;
                if (percentageStep < 0)
                {
                    this.percentageStep = 10;
                }
                if (percentageStep > 100)
                {
                    this.percentageStep = 10;
                }
                currentPercentage = 0;
                nextUpdate = (int)(limit * 0.01
                        * (currentPercentage + percentageStep));
                caller = null;
                text = null;
            }

            public void increment()
            {
                increment(1);
            }

            public void increment(int inc)
            {
                //Console.WriteLine(count);
                count += inc;
                if (count >= nextUpdate)
                {
                    while (nextUpdate <= count)
                    {
                        currentPercentage += percentageStep;
                        nextUpdate = (int)(limit * 0.01
                                * (currentPercentage + percentageStep));
                    }
                    tracker.setCounterStatusStr(caller, text, this);
                }
                else if (count >= limit)
                {
                    tracker.setCounterStatusStr(caller, text, this);
                }
            }

            protected internal int getCount()
            {
                return count;
            }

            protected internal int getLimit()
            {
                return limit;
            }
        }
        public class HS_ProgressTracker
        {
            protected internal Queue<HS_ProgressStatus> statuses;
            protected internal volatile int level;
            public static int indent = 3;
            protected volatile int maxLevel;
            int INCLVL = +1;
            int DECLVL = -1;
            String sdf = "yyyy-MM-dd HH:mm:ss.SSS";

            protected HS_ProgressTracker()
            {
                statuses = new Queue<HS_ProgressStatus>();
                level = 0;
                maxLevel = 2;
            }

            private static HS_ProgressTracker tracker = new HS_ProgressTracker();

            public static HS_ProgressTracker instance()
            {
                return tracker;
            }

            public void setIndent(int indent)
            {
                HS_ProgressTracker.indent = Math.Max(0, indent);
            }

            protected internal HS_ProgressStatus getStatus()
            {
                if (statuses.Count > 0)
                {
                    return statuses.Peek();
                }
                return null;
            }

            public void setStartStatus(Object caller, String status)
            {
                if (level <= maxLevel)
                {
                    String key = "";
                    if (caller is GE_Object)
                    {
                        key = " (key: " + ((GE_Object)caller).GetKey() + ")";
                    }
                    Console.WriteLine(caller.GetType().Name + " " + status);
                    statuses.Enqueue(new HS_ProgressStatus("\u250C",caller.GetType().Name + key, status, level, DateTime.Now.ToString(sdf)));
                    
                }
                level = Math.Max(0, level + INCLVL);
            }

            public void setStopStatus(Object caller, String status)
            {
                level = Math.Max(0, level + DECLVL);
                if (level <= maxLevel)
                {
                    String key = "";
                    if (caller is GE_Object)
                    {
                        key = " (key: " + ((GE_Object)caller).GetKey() + ")";
                    }
                    statuses.Enqueue(new HS_ProgressStatus("\u2514",caller.GetType().Name + key, status, level,DateTime.Now.ToString(sdf)));
                }
            }

            public void setDuringStatus(Object caller, String status)
            {
                if (level <= maxLevel)
                {
                    String key = "";
                    if (caller is GE_Object)
                    {
                        key = " (key: " + ((GE_Object)caller).GetKey() + ")";
                    }
                    statuses.Enqueue(new HS_ProgressStatus("|",caller.GetType().Name + key, status, level, DateTime.Now.ToString(sdf)));
                }
            }

            public void setStartStatusStr(String caller,
                     String status)
            {
                if (level <= maxLevel)
                {
                    statuses.Enqueue(new HS_ProgressStatus("\u250C", caller, status,level, DateTime.Now.ToString(sdf)));
                }
                level = Math.Max(0, level + INCLVL);
            }

            public void setStopStatusStr(String caller, String status)
            {
                level = Math.Max(0, level + DECLVL);
                if (level <= maxLevel)
                {
                    statuses.Enqueue(new HS_ProgressStatus("\u2514", caller, status,level, DateTime.Now.ToString(sdf)));
                }
            }

            public void setDuringStatusStr(String caller,
                     String status)
            {
                if (level <= maxLevel)
                {
                    statuses.Enqueue(new HS_ProgressStatus("|", caller, status, level,DateTime.Now.ToString(sdf)));
                }
            }

            public void setCounterStatus(Object caller, String status,
                     HS_ProgressCounter counter)
            {
                if (counter.getLimit() > 0)
                {
                    String key = "";
                    if (caller is GE_Object)
                    {
                        key = " (key: " + ((GE_Object)caller).GetKey() + ")";
                    }
                    counter.caller = caller.GetType().Name + key;
                    counter.text = status;
                    if (level <= maxLevel)
                    {
                        statuses.Enqueue(new HS_ProgressStatus("|",caller.GetType().Name + key, status,counter, level, DateTime.Now.ToString(sdf)));
                    }
                }
            }

            public void setCounterStatusStr(String caller,
                     String status, HS_ProgressCounter counter)
            {
                if (counter.getLimit() > 0)
                {
                    if (level <= maxLevel)
                    {
                        statuses.Enqueue(new HS_ProgressStatus("|", caller, status, counter, level, DateTime.Now.ToString(sdf)));
                    }
                }
            }

            public void setSpacer(String caller)
            {
                if (level <= maxLevel)
                {
                    statuses.Enqueue(new HS_ProgressStatus(caller, level, DateTime.Now.ToString(sdf)));
                }
            }

            public void setSpacer(Object caller)
            {
                if (level <= maxLevel)
                {
                    statuses.Enqueue(new HS_ProgressStatus(caller.GetType().Name, level, DateTime.Now.ToString(sdf)));
                }
            }

            public bool isUpdated()
            {
                return statuses.Count > 0;
            }

            public void setMaxLevel(int maxLevel)
            {
                this.maxLevel = maxLevel;
            }
        }

        public class HS_ProgressStatus
        {
            public String caller;
            public String text;
            public String counterString;
            public String indent;
            public String time;
            public String delim;
            public int level;

            public HS_ProgressStatus(String delim, String caller, String text, HS_ProgressCounter counter, int depth, String time)
            {
                this.caller = caller;
                this.text = text;
                StringBuilder outputBuffer = new StringBuilder(depth);
                for (int i = 0; i < depth; i++)
                {
                    outputBuffer.Append("|");
                    for (int j = 0; j < HS_ProgressTracker.indent; j++)
                    {
                        outputBuffer.Append(" ");
                    }
                }
                this.indent = outputBuffer.ToString();
                this.counterString = counter.getLimit() > 0 ? " ("
                        + counter.getCount() + " of " + counter.getLimit() + ")"
                        : "";
                level = depth;
                this.time = new String(time.ToCharArray());
                this.delim = delim;
            }

            public HS_ProgressStatus(String delim, String caller, String text, int depth, String time)
            {
                this.caller = caller;
                this.text = text;
                StringBuilder outputBuffer = new StringBuilder(depth);
                for (int i = 0; i < depth; i++)
                {
                    outputBuffer.Append("|");
                    for (int j = 0; j < HS_ProgressTracker.indent; j++)
                    {
                        outputBuffer.Append(" ");
                    }
                }
                this.indent = outputBuffer.ToString();
                this.counterString = null;
                this.time = new String(time.ToCharArray());
                this.delim = delim;
                level = depth;
            }

            public HS_ProgressStatus(String caller, int depth, String time)
            {
                this.caller = "spacer";
                StringBuilder outputBuffer = new StringBuilder(caller.Length);
                for (int i = 0; i < caller.Length + 1; i++)
                {
                    outputBuffer.Append("-");
                }
                this.text = outputBuffer.ToString();
                outputBuffer = new StringBuilder(depth);
                for (int i = 0; i < time.Length + 1; i++)
                {
                    outputBuffer.Append(" ");
                }
                for (int i = 0; i < depth; i++)
                {
                    outputBuffer.Append("|");
                    for (int j = 0; j < HS_ProgressTracker.indent; j++)
                    {
                        outputBuffer.Append(" ");
                    }
                }
                this.indent = outputBuffer.ToString();
                this.counterString = null;
                this.time = new String(time.ToCharArray());
                this.delim = "|";
                level = depth;
            }

            public String getText()
            {
                if (caller == null)
                {
                    return null;
                }
                else if (caller.Equals("spacer"))
                {
                    return indent + text;
                }
                if (text == " ")
                {
                    return "";
                }
                if (counterString == null)
                {
                    return time + " " + indent + delim + " " + caller + ": " + text;
                }
                return time + " " + indent + delim + " " + caller + ": " + text
                        + counterString;
            }

            public String getConsoleText()
            {
                if (caller == null)
                {
                    return null;
                }
                else if (caller.Equals("spacer"))
                {
                    return indent + text;
                }
                if (text == " ")
                {
                    return "";
                }
                if (counterString == null)
                {
                    return time + " " + indent + (delim.Equals("|") ? "|" : "*")
                            + " " + caller + ": " + text;
                }
                return time + " " + indent + (delim.Equals("|") ? "|" : "*") + " "
                        + caller + ": " + text + counterString;
            }

            public int getLevel()
            {
                return level;
            }
        }
    }
}
