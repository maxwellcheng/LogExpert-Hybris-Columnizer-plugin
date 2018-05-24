using System;
using LogExpert;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String l1 = "INFO   | jvm 1    | main    | 2018/04/17 16:16:01.125 | HOTSWAP AGENT: 16:16:01.069 INFO (org.hotswap.agent.watch.nio.TreeWatcherNIO) - Registering directory target C:\\";
            String l2 = "INFO   | jvm 1    | main    | 2018/04/17 16:24:05.696 | 	at org.springframework.beans.factory.support.DefaultListableBeanFactory.doResolveDependency(DefaultListableBeanFactory.java:790)";
            String l3 = "INFO   | jvm 1    | main    | 2018/04/17 16:24:26.957 | DEBUG [WrapperSimpleAppMain] [SoapMessageDispatcher] No EndpointAdapters found, using defaults";
            HybrisColumnizer test = new HybrisColumnizer();
            string[] res = test.SplitLine(null, l1);
            
        }
    }
}
