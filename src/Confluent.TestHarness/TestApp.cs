using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.RestClient;
using Confluent.RestClient.Model;
using Newtonsoft.Json;

namespace Confluent.TestHarness
{
    public partial class TestApp : Form
    {
        private const string ValueSchema = @"{""type"":""record"",""name"":""Confluent.TestHarness.Person"",""fields"":[{""name"":""Name"",""type"":""string""},{""name"":""Age"",""type"":""int""}]}";
        private readonly ConfluentClient _confluentClient;
        private readonly Random _random = new Random();

        public TestApp()
        {
            InitializeComponent();
            _confluentClient = new ConfluentClient();
        }

        private Person GetPerson()
        {
            return new Person { Name = Guid.NewGuid().ToString("N"), Age = _random.Next(20, 100) };
        }

        private static string ToBase64<T>(T data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        private void Run<TResponse>(Func<ConfluentResponse<TResponse>> operation) where TResponse : class
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

        private void WriteLog<TResponse>(ConfluentResponse<TResponse> response) where TResponse : class
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

        private void textBoxPartitionId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                    new Record<string, string>
                    {
                        PartitionId = Convert.ToInt32(textBoxPartitionId.Text),
                        Value = ToBase64(GetPerson())
                    },
                    new Record<string, string>
                    {
                        Key = Guid.NewGuid().ToString("N"),
                        Value = ToBase64(GetPerson())
                    }
                };
                var recordSet = new RecordSet<string, string>(records);

                return _confluentClient.PublishAsBinaryAsync(textBoxTopic.Text, recordSet).Result;
            });
        }

        private void buttonAvro_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                var records = new[]
                {
                    new Record<string, Person>
                    {
                        PartitionId = Convert.ToInt32(textBoxPartitionId.Text),
                        Value = GetPerson()
                    },
                    new Record<string, Person>
                    {
                        Value = GetPerson()
                    }
                };
                var recordSet = new RecordSet<string, Person>(records) { ValueSchema = ValueSchema };

                return _confluentClient.PublishAsAvroAsync(textBoxTopic.Text, recordSet).Result;
            });
        }
    }
}
