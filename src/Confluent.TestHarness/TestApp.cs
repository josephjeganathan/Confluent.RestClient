using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.RestClient;
using Confluent.RestClient.Model;
using Newtonsoft.Json;

namespace Confluent.TestHarness
{
    public partial class TestApp : Form
    {
        private readonly ConfluentClient _confluentClient;

        public TestApp()
        {
            InitializeComponent();
            _confluentClient = new ConfluentClient();
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
    }
}
