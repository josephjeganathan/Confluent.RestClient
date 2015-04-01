using System;
using System.Configuration;
using System.Drawing.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.RestClient;
using Confluent.RestClient.Model;
using Microsoft.Hadoop.Avro;
using Newtonsoft.Json;

namespace Confluent.TestHarness
{
    public partial class TestApp : Form
    {
        private readonly IConfluentClient _confluentClient;
        private readonly Random _random = new Random();
        private readonly string _baseUrl = ConfigurationManager.AppSettings["Confluent.KafkaBaseUrl"];

        public TestApp()
        {
            InitializeComponent();
            _confluentClient = new ConfluentClient(new ConfluentClientSettings(_baseUrl));
        }

        private Person GetPerson()
        {
            return new Person { Name = Guid.NewGuid().ToString("N"), Age = _random.Next(20, 100) };
        }

        private static string ToBase64<T>(T data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        private void Run(Func<ConfluentResponse> operation)
        {
            Task.Run(() =>
            {
                try
                {
                    WriteLog(operation());
                }
                catch (Exception ex)
                {
                    WriteLog(JsonConvert.SerializeObject(ex, Formatting.Indented));
                }
            });

        }

        private void WriteLog(ConfluentResponse response)
        {
            WriteLog(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        private void WriteLog(string logMessage)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.BeginInvoke((MethodInvoker)delegate
                {
                    textBoxLog.Text = logMessage;
                });
            }
            else
            {
                textBoxLog.Text = logMessage;
            }
        }

        private int? GetMaxBytesOrNull()
        {
            int maxBytes = Convert.ToInt32(textBoxMaxBytes.Text);
            if (maxBytes > 0)
            {
                return maxBytes;
            }
            return null;
        }

        private int GetPartitionId()
        {
            return Convert.ToInt32(textBoxPartitionId.Text);
        }

        private static void DigitsOnly(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxPartitionId_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(e);
        }

        private void textBoxMaxBytes_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(e);
        }

        private void buttonGetTopics_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.GetTopicsAsync().Result);
        }

        private void buttonGetTopicMetadata_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.GetTopicMetadataAsync(textBoxTopic.Text).Result);
        }

        private void buttonGetPartitions_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.GetTopicPartitionsAsync(textBoxTopic.Text).Result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.GetTopicPartitionAsync(textBoxTopic.Text, Convert.ToInt32(textBoxPartitionId.Text)).Result);
        }

        private void buttonBinary_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                var records = new[]
                {
                    new BinaryRecord
                    {
                        PartitionId = GetPartitionId(),
                        Value = ToBase64(GetPerson())
                    },
                    new BinaryRecord
                    {
                        Key = Guid.NewGuid().ToString("N"),
                        Value = ToBase64(GetPerson())
                    }
                };
                var recordSet = new BinaryRecordSet(records);

                return _confluentClient.PublishAsBinaryAsync(textBoxTopic.Text, recordSet).Result;
            });
        }

        private void buttonAvro_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                var records = new[]
                {
                    new AvroRecord<string, Person>
                    {
                        PartitionId = GetPartitionId(),
                        Value = GetPerson()
                    },
                    new AvroRecord<string, Person>
                    {
                        Value = GetPerson()
                    }
                };
                var recordSet = new AvroRecordSet<string, Person>(records)
                {
                    ValueSchema = AvroSerializer.Create<Person>().ReaderSchema.ToString()
                };

                return _confluentClient.PublishAsAvroAsync(textBoxTopic.Text, recordSet).Result;
            });
        }

        private void buttonCreateBinaryConsumer_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.CreateConsumerAsync(textBoxConsumerGroup.Text, new CreateConsumerRequest
            {
                InstanceId = textBoxConsumerId.Text,
                MessageFormat = MessageFormat.Binary
            }).Result);
        }

        private void buttonCreateAvroConsumer_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.CreateConsumerAsync(textBoxConsumerGroup.Text, new CreateConsumerRequest
            {
                InstanceId = textBoxConsumerId.Text,
                MessageFormat = MessageFormat.Avro
            }).Result);
        }

        private void buttonConsumerBinary_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.ConsumeAsBinaryAsync(new ConsumerInstance
            {
                BaseUri = string.Format("{0}/consumers/{1}/instances/{2}", _baseUrl, textBoxConsumerGroup.Text, textBoxConsumerId.Text),
                InstanceId = textBoxConsumerId.Text
            }, textBoxTopic.Text, GetMaxBytesOrNull()).Result);
        }

        private void buttonConsumeAvro_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.ConsumeAsAvroAsync<string, Person>(new ConsumerInstance
            {
                BaseUri = string.Format("{0}/consumers/{1}/instances/{2}", _baseUrl, textBoxConsumerGroup.Text, textBoxConsumerId.Text),
                InstanceId = textBoxConsumerId.Text
            }, textBoxTopic.Text, GetMaxBytesOrNull()).Result);

        }

        private void buttonCommitOffset_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.CommitOffsetAsync(new ConsumerInstance
            {
                BaseUri = string.Format("{0}/consumers/{1}/instances/{2}", _baseUrl, textBoxConsumerGroup.Text, textBoxConsumerId.Text),
                InstanceId = textBoxConsumerId.Text
            }).Result);
        }

        private void buttonDeleteConsumer_Click(object sender, EventArgs e)
        {
            Run(() => _confluentClient.DeleteConsumerAsync(new ConsumerInstance
            {
                BaseUri = string.Format("{0}/consumers/{1}/instances/{2}", _baseUrl, textBoxConsumerGroup.Text, textBoxConsumerId.Text),
                InstanceId = textBoxConsumerId.Text
            }).Result);
        }
    }
}
