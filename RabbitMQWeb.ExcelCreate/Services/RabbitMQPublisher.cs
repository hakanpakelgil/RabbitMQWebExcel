﻿using System.Text.Json;
using System.Text;
using RabbitMQWeb.ExcelCreate.Models;
using RabbitMQ.Client;

namespace RabbitMQWeb.ExcelCreate.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitmqClientService)
        {
            _rabbitmqClientService = rabbitmqClientService;
        }

        public void Publish(CreateExcelMessage createExcelMessage)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(createExcelMessage);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();

            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel, basicProperties: properties, body: bodyByte);
        }
    }
}
