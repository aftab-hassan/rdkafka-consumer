﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RdKafka;

namespace KafkaConsumerRDKAFKA
{
    class Program
    {
        public static void Main(string[] args)
        {
            string brokerList = "localhost";
            var topics = args.Skip(1).ToList();

            var config = new Config() { GroupId = "simple-csharp-consumer" };
            using (var consumer = new EventConsumer(config, brokerList))
            {
                consumer.OnMessage += (obj, msg) =>
                {
                    string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
                    Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
                };

                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset("topicName", 0, 5) });
                consumer.Start();

                Console.WriteLine("Started consumer, press enter to stop consuming");
                Console.ReadLine();
            }
        }
    }
}
