using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CleanArchitectureWIthCQRSAndMediator.Model
{
    public static class KafkaConfig
    {
        public static IServiceCollection AddKafkaConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind Kafka settings from the configuration
            var kafkaSettings = new KafkaSettings();
            configuration.GetSection("Kafka").Bind(kafkaSettings);

            // Register KafkaSettings as a singleton
            services.AddSingleton(kafkaSettings);

            // Create Kafka producer configuration
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = kafkaSettings.SaslUsername,
                SaslPassword = kafkaSettings.SaslPassword
            };

            // Create Kafka consumer configuration
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = "csharp-group-1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = kafkaSettings.SaslUsername,
                SaslPassword = kafkaSettings.SaslPassword,
                EnableAutoCommit = true,
                AutoCommitIntervalMs = 5000,
            };

            // Register the Kafka producer as a singleton service
            services.AddSingleton<IProducer<string, string>>(sp =>
            {
                return new ProducerBuilder<string, string>(producerConfig).Build();
            });

            // Register the Kafka consumer as a singleton service
            services.AddSingleton<IConsumer<string, string>>(sp =>
            {
                return new ConsumerBuilder<string, string>(consumerConfig).Build();
            });

            return services;
        }
    }
}
