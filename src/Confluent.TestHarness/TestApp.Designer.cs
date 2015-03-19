namespace Confluent.TestHarness
{
    partial class TestApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConsumerGroup = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConsumerId = new System.Windows.Forms.TextBox();
            this.buttonGetTopics = new System.Windows.Forms.Button();
            this.buttonGetTopicMetadata = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPartitionId = new System.Windows.Forms.TextBox();
            this.buttonGetPartitions = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(13, 65);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(853, 441);
            this.textBoxLog.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Topic: ";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(59, 10);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(119, 20);
            this.textBoxTopic.TabIndex = 2;
            this.textBoxTopic.Text = "test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Consumer Group:";
            // 
            // textBoxConsumerGroup
            // 
            this.textBoxConsumerGroup.Location = new System.Drawing.Point(279, 10);
            this.textBoxConsumerGroup.Name = "textBoxConsumerGroup";
            this.textBoxConsumerGroup.Size = new System.Drawing.Size(119, 20);
            this.textBoxConsumerGroup.TabIndex = 2;
            this.textBoxConsumerGroup.Text = "testgroup";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Consumer Id:";
            // 
            // textBoxConsumerId
            // 
            this.textBoxConsumerId.Location = new System.Drawing.Point(479, 10);
            this.textBoxConsumerId.Name = "textBoxConsumerId";
            this.textBoxConsumerId.Size = new System.Drawing.Size(119, 20);
            this.textBoxConsumerId.TabIndex = 2;
            this.textBoxConsumerId.Text = "testconsumer";
            // 
            // buttonGetTopics
            // 
            this.buttonGetTopics.Location = new System.Drawing.Point(12, 36);
            this.buttonGetTopics.Name = "buttonGetTopics";
            this.buttonGetTopics.Size = new System.Drawing.Size(126, 23);
            this.buttonGetTopics.TabIndex = 3;
            this.buttonGetTopics.Text = "Get Topics";
            this.buttonGetTopics.UseVisualStyleBackColor = true;
            this.buttonGetTopics.Click += new System.EventHandler(this.buttonGetTopics_Click);
            // 
            // buttonGetTopicMetadata
            // 
            this.buttonGetTopicMetadata.Location = new System.Drawing.Point(140, 36);
            this.buttonGetTopicMetadata.Name = "buttonGetTopicMetadata";
            this.buttonGetTopicMetadata.Size = new System.Drawing.Size(126, 23);
            this.buttonGetTopicMetadata.TabIndex = 3;
            this.buttonGetTopicMetadata.Text = "Get Topic Metadata";
            this.buttonGetTopicMetadata.UseVisualStyleBackColor = true;
            this.buttonGetTopicMetadata.Click += new System.EventHandler(this.buttonGetTopicMetadata_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(604, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Partition Id:";
            // 
            // textBoxPartitionId
            // 
            this.textBoxPartitionId.Location = new System.Drawing.Point(670, 10);
            this.textBoxPartitionId.Name = "textBoxPartitionId";
            this.textBoxPartitionId.Size = new System.Drawing.Size(54, 20);
            this.textBoxPartitionId.TabIndex = 2;
            this.textBoxPartitionId.Text = "0";
            this.textBoxPartitionId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPartitionId_KeyPress);
            // 
            // buttonGetPartitions
            // 
            this.buttonGetPartitions.Location = new System.Drawing.Point(272, 36);
            this.buttonGetPartitions.Name = "buttonGetPartitions";
            this.buttonGetPartitions.Size = new System.Drawing.Size(126, 23);
            this.buttonGetPartitions.TabIndex = 3;
            this.buttonGetPartitions.Text = "Get Partitions";
            this.buttonGetPartitions.UseVisualStyleBackColor = true;
            this.buttonGetPartitions.Click += new System.EventHandler(this.buttonGetPartitions_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(404, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Get Partition";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 518);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonGetPartitions);
            this.Controls.Add(this.buttonGetTopicMetadata);
            this.Controls.Add(this.buttonGetTopics);
            this.Controls.Add(this.textBoxPartitionId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxConsumerId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxConsumerGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTopic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLog);
            this.Name = "TestApp";
            this.Text = "Test Harness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxConsumerGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConsumerId;
        private System.Windows.Forms.Button buttonGetTopics;
        private System.Windows.Forms.Button buttonGetTopicMetadata;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPartitionId;
        private System.Windows.Forms.Button buttonGetPartitions;
        private System.Windows.Forms.Button button2;
    }
}

