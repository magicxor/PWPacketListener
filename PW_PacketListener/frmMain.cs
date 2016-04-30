using PWFrameWork;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace PW_PacketListener
{
	public class frmMain : Form
	{
		private bool bInjectMode;

		private cPacket pSendPacket = new cPacket();

		private cPacketInspector PacketInspector = new cPacketInspector();

		private cHookModule Hook = new cHookModule();

		private myClientFinder ClientFinder;

		private IContainer components;

		private System.Windows.Forms.Timer Timer;

		private Button cmdStop;

		private ComboBox cmbPW;

		private Button cmdRefreshPW;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tab_PacketProcessor;

		private ListBox lstPackets;

		private TabPage tabPage4;

		private Label label3;

		private Label label2;

		private Label label1;

		private Button cmdSendPacket;

		private Label label4;

		private TextBox txtSendPacket;

		private Label label5;

		private Label lblSendPacket;

		private Label label6;

		private GroupBox groupBox1;

		private Button cmdStart;

		private GroupBox groupBox2;

		private Button cmdInjectOn;

		private Button cmdInjectOff;

		private TabPage tabPage5;

		private Label label7;

		private ComboBox cmbKnownPackets;

		private WebBrowser webPacket;

		private Button SaveToBin;

		private Button SaveToTXT;

		private System.Windows.Forms.ContextMenuStrip contextMenulstPackets;

		private ToolStripMenuItem menuPacketCopy;

		private ToolStripMenuItem menuPacketSend;

		private Button cmdClearList;

		private System.Windows.Forms.Timer timPacketSend;

		private GroupBox groupBox3;

		private Button cmdTimerSendPacketOff;

		private Button cmdTimerSendPacketOn;

		private Label label8;

		private TextBox txtSendPacketInterval;

		public frmMain()
		{
			this.InitializeComponent();
		}

		private void cmbKnownPackets_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.webPacket.DocumentText = ((cKnownPacket)this.cmbKnownPackets.SelectedItem).GetDescription();
		}

		private void cmdClearList_Click(object sender, EventArgs e)
		{
			this.lstPackets.Items.Clear();
		}

		private void cmdInjectOff_Click(object sender, EventArgs e)
		{
			this.cmdStop_Click(null, null);
			MemoryManager.CloseProcess();
			this.bInjectMode = false;
			this.RefreshInterface();
		}

		private void cmdInjectOn_Click(object sender, EventArgs e)
		{
			ClientWindow item = this.cmbPW.Items[this.cmbPW.SelectedIndex] as ClientWindow;
			MemoryManager.OpenProcess(item.ProcessId);
			this.bInjectMode = true;
			this.RefreshInterface();
		}

		private void cmdRefreshPW_Click(object sender, EventArgs e)
		{
			this.RefreshPW();
		}

		private void cmdSendPacket_Click(object sender, EventArgs e)
		{
			cPacketInjection _cPacketInjection = new cPacketInjection();
			if (!this.pSendPacket.isEmpty())
			{
				_cPacketInjection.SendPacket(this.pSendPacket.GetPacket());
			}
		}

		private void cmdStart_Click(object sender, EventArgs e)
		{
			this.Hook.StartHook();
			this.Timer.Enabled = true;
			this.RefreshInterface();
		}

		private void cmdStop_Click(object sender, EventArgs e)
		{
			if (this.Timer.Enabled)
			{
				this.Timer.Enabled = false;
				this.Hook.StopHook();
				this.RefreshInterface();
			}
		}

		private void cmdTimerSendPacketOff_Click(object sender, EventArgs e)
		{
		}

		private void cmdTimerSendPacketOn_Click(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.cmdStop_Click(null, null);
			this.cmdInjectOff_Click(null, null);
		}

		private void frmMain_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("PW PacketListener. N00bSa1b0t. 2011-2013");
			e.Cancel = true;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.tabControl1.TabPages.Remove(this.tab_PacketProcessor);
			cOptions.ReadConfigFile();
			this.ClientFinder = new myClientFinder();
			this.RefreshInterface();
			this.RefreshPW();
			this.cmbKnownPackets.Items.AddRange(this.PacketInspector.GetPackets());
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMain));
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.cmdStop = new Button();
			this.cmbPW = new ComboBox();
			this.cmdRefreshPW = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.cmdClearList = new Button();
			this.SaveToBin = new Button();
			this.SaveToTXT = new Button();
			this.lstPackets = new ListBox();
			this.tab_PacketProcessor = new TabPage();
			this.webPacket = new WebBrowser();
			this.cmbKnownPackets = new ComboBox();
			this.tabPage4 = new TabPage();
			this.groupBox3 = new GroupBox();
			this.cmdTimerSendPacketOff = new Button();
			this.cmdTimerSendPacketOn = new Button();
			this.label8 = new Label();
			this.txtSendPacketInterval = new TextBox();
			this.lblSendPacket = new Label();
			this.label6 = new Label();
			this.txtSendPacket = new TextBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.cmdSendPacket = new Button();
			this.tabPage5 = new TabPage();
			this.label7 = new Label();
			this.contextMenulstPackets = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuPacketCopy = new ToolStripMenuItem();
			this.menuPacketSend = new ToolStripMenuItem();
			this.groupBox1 = new GroupBox();
			this.cmdStart = new Button();
			this.groupBox2 = new GroupBox();
			this.cmdInjectOn = new Button();
			this.cmdInjectOff = new Button();
			this.timPacketSend = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tab_PacketProcessor.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.contextMenulstPackets.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.Timer.Interval = 1;
			this.Timer.Tick += new EventHandler(this.Timer_Tick);
			this.cmdStop.Location = new Point(4, 69);
			this.cmdStop.Name = "cmdStop";
			this.cmdStop.Size = new System.Drawing.Size(189, 46);
			this.cmdStop.TabIndex = 1;
			this.cmdStop.Text = "Стоп";
			this.cmdStop.UseVisualStyleBackColor = true;
			this.cmdStop.Click += new EventHandler(this.cmdStop_Click);
			this.cmbPW.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPW.FormattingEnabled = true;
			this.cmbPW.Location = new Point(12, 12);
			this.cmbPW.Name = "cmbPW";
			this.cmbPW.Size = new System.Drawing.Size(199, 21);
			this.cmbPW.TabIndex = 2;
			this.cmdRefreshPW.Location = new Point(12, 418);
			this.cmdRefreshPW.Name = "cmdRefreshPW";
			this.cmdRefreshPW.Size = new System.Drawing.Size(199, 23);
			this.cmdRefreshPW.TabIndex = 3;
			this.cmdRefreshPW.Text = "Поискать окна PW еще разок";
			this.cmdRefreshPW.UseVisualStyleBackColor = true;
			this.cmdRefreshPW.Click += new EventHandler(this.cmdRefreshPW_Click);
			this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tab_PacketProcessor);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Location = new Point(217, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(652, 429);
			this.tabControl1.TabIndex = 4;
			this.tabPage1.Controls.Add(this.cmdClearList);
			this.tabPage1.Controls.Add(this.SaveToBin);
			this.tabPage1.Controls.Add(this.SaveToTXT);
			this.tabPage1.Controls.Add(this.lstPackets);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(644, 403);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Пакеты";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.cmdClearList.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cmdClearList.Location = new Point(502, 374);
			this.cmdClearList.Name = "cmdClearList";
			this.cmdClearList.Size = new System.Drawing.Size(136, 23);
			this.cmdClearList.TabIndex = 4;
			this.cmdClearList.Text = "Очистить список";
			this.cmdClearList.UseVisualStyleBackColor = true;
			this.cmdClearList.Click += new EventHandler(this.cmdClearList_Click);
			this.SaveToBin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.SaveToBin.Enabled = false;
			this.SaveToBin.Location = new Point(360, 374);
			this.SaveToBin.Name = "SaveToBin";
			this.SaveToBin.Size = new System.Drawing.Size(136, 23);
			this.SaveToBin.TabIndex = 3;
			this.SaveToBin.Text = "Сохранить пакеты в bin";
			this.SaveToBin.UseVisualStyleBackColor = true;
			this.SaveToTXT.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.SaveToTXT.Location = new Point(218, 374);
			this.SaveToTXT.Name = "SaveToTXT";
			this.SaveToTXT.Size = new System.Drawing.Size(136, 23);
			this.SaveToTXT.TabIndex = 2;
			this.SaveToTXT.Text = "Сохранить пакеты в txt";
			this.SaveToTXT.UseVisualStyleBackColor = true;
			this.SaveToTXT.Click += new EventHandler(this.SaveToTXT_Click);
			this.lstPackets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lstPackets.Font = new System.Drawing.Font("Lucida Console", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstPackets.FormattingEnabled = true;
			this.lstPackets.Location = new Point(3, 3);
			this.lstPackets.Name = "lstPackets";
			this.lstPackets.Size = new System.Drawing.Size(638, 368);
			this.lstPackets.TabIndex = 0;
			this.lstPackets.SelectedIndexChanged += new EventHandler(this.lstPackets_SelectedIndexChanged);
			this.lstPackets.MouseDown += new MouseEventHandler(this.lstPackets_MouseDown);
			this.lstPackets.MouseUp += new MouseEventHandler(this.lstPackets_MouseUp);
			this.tab_PacketProcessor.Controls.Add(this.webPacket);
			this.tab_PacketProcessor.Controls.Add(this.cmbKnownPackets);
			this.tab_PacketProcessor.Location = new Point(4, 22);
			this.tab_PacketProcessor.Name = "tab_PacketProcessor";
			this.tab_PacketProcessor.Size = new System.Drawing.Size(644, 403);
			this.tab_PacketProcessor.TabIndex = 2;
			this.tab_PacketProcessor.Text = "Распознавание пакетов";
			this.tab_PacketProcessor.UseVisualStyleBackColor = true;
			this.webPacket.Location = new Point(3, 33);
			this.webPacket.MinimumSize = new System.Drawing.Size(20, 20);
			this.webPacket.Name = "webPacket";
			this.webPacket.ScriptErrorsSuppressed = true;
			this.webPacket.Size = new System.Drawing.Size(638, 367);
			this.webPacket.TabIndex = 1;
			this.cmbKnownPackets.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbKnownPackets.FormattingEnabled = true;
			this.cmbKnownPackets.Location = new Point(3, 6);
			this.cmbKnownPackets.Name = "cmbKnownPackets";
			this.cmbKnownPackets.Size = new System.Drawing.Size(273, 21);
			this.cmbKnownPackets.TabIndex = 0;
			this.cmbKnownPackets.SelectedIndexChanged += new EventHandler(this.cmbKnownPackets_SelectedIndexChanged);
			this.tabPage4.Controls.Add(this.groupBox3);
			this.tabPage4.Controls.Add(this.lblSendPacket);
			this.tabPage4.Controls.Add(this.label6);
			this.tabPage4.Controls.Add(this.txtSendPacket);
			this.tabPage4.Controls.Add(this.label5);
			this.tabPage4.Controls.Add(this.label4);
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.label2);
			this.tabPage4.Controls.Add(this.label1);
			this.tabPage4.Controls.Add(this.cmdSendPacket);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(644, 403);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Отправка пакетов";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.groupBox3.Controls.Add(this.cmdTimerSendPacketOff);
			this.groupBox3.Controls.Add(this.cmdTimerSendPacketOn);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.txtSendPacketInterval);
			this.groupBox3.Location = new Point(349, 93);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(284, 79);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Повторяющаяся отправка";
			this.groupBox3.Visible = false;
			this.cmdTimerSendPacketOff.Enabled = false;
			this.cmdTimerSendPacketOff.Location = new Point(96, 50);
			this.cmdTimerSendPacketOff.Name = "cmdTimerSendPacketOff";
			this.cmdTimerSendPacketOff.Size = new System.Drawing.Size(75, 23);
			this.cmdTimerSendPacketOff.TabIndex = 3;
			this.cmdTimerSendPacketOff.Text = "Выкл";
			this.cmdTimerSendPacketOff.UseVisualStyleBackColor = true;
			this.cmdTimerSendPacketOff.Visible = false;
			this.cmdTimerSendPacketOff.Click += new EventHandler(this.cmdTimerSendPacketOff_Click);
			this.cmdTimerSendPacketOn.Location = new Point(6, 50);
			this.cmdTimerSendPacketOn.Name = "cmdTimerSendPacketOn";
			this.cmdTimerSendPacketOn.Size = new System.Drawing.Size(75, 23);
			this.cmdTimerSendPacketOn.TabIndex = 2;
			this.cmdTimerSendPacketOn.Text = "Вкл";
			this.cmdTimerSendPacketOn.UseVisualStyleBackColor = true;
			this.cmdTimerSendPacketOn.Visible = false;
			this.cmdTimerSendPacketOn.Click += new EventHandler(this.cmdTimerSendPacketOn_Click);
			this.label8.AutoSize = true;
			this.label8.Location = new Point(77, 20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 13);
			this.label8.TabIndex = 1;
			this.label8.Text = "милисекунд";
			this.label8.Visible = false;
			this.txtSendPacketInterval.Location = new Point(6, 17);
			this.txtSendPacketInterval.Name = "txtSendPacketInterval";
			this.txtSendPacketInterval.Size = new System.Drawing.Size(65, 20);
			this.txtSendPacketInterval.TabIndex = 0;
			this.txtSendPacketInterval.Text = "200";
			this.txtSendPacketInterval.Visible = false;
			this.lblSendPacket.AutoSize = true;
			this.lblSendPacket.Location = new Point(6, 302);
			this.lblSendPacket.Name = "lblSendPacket";
			this.lblSendPacket.Size = new System.Drawing.Size(0, 13);
			this.lblSendPacket.TabIndex = 8;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(6, 280);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(242, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Таким Ваш пакет увидела данная программа:";
			this.txtSendPacket.Location = new Point(9, 217);
			this.txtSendPacket.Name = "txtSendPacket";
			this.txtSendPacket.Size = new System.Drawing.Size(632, 20);
			this.txtSendPacket.TabIndex = 6;
			this.txtSendPacket.TextChanged += new EventHandler(this.txtSendPacket_TextChanged);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(6, 175);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(435, 39);
			this.label5.TabIndex = 5;
			this.label5.Text = "Отправленный пакет также будет пойман программой, если включен режим ловли.\r\n\r\nВведите свой пакет в поле:\r\n";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(6, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 52);
			this.label4.TabIndex = 4;
			this.label4.Text = "55 00\r\n0800\r\n2e 00\r\n2f00";
			this.label3.Location = new Point(6, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(638, 30);
			this.label3.TabIndex = 3;
			this.label3.Text = "Пакеты вводить в шестандцатеричном виде. Пробел между байтами можно оставлять, можно и убирать. Ниже пример правильных пакетов:";
			this.label2.Location = new Point(3, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(638, 32);
			this.label2.TabIndex = 2;
			this.label2.Text = componentResourceManager.GetString("label2.Text");
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.label1.Location = new Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "ВНИМАНИЕ";
			this.cmdSendPacket.Location = new Point(9, 243);
			this.cmdSendPacket.Name = "cmdSendPacket";
			this.cmdSendPacket.Size = new System.Drawing.Size(122, 23);
			this.cmdSendPacket.TabIndex = 0;
			this.cmdSendPacket.Text = "Отправить";
			this.cmdSendPacket.UseVisualStyleBackColor = true;
			this.cmdSendPacket.Click += new EventHandler(this.cmdSendPacket_Click);
			this.tabPage5.Controls.Add(this.label7);
			this.tabPage5.Location = new Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(644, 403);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Помощь";
			this.tabPage5.UseVisualStyleBackColor = true;
			this.label7.Dock = DockStyle.Fill;
			this.label7.Location = new Point(0, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(644, 403);
			this.label7.TabIndex = 0;
			this.label7.Text = "1. Сначала выберите окно игры в списке в левом верхнем углу.\r\n2. Далее надо внедриться в процесс\r\n3. После этого можно либо отправлять пакеты в игру, либо запустить прослушку отправленных пакетов.";
			ToolStripItemCollection items = this.contextMenulstPackets.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.menuPacketCopy, this.menuPacketSend };
			items.AddRange(toolStripItemArray);
			this.contextMenulstPackets.Name = "contextMenulstPackets";
			this.contextMenulstPackets.Size = new System.Drawing.Size(140, 48);
			this.menuPacketCopy.Name = "menuPacketCopy";
			this.menuPacketCopy.Size = new System.Drawing.Size(139, 22);
			this.menuPacketCopy.Text = "Копировать";
			this.menuPacketCopy.Click += new EventHandler(this.menuPacketCopy_Click);
			this.menuPacketSend.Name = "menuPacketSend";
			this.menuPacketSend.Size = new System.Drawing.Size(139, 22);
			this.menuPacketSend.Text = "Отправить";
			this.menuPacketSend.Click += new EventHandler(this.menuPacketSend_Click);
			this.groupBox1.Controls.Add(this.cmdStart);
			this.groupBox1.Controls.Add(this.cmdStop);
			this.groupBox1.Location = new Point(12, 176);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(199, 124);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Захват пакетов";
			this.cmdStart.Location = new Point(6, 17);
			this.cmdStart.Name = "cmdStart";
			this.cmdStart.Size = new System.Drawing.Size(187, 46);
			this.cmdStart.TabIndex = 1;
			this.cmdStart.Text = "Старт";
			this.cmdStart.UseVisualStyleBackColor = true;
			this.cmdStart.Click += new EventHandler(this.cmdStart_Click);
			this.groupBox2.Controls.Add(this.cmdInjectOn);
			this.groupBox2.Controls.Add(this.cmdInjectOff);
			this.groupBox2.Location = new Point(12, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(199, 124);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Внедрение в процесс";
			this.cmdInjectOn.Location = new Point(6, 17);
			this.cmdInjectOn.Name = "cmdInjectOn";
			this.cmdInjectOn.Size = new System.Drawing.Size(187, 46);
			this.cmdInjectOn.TabIndex = 1;
			this.cmdInjectOn.Text = "Вкл";
			this.cmdInjectOn.UseVisualStyleBackColor = true;
			this.cmdInjectOn.Click += new EventHandler(this.cmdInjectOn_Click);
			this.cmdInjectOff.Location = new Point(4, 69);
			this.cmdInjectOff.Name = "cmdInjectOff";
			this.cmdInjectOff.Size = new System.Drawing.Size(189, 46);
			this.cmdInjectOff.TabIndex = 1;
			this.cmdInjectOff.Text = "Выкл";
			this.cmdInjectOff.UseVisualStyleBackColor = true;
			this.cmdInjectOff.Click += new EventHandler(this.cmdInjectOff_Click);
			this.timPacketSend.Tick += new EventHandler(this.timPacketSend_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(881, 453);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cmdRefreshPW);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.cmbPW);
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(897, 491);
			base.Name = "frmMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "PWPacketListener";
			base.HelpButtonClicked += new CancelEventHandler(this.frmMain_HelpButtonClicked);
			base.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
			base.Load += new EventHandler(this.frmMain_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tab_PacketProcessor.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.contextMenulstPackets.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void lstPackets_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right)
			{
				return;
			}
			this.lstPackets.SelectedIndex = this.lstPackets.IndexFromPoint(new Point(e.X, e.Y));
		}

		private void lstPackets_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right)
			{
				return;
			}
			if (this.lstPackets.SelectedIndex == -1)
			{
				return;
			}
			this.contextMenulstPackets.Show(this.lstPackets, e.X, e.Y);
		}

		private void lstPackets_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void menuPacketCopy_Click(object sender, EventArgs e)
		{
			if (this.lstPackets.SelectedIndex > -1)
			{
				Clipboard.SetText(this.lstPackets.SelectedItem.ToString());
			}
		}

		private void menuPacketSend_Click(object sender, EventArgs e)
		{
			if (!this.bInjectMode || this.lstPackets.SelectedIndex < 0)
			{
				return;
			}
			cPacketInjection _cPacketInjection = new cPacketInjection();
			_cPacketInjection.SendPacket(((cPacket)this.lstPackets.SelectedItem).GetPacket());
		}

		private void RefreshInterface()
		{
			this.cmdStart.Enabled = (!this.bInjectMode ? false : !this.Timer.Enabled);
			this.cmdStop.Enabled = (!this.bInjectMode ? false : this.Timer.Enabled);
			this.cmdRefreshPW.Enabled = (this.bInjectMode ? false : !this.Timer.Enabled);
			this.cmbPW.Enabled = (this.bInjectMode ? false : !this.Timer.Enabled);
			this.cmdInjectOn.Enabled = !this.bInjectMode;
			this.cmdInjectOff.Enabled = this.bInjectMode;
			this.cmdSendPacket.Enabled = this.bInjectMode;
		}

		private void RefreshPW()
		{
			this.cmbPW.Items.Clear();
			this.cmbPW.Items.AddRange(this.ClientFinder.GetWindows());
			if (this.cmbPW.Items.Count <= 0)
			{
				MessageBox.Show("Клиент PW не был найден. Запустите игру и нажмите на кнопку ниже.");
				this.cmdInjectOn.Enabled = false;
				return;
			}
			this.cmbPW.SelectedIndex = 0;
			this.cmdInjectOn.Enabled = true;
		}

		private void SaveToTXT_Click(object sender, EventArgs e)
		{
			if (this.lstPackets.Items.Count == 0)
			{
				MessageBox.Show("Нет пакетов, нечего сохранять.");
				return;
			}
			string str = "";
			for (int i = 0; i < this.lstPackets.Items.Count; i++)
			{
				str = string.Concat(str, this.lstPackets.Items[i], "\r\n");
			}
			DateTime now = DateTime.Now;
			string str1 = string.Concat("packets_", now.ToString(), ".txt");
			str1 = str1.Replace(":", ".");
			StreamWriter streamWriter = File.CreateText(str1);
			streamWriter.Write(str);
			streamWriter.Close();
			MessageBox.Show(string.Concat("Пакеты сохранены в файл ", str1));
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			byte[] numArray = this.Hook.TimerTick();
			if (numArray != null && !cIgnore.IsPacketIgnored(numArray))
			{
				this.lstPackets.Items.Add(this.PacketInspector.ParseByteArray(numArray));
			}
		}

		private void timPacketSend_Tick(object sender, EventArgs e)
		{
		}

		private void txtSendPacket_TextChanged(object sender, EventArgs e)
		{
			this.pSendPacket.ConvertFromString(this.txtSendPacket.Text);
			this.lblSendPacket.Text = this.pSendPacket.ToString();
		}
	}
}